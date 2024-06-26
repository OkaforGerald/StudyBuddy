﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IMessageService> _messageService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IPublishMessageService> _publishService;
        private readonly Lazy<IMatchService> matchService;
        private readonly Lazy<INotificationService> notificationService;
        private readonly Lazy<ISchoolService> _schoolService;

        public ServiceManager(IMapper mapper, UserManager<User> userManager, IConfiguration config, IRepositoryManager manager)
        {
            _authService = new Lazy<IAuthService> (new AuthService(mapper, userManager, config));
            _messageService = new Lazy<IMessageService>(new MessageService(manager, userManager, mapper));
            _publishService = new Lazy<IPublishMessageService>(new PublishMessageService());
            _userService = new Lazy<IUserService>(new UserService(manager, mapper, userManager));
            _schoolService = new Lazy<ISchoolService>(new SchoolService(manager, mapper, userManager));
            matchService = new Lazy<IMatchService>(new MatchService(manager, mapper, userManager));
            notificationService = new Lazy<INotificationService>(new NotificationService(manager, mapper, userManager));
        }
        public IAuthService AuthService => _authService.Value;

        public IMessageService MessageService => _messageService.Value;

        public IPublishMessageService PublishMessageService => _publishService.Value;

        public IUserService UserService => _userService.Value;

        public IMatchService MatchService => matchService.Value;

        public INotificationService NotificationService => notificationService.Value;

        public ISchoolService SchoolService => _schoolService.Value;
    }
}
