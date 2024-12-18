using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class Sweden : PageModel
    {
        readonly IFriendsService _service;

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
        
        public async Task <IActionResult> OnGet()
        {
            GstUsrInfoAllDto dbInfo = await _service.InfoAsync;

            //Sweden
            NrFriendsStockholm = dbInfo.Friends.Where(f => f.Country == "Stockholm").Sum(f => f.NrFriends);
            NrPetsStockholm = dbInfo.Pets.Count(f => f.Country == "Stockholm");

            //Norway
            NrFriendsNorway = dbInfo.Friends.Where(f => f.Country == "Norway").Sum(f => f.NrFriends);
            NrPetsNorway = dbInfo.Pets.Count(f => f.Country == "Norway");

            //Denmark
            NrFriendsDenmark = dbInfo.Friends.Where(f => f.Country == "Denmark").Sum(f => f.NrFriends);
            NrPetsDenmark = dbInfo.Pets.Count(f => f.Country == "Denmark");

            //Finland
            NrFriendsFinland = dbInfo.Friends.Where(f => f.Country == "Finland").Sum(f => f.NrFriends);
            NrPetsFinland = dbInfo.Pets.Count(f => f.Country == "Finland");

            return Page();
        }

        public Sweden (IFriendsService service)
        {
            _service = service;
        }
    }
}

