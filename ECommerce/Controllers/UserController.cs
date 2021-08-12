﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ECommerce.Api.Controllers;
using ECommerce.Entities.Concrete;
using ECommmerce.Entities.Dtos;
using ECommmerce.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// TODO: Add Authentication and Authorization
// TODO: Use AutoMapper to map UserDtos to user (make it Service Layer)
// TODO: Change paramaters and returning values on methods in Service Layer.

namespace ECommerce
{   
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
                    return user;
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
    }
}
