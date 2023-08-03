using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using EdgeDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ContactDatabase.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public LoginInput LoginInput { get; set; }
    private readonly EdgeDBClient _client;
    private readonly IPasswordHasher<ContactView> _passwordHasher;

    public IndexModel(EdgeDBClient client, IPasswordHasher<ContactView> passwordHasher)
    {
        _client = client;
        _passwordHasher = passwordHasher;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid Login Attempt");
            return Page();
        }
        var results = await _client.QueryAsync<ContactView>("SELECT Contact {first_name, last_name, email, title, description, date_of_birth, marriage_status, username, password, role } FILTER .username = <str>$username", new Dictionary<string, object?>
        {
            { "username", LoginInput.Username }
        });
        ContactView? user = results.FirstOrDefault();

        if (user is not null)
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, LoginInput.Password);
            if(result == PasswordVerificationResult.Success)
            {
                if (user.Role == "admin")
                    return RedirectToPage("/Admin");
                else
                    return RedirectToPage("/User");
            }
        }
        return Page();
    }
    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage();
    }
}

public class LoginInput
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}