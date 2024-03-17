using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Services.Contracts;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager manager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserService(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<UserDetailsDto> GetUserDetails(string requester, string username, bool trackChanges)
        {
            var user = await userManager.FindByNameAsync(username);
            var requesterUser = await userManager.FindByNameAsync(requester);


            if(user is null)
            {
                throw new Exception($"User {user} not found");
            }

            var details = await manager.UserRepository.GetUserDetails(user.Id, trackChanges);
            if (details != null)
            {
                var courseOfStudy = await manager.CourseOfStudyRepository.GetCourseById(details.CourseId, trackChanges);
                var department = await manager.DepartmentRepository.GetDepartmentById(details.DepartmentId, trackChanges);
                var proficiencySelection = await manager.SelectionRepository.GetProficiencySelectionsForUser(details.Id, trackChanges);
                var MatchStatus = await manager.MatchRepository.GetMatchStatusBetweenUsers(requesterUser.Id,user.Id,trackChanges);
                var Match = await manager.MatchRepository.GetMatchBetweenUsers(requesterUser.Id,user.Id,trackChanges);

                List<ProficientCoursesDto> courses = new List<ProficientCoursesDto>();

                foreach (var proficiency in proficiencySelection)
                {
                    var course = await manager.CourseRepository.GetCourseById(proficiency.CourseId, trackChanges);

                    courses.Add(new ProficientCoursesDto
                    {
                        Course = course.Name,
                        Level = (int)(object)proficiency.Level
                    });

                }

                Notification notif = new Notification
                {
                    OwnerId = user.Id,
                    ProfileViewerId = requesterUser.Id,
                    NotifType = NotificationType.ProfileView,
                    CreatedAt = DateTime.UtcNow
                };

                manager.NotificationRepository.CreateNotification(notif);
                await manager.Save();

                return new UserDetailsDto
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Bio = user.Bio,
                    ImageUrl = user.ImageUrl,
                    CourseOfStudy = courseOfStudy.Name,
                    MatchCount = user.Matches,
                    MatchStatus = MatchStatus == 3 ? "No Match" : MatchStatus == 1 ? "Friends" : (MatchStatus == 0 && Match?.MatcherId == user.Id) ? "PendingAck" : "Pending",
                    Department = department.Name,
                    Mode = ((int)(object)details.Mode == 1) ? "Onsite" : "Virtual",
                    LinkedinUrl = details.LinkedInUrl,
                    Website = details.Website,
                    Github = details.Github,
                    Twitter = details.Twitter,
                    Rating = details.Rating,
                    RatedBy = details.RatingNum,
                    ProficientCourses = courses
                };
            }
            throw new Exception("No User Details");
        }

        public async Task<(List<UsersDto> users,Metadata metadata)> GetUsers(string requester, RequestParameters parameters, bool trackChanges)
        {
            var requesterUser = await userManager.FindByNameAsync(requester);

            var MatchingUsers = userManager.Users.GetUsers(parameters.CourseSearchTerm is not null ? "" : parameters.SearchTerm);

            var users = PagedList<User>.ToPagedList(MatchingUsers,parameters.PageNumber, parameters.PageSize);

            List<User> MatchedUsers = new List<User>();

            List<UsersDto> userDetails = new List<UsersDto>();

            foreach (var user in users)
            {
                var details = await manager.UserRepository.GetUserDetails(user.Id, trackChanges);
                if (details != null)
                {
                    if (!parameters.DepartmmentId.Equals(Guid.Empty) && !details.DepartmentId.Equals(parameters.DepartmmentId))
                    {
                        continue;
                    }
                    if (!parameters.CourseId.Equals(Guid.Empty) && !details.CourseId.Equals(parameters.CourseId))
                    {
                        continue;
                    }
                    MatchedUsers.Add(user);
                }
            }

            foreach (var user in MatchedUsers)
            {
                var details = await manager.UserRepository.GetUserDetails(user.Id, trackChanges);
                if (details != null)
                {
                    var courseOfStudy = await manager.CourseOfStudyRepository.GetCourseById(details.CourseId, trackChanges);
                    var proficiencySelection = await manager.SelectionRepository.GetProficiencySelectionsForUser(details.Id, trackChanges);
                    var course = await manager.CourseRepository.GetCourseById(proficiencySelection.Take(1).FirstOrDefault().CourseId, trackChanges);
                    var MatchStatus = await manager.MatchRepository.GetMatchStatusBetweenUsers(requesterUser.Id, user.Id, trackChanges);
                    var Match = await manager.MatchRepository.GetMatchBetweenUsers(requesterUser.Id, user.Id, trackChanges);

                    if (!string.IsNullOrEmpty(parameters.CourseSearchTerm))
                    {
                        // Check if the user has the specified course in their proficiency selection
                        var hasCourse = proficiencySelection.Any(s => s.Course.Name.ToLower().Contains(parameters.CourseSearchTerm.ToLower()));
                        if (!hasCourse)
                            continue; // Skip this user if they don't have the specified course
                    }


                    userDetails.Add(new UsersDto
                    {
                        ImageUrl = user.ImageUrl,
                        FullName = $"{user.FirstName} {user.LastName}",
                        UserName = user.UserName,
                        Course = courseOfStudy.Name,
                        ProficientCourse = course.Name,
                        MatchStatus = MatchStatus == 3 ? "No Match" : MatchStatus == 1 ? "Friends" : (MatchStatus == 0 && Match?.MatcherId == user.Id) ? "PendingAck" : "Pending",
                        Rating = details.Rating
                    });
                }
            }
            users.metadata.TotalCount = userDetails.Count();

            return (users: userDetails, metadata: users.metadata);
        }

        public async Task<DashboardDto> Dashboard(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            var details = await manager.UserRepository.GetUserDetails(user.Id, false);
            int NumMatches = await manager.MatchRepository.GetNumberOfMatches(user.Id, trackChanges: false);
            int pendingMatches = await manager.MatchRepository.GetNumberOfPendingMatches(user.Id, trackChanges: false);
            int profileViews = await manager.NotificationRepository.GetNumberOfProfileViews(user.Id, trackChanges: false);

            return new DashboardDto
            {
                ProfileViews = profileViews,
                PendingMatches = pendingMatches,
                NumMatches = NumMatches,
                TimeRequested = DateTime.UtcNow,
                username = user.FirstName,
                firstVisit = details == null
            };
        }
    }
}
