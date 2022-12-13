using System.ComponentModel.DataAnnotations;
using System;
using IKnowTechnology.CORE.Enums;

namespace IKnowTechnology.BLL.Models.DTOs
{
    public class UpdateTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public DateTime WorkTime { get; set; }
        public bool State => true;
        public Status Status { get; set; }
    }
}