using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.BLL.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace IKnowTechnology.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<UserCardVM> GetUserCardByID(string id);
        Task<bool> UpdateUser(UpdateUserDTO model);
        Task<IdentityResult> Register(RegisterDTO model);
        Task<UpdateUserDTO> GetUserEditInfo(string id);
    }
}
