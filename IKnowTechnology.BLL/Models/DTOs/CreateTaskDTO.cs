using System.ComponentModel.DataAnnotations;
using System;
using IKnowTechnology.CORE.Enums;

namespace IKnowTechnology.BLL.Models.DTOs
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate => DateTime.Now;
        public DateTime WorkTime {get; set;}
        public bool IsActive => true;
        public string UserId { get; set; }
        public Status Status => Status.Bekliyor;

    }
}