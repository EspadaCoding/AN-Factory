using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Models
{
    public class WatchModelDetails : ICloneable
    {
        public WatchModelDetails(string iD, string property,decimal gramms, decimal uSD)
        {
            ID = iD;
            Property = property;
            Gramms = gramms;
            USD = uSD;
        }

        public WatchModelDetails()
        {
            ID = string.Empty;
            Property = string.Empty;
            Gramms = 0;
            USD = 0;
        }

        public string ID { get; set; }
        public string Property { get; set; }
        public decimal Gramms { get; set; }
        public decimal USD { get; set; }

        public object Clone()
        {
            return new WatchModelDetails(this.ID,this.Property,this.Gramms,this.USD);
        }
    }

}
