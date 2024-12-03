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

        public ContactService()
        {
            _contactRepository = new ContactRepository();
        }

        public bool RegisterUser(User user)
        {
            return _contactRepository.RegisterUser(user);
        }
        
        public string Greetings(string name)
        {
            return $"Hi {name}....";
        }
    }
}
