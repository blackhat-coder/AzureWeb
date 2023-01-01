using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;

namespace COREIdentityAuth.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential { get; set; }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (credential.UserName == "admin" && credential.Password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "admin@gmail.com"),
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim("Department", "hr"),
                    new Claim("EmploymentDate", "2022-10-10")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuthA");
                ClaimsPrincipal claimsprincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimsprincipal);

                return Redirect("/");
            }else if (credential.UserName == "user" && credential.Password == "password")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, "user@gmail.com"),
                    new Claim(ClaimTypes.Name, credential.UserName),
                    new Claim("Department", "general")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "My");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return Redirect("/");
            }

            return Page();
        }
    }
    public class Credential
    {
        [Required]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
