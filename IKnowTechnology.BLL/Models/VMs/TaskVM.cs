using IKnowTechnology.CORE.Enums;
using System;

namespace IKnowTechnology.BLL.Models.VMs
{
    public class TaskVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime WorkTime { get; set; }
        public Status Status { get; set; }
    }
}