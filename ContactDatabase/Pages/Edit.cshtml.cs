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
        public string ID { get; set; }

        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("/Index");
        }

        private async Task<Contact> GetContactByIDAsync(string id)
        {
            var result = await _client.QueryAsync<Contact>("SELECT YourEdgeDBModel FILTER .id = <uuid>$id", new { id });
            return result.FirstOrDefault();
        }
    }
}
