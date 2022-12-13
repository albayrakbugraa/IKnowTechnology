using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Entities;
using IKnowTechnology.UI.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace IKnowTechnology.UI.Controllers
{
    public class AccountController : Controller
    {
        const string apiUrl = "https://localhost:44344/api/";
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ClaimService claimService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            claimService= new ClaimService(userManager);

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            User user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Giriş başarısız..", "Geçersiz kullanıcı adı veya şifre !");
                return View(model);
            }

            await claimService.AddClaimsToUser(user);
            SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction(actionName: "Index", controllerName: "User");
            }

            ModelState.AddModelError("Giriş başarısız..", "Geçersiz kullanıcı adı veya şifre !");
            return View(model);

        }

        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            string url = apiUrl + "Account/CreateUser";
            HttpClient client = new HttpClient();
            string jsonObject = JsonConvert.SerializeObject(model);
            HttpResponseMessage response = await client.PostAsJsonAsync(url, model);
            return RedirectToAction(actionName: "Login", controllerName: "Account");
        }

        public async Task<IActionResult> LogOut()
        {
            User user = await userManager.GetUserAsync(User);
            await claimService.RemoveClaims(user);
            await signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
