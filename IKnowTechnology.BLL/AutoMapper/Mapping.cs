using AutoMapper;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.BLL.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<User, RegisterDTO>().ReverseMap().ForAllMembers(a => a.UseDestinationValue());
            CreateMap<User, UpdateUserDTO>().ReverseMap().ForAllMembers(a => a.UseDestinationValue());
            CreateMap<TaskList, UpdateTaskDTO>().ReverseMap().ForAllMembers(a => a.UseDestinationValue());
            CreateMap<TaskList, CreateTaskDTO>().ReverseMap().ForAllMembers(a => a.UseDestinationValue());
            CreateMap<TaskList, TaskVM>().ReverseMap().ForAllMembers(a => a.UseDestinationValue());
        }
    }
}
