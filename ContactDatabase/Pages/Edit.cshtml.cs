using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Contact = await GetContactByIDAsync(ID.ToString());
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

            return RedirectToPage("/Index");
        }

        private async Task<Contact> GetContactByIDAsync(string id)
        {
            Console.WriteLine("h");
            var result = await _client.QueryAsync<Contact>("SELECT Contact FILTER .id = <uuid>$id", new { id });
            return result.FirstOrDefault();
        }
        private async Task UpdateContactAsync()
        {
            await _client.ExecuteAsync(@"
            UPDATE Contact FILTER .id = <uuid>$id
            SET {
                FirstName := <str>$firstName,
                LastName := <str>$lastName,
                Email := <str>$email,
                Title := <str>$title,
                Description := <str>$description,
                DateOfBirth := <cal::local_date>$dateOfBirth,
                MarriageStatus := <bool>$marriageStatus
            }",
                new
                {
                    id = ID,
                    firstName = Contact.FirstName,
                    lastName = Contact.LastName,
                    email = Contact.Email,
                    title = Contact.Title,
                    description = Contact.Description,
                    dateOfBirth = Contact.DateOfBirth,
                    marriageStatus = Contact.MarriageStatus
                });
        }
    }
}
