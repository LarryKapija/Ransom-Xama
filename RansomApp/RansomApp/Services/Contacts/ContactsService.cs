using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RansomApp.Services.Contacts
{
    public class ContactsService
    {
        public async Task<List<Models.Contact>> GetContactsAsync()
        {
            var contacts = await Xamarin.Essentials.Contacts.GetAllAsync();
            List<Models.Contact> contactsList = new List<Models.Contact>();
            foreach (var contact in contacts)
            {
                Models.Contact tempContact = new Models.Contact
                {
                    Id = contact.Id,
                    Name = contact.DisplayName
                };
                contact.Phones.ForEach((phoneNumber) => tempContact.Phone = phoneNumber.ToString());

                contactsList.Add(tempContact);

            }

            return await Task.FromResult(contactsList);
        }
    }
}
