using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsing
{
    [Serializable]
    class JtokenTicketTypes
    {
        public JToken price_breakdown { get; set; }
        public JToken description { get; set; }
        public JToken v { get; set; }
        public JToken parent_ext_ticket_type { get; set; }
        public JToken quantity_limits { get; set; }
        public JToken password { get; set; }
        public JToken qmask_level { get; set; }
        public JToken isProtected { get; set; }
    }
}
