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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }
    }
}
