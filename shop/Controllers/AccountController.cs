﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shop.Data;
using shop.Models;
using shop.ViewModels;


namespace shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View();

            if (loginViewModel.EmailAddress == null)
            {
                throw new InvalidOperationException("Email is null.");
            }
            if (loginViewModel.Password == null) 
            {
                throw new InvalidOperationException("Password is null.");
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                if (!addToRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(newUser);
                    TempData["Error"] = "Failed to assign role to the user";
                    return View(registerViewModel);
                }

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
