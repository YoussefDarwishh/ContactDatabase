using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using EdgeDB;
namespace ContactDatabase.Pages;

public class AdminModel : PageModel
{
    private readonly EdgeDBClient _client;
    public List<Contact> Contacts { get; set; }

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
        var contact = new Contact
        {
            FirstName = Request.Form["FirstName"],
            LastName = Request.Form["LastName"],
            Email = Request.Form["Email"],
            Title = Request.Form["Title"],
            Description = Request.Form["Description"],
            DateOfBirth = DateTime.Parse(Request.Form["DateOfBirth"]),
            MarriageStatus = Request.Form.ContainsKey("MarriageStatus"),
            Username = Request.Form["Username"],
            Password = Request.Form["Password"],
            Role = Request.Form["Role"]
        };

        await _client.ExecuteAsync($$"""                       
        INSERT Contact {                                   
            first_name := "{{contact.FirstName}}",                                   
            last_name := "{{contact.LastName}}",                                   
            email := "{{contact.Email}}",                                   
            title := "{{contact.Title}}",                                   
            description := "{{contact.Description}}",                                   
            date_of_birth := <datetime>"{{contact.DateOfBirth.ToString("yyyy-MM-ddTHH:mm:ssZ")}}",                                   
            marriage_status := {{contact.MarriageStatus}},
            username := "{{contact.Username}}",                                   
            password := "{{contact.Password}}",                                   
            role := "{{contact.Role}}"
        }           
    """);




        return RedirectToPage();
    }



    private async Task<List<Contact>> GetContactsFromDatabaseAsync()
    {
        var result = await _client.QueryAsync<Contact>("SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status }");
        return result.ToList();
    }


}