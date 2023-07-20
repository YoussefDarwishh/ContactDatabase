using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using EdgeDB;
namespace ContactDatabase.Pages;

public class IndexModel : PageModel
{
    private readonly EdgeDBClient _client;
    public List<Contact> Contacts { get; set; }

    public IndexModel(EdgeDBClient client)
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
            MarriageStatus = Request.Form.ContainsKey("MarriageStatus")
        };

        await _client.ExecuteAsync("INSERT Contact { FirstName := <str>$firstName, LastName := <str>$lastName, Email := <str>$email, Title := <str>$title, Description := <str>$description, DateOfBirth := <datetime>$dateOfBirth, MarriageStatus := <bool>$marriageStatus }",
                        new Dictionary<string, object>
                        {
                        { "firstName", contact.FirstName },
                        { "lastName", contact.LastName },
                        { "email", contact.Email },
                        { "title", contact.Title },
                        { "description", contact.Description },
                        { "dateOfBirth", contact.DateOfBirth },
                        { "marriageStatus", contact.MarriageStatus }
                        });

        return RedirectToPage();
    }



    private async Task<List<Contact>> GetContactsFromDatabaseAsync()
    {
        var result = await _client.QueryAsync<Contact>("SELECT Contact { FirstName, LastName, Email, Title, Description, DateOfBirth, MarriageStatus }");
        return result.ToList();
    }


}

public class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool MarriageStatus { get; set; }
}