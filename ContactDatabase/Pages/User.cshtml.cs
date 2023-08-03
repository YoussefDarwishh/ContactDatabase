using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using EdgeDB;

namespace ContactDatabase.Pages;

public class UserModel : PageModel
{
    private readonly EdgeDBClient _client;
    public List<ContactView> Contacts { get; set; }

    public UserModel(EdgeDBClient client)
    {
        _client = client;
    }

    public async Task OnGetAsync()
    {
        Contacts = await GetContactsFromDatabaseAsync();
    }

    private async Task<List<ContactView>> GetContactsFromDatabaseAsync()
    {
        var result = await _client.QueryAsync<ContactView>("SELECT Contact { id, first_name, last_name, email, title, description, date_of_birth, marriage_status } FILTER .role = 'normal'");
        return result.ToList();
    }
}
