using AutoMapper;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Entities;
using IKnowTechnology.UI.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace IKnowTechnology.UI.Controllers
{
    public class UserController : Controller
    {
        const string apiUrl = "https://localhost:44344/api/";
        private readonly UserManager<User> usermanager;
        private readonly IMapper mapper;
        private readonly ClaimService claimService;

        public UserController(UserManager<User> usermanager,IMapper mapper)
        {
            this.usermanager = usermanager;
            this.mapper = mapper;
            claimService = new ClaimService(usermanager);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.Claims.First(z => z.Type == "userId").Value;
            string url = apiUrl + "User/GetUserCardByID/" + userId;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            UserCardVM vm = JsonConvert.DeserializeObject<UserCardVM>(result);
            return View(vm);
        }
        public async Task<IActionResult> GetUserCard()
        {
            string userId = User.Claims.First(z => z.Type == "userId").Value;
            string url = apiUrl + "User/GetUserCardByID/" + userId;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            UserCardVM vm = JsonConvert.DeserializeObject<UserCardVM>(result);
            return PartialView("_UserCardPartial", vm);
        }

        public async Task<IActionResult> GetUserInfo()
        {
            // login olmuş olan userın id'si ile apiye talepte bulunulacak
            string userId = User.Claims.First(z => z.Type == "userId").Value;
            string url = apiUrl + "User/GetUserCardByID/" + userId;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                UpdateUserDTO updateUserDTO = JsonConvert.DeserializeObject<UpdateUserDTO>(result);
                return Json(updateUserDTO);
            }

            return BadRequest("error");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO model)
        {
            string url = apiUrl + "User/UpdateUser";

            if (ModelState.IsValid)
            {
                if (model.newPhoto != null)
                {
                    using var image = Image.Load(model.newPhoto.OpenReadStream());
                    if (image.Height > 512 && image.Width > 512)
                        image.Mutate(x => x.Resize(512, 512));

                    string fileName = $"{model.Id}.jpg";
                    image.Save($"wwwroot/images/users/{fileName}");

                    model.ImagePath = $"/images/users/{fileName}";
                    model.newPhoto = null;

                    User user = await usermanager.GetUserAsync(User);
                    await claimService.ReplaceClaims(user);
                }

                HttpClient client = new HttpClient();
                string jsonObject = JsonConvert.SerializeObject(model);
                HttpResponseMessage response = await client.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode) return Json(new { result = true });
                return Json(new { result = false });
            }
            else
            {
                var errs = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(errs);
            }
        }


    }
}
