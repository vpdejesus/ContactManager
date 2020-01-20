using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using ApplicationCore.Entitites;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Repositories
{
    public class ContactRepositoryTest
    {
        private readonly ContactContext _contactContext;
        private readonly ContactRepository _contactRepository;
        private readonly ITestOutputHelper _output;

        public ContactRepositoryTest(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;

            _contactContext = new ContactContext(dbOptions);
            _contactRepository = new ContactRepository(_contactContext);
        }

        [Fact]
        public async Task SaveToDatabase()
        {
            var contact = GetContact();
            await _contactRepository.AddAsync(contact);
            _contactContext.SaveChanges();

            string name = contact.FirstName + " " + contact.LastName;
            _output.WriteLine($"Name: {name}");

            // Assert
            Assert.Equal(1, _contactContext.Contacts.Count());
        }

        private Contact GetContact()
        {
            var contact = new Contact
            {
                Id = 1,
                FirstName = "Vince",
                LastName = "de Jesus",
                MobileNo = "778-976-4325",
                EmailAddress = "vpdejesus@yahoo.com",
                Address = "4275 Imperial St., Burnaby, BC, Canada"
            };

            return contact;
        }
    }
}
