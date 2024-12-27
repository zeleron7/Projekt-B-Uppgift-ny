using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
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
       
        public string City { get; set; }
        public int NrFriends { get; set; }
        public int NrPets { get; set; }
        public List<CitiesInfo> CitiesInfo { get; set; } = new List<CitiesInfo>();


        //public List<dynamic> CityInfos { get; set; } = new List<dynamic>();
        public string SelectedCountry { get; set; }

        public async Task <IActionResult> OnGet(string country)
        {
            /*var dbInfo = await _service.InfoAsync;

            SelectedCountry = country;

             CityInfos = (from friend in dbInfo.Friends
                         join pet in dbInfo.Pets on friend.City equals pet.City into petGroup
                         from pet in petGroup.DefaultIfEmpty()
                         where friend.Country == country
                         group new { friend, pet } by friend.City into cityGroup
                         select new
                         {
                             City = cityGroup.Key,
                             NrFriends = cityGroup.Sum(x => x.friend.NrFriends),
                             NrPets = cityGroup.Sum(x => x.pet?.NrPets ?? 0),
                         })
                         .Where(a => a.City != null)
                         .ToList<dynamic>();*/


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

