﻿using System;
using System.Security.Claims;
using ECommerce.Api.Controllers;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Authentication;
using ECommmerce.Entities.Dtos;
using ECommmerce.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// TODO: Add Authentication and Authorization
// TODO: Use AutoMapper to map UserDtos to user (make it Service Layer)
// TODO: Change paramaters and returning values on methods in Service Layer.

namespace ECommerce
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<User> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByEmailAndPassword(userLoginDto.Email, userLoginDto.Password);
                if (user.UserName != null && user.Password != null)
                {
                    _authenticationService.Authenticate(user);
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Wrong Email or Password");
                }
            }
            else
            {
                return BadRequest("Wrong Email or Password");
            }
        }
        [HttpPost("adduser")]
        public ActionResult Add(UserAddDto userAddDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (ModelState.IsValid)
            {
                _userService.Add(new User
                {
                    Email = userAddDto.Email,
                    Password = userAddDto.Password,
                    UserName = userAddDto.UserName,
                    CreatedByName = "",
                    ModifiedByName = "",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Description = "",
                    Note = "",
                }, "");
                return Ok("Success.");
            }
            else
            {
                return BadRequest("Wrong Email or Password");
            }
        }
        [HttpPost("changeLanguage")]
        public ActionResult SetLanguage(int id)
        {
            _authenticationService.SetLanguage(id);
            return Ok("success");
        }
        [HttpGet("logout")]
        public ActionResult Logout()
        {
            try
            {
                _authenticationService.Logout();
            }
            catch (Exception exception)
            {
                return Unauthorized();
            }
            return Ok("Logged Out");
        }
    }
}
