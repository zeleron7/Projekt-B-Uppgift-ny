using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class Countries : PageModel
    {
        readonly IFriendsService _service;

        //Sweden
        public int NrFriendsSweden {get; set;}
        public int NrCitiesSweden {get; set;}

        //Norway
        public int NrFriendsNorway {get; set;}
        public int NrCitiesNorway {get; set;}

        //Denmark
        public int NrFriendsDenmark {get; set;}
        public int NrCitiesDenmark {get; set;}

        //Finland
        public int NrFriendsFinland {get; set;}
        public int NrCitiesFinland {get; set;}
        
        public async Task <IActionResult> OnGet()
        {
            GstUsrInfoAllDto dbInfo = await _service.InfoAsync;

            //Sweden
            NrFriendsSweden = dbInfo.Friends.Where(f => f.Country == "Sweden").Sum(f => f.NrFriends);
            NrCitiesSweden = dbInfo.Friends.Count(f => f.Country == "Sweden");

            //Norway
            NrFriendsNorway = dbInfo.Friends.Where(f => f.Country == "Norway").Sum(f => f.NrFriends);
            NrCitiesNorway = dbInfo.Friends.Count(f => f.Country == "Norway");

            //Denmark
            NrFriendsDenmark = dbInfo.Friends.Where(f => f.Country == "Denmark").Sum(f => f.NrFriends);
            NrCitiesDenmark = dbInfo.Friends.Count(f => f.Country == "Denmark");

            //Finland
            NrFriendsFinland = dbInfo.Friends.Where(f => f.Country == "Finland").Sum(f => f.NrFriends);
            NrCitiesFinland = dbInfo.Friends.Count(f => f.Country == "Finland");

            return Page();
        }

        public Countries (IFriendsService service)
        {
            _service = service;
        }
    }
}
