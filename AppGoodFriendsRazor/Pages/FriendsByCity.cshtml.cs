using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class FriendsByCity : PageModel
    {
        readonly IFriendsService _service;

        //friends
        public List<IFriend> Friends {get; set;}
        
        public string SelectedCity { get; set; }

        public async Task <IActionResult> OnGet(string city)
        {
            SelectedCity = city;

            var dbInfo = await _service.InfoAsync;

            var result = await _service.ReadFriendsAsync(true, false, city, 0, 100);
            Friends = result.PageItems.ToList();


            /*var friends = result.PageItems.Select(f => new 
            {
                f.FirstName,
                f.LastName,
            })
            .ToList();*/
            

            return Page();
        }

        public FriendsByCity (IFriendsService service)
        {
            _service = service;
        }
    }
}