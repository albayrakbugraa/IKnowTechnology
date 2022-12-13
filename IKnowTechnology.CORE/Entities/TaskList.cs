using IKnowTechnology.CORE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.CORE.Entities
{
    public class TaskList : IBaseEntity
    {
        public TaskList()
        {
            CreationDate= DateTime.Now;
            State = true;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate {get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime WorkTime { get; set; }        
        public bool State { get; set; }
        public Status Status { get; set; }
      
    }
}
