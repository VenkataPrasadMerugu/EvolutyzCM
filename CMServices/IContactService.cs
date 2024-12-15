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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IContactService
    {

        [OperationContract]
        bool RegisterUser(User user);

        [OperationContract]
        string Greetings(string name);

        [OperationContract]
        LoginResponse Login(string username, string password);

        [OperationContract]
        void Logout(string sessionId);

        [OperationContract]
        bool AddCategory(string sessionId, string categoryName);
                
        [OperationContract]
        List<Category> GetCategories(string sessionId);

        [OperationContract]
        bool DeleteCategory(string sessionId, int categoryID);

        [OperationContract]
        List<Contact> GetContacts(string sessionId);

        [OperationContract]
        bool AddContact(string sessionId, Contact contact);

        [OperationContract]
        bool UpdateContact(string sessionId, Contact contact);

        [OperationContract]
        bool DeleteContact(string sessionId, int contactId);

        [OperationContract]
        bool UpdateCategory(string sessionId, int categoryId, string categoryName);

    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public string SessionId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

}
