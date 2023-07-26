using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using EdgeDB;

namespace ContactDatabase.Pages;

public class UserModel : PageModel
{
    private readonly EdgeDBClient _client;
    public List<Contact> Contacts { get; set; }

    public UserModel(EdgeDBClient client)
    {
        _client = client;
    }

    public async Task OnGetAsync()
    {
        Contacts = await GetContactsFromDatabaseAsync();
    }

    private async Task<List<Contact>> GetContactsFromDatabaseAsync()
    {
        var result = await _client.QueryAsync<Contact>("SELECT Contact { first_name, last_name, email, title, description, date_of_birth, marriage_status } FILTER .role = 'normal'");
        return result.ToList();
    }
}
