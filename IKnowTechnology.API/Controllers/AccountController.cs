using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IKnowTechnology.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterDTO model)
        {
            var result = await userService.Register(model);
            if (result.Succeeded) return Ok();
            return BadRequest();
        }
    }
    
}
