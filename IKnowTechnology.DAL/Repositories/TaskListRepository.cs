using IKnowTechnology.CORE.Entities;
using IKnowTechnology.CORE.IRepositories;
using IKnowTechnology.DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.DAL.Repositories
{
    public class TaskListRepository : BaseRepository<TaskList>, ITaskListRepository
    {
        public TaskListRepository(AppDbContext db) : base(db)
        {
        }
    }
}
