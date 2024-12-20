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
        
        //selected city
        public string SelectedCity { get; set; }

        //pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public async Task <IActionResult> OnGet(string city, int pageNumber = 1)
        {
            SelectedCity = city;

            CurrentPage = pageNumber;
            int pageSize = 10;

            //var dbInfo = await _service.InfoAsync;

            var result = await _service.ReadAddressesAsync(true, false, SelectedCity, 0, 100);
            Friends = result.PageItems.SelectMany(a => a.Friends).ToList();
            
            return Page();
        }

        public FriendsByCity (IFriendsService service)
        {
            _service = service;
        }
    }
}