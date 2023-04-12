using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Arthouse_MAUI.Models
{
    public class Artwork
    {
        public int ID { get; set; }

        [JsonIgnore]
        public string Summary
        {
            get
            {
                return Name + " - " + Completed.ToShortDateString();
            }
        }
        [JsonIgnore]
        public string ValueSummary
        {
            get
            {
                return ArtType?.Type + " Valued at: " + Value.ToString("c");
            }
        }

        public string Name { get; set; }

        public DateTime Completed { get; set; } = DateTime.Today;

        public string Description { get; set; }

        public double Value { get; set; }

        public byte[] RowVersion { get; set; }

        public int ArtTypeID { get; set; }

        public ArtType ArtType { get; set; }

    }
}
