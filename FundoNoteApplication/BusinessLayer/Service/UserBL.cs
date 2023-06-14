using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserDL userDL;
        public UserBL(IUserDL userDL)
        {
            this.userDL = userDL;
        }

        public UserEntity Register(UserRegistration user)
        {
            try
            {
                return userDL.Register(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin Login)
        {

            try
            {
                return this.userDL.Login(Login);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string EmailId)
        {

            try
            {
                return this.userDL.ForgetPassword(EmailId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return this.userDL.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
