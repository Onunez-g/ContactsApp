using ContactsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace ContactsApp.Data
{
    public class ContactDatabaseController
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });
        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;
        public ContactDatabaseController()
        {
            InitiaLizeAsync().SafeFireAndForget(false);
        }
        async Task InitiaLizeAsync()
        {
            if (!initialized)
            {
                if(!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Contact).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Contact)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await Database.Table<Contact>().ToListAsync();
        }
        public async Task<int> SaveContact(Contact contact)
        {
            if (contact.Id != 0)
            {
                await Database.UpdateAsync(contact);
                return contact.Id;
            }
            else
            {
                return await Database.InsertAsync(contact);
            }
        }
        public async Task<int> DeleteContact(int id)
        {
            return await Database.DeleteAsync<Contact>(id);
        }
    }
}
