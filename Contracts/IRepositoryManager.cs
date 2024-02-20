using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IMessageRepository MessageRepository { get; }

        IUserRepository UserRepository { get; }

        ISelectionRepository SelectionRepository { get; }

        ICourseRepository CourseRepository { get; }

        ICourseOfStudyRepository CourseOfStudyRepository { get; }

        IDepartmentRepository DepartmentRepository { get; }

        IMatchRepository MatchRepository { get; }

        INotificationRepository NotificationRepository { get; }

        Task Save();
    }
}
