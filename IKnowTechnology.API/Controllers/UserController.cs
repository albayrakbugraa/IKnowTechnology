using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.BLL.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IKnowTechnology.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetUserCardByID(string id)
        {
            UserCardVM card = await userService.GetUserCardByID(id);
            return Ok(card);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO model)
        {
            bool result = await userService.UpdateUser(model);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            UpdateUserDTO updateDTO = await userService.GetUserEditInfo(id);
            if (updateDTO == null) return BadRequest();
            return Ok(updateDTO);
        }

    }
}
