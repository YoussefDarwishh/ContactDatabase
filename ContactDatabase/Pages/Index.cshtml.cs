using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
namespace ContactDatabase.Pages;

public class IndexModel : PageModel
{
    public List<Contact> Contacts { get; set; }

    public void OnGet()
    {
        Contacts = GetContactsFromDatabase();
    }

    public IActionResult OnPost()
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
        return RedirectToPage();
    }

    private List<Contact> GetContactsFromDatabase()
    {

        return new List<Contact>
        {
        };
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