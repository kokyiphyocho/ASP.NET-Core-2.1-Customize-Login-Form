using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityExtension.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IdentityExtension.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]            
            [Display(Name = "Step")]
            public string Step { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            [StringLength(20, MinimumLength = 3, ErrorMessage ="The {0} must between {1} to {2} characters.")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime DateofBirth { get; set; }
                        
            [DataType(DataType.Text)]
            [Display(Name = "Mobile Contact No.")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string MobileContact { get; set; }
                        
            [DataType(DataType.Text)]
            [Display(Name = "Home Contact No.")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string HomeContact { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Street Address")]            
            public string StreetAddress { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "City")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string City { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postal Code")]
            [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string PostalCode { get; set; }
                        
            [DataType(DataType.Text)]
            [Display(Name = "Province")]            
            public string Province { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} must between {1} to {2} characters.")]
            public string Country { get; set; }            
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Gender = Input.Gender,
                    DateOfBirth = Input.DateofBirth,
                    MobileContact = Input.MobileContact,
                    HomeContact = Input.HomeContact,
                    StreetAddress = Input.StreetAddress,
                    City = Input.City,
                    PostalCode = Input.PostalCode,
                    Province = Input.Province,
                    Country = Input.Country,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
