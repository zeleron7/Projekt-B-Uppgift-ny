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

        //public List<string> countries {get; set;} = new List<string>();

        public async Task <IActionResult> OnGet()
        {
            
            var dbInfo = await _service.InfoAsync;

            countries = dbInfo.Friends
            .GroupBy(f => f.Country)
            .Select(g => new GstUsrInfoFriendsDto
            {
                Country = g.Key,
                NrFriends = g.Sum(f => f.NrFriends),
                City = g.Where(a => a.City != null).Count().ToString()
            })
            .Where(a => a.Country != null)
            .ToList();

            return Page();
        }

        public Countries (IFriendsService service)
        {
            _service = service;
        }
    }
}
