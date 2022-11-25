using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Dtos.Email;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IProductService _productService;


        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IProductService productService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _productService = productService;
        }

        //Methods

        //Method for login
        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credentials for {request.Email}.";
                return response;
            }
            if (!user.IsVerified)
            {
                response.HasError = true;
                response.Error = $"Cuenta inactiva comuniquese con el administrador.";
                return response;
            }

            response.Id = user.Id;
            response.IdCard = user.IdCard;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var listRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = listRoles.ToList();
            response.IsVerified = user.IsVerified;

            return response;
        }

        //method for signout
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //method for create a new basic user
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (userNameExist != null)
            {
                response.HasError = true;
                response.Error = $"User '{request.UserName}' already exist.";
                return response;
            }

            var emailExist = await _userManager.FindByEmailAsync(request.Email);
            if (emailExist != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                IdCard = request.IdCard,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsVerified = request.IsVerified,
                TypeUser = request.TypeUser
                
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred when trying to register the user.";
                return response;
            }

            if (user.TypeUser == (int)Roles.Admin)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                
            } else
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                await _productService.AddSavingAccountAsync(user.Id, request.Amount);
            }

            await _emailService.SendAsync(new EmailRequest
            {
                To = request.Email,
                Subject = "Your Internet Banking App account has been created!\r\n",
                Body = $"<h1>Welcome to Internet Banking App 🏦</h1>" +
                            $"<p>Hi {request.FirstName} {request.LastName} 😃</p>" +
                            $"<p>Thanks for select us for manage your preducts and make your transfers. Your username is <strong>{request.UserName}</strong>.</p>"

            });

            return response;
        }

        //method for update an user
        public async Task<UpdateResponse> UpdateUserAsync(UpdateRequest request, string id)
        {
            UpdateResponse response = new();
            response.HasError = false;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.TypeUser == (int)Roles.Admin)
                {
                    user.IdCard = request.IdCard;
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.UserName = request.UserName;
                    user.Email = request.Email;
                    user.PhoneNumber = request.PhoneNumber;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error when trying update the user";
                        return response;

                    }
                    return response;
                }
                else
                {
                    user.IdCard = request.IdCard;
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.UserName = request.UserName;
                    user.Email = request.Email;
                    user.PhoneNumber = request.PhoneNumber;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error when trying update the user";
                        return response;
                    }
                    await _productService.AddAmountSavingAccount(user.Id, request.Amount);
                    return response;
                }
                
            }
            else
            {
                response.HasError = true;
                response.Error = $"No accounts exists with this id: {id}";
                return response;
            }
        }

        //method to activate an user
        public async Task<UpdateResponse> ActivedUserAsync(string id)
        {
            UpdateResponse response = new();
            response.HasError = false;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.IsVerified)
                {
                    user.IsVerified = false;
                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error when trying desactive the user";
                        return response;

                    }
                    return response;
                }
                else
                {
                    user.IsVerified = true;
                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = "Error when trying desactive the user";
                        return response;

                    }
                    return response;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"No accounts exists with this id: {id}";
                return response;
            }
        }
        //method to confirm the account of the user
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No Accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return $"An error occurred while confirming {user.Email}.";
            }

            return $"Account confirmed for {user.Email}. You can now use the app";
        }

        //method to send an email to the user for reset their password
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new(); 
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No user registered with {request.Email}.";
                return response;
            }

            var forgotPasswordUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Restore Your Imternet Banking Password!\r\n",
                Body = $"<h1>Welcome back to Internet Banking App 🏦</h1>" +
                $"<p>Hi {user.FirstName} {user.LastName},</p>" +
                $"<p>Your restore password request has been received successfully. ✅"+
                $"<p>Please try to don't forget your password again. 😕</p>" +
                $"<p><strong>Click the following URL to restore your password</strong> 👉🏻 {forgotPasswordUri}</p>"

            });

            return response;
        }


        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request) 
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No user registered with {request.Email}.";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset the password.";
                return response;
            }

            return response;
        }


        //Method to get all users
        public async Task<List<AuthenticationResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            List<AuthenticationResponse> response = new();

            foreach (var user in users)
            {
                var rol = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                AuthenticationResponse user_res = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IdCard = user.IdCard,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = rol.ToList(),
                    IsVerified = user.IsVerified
                };

                response.Add(user_res);
            };

            return response;
        }

        //Method to get all users
        public async Task<AuthenticationResponse> GetUserById(string id)
        {
            AuthenticationResponse response = new();
            ApplicationUser user = new();
            user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                response.Id = user.Id;
                response.IdCard = user.IdCard;
                response.Email = user.Email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.UserName = user.UserName;
                response.PhoneNumber = user.PhoneNumber;
                response.IsVerified = user.IsVerified;
                response.TypeUser = user.TypeUser;

                return response;
            }

            response.HasError = true;
            response.Error = $"Not user exists with this id: {id}";
            return response;
        }

        //Method private to form the url for the emailVerificationUser page
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", token);

            return verificationUri;
        }

        //Method private to form the url for the ForgotPassword page
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var forgotPasswordUri = QueryHelpers.AddQueryString(uri.ToString(), "token", token);
            
            return forgotPasswordUri;
        }

        //private async Task<int[]> 
    }
}
