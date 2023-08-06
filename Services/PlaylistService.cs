using Aloha_MusicLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Services
{
    public class PlaylistService
    {
        private readonly MusicStoreContext _context;

        public PlaylistService(MusicStoreContext context)
        {
            _context = context;
        }

        public void ListAllPlaylist() // tüm çalma listesini görme
        {
            var playLists = _context.Playlists.ToList();
            foreach (var playlist in playLists)
            {
                Console.WriteLine($"Müzik Listesi ID: {playlist.Id} Adı: {playlist.Name} Açıklama: {playlist.Description}");
            }

        }

        public void addNewPlaylist(string name, string description) // yeni çalma listesi oluşturma
        {
            Playlist playlist = new Playlist() // dışarıdan alınan name ve description bilgileriyle yeni çalma listesi oluşturuldu
            {
                Name = name,
                Description = description
            };
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
        }

        public void updatePlaylist(int playlistId, string name, string description)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(pl => pl.Id == playlistId);
            playlist.Name = name;
            playlist.Description = description;

            _context.SaveChanges();

        }

        public void deletePlaylist(int playlistId)
        {
            var deleteplaylist = _context.Playlists.Find(playlistId);

            _context.Playlists.Remove(deleteplaylist);
            _context.SaveChanges();
        }
    }
}
