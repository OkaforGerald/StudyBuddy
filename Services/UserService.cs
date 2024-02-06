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

        public async Task<UserDetailsDto> GetUserDetails(string username, bool trackChanges)
        {
            var user = await userManager.FindByNameAsync(username);

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

                return new UserDetailsDto
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Username = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Bio = user.Bio,
                    ImageUrl = user.ImageUrl,
                    CourseOfStudy = courseOfStudy.Name,
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

        public async Task<(List<UsersDto> users,Metadata metadata)> GetUsers(RequestParameters parameters, bool trackChanges)
        {
            var MatchingUsers = userManager.Users.GetUsers(parameters.SearchTerm ?? "");

            var users = PagedList<User>.ToPagedList(MatchingUsers,parameters.PageNumber, parameters.PageSize);

            List<UsersDto> userDetails = new List<UsersDto>();

            foreach (var user in users)
            {
                var details = await manager.UserRepository.GetUserDetails(user.Id, trackChanges);
                if (details != null) {
                    var courseOfStudy = await manager.CourseOfStudyRepository.GetCourseById(details.CourseId, trackChanges);
                    var proficiencySelection = await manager.SelectionRepository.GetProficiencySelectionsForUser(details.Id, trackChanges);
                    var course = await manager.CourseRepository.GetCourseById(proficiencySelection.Take(1).FirstOrDefault().CourseId, trackChanges);

                    userDetails.Add(new UsersDto
                    {
                        ImageUrl = user.ImageUrl,
                        FullName = $"{user.FirstName} {user.LastName}",
                        UserName = user.UserName,
                        Course = courseOfStudy.Name,
                        ProficientCourse = course.Name,
                        MatchStatus = "Pending",
                        Rating = details.Rating
                    });
                }
            }

            return (users: userDetails, metadata: users.metadata);
        }
    }
}
