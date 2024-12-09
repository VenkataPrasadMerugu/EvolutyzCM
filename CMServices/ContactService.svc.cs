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
            // Attempt to authenticate the user using the repository
            var userId = _contactRepository.login(username, password);

            if (userId > 0) // If a valid user ID is returned
            {
                // Generate a session ID
                string sessionId = Guid.NewGuid().ToString();

                // Store the session ID (you might want to save this in a database or cache)
                Sessions[sessionId] = username; // Ensure `Sessions` is a defined dictionary

                return new LoginResponse
                {
                    UserId = userId,            // The authenticated user's ID
                    SessionId = sessionId,      // The generated session ID
                    Success = true,             // Indicate success
                    Message = "Login successful." // Success message
                };
            }

            // Return failure response if authentication fails
            return new LoginResponse
            {
                UserId = 0,                 // No valid user ID
                SessionId = null,           // No session ID
                Success = false,            // Indicate failure
                Message = "Invalid username or password." // Failure message
            };
        }


        public string Greetings(string name)
        {
            return $"Hi {name}....";
        }
    }
}
