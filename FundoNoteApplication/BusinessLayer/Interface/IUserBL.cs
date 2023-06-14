using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {

        public UserEntity Register(UserRegistration user);
        
        public string Login(UserLogin Login);
        public string ForgetPassword(string EmailId);
        public bool ResetPassword(string email, string password, string confirmPassword);

    }
}
