using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stone.Core.DTOs.UserDTO;
using Stone.Core.Sender;
using Stone.Core.Services.Interfaces;

namespace API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Register(UserViewModelDto registerViewModel)
        {
            if (await _userService.IsExistMobile(registerViewModel.Mobile))
            {
                //Sending Mobile Exist Error To User 
                return BadRequest(new { message = "Mobile Is Exist, Please Login Using Your Password" });
            }
            else
            {
                CheckActiveCodeViewModelDto CheckAC = await _userService.AddUser(registerViewModel);
                if (CheckAC != null)
                {
                    //SMS sms = new SMS();
                    //await sms.Send(CheckAC.Mobile, CheckAC.APIAvtiveCode);
                    return Ok(CheckAC);
                }
                else
                {
                    //Unexpected ERROR
                    return BadRequest(new { message = "Error While Registering" });
                }
            }
        }

        [HttpPost("[action]")]
        //sobhanallah
        public async Task<IActionResult> Activated(CheckActiveCodeViewModelDto checkAC)
        {
            ProfileViewModelDto profileViewModel =await _userService.Activated(checkAC);
            return Ok(profileViewModel);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModelDto loginViewModelDto)
        {
            var userProfile = await _userService.Login(loginViewModelDto);
            if (userProfile== null)
            {
                return BadRequest(new { message = "Username or Password in incorrect" });
            }
            return Ok(userProfile);
        }

        //[Authorize]
        //[HttpPost("[action]")]
        //public async Task<IActionResult> Siamak()
        //{
        //    return Ok();
        //}
    }
}
