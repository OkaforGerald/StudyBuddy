using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using SharedAPI.Data;

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
    }
}
