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
    public class SchoolService : ISchoolService
    {
        private readonly IRepositoryManager manager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public SchoolService(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<DepartmentDto>> GetDepartments(bool trackChanges)
        {
            var depts = await manager.DepartmentRepository.GetDepartments(trackChanges);

            var department = mapper.Map<List<DepartmentDto>>(depts);

            return department;
        }

        public async Task<List<CourseDto>> GetCourses(bool trackChanges)
        {
            var depts = await manager.CourseRepository.GetCourses(trackChanges);

            var courses = depts.Select(x => new CourseDto { Id = x.Id, Name = x.Name }).ToList();

            return courses;
        }

        public async Task<List<CourseDto>> GetCourseByDeptId(Guid Id, bool trackChanges)
        {
            var depts = await manager.DepartmentRepository.GetDepartmentById(Id, trackChanges);

            if(depts is null)
            {
                throw new Exception("Department not found");
            }

            var courses = await manager.CourseOfStudyRepository.CourseByDepartmentId(Id, trackChanges);

            var response = courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return response;
        }
    }
}
