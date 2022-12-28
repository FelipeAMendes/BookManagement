using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BookManagement.Modules.Web.Models;

public class PaginationPageModel : PageModel
{
    public Dictionary<string, string> LinkData { get; set; }

    public PaginationPageModel()
    {
        LinkData ??= new Dictionary<string, string>();
        LinkData.Add("sort", Sort);
    }

    [BindProperty(SupportsGet = true, Name = "p")]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string Sort { get; set; }

    public void AddPaginationData(string key, string value)
    {
        LinkData.Add(key, value);
    }
}