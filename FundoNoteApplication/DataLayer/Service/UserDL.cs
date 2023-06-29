using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Sockets;

namespace DataLayer.Service
{
    public class UserDL : IUserDL
    {
        private readonly FundoContext context;
        private readonly string _secret;
        private readonly string _expDate;
        public UserDL(FundoContext context, IConfiguration appsettings)
        {
            this.context = context;
            _secret = appsettings.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = appsettings.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }
        /// <summary>
        /// Register the user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserEntity Register(UserRegistration user)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.Email = user.Email;
                userEntity.Password = user.Password;
                userEntity.RegisterAt = DateTime.Now;
                context.UserTable.Add(userEntity);
                context.SaveChanges();
                if (userEntity != null)
                {
                    return userEntity;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        } 
        /// <summary>
        /// Login the user who was registered
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        public string Login(UserLogin Login)
        {
            try
            {
                UserEntity entity = new UserEntity();
                entity = this.context.UserTable.FirstOrDefault(x => x.Email == Login.Email && x.Password == Login.Password);
                if (entity != null)
                {
                    var mail = entity.Email;
                    var id = entity.UserId;
                  //  string pass = ;
                    var Token = GenerateSecurityToken(mail, id);
                    return Token;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Generating Token Using Email and UserId
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GenerateSecurityToken(string Email, long UserID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim("userID", UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        /// <summary>
        /// If user Forget the PassWord we can send token to the Email
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        public string ForgetPassword(string EmailId)
        {
            try
            {

                var Result = context.UserTable.FirstOrDefault(x => x.Email == EmailId);
                if (Result != null)
                {
                    var Token = GenerateSecurityToken(EmailId, Result.UserId);
                    new MSMQModel().sendData2Queue(Token);
                    return Token;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Reseting the Password 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public string ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    UserEntity userEntity = context.UserTable.FirstOrDefault(x => x.Email == email);
                    userEntity.Password = password;
                    if (userEntity != null)
                    {
                        context.SaveChanges();
                        return "Your password has been reset.";
                    }
                    else
                    {
                        return null;
                    }
                }
                //UserEntity userEntity = new UserEntity();
                //userEntity = context.UserTable.FirstOrDefault(x => x.Email == email);
                //if (userEntity != null)
                //{
                //    userEntity.Password = password;
                //    context.SaveChanges();
                //    return "Done";
                //}
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Retriving all registered Users
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAllUsers()
        {
           var entity = this.context.UserTable.FirstOrDefault();

            if (entity != null)
            {
                return context.UserTable.ToList();
            }
            else return null;
        }
        /// <summary>
        /// Retriving all registered Users by ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserEntity GetAllUsersbyID(long userId)
        {
            UserEntity entity = new UserEntity();
            entity = this.context.UserTable.FirstOrDefault(e => e.UserId == userId);
            if (entity != null)
            {
                return entity;
            }
            else return null;
        
        }
    }
}
