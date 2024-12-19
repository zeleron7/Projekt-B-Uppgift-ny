using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class Cities : PageModel
    {
        readonly IFriendsService _service;

        #region test
        //Sweden
        #region Sweden

        //Stockholm
        public int NrFriendsStockholm {get; set;}
        public int NrPetsStockholm {get; set;}

        //Gothenburg
        public int NrFriendsGothenburg {get; set;}
        public int NrPetsGothenburg {get; set;}

        //Malmo
        public int NrFriendsMalmo {get; set;}
        public int NrPetsMalmo {get; set;}

        //Gavle
        public int NrFriendsGavle {get; set;}
        public int NrPetsGavle {get; set;}

        //Akersberga
        public int NrFriendsAkersberga {get; set;}
        public int NrPetsAkersberga {get; set;}

        #endregion Sweden

        //Norway
        public int NrFriendsNorway {get; set;}
        public int NrPetsNorway {get; set;}

        //Denmark
        public int NrFriendsDenmark {get; set;}
        public int NrPetsDenmark {get; set;}

        //Finland
        public int NrFriendsFinland {get; set;}
        public int NrPetsFinland {get; set;}
        #endregion test
        
        //cities
        //public List<GstUsrInfoPetsDto> cities {get; set;} = new List<GstUsrInfoPetsDto>();

        public List<dynamic> CityInfos { get; set; } = new List<dynamic>();
        public string SelectedCountry { get; set; }

        public async Task <IActionResult> OnGet(string country)
        {
            var dbInfo = await _service.InfoAsync;

            /*cities = dbInfo.Pets
            .GroupBy(f => f.City)
            .Select(g => new GstUsrInfoPetsDto
            {
                City = g.Key,
                NrPets = g.Sum(f => f.NrPets),
                Country = g.Count().ToString()
            })
            .ToList();*/

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
                         .ToList<dynamic>();
            
            return Page();
        }

        public Cities (IFriendsService service)
        {
            _service = service;
        }
    }
}

