using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestIldasm
{
    [DataContract]
    public class Class2
    {
        [DataMember]
        public string MyName { get; set; }
    }
}
