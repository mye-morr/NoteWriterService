using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NoteWriter
{
    [DataContract]
    public class wsTestDbItem
    {
        [DataMember]
        public int numRow { get; set; }

        [DataMember]
        public string usr { get; set; }

        [DataMember]
        public string cat { get; set; }

        [DataMember]
        public string subcat { get; set; }

        [DataMember]
        public string item { get; set; }

        [DataMember]
        public string dialog { get; set; }
    }
}