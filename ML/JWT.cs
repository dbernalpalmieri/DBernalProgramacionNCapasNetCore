using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class JWT
    {

        string key;
        string issuer;
        string audience;
        string subject;

        public string Key { get => key; set => key = value; }
        public string Issuer { get => issuer; set => issuer = value; }
        public string Audience { get => audience; set => audience = value; }
        public string Subject { get => subject; set => subject = value; }


        
    }
}
