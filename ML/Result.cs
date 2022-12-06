using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        bool correct;
        string message;
        Exception exeption;
        object _object;
        List<object> objects;

        public bool Correct { get => correct; set => correct = value; }
        public string Message { get => message; set => message = value; }
        public Exception Exeption { get => exeption; set => exeption = value; }
        public object Object { get => _object; set => _object = value; }
        public List<object> Objects { get => objects; set => objects = value; }
    }
}
