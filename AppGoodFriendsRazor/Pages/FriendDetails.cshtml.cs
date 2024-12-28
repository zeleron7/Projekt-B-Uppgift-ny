using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class FriendInfo 
    {
        public Guid FriendId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
         public string Address { get; set; }
        public string Birthday { get; set; }
        public string Pets { get; set; }
        public string Quotes { get; set; } 
    }

    public class FriendDetails : PageModel
    {
        readonly IFriendsService _service;

        //friends
        public List<IFriend> Friends {get; set;}

        public string FriendId { get; set; }

        public async Task <IActionResult> OnGet(string friendId)
        {
            FriendId = friendId;

             
            var result = await _service.ReadFriendAsync(Guid.Parse(FriendId), false);
            Friends = new List<IFriend> { result };

            

            
            

            return Page();
        }

        public FriendDetails (IFriendsService service)
        {
            _service = service;
        }
    }
}
