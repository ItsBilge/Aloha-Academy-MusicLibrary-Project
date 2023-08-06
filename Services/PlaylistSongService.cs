using Aloha_MusicLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Services
{
    public class PlaylistSongService
    {
        private readonly MusicStoreContext _context;

        public PlaylistSongService(MusicStoreContext context)
        {
            _context = context;
        }

        public int getPlayListSong(int playListId) // çalma listesi detayını görüntüleme
        {
            try
            {
                var songsInPlaylist = _context.PlaylistSongs
                          .Where(ps => ps.PlaylistId == playListId)
                          .Join(_context.Songs, // çalma listesi tablosuyla şarkı tablosu birleşimi yapıldı
                              ps => ps.SongId,
                              song => song.Id,
                              (ps, song) => new { SongTitle = song.Title, songId = song.Id, SongDuration = song.Duration, ArtistName = song.Artist.Name }).ToList();

                if (songsInPlaylist.Any()) // eğer songsInPlaylist'te değer var mı kontrolü sağlandı
                {
                    Console.WriteLine($"Çalma Listesi #{playListId} İçerisindeki Şarkılar:");
                    foreach (var song in songsInPlaylist)
                    {
                        Console.WriteLine($"Şarkı ID: {song.songId}, Şarkı Adı: {song.SongTitle}, Süresi: {song.SongDuration} Sanatçı: {song.ArtistName}");
                    }
                }
                else // eğer seçilen çalma listesi içinde şarkı yoksa bu blok çalışır.
                {
                    Console.WriteLine("Çalma listesinde şarkı bulunamadı.");
                    return -1;
                }
                return playListId;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
           
        }

        public void playlistSongAddSong(int songId, int playlistId) // Çalma listesine şarkı ekleme
        {
            var song = _context.Songs.FirstOrDefault(s => s.Id == songId); // şarkı ıd si ile dışarıdan alınan songId eşleştirildi
            var playlist = _context.PlaylistSongs.FirstOrDefault(ps=> ps.PlaylistId == playlistId); // çalma listesi (PlaylistId) ıd si ile dışarıdan alınan playlistId eşleştirildi

            var playlistSong = new PlaylistSong() // alınan id bilgileriyle iki tablo arasında eşleştirme yapıldı
            {
                PlaylistId = playlistId,
                SongId = songId
            };

            _context.PlaylistSongs.Add(playlistSong);
            _context.SaveChanges();
        }

        public void playlistSongRemoveSong(int songId) // Çalma listesinden şarkı silme
        {
            var playlistSong = _context.PlaylistSongs
                             .FirstOrDefault(ps => ps.SongId == songId);

            _context.PlaylistSongs.Remove(playlistSong);
            _context.SaveChanges();
        }
    
    }
    
}
