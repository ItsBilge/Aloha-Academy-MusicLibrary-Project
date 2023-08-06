using Aloha_MusicLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloha_MusicLibrary.Services
{
    public class ArtistService
    {
        private readonly MusicStoreContext _Context;

        public ArtistService(MusicStoreContext contex)
        {
            _Context = contex;
        }
        public void listAllArtist()
        {
            var artists = _Context.Artists.ToList();
            Console.WriteLine("Tüm Sanatçılar ve Müzik Türleri: ");
            foreach (var artist in artists)
            {
                Console.WriteLine($"Sanatçı ID'si: {artist.Id} sanatçının adı: {artist.Name}, müzik türü: {artist.Genre} ");
            }
        }

        public void addArtist(string _name, string _genre) 
        {
            Artist newArtist = new Artist()
            {
                Name = _name,
                Genre = _genre,
            };
            _Context.Artists.Add(newArtist);
            _Context.SaveChanges();
        }

        public void updateArtist(int id, string _name, string _genre)
        {
            try
            {
                Artist artist = _Context.Artists.FirstOrDefault(a => a.Id == id);
                artist.Name = _name;
                artist.Genre = _genre;

                if(!string.IsNullOrEmpty(artist.Name))
                {
                    _Context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu. {ex.Message}");
            }
            
        }

        public void deleteArtist(int ID)
        {
            var deleteArtist = _Context.Artists.Find(ID);

            _Context.Artists.Remove(deleteArtist);
            _Context.SaveChanges();
        }
    }
}
