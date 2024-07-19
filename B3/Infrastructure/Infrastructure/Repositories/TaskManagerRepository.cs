using Applications.Interfaces.Repository;
using Domain.Domain;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class TaskManagerRepository : Repository<TaskManager>, ITaskManagerRepository
    {
        private readonly B3Context _context;

        public TaskManagerRepository(B3Context context) : base(context)
        { 
            _context = context;
        }
    }
}
