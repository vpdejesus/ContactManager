using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entitites;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactContext _context;

        public ContactRepository(ContactContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetAsync(int id)
        {
            return await _context.Contacts.FindAsync(id); 
        }

        public async Task AddAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            try
            {
                _context.Entry(contact).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var result = _context.Contacts.FirstOrDefault(c => c.Id == id);
                _context.Contacts.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
