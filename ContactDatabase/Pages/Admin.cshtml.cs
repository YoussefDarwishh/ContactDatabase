using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using EdgeDB;
using Microsoft.AspNetCore.Identity;

namespace ContactDatabase.Pages;

public class AdminModel : PageModel
{
    private readonly EdgeDBClient _client;
    public List<ContactView> Contacts { get; set; }

    [BindProperty]
    public ContactInput Contact { get; set; }

    public AdminModel(EdgeDBClient client)
    {
        _client = client;
    }

    public async Task OnGetAsync()
    {
        Contacts = await GetContactsFromDatabaseAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var passwordHasher = new PasswordHasher<string>();
        Contact.Password = passwordHasher.HashPassword(null, Contact.Password);

        var parameters = new Dictionary<string, object>
        {
            { "first_name", Contact.FirstName },
            { "last_name", Contact.LastName },
            { "email", Contact.Email },
            { "title", Contact.Title },
            { "description", Contact.Description },
            { "date_of_birth", Contact.DateOfBirth },
            { "marriage_status", Contact.MarriageStatus },
            { "username", Contact.Username },
            { "password", Contact.Password },
            { "role", Contact.Role }
        };

        await _client.ExecuteAsync($$"""                       
        INSERT Contact {                                   
            first_name := <str>$first_name,                                   
            last_name := <str>$last_name,                                   
            email := <str>$email,                                   
            title := <str>$title,                                   
            description := <str>$description,                                   
            date_of_birth := <datetime>$date_of_birth,                                   
            marriage_status := <bool>$marriage_status,
            username := <str>$username,                                   
            password := <str>$password,                                   
            role := <str>$role
        }           
    """, parameters);

        return RedirectToPage();
    }

    private async Task<List<ContactView>> GetContactsFromDatabaseAsync()
    {
        var result = await _client.QueryAsync<ContactView>("SELECT Contact {first_name, last_name, email, title, description, username, date_of_birth, marriage_status }");
        return result.ToList();
    }
}

public class ContactInput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool MarriageStatus { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}

public class ContactView
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool MarriageStatus { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}