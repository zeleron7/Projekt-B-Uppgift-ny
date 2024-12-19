using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class FriendsByCity : PageModel
    {
        readonly IFriendsService _service;

        //friends
        public List<GstUsrInfoFriendsDto> friends {get; set;} = new List<GstUsrInfoFriendsDto>();
        public string SelectedCity { get; set; }

        public async Task <IActionResult> OnGet(string city)
        {
            SelectedCity = city;

            var dbInfo = await _service.InfoAsync;

            friends = dbInfo.Friends
            .GroupBy(f => f.City)
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

        public FriendsByCity (IFriendsService service)
        {
            _service = service;
        }
    }
}