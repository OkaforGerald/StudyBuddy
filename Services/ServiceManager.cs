using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IMessageService> _messageService;

        public ServiceManager(IMapper mapper, UserManager<User> userManager, IConfiguration config, IRepositoryManager manager)
        {
            _authService = new Lazy<IAuthService> (new AuthService(mapper, userManager, config));
            _messageService = new Lazy<IMessageService>(new MessageService(manager, userManager, mapper));
        }
        public IAuthService AuthService => _authService.Value;

        public IMessageService MessageService => _messageService.Value;
    }
}
