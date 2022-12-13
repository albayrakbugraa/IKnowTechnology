using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.CORE.Entities;
using AutoMapper;
using IKnowTechnology.UI.Utils;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using Newtonsoft.Json;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Enums;

namespace IKnowTechnology.UI.Controllers
{
    public class TaskController : Controller
    {
        const string apiUrl = "https://localhost:44344/api/";
        private readonly UserManager<User> usermanager;
        private readonly IMapper mapper;
        private readonly ClaimService claimService;

        public TaskController(UserManager<User> userManager)
        {
            this.usermanager = userManager;
            claimService = new ClaimService(userManager);
        }
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDTO model)
        {
            User user = await usermanager.GetUserAsync(HttpContext.User);
            model.UserId = user.Id;
            string url = apiUrl + "Task/CreateTask";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = true;
                return View();
            }
            var errs = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            ViewBag.errors = errs;
            ViewBag.result = false;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskList()
        {
            User user = await usermanager.GetUserAsync(HttpContext.User);
            string url = apiUrl + "Task/GetTaskListByUserId/" + user.Id;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            List<TaskVM> requests = new List<TaskVM>();
            string result = await response.Content.ReadAsStringAsync();
            requests = JsonConvert.DeserializeObject<List<TaskVM>>(result);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            string url = apiUrl + "Task/DeleteTask/" + taskId;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            return RedirectToAction("GetTaskList");
        }

        [HttpGet]
        public async Task<IActionResult> SuccessTask(int taskId)
        {
            string url = apiUrl + "Task/SuccessTask/" + taskId;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            return RedirectToAction("GetTaskList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTask(int taskId)
        {
            ViewBag.Status = Enum.GetValues(typeof(Status));
            string url = apiUrl + "Task/GetTaskById/" + taskId;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            UpdateTaskDTO vm = JsonConvert.DeserializeObject<UpdateTaskDTO>(result);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(UpdateTaskDTO model,int taskId)
        {
            model.Id= taskId;
            ViewBag.Status = Enum.GetValues(typeof(Status));
            string url = apiUrl + "Task/UpdateTask";            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = true;
                return RedirectToAction("GetTaskList");
            }
            var errs = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            ViewBag.errors = errs;
            ViewBag.result = false;
            return View(model);
        }
    }
}
