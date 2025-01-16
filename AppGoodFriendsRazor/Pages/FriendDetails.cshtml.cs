using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace AppGoodFriendsRazor.Pages
{
    
    public class FriendDetails : PageModel
    {
        readonly IFriendsService _service;

        //Friend
        public IFriend Friend { get; set; }

        public async Task <IActionResult> OnGet(Guid friendId)
        {
            Friend = await _service.ReadFriendAsync(friendId, false);
            
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(Guid deleteId, string deleteType, Guid friendId)
        {
            if (deleteType == "pet" && deleteId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
            if(await _service.ReadPetAsync(deleteId, false) != null)
            {
                await _service.DeletePetAsync(deleteId);
            } 
            }

            if (deleteType == "quote" && deleteId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                if(await _service.ReadQuoteAsync(deleteId, false) != null)
                {
                    await _service.DeleteQuoteAsync(deleteId);
                }
            }

            return await OnGet(friendId);
        }

        public FriendDetails (IFriendsService service)
        {
            _service = service;
        }
    }
}
