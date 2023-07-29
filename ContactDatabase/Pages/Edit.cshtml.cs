using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ContactDatabase.Pages
{
    public class EditModel : PageModel
    {
        private readonly EdgeDBClient _client;

        public EditModel(EdgeDBClient client)
        {
            _client = client;
        }

        [BindProperty(SupportsGet = true)]
        public Guid ID { get; set; }
        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Contact = await GetContactByIDAsync(ID);
            if(Contact == null)
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

        private async Task<Contact> GetContactByIDAsync(Guid id)
        {
            string query = "SELECT Contact {*} FILTER .id = <uuid>$id";
            var parameters = new Dictionary<string, object> { { "id", id } };
            var result = await _client.QueryAsync<Contact>(query, parameters);
            return result.FirstOrDefault();
        }


        private async Task UpdateContactAsync()
        {
            var passwordHasher = new PasswordHasher<string>();
            Contact.Password = passwordHasher.HashPassword(null, Contact.Password);
            string query = @"
            UPDATE Contact FILTER .id = <uuid>$id
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
                  { "id", ID },
                { "firstName", Contact.FirstName },
                { "lastName", Contact.LastName },
                { "email", Contact.Email },
                { "title", Contact.Title },
                { "description", Contact.Description },
                { "dateOfBirth", Contact.DateOfBirth },
                { "marriageStatus", Contact.MarriageStatus },
                { "username", Contact.Username },
                { "password", Contact.Password },
                { "role", Contact.Role }
            };

            await _client.ExecuteAsync(query, parameters);
        }
    }
}
