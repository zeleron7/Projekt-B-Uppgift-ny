using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace AppGoodFriendsRazor.Pages
{
     public class CitiesInfo
    {
        public string City { get; set; }
        public int NrFriends { get; set; }
        public int NrPets { get; set; }
    }

    public class Cities : PageModel
    {
        readonly IFriendsService _service;
       
        public List<CitiesInfo> CitiesInfo { get; set; } = new List<CitiesInfo>();

        public string SelectedCountry { get; set; }

        public async Task <IActionResult> OnGet(string country)
        {

            SelectedCountry = country;

            var dbInfo = await _service.InfoAsync;

            var friends = dbInfo.Friends
                .Where(f => f.Country == country && f.City != null)
                .ToList();

            var pets = dbInfo.Pets
                .Where(p => p.Country == country && p.City != null)
                .ToList();

            var cityGroups = friends.GroupBy(f => f.City)
                .Select(g => new CitiesInfo
                {
                    City = g.Key,
                    NrFriends = g.Sum(f => f.NrFriends),
                    NrPets = pets.Where(p => p.City == g.Key).Sum(p => p.NrPets)
                })
                .ToList();

            CitiesInfo.AddRange(cityGroups);
            
            return Page();
        }

        public Cities (IFriendsService service)
        {
            _service = service;
        }
    }
}

