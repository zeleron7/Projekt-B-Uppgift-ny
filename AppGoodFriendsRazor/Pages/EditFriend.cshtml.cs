using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class EditFriendModel : PageModel
    {
         readonly IFriendsService _service;

        //friend
        public IFriend Friend { get; set; }

        public async Task <IActionResult> OnGet(Guid friendId)
        {
            Friend = await _service.ReadFriendAsync(friendId, false);
            
            return Page();
        }

        public async Task<IActionResult> OnPostEdit(Guid friendId, string FirstName, string LastName, string Email, DateTime Birthday, string Address, int ZipCode, string City, string Country)
        {
            
            if (friendId != Guid.Parse("00000000-0000-0000-0000-000000000000")) 
            {
                var friend = await _service.ReadFriendAsync(friendId, false);

                if (friend != null) 
                {
                    var friendCUdto = new FriendCUdto(friend);

                    var address = await _service.ReadAddressAsync((Guid)friendCUdto.AddressId, false);

                    friendCUdto.FirstName = FirstName;
                    friendCUdto.LastName = LastName;
                    friendCUdto.Email = Email;
                    friendCUdto.Birthday = Birthday;

                    var addressCUdto = new AddressCUdto(address);

                    addressCUdto.StreetAddress = Address;
                    addressCUdto.City = City;
                    addressCUdto.Country = Country;
                    addressCUdto.ZipCode = ZipCode;

                    await _service.UpdateFriendAsync(friendCUdto);
                    await _service.UpdateAddressAsync(addressCUdto);
                }
            }
        

            return await OnGet(friendId);
        }

        public EditFriendModel (IFriendsService service)
        {
            _service = service;
        }
    }
}
