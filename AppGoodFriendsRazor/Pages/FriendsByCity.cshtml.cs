using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace AppGoodFriendsRazor.Pages
{
    public class FriendsByCity : PageModel
    {
        readonly IFriendsService _service;

        //friends
        public List<IFriend> Friends {get; set;}

        public int NrFriends { get; set; }
        
        //selected city
        public string SelectedCity { get; set; }

        //pagination
        public int NrOfPages { get; set; }
        public int PageSize { get; } = 10;

        public int ThisPageNr { get; set; } = 0;
        public int PrevPageNr { get; set; } = 0;
        public int NextPageNr { get; set; } = 0;
        public int NrVisiblePages { get; set; } = 0;

        [BindProperty] 
        public string SearchFilter { get; set; } = null;


        public async Task <IActionResult> OnGet(string city)
        {
            SelectedCity = city;
            
            if (int.TryParse(Request.Query["pagenr"], out int pagenr))
            {
                ThisPageNr = pagenr;
            }

            SearchFilter = Request.Query["search"];
            
            var result = await _service.ReadAddressesAsync(true, false, SelectedCity, ThisPageNr, PageSize);
            Friends = result.PageItems.SelectMany(a => a.Friends).ToList();
            NrFriends = result.DbItemsCount;

            UpdatePagination(result.DbItemsCount);
            
            return Page();
        }

        private void UpdatePagination(int nrOfItems)
        {
            //Pagination
            NrOfPages = (int)Math.Ceiling((double)nrOfItems / PageSize);
            PrevPageNr = Math.Max(0, ThisPageNr - 1);
            NextPageNr = Math.Min(NrOfPages - 1, ThisPageNr + 1);
            NrVisiblePages = Math.Min(10, NrOfPages);
        }

        public FriendsByCity (IFriendsService service)
        {
            _service = service;
        }
    }
}