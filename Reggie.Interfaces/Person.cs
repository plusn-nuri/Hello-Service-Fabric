using System.Runtime.Serialization;

namespace Reggie.Interfaces
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Email { get; set; }
    }
}
