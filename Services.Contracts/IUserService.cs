﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetUserDetails(string requesterId, string username, bool trackChanges);

        Task<(List<UsersDto> users, Metadata metadata)> GetUsers(string requester, RequestParameters parameters, bool trackChanges);

        Task AddDetails(string username, AddDetailsDto details);

        Task<DashboardDto> Dashboard(string username);
    }
}
