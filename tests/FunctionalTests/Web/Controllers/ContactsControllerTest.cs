using Moq;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using ApplicationCore.Entitites;
using ApplicationCore.Interfaces;

namespace FunctionalTests.Web.Controllers
{
    public class ContactsControllerTest
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            var mockRepo = new Mock<IContactRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetContacts());
            var controller = new ContactsController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Contact> GetContacts()
        {
            var contacts = new List<Contact>
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "Vince",
                    LastName = "de Jesus",
                    MobileNo = "778-976-4325",
                    EmailAddress = "vpdejesus@yahoo.com",
                    Address = "4275 Imperial St., Burnaby, BC, Canada"
                },

                new Contact()
                {
                    Id = 2,
                    FirstName = "Cynthia",
                    LastName = "de Jesus",
                    MobileNo = "777-979-2325",
                    EmailAddress = "cadejesus@yahoo.com",
                    Address = "4275 Imperial St., Burnaby, BC, Canada"
                }
            };

            return contacts;
        }
    }
}
