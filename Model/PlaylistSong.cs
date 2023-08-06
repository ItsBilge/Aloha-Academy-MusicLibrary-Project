using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Model
{
    public class PlaylistSong // Çalma listesi ile şarkı arasındaki çoka çok ilişki
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
