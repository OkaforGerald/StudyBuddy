using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(IMapper mapper, UserManager<User> userManager, IConfiguration config)
        {
            _authService = new Lazy<IAuthService> (new AuthService(mapper, userManager, config));
        }
        public IAuthService AuthService => _authService.Value;
    }
}
