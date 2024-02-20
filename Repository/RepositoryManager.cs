using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext context;
        private readonly Lazy<IMessageRepository> messageRepository;
        private readonly Lazy<IUserRepository> userRepository;
        private readonly Lazy<ISelectionRepository> selectionRepository;
        private readonly Lazy<ICourseRepository> courseRepository;
        private readonly Lazy<INotificationRepository> notificationRepository;
        private readonly Lazy<ICourseOfStudyRepository> courseOfStudyRepository;
        private readonly Lazy<IDepartmentRepository> departmentRepository;
        private readonly Lazy<IMatchRepository> matchRepository;

        public RepositoryManager(RepositoryContext context)
        {
            this.context = context;
            this.messageRepository = new Lazy<IMessageRepository>(new MessageRepository(context));
            this.userRepository = new Lazy<IUserRepository>(new UserRepository(context));
            this.selectionRepository = new Lazy<ISelectionRepository>(new SelectionRepository(context));
            this.courseRepository = new Lazy<ICourseRepository>(new CourseRepository(context));
            this.courseOfStudyRepository = new Lazy<ICourseOfStudyRepository>(new CourseofStudyRepository(context));
            this.departmentRepository = new Lazy<IDepartmentRepository>(new DepartmentRepository(context));
            this.matchRepository = new Lazy<IMatchRepository>(new MatchRepository(context));
            this.notificationRepository = new Lazy<INotificationRepository>(new NotificationRepository(context));
        }

        public IMessageRepository MessageRepository => messageRepository.Value;

        public IUserRepository UserRepository => userRepository.Value;

        public ISelectionRepository SelectionRepository => selectionRepository.Value;

        public ICourseRepository CourseRepository => courseRepository.Value;

        public IDepartmentRepository DepartmentRepository => departmentRepository.Value;

        public ICourseOfStudyRepository CourseOfStudyRepository => courseOfStudyRepository.Value;

        public IMatchRepository MatchRepository => matchRepository.Value;

        public INotificationRepository NotificationRepository => notificationRepository.Value;

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
