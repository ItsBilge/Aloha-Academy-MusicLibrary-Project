using Aloha_MusicLibrary.Migrations;
using Aloha_MusicLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Song = Aloha_MusicLibrary.Model.Song;

namespace Aloha_MusicLibrary.Services
{
    public class SongService
    {

        private readonly MusicStoreContext _Context;

        public SongService(MusicStoreContext contex)
        {
            _Context = contex;
        }

        public void listAllSongs()
        {
            var songs = _Context.Songs.Join(_Context.Artists,
                    song => song.ArtistId,
                    artist => artist.Id,
                    (song, artist) => new { SongId = song.Id, songDuration = song.Duration, songReleaseYear = song.ReleaseYear, SongTitle = song.Title, ArtistName = artist.Name })
                    .ToList();

            if (songs.Any())
            {
                Console.WriteLine("Tüm Şarkılar:");
                foreach (var song in songs)
                {
                    Console.WriteLine($"Şarkı ID: {song.SongId}, Şarkı Adı: {song.SongTitle}, Şarkı Süresi(dk): {song.songDuration}, Şarkının Çıkış Yılı {song.songReleaseYear}, Sanatçı: {song.ArtistName}");
                }
            }
            else
            {
                Console.WriteLine("Şarkı bulunamadı!");
            }
        }
        public void addSong(string _title, double _duration, int _releaseYear, int _artistId)
        {
            Song song = new Song()
            {
                Title = _title,
                Duration = _duration,
                ReleaseYear = _releaseYear,
                ArtistId = _artistId
            };

            _Context.Songs.Add(song);
            _Context.SaveChanges();
        }

        public void updateSong(int ID, string _title, double _duration, int _releaseYear, int _artistId)
        {
            try
            {
                Song choseSong = _Context.Songs.FirstOrDefault(s => s.Id == ID);
                if (choseSong != null)
                {
                    choseSong.Title = _title;
                    choseSong.Duration = _duration;
                    choseSong.ReleaseYear = _releaseYear;
                    choseSong.ArtistId = _artistId;

                    _Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
           
        }

        public void deleteSong(int ID)
        {
            var deleteSong = _Context.Songs.Find(ID);
            
            _Context.Remove(deleteSong);
            _Context.SaveChanges();
        }

       
    }
}
