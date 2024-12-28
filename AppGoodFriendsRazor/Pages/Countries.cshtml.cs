using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class CountryInfo {
        public string Country { get; set; }
        public int NrFriends { get; set; }
        public string City { get; set; }
    }
        
    public class Countries : PageModel
    {

        readonly IFriendsService _service;

        //countries
        public List<CountryInfo> countries {get; set;} = new List<CountryInfo>();

        //public List<string> countries {get; set;} = new List<string>();

        public async Task <IActionResult> OnGet()
        {
            var dbInfo = await _service.InfoAsync;

            var noNullFriends = dbInfo.Friends
            .Where(x => x.Country != null && x.City != null).ToList();

            countries = noNullFriends
            .GroupBy(f => f.Country)
            .Select(g => new CountryInfo
            {
                Country = g.Key,
                NrFriends = g.Sum(f => f.NrFriends),
                City = g.Select(a => a.City).Distinct().Count().ToString()
                //City = g.Where(a => a.City != null).Count().ToString()
            })
            .Where(a => a.Country != null && a.City != null)
            .ToList();

            return Page();
        }

        public Countries (IFriendsService service)
        {
            _service = service;
        }
    }
}
