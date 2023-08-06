using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Model
{
    public class Playlist : BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } // çoka çok ilişki
    }
}
