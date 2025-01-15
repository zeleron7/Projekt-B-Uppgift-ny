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
        public Guid FriendId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; } = null;

        //BIND friend with address

        [BindProperty]
        public AddressIM addressIM { get; set; }

        public FriendIM() { }

        public FriendIM(FriendIM friend)
        {
            FriendId = friend.FriendId;
            FirstName = friend.FirstName;
            LastName = friend.LastName;
            Email = friend.Email;
            Birthday = friend.Birthday;
        }

        public FriendIM(IFriend friend)
        {
            FriendId = friend.FriendId;
            FirstName = friend.FirstName;
            LastName = friend.LastName;
            Email = friend.Email;
            Birthday = friend.Birthday;
            addressIM = new AddressIM(friend.Address);
        }

        public IFriend UpdateModel(IFriend model)
        {
            
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.Email = Email;
            model.Birthday = this?.Birthday;
            model.Address.AddressId = addressIM.AddressId;
            
            return model;
        }
    }
    public class AddressIM 
    {
        public Guid AddressId { get; set; }   
        public string StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public AddressIM() { }
        
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

        [BindProperty]
        public FriendIM Friend { get; set; }

        public async Task <IActionResult> OnGet(Guid friendId)
        {
            try
            {
                Friend = new FriendIM(await _service.ReadFriendAsync(friendId, false));  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return Page();

        }

        public async Task<IActionResult> OnPostEdit() 
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return await OnGet(Friend.FriendId);
                }
                else
                {
                    if (Friend.FriendId != Guid.Parse("00000000-0000-0000-0000-000000000000")) 
                    {
                    
                        var friend = await _service.ReadFriendAsync(Friend.FriendId, false);

                        if (friend != null) 
                        {
                            var friendCUdto = new FriendCUdto(friend);

                            var address = await _service.ReadAddressAsync((Guid)friendCUdto.AddressId, false);

                            friendCUdto.FirstName = Friend.FirstName;
                            friendCUdto.LastName = Friend.LastName;
                            friendCUdto.Birthday = Friend.Birthday;

                            var addressCUdto = new AddressCUdto(address);

                            addressCUdto.StreetAddress = Friend.addressIM.StreetAddress;
                            addressCUdto.City = Friend.addressIM.City;
                            addressCUdto.Country = Friend.addressIM.Country;
                            addressCUdto.ZipCode = Friend.addressIM.ZipCode;

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
        
            return await OnGet(Friend.FriendId);

        }

        public EditFriendModel (IFriendsService service)
        {
            _service = service;
        }
    }
}


