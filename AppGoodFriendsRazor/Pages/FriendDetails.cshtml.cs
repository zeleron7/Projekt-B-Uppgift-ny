using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    
    public class FriendDetails : PageModel
    {
        readonly IFriendsService _service;

        //friends
        public IFriend Friend { get; set; }

        //public Guid FriendId { get; set; }

        public async Task <IActionResult> OnGet(Guid friendId, Guid petId, Guid quoteId)
        {
            
            Friend = await _service.ReadFriendAsync(friendId, false);
            var pets = await _service.ReadPetAsync(petId, false);
            var quotes = await _service.ReadQuoteAsync(quoteId, false);
            
            Friend.Pets.Where(p => p.PetId == friendId);
            Friend.Quotes.Where(q => q.QuoteId == friendId);

            Friend.Pets.Add(pets);
            Friend.Quotes.Add(quotes);
            
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(Guid friendId, Guid petId, Guid quoteId)
        {
            if (petId != Guid.Empty)
        {
            await _service.DeletePetAsync(petId);
        }
        else
        {
            Console.WriteLine("test");
        }

        // Reload the friend details after deletion
        Friend = await _service.ReadFriendAsync(friendId, false);
        
        return Page();
            
        }

        public FriendDetails (IFriendsService service)
        {
            _service = service;
        }
    }
}
