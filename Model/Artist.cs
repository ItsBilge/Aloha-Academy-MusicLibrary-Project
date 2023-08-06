using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Model
{
    public class Artist : BaseModel
    {
        public string Name { get; set; }
        public string? Genre { get; set; } //Müzik türü
        public ICollection<Song> Songs { get; set; }

    }
}
