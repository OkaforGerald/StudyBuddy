﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseById(Guid Id, bool trackChanges);

        Task<List<Course>> GetCourses(bool trackChanges);
    }
}
