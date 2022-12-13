using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;

namespace IKnowTechnology.BLL.Services.TaskListService
{
    public interface ITaskListService
    {
        Task<bool> AddTask(CreateTaskDTO taskDTO);
        Task<bool> UpdateTask(UpdateTaskDTO updateTask);
        Task<bool> DeleteTask(int id);
        Task<bool> SuccessTask(int id);
        Task<List<TaskVM>> GetTaskList(string UserId);
        Task<UpdateTaskDTO> GetById(int id);
        Task<TaskVM> GetTaskById(int id);
    }
}
