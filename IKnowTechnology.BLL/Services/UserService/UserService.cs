using AutoMapper;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using IKnowTechnology.CORE.Entities;
using IKnowTechnology.CORE.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        public UserService(UserManager<User> userManager, IUserRepository userRepository, SignInManager<User> signInManager, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UserCardVM> GetUserCardByID(string id)
        {
            UserCardVM userCard = await userRepository.GetFilteredFirstOrDefault(
                selector: x => new UserCardVM
                {
                    Id = id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ImagePath= x.ImagePath,                    
                },
                expression: x => x.Id == id && x.State == true
                );
            return userCard;
        }

        public async Task<UpdateUserDTO> GetUserEditInfo(string id)
        {
            UpdateUserDTO updateDTO = await userRepository.GetFilteredFirstOrDefault(
                 selector: x => new UpdateUserDTO
                 {
                     Id = id,                     
                     ImagePath = x.ImagePath,
                     FirstName = x.FirstName,
                     LastName = x.LastName,                    
                 },
                 expression: x => x.Id == id
                 );
            return updateDTO;
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = new User();
            user = mapper.Map(model, user);
            var result = await userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<bool> UpdateUser(UpdateUserDTO model)
        {
            User user = await userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                user = mapper.Map(model, user);
                IdentityResult result = await userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            else
                return false;
        }
    }
}
