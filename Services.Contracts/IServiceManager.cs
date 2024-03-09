using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }

        IMessageService MessageService { get; }

        INotificationService NotificationService { get; }

        IUserService UserService { get; }

        IMatchService MatchService { get; }

        ISchoolService SchoolService { get; }

        IPublishMessageService PublishMessageService { get; }
    }
}
