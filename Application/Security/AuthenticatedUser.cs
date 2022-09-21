using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    [DataContract]
    public sealed class AuthenticatedUser
    {
        [DataMember(Name = "user")]
        public string User { get; private set; }

        [DataMember(Name = "full_name")]
        public string Name { get; private set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; private set; }

        [DataMember(Name = "mother_lastname")]
        public string MotherLastName { get; private set; }

        public AuthenticatedUser(string user, string name, string lastName, string motherLastname)
        {
            User = user;
            Name = name;
            LastName = lastName;
            MotherLastName = motherLastname;
        }
    }
}
