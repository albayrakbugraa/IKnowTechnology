using AutoMapper;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Entities;
using IKnowTechnology.CORE.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.BLL.Services.TaskListService
{
    public class TaskListService : ITaskListService
    {
        private readonly IMapper mapper;
        private readonly ITaskListRepository taskListRepository;
        public TaskListService(IMapper mapper, ITaskListRepository taskListRepository)
        {
            this.mapper = mapper;
            this.taskListRepository = taskListRepository;
        }

        public async Task<bool> AddTask(CreateTaskDTO taskDTO)
        {
            TaskList task = new TaskList();
            task = mapper.Map(taskDTO, task);
            var result = await taskListRepository.Create(task);
            return result;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await taskListRepository.GetWhere(x => x.Id == id);
            if (task == null) return false;
            var result = taskListRepository.Delete(task);
            return result;
        }

        public async Task<UpdateTaskDTO> GetById(int id)
        {
            var task = await taskListRepository.GetFilteredFirstOrDefault(
                selector: x => new UpdateTaskDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    WorkTime=x.WorkTime,
                    Status=x.Status
                },
                expression: x => x.Id == id && x.State == true

                );
            return task;
        }

        public async Task<TaskVM> GetTaskById(int id)
        {
            var task = await taskListRepository.GetWhere(x => x.Id == id);
            TaskVM vm = new TaskVM();
            vm = mapper.Map(task, vm);
            return vm;
        }

        public async Task<List<TaskVM>> GetTaskList(string UserId)
        {
            var tasks = await taskListRepository.GetFilteredList(
               selector: x => new TaskVM
               {
                   Id = x.Id,
                   Title = x.Title,
                   Description = x.Description,
                   CreationDate = x.CreationDate,
                   WorkTime= x.WorkTime,
                   UpdateDate= x.UpdateDate,
                   Status=x.Status                   
               },
               expression: x => x.State == true && x.UserId == UserId,
               orderBy: x => x.OrderBy(x => x.WorkTime)
               );
            return tasks;
        }

        public async Task<bool> SuccessTask(int id)
        {
            var task = await taskListRepository.GetWhere(x => x.Id == id);
            if (task != null)
            {
                task.Status = CORE.Enums.Status.Yapıldı;
                return taskListRepository.Update(task);
            }
            else return false;
        }

        public async Task<bool> UpdateTask(UpdateTaskDTO updateTask)
        {
            var task = await taskListRepository.GetWhere(a => a.Id == updateTask.Id);
            if (task != null)
            {
                task = mapper.Map(updateTask, task);
                return taskListRepository.Update(task);
            }
            else return false;
        }
    }
}
