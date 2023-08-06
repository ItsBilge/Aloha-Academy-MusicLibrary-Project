using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Model
{
    public class Song : BaseModel
    {
        public string Title { get; set; }
        public double Duration { get; set; }
        public int? ReleaseYear { get; set; } // Çıkış tarihi
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } // PlaylistSong ile çoka çok ilişki

    }

}
