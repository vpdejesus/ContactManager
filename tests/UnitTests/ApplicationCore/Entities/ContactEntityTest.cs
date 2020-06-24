using Xunit;
using ApplicationCore.Entitites;

namespace UnitTests.ApplicationCore.Entities
{
    public class ContactEntityTest
    {
        private int _id = 1;
        private string _firstName = "Vince";
        private string _lastName = "de Jesus";
        private string _mobileNo = "778-955-7748";
        private string _emailAddress = "vpdejesus@yahoo.com";
        private string _address = "4275 Imperial St., Burnaby, BC, Canada";

        [Fact]
        public void AddContactIfNotPresent()
        {
            var contact = new Contact
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                MobileNo = _mobileNo,
                EmailAddress = _emailAddress,
                Address = _address
            };

            Assert.Equal(_firstName, contact.FirstName);
            Assert.Equal(_lastName, contact.LastName);
            Assert.Equal(_mobileNo, contact.MobileNo);
            Assert.Equal(_emailAddress, contact.EmailAddress);
            Assert.Equal(_address, contact.Address);
        }
    }
}
