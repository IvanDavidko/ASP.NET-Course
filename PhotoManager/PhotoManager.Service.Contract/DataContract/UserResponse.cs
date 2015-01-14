using System.Runtime.Serialization;

namespace PhotoManager.Service.Contract.DataContract
{
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public virtual RoleResponse Role { get; set; }

    }
}
