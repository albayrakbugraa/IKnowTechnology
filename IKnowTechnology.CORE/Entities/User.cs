using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.CORE.Entities
{
    public class User : IdentityUser, IBaseEntity
    {
        public User()
        {
            TaskLists = new HashSet<TaskList>();
            State = true;
            CreationDate = DateTime.Now;
            ImagePath = "/images/users/account-add-photo.svg";
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; } 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool State { get; set; }
        public ICollection<TaskList> TaskLists { get; set;}
        
    }
}
