using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsing
{
    class JsonDataModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string VenueID { get; set; }
        public string EventDate { get; set; }
        public string VenueName { get; set; }
        public string OfferID { get; set; }
        public string OfferName { get; set; }
        public string EventLocation { get; set; }
        public int QuantitiesMINIMUM { get; set; }
        public int QuantitiesLIMIT { get; set; }
        public int Quantitiesmultiple { get; set; }
        public int Quantitiesexact { get; set; }
    }
}
