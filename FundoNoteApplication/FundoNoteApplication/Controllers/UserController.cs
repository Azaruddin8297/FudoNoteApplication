﻿using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, msg = "Registration Sucessfull", data = result });

                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Registration UnSucessfull" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Add([FromBody] UserLogin LoginRegistration)
        {
            try
            {
                var reg = this.userBL.Login(LoginRegistration);
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (reg != null)
                {
           
                    return this.Ok(new { Success = true, message = "Login Sucessfull", Data = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Unsucessfull" });
                }
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassWord")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {

                var reg = this.userBL.ForgetPassword(EmailId);
                if (reg != null)

                {
                    
                    return this.Ok(new { Success = true, message = "Token sent Sucessfully please check your mail" ,Data = reg});
                }
                else
                {
                
                    return this.BadRequest(new { Success = false, message = "unable to send token to mail" });
                }
            }
            catch (Exception ex)
            {
              
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
              
                var email = User.Claims.First(e => e.Type == "Email").Value;
                var result = userBL.ResetPassword(email, password, confirmPassword);

                if (userBL.ResetPassword(email, password, confirmPassword))
                {
                   
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
              
                throw;
            }
        }
    }
}
