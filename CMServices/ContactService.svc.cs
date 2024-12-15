using CM.DAC;
using CM.DAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CMServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ContactService : IContactService
    {
        private readonly ContactRepository _contactRepository;
        private static Dictionary<string, string> Sessions = new Dictionary<string, string>();

        public ContactService()
        {
            _contactRepository = new ContactRepository();
        }

        public bool RegisterUser(User user)
        {
            return _contactRepository.RegisterUser(user);
        }

        public LoginResponse Login(string username, string password)
        {
            
            var userId = _contactRepository.login(username, password);

            if (userId > 0) 
            {
               
                string sessionId = Guid.NewGuid().ToString();

                
                Sessions[sessionId] = username; 

                return new LoginResponse
                {
                    UserId = userId,            
                    SessionId = sessionId,      
                    Success = true,             
                    Message = "Login successful." 
                };
            }

            
            return new LoginResponse
            {
                UserId = 0,                
                SessionId = null,         
                Success = false,           
                Message = "Invalid username or password." 
            };
        }

        public void Logout(string sessionId)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                // Remove the session from the dictionary if it exists
                if (Sessions.ContainsKey(sessionId))
                {
                    Sessions.Remove(sessionId);
                }
            }
        }

        public bool AddCategory(string sessionId, string categoryName)
        {
            ValidateSession(sessionId);

            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);

            return _contactRepository.AddCategory(categoryName, userId);
        }

        public List<Category> GetCategories(string sessionId)
        {
            ValidateSession(sessionId);

            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);

            return _contactRepository.GetCategoriesByUser(userId);
        }

        public bool DeleteCategory(string sessionId, int categoryID)
        {
            ValidateSession(sessionId);

            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);

            return _contactRepository.DeleteCategory(categoryID, userId);
        }

        public bool UpdateCategory(string sessionId, int categoryId, string categoryName)
        {
            ValidateSession(sessionId);

            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);

            return _contactRepository.UpdateCategory(categoryId, categoryName, userId);
        }

        public string Greetings(string name)
        {
            return $"Hi {name}....";
        }

        public List<Contact> GetContacts(string sessionId)
        {
            ValidateSession(sessionId);
            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);
            return _contactRepository.GetContacts(userId);
        }

        public bool AddContact(string sessionId, Contact contact)
        {
            ValidateSession(sessionId);
            string username = Sessions[sessionId];
            int userId = _contactRepository.GetUserIdByUsername(username);
            contact.UserID = userId;
            return _contactRepository.AddContact(contact);
        }

        public bool UpdateContact(string sessionId, Contact contact)
        {
            ValidateSession(sessionId);
            return _contactRepository.UpdateContact(contact);
        }

        public bool DeleteContact(string sessionId, int contactId)
        {
            ValidateSession(sessionId);
            return _contactRepository.DeleteContact(contactId);
        }

        private void ValidateSession(string sessionId)
        {
            if (!Sessions.ContainsKey(sessionId))
            {
                throw new FaultException("Invalid session.");
            }
        }
    }
}
