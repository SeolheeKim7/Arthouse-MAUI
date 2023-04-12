
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arthouse_MAUI.Models
{
    public class ArtType
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public ICollection<Artwork> Artworks { get; set; } = new HashSet<Artwork>();
    }
}
