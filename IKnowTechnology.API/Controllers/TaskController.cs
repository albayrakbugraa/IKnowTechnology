using FluentValidation.Results;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.BLL.Services.TaskListService;
using IKnowTechnology.BLL.Services.UserService;
using IKnowTechnology.BLL.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IKnowTechnology.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskListService taskListService;
        public TaskController(ITaskListService taskListService)
        {
            this.taskListService = taskListService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDTO model)
        {
            CreateTaskDTOValidator vl = new CreateTaskDTOValidator();
            ValidationResult result = await vl.ValidateAsync(model);
            await taskListService.AddTask(model);
            return Ok();
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetTaskListByUserId(string id)
        {
            List<TaskVM> taskVMs = await taskListService.GetTaskList(id);
            return Ok(taskVMs);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            bool result = await taskListService.DeleteTask(id);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> SuccessTask(int id)
        {
            bool result = await taskListService.SuccessTask(id);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await taskListService.GetTaskById(id);
            if (task!=null) return Ok(task);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(UpdateTaskDTO model)
        {
            bool result = await taskListService.UpdateTask(model);
            if (result) return Ok();
            return BadRequest();
        }


    }
}
