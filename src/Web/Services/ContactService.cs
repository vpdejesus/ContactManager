using System;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ApplicationCore.Entitites;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Services
{
    public class ContactService
    {
        private readonly HttpClient _client;
   
        public ContactService()
        {
            _client = new HttpClient();
            var auth = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            _client.DefaultRequestHeaders.Authorization = auth;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            var contacts = new List<Contact>();
            var uri = new Uri(string.Format(Constants.ContactsUrl, string.Empty));

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    contacts = JsonConvert.DeserializeObject<List<Contact>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return contacts;
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contact = new Contact();
            var uri = new Uri(string.Format(Constants.ContactsUrl, id));

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    contact = JsonConvert.DeserializeObject<Contact>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return contact;
        }

        public async Task<HttpResponseMessage> InsertContactAsync(Contact contact)
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.ContactsUrl, string.Empty));

            try
            {
                response = await _client.PostAsync(uri, GetStringContentFromObject(contact));

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tContact successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return response;
        }

        public async Task<HttpResponseMessage> UpdateContactAsync(int id, Contact contact)
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.ContactsUrl, id));

            try
            {
                response = await _client.PutAsync(uri, GetStringContentFromObject(contact));
                
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tContact successfully updated.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteContactAsync(int id)
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.ContactsUrl, id));

            try
            {
                response = await _client.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return response;
        }

        private StringContent GetStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            return content;
        }        
    }
}
