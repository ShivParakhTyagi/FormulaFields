using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.MobilizeDataTypes
{
    public class MobiDocDBEntry
    {
        public string id;
        public string _self;
        public string Organization { get; set; }
        public string FormName { get; set; }
        public string Type { get; set; }
        //TODO Note: if List [ property name ] is renamed it also need to renamed in the querry string in DocDbHandlers while read. otherwise NO document will be fetched.
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
        public string Version { get; set; }
        public bool Expired { get; set; }
    }
}
