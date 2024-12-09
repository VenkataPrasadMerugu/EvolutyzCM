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

    //[DataContract]
    //public class User
    //{
    //    [DataMember]
    //    public int UserID { get; set; }
    //    [DataMember]
    //    public string UserName { get; set; }
    //    [DataMember]
    //    public string Name { get; set; }
    //    [DataMember]
    //    public string Password { get; set; }
    //    [DataMember]
    //    public string Email { get; set; }
    //    [DataMember]
    //    public bool Active { get; set; }
    //}
}
