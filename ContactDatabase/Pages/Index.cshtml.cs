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
        // Retrieve contacts from the database (EdgeDB) and populate the Contacts property
        Contacts = GetContactsFromDatabase();
    }

    public IActionResult OnPost()
    {
        // Create a new Contact object and populate its properties from the form data
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

        // Store the contact in the database (EdgeDB) or perform any other desired action

        // Redirect back to the same page to display the updated list of contacts
        return RedirectToPage();
    }

    private List<Contact> GetContactsFromDatabase()
    {
        // Retrieve contacts from the database (EdgeDB) and return them as a list
        // Implement your logic to fetch the contacts from the database
        // For this example, we'll return a hardcoded list of contacts

        return new List<Contact>
        {
            new Contact
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Title = "Mr",
                Description = "Lorem ipsum dolor sit amet",
                DateOfBirth = new DateTime(1990, 5, 10),
                MarriageStatus = true
            },
            new Contact
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "janesmith@example.com",
                Title = "Mrs",
                Description = "Consectetur adipiscing elit",
                DateOfBirth = new DateTime(1985, 8, 15),
                MarriageStatus = false
            }
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