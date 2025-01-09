using System.ComponentModel.DataAnnotations;
using DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.DTO;
using Services;

namespace MyApp.Namespace
{
    public class FriendIM 
    {
        public Guid? FriendId { get; set; } = new Guid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; } = null;

        //BIND friend with address
        public AddressIM addressIM { get; set; }

        public FriendIM(FriendIM friend)
        {
            FriendId = friend.FriendId;
            FirstName = friend.FirstName;
            LastName = friend.LastName;
            Birthday = friend.Birthday;
            
            
        }

        public FriendIM(IFriend friend)
        {
            FriendId = friend.FriendId;
            FirstName = friend.FirstName;
            LastName = friend.LastName;
            Birthday = friend.Birthday;
            
        }

    }
    public class AddressIM 
    {
        public Guid? AddressId { get; set; } = new Guid();  
        public string StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        

        public AddressIM(AddressIM address)
        {
            AddressId = address.AddressId;
            StreetAddress = address.StreetAddress;
            ZipCode = address.ZipCode;
            City = address.City;
            Country = address.Country;
            
        }

        public AddressIM(IAddress address)
        {
            AddressId = address.AddressId;
            StreetAddress = address.StreetAddress;
            ZipCode = address.ZipCode;
            City = address.City;
            Country = address.Country;
            
        }
    }

    public class EditFriendModel : PageModel
    {
        readonly IFriendsService _service;

        //[BindProperty]
        //public AddressCUdto Address { get; set; }

        //[BindProperty]
        //public FriendCUdto Friend { get; set; }

        //public IFriend Friend { get; set; }

        [BindProperty]
        public FriendIM Friend { get; set; }

        public async Task <IActionResult> OnGet(Guid friendId)
        {
            Friend = new FriendIM(await _service.ReadFriendAsync(friendId, false));  
            

            return Page();

            

        }

        public async Task<IActionResult> OnPostEdit(Guid friendId, string FirstName, string LastName, DateTime Birthday, string Address, int ZipCode, string City, string Country)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return await OnGet(friendId);
                }
                else
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
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
            return await OnGet(friendId);
        }

        public EditFriendModel (IFriendsService service)
        {
            _service = service;
        }
    }
}


