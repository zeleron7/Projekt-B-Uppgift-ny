using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class Countries : PageModel
    {
        readonly IFriendsService _service;

        #region test
        
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

        #endregion test
        
        //countries
        public List<GstUsrInfoFriendsDto> countries {get; set;} = new List<GstUsrInfoFriendsDto>();
        //public List<GstUsrInfoAllDto> countries {get; set;} = new List<GstUsrInfoAllDto>();

        public async Task <IActionResult> OnGet()
        {
            
            var dbInfo = await _service.InfoAsync;

            countries = dbInfo.Friends
            .GroupBy(f => f.Country)
            .Select(g => new GstUsrInfoFriendsDto
            {
                Country = g.Key,
                NrFriends = g.Sum(f => f.NrFriends),
                City = g.Count().ToString()
            })
            .ToList();

            /*countries = dbInfo.Friends
            .GroupBy(f => f.Country)
            .Select(g => new GstUsrInfoAllDto
            {
                Db = null, // Or populate this if needed
                Friends = g.Select(f => new GstUsrInfoFriendsDto
                {
                    Country = f.Country,
                    NrFriends = f.NrFriends,
                    City = f.City
                }).ToList(),
                Pets = null, // Or populate this if needed
                Quotes = null // Or populate this if needed
            })
            .ToList();*/
            
            #region test
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
            #endregion test

            return Page();
        }

        public Countries (IFriendsService service)
        {
            _service = service;
        }
    }
}
