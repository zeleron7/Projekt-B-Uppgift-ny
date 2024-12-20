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
        public int NrOfPages { get; set; }
        public int PageSize { get; } = 10;

        public int ThisPageNr { get; set; } = 0;
        public int PrevPageNr { get; set; } = 0;
        public int NextPageNr { get; set; } = 0;
        public int PresentPages { get; set; } = 0;


        public async Task <IActionResult> OnGet(string city, string pagenr)
        {
            SelectedCity = city;

            //read a queryparameter
            /*if (int.TryParse(pagenr, out int _pagenr))
            {
                ThisPageNr = _pagenr;
            }*/
            
            //Friends = _service.ReadAddressesAsync(true, false, SelectedCity, ThisPageNr, PageSize).Result.PageItems.SelectMany(a => a.Friends).ToList();

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