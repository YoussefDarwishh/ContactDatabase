using ContactDatabase.Pages;
using EdgeDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class EditModel : PageModel
{
    private readonly EdgeDBClient _client;

    public EditModel(EdgeDBClient client)
    {
        _client = client;
    }

    [BindProperty(SupportsGet = true)]
    public string Username { get; set; }

    [BindProperty]
    public ContactInput ContactInput { get; set; }

    public ContactView ContactView { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        ContactView = await GetContactByUsernameAsync(Username);
        if (ContactView == null)
        {
            return NotFound();
        }
        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await UpdateContactAsync();

        return RedirectToPage("/Admin");
    }

    private async Task<ContactView> GetContactByUsernameAsync(string username)
    {
        string query = "SELECT Contact {*} FILTER .username = <str>$username";
        var parameters = new Dictionary<string, object> { { "username", username } };
        var result = await _client.QueryAsync<ContactView>(query, parameters);
        return result.FirstOrDefault();
    }

    private async Task UpdateContactAsync()
    {
        var passwordHasher = new PasswordHasher<string>();
        ContactInput.Password = passwordHasher.HashPassword(null, ContactInput.Password);
        string query = @"
            UPDATE Contact FILTER .username = <str>$username
            SET {
                first_name := <str>$firstName,
                last_name := <str>$lastName,
                email := <str>$email,
                title := <str>$title,
                description := <str>$description,
                date_of_birth := <datetime>$dateOfBirth,
                marriage_status := <bool>$marriageStatus,
                username := <str>$username,
                password := <str>$password,
                role := <str>$role
            }";


        var parameters = new Dictionary<string, object>
        {
            { "firstName", ContactInput.FirstName },
            { "lastName", ContactInput.LastName },
            { "email", ContactInput.Email },
            { "title", ContactInput.Title },
            { "description", ContactInput.Description },
            { "dateOfBirth", ContactInput.DateOfBirth },
            { "marriageStatus", ContactInput.MarriageStatus },
            { "username", ContactInput.Username },
            { "password", ContactInput.Password },
            { "role", ContactInput.Role }
        };

        await _client.ExecuteAsync(query, parameters);
    }
}
