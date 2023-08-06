using Aloha_MusicLibrary.Model;
using Aloha_MusicLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Aloha_MusicLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
             .AddDbContext<MusicStoreContext>()
             .AddScoped<ArtistService>()
             .AddScoped<SongService>()
             .AddScoped<PlaylistService>()
             .AddScoped<PlaylistSongService>()
             .BuildServiceProvider();

            var artistService = serviceProvider.GetRequiredService<ArtistService>();
            var songService = serviceProvider.GetRequiredService<SongService>();
            var playlistService = serviceProvider.GetRequiredService<PlaylistService>();

            bool chose = true;

            while (chose)
            {
                try
                {
                    Console.WriteLine("\nSeçeneğinizi numaraya göre yapınız.");
                    Console.WriteLine("1- Tüm Şarkıları Listele \n2- Şarkı Ekle \n3- Şarkıyı Düzenle \n4- Şarkıyı Sil \n5- Tüm Sanatçıları Listele \n6- Sanatçı Ekle \n7- Sanatçıyı Düzenle \n8- Sanatçıyı Sil \n9- Tüm Çalma Listelerini Listele \n10- Çalma Listesini Görüntüle \n11- Çalma Listesi Ekle \n12- Çalma Listesini Düzenle \n13- Çalma Listesini Sil \n14- Çalma Listesine Şarkı Ekle \n15- Çalma Listesinden Şarkı Kaldır \n16- Çıkış yapmak için 16 yazınız.");
                    var newChose = int.Parse(Console.ReadLine());

                    switch (newChose)
                    {
                        case 1:
                            songService.listAllSongs();
                            break;
                        case 2:
                            AddSong();
                            break;
                        case 3:
                            UpdateSong();
                            break;
                        case 4:
                            DeleteSong();
                            break;
                        case 5:
                            artistService.listAllArtist();
                            break;
                        case 6:
                            AddArtist();
                            break;
                        case 7:
                            UpdateArtist();
                            break;
                        case 8:
                            DeleteArtist();
                            break;
                        case 9:
                            playlistService.ListAllPlaylist();
                            break;
                        case 10:
                            GetPlaylistSong();
                            break;
                        case 11:
                            AddNewPlaylist();
                            break;
                        case 12:
                            UpdatePlaylist();
                            break;
                        case 13:
                            DeletePlaylist();
                            break;
                        case 14:
                            PlaylistSongAddSong();
                            break;
                        case 15:
                            PlaylistSongRemoveSong();
                            break;
                        case 16:
                            Console.WriteLine("Çıkış yapıldı.");
                            chose = false;
                            break;
                        default:
                            Console.WriteLine("Geçersiz işlem");
                            break;
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Bir hata oluştur! {ex.Message}");
                }

            }

        }

        static void AddSong() // şarkı ekleme
        {
            var context = new MusicStoreContext();

            var artistService = new ArtistService(context);
            var songService = new SongService(context);

            try
            {
                Console.Write("Şarkının adı: ");
                var songName = Console.ReadLine();
                Console.Write("Süresi (dk cinsinden belirtiniz): ");
                double songDuration = double.Parse(Console.ReadLine());
                Console.Write("Şarkının çıkış yılı: ");
                int songReleaseYear = int.Parse(Console.ReadLine());

                artistService.listAllArtist(); //Her şarkının mutlaka bir sanatçısı olması gerektiği için burada şarkıcılar listelendi.

                Console.Write("\nYukarıda belirtilen sanaçının ID'sine göre seçim yapınız. (Eğer sanatçı listede yoksa önce sanatçıyı eklemeniz gerekir.)\nSanatçı eklemek için 0'ı tuşlayınız: ");
                var artistIdchose = int.Parse(Console.ReadLine());
                if (artistIdchose == 0)
                {
                    AddArtist();
                }
                else
                {
                    songService.addSong(songName.ToLower(), songDuration, songReleaseYear, artistIdchose); //kullanıcının girdiği veriler şarkı listesine eklendi.
                    Console.WriteLine("Ekleme yapıldı");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"\nGeçerli bir değer girilmedi, hatayı kontrol ediniz. \n{ex.Message}");
            }


        }

        static void AddArtist() // sanatçı ekleme
        {
            try
            {
                var context = new MusicStoreContext();

                var artistService = new ArtistService(context);

                Console.Write("\nSanatçının ad: ");
                var artistName = Console.ReadLine();
                Console.Write("Müzik türü: ");
                var artistGenre = Console.ReadLine();

                if (!string.IsNullOrEmpty(artistName)) // Sanatçı adının tanımsız veya boş değilse sanatçı listesine ekleme yapılacak.
                {
                    artistService.addArtist(artistName.ToLower(), artistGenre.ToLower());
                    Console.WriteLine("Sanatçı eklenmiştir.");
                }
                else
                {
                    Console.WriteLine("Değerler boş geçilemez. Kontrol ediniz.");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }

        }

        static void UpdateSong() // şarkı güncelleme
        {
            var context = new MusicStoreContext();

            var artistService = new ArtistService(context);
            var songService = new SongService(context);

            songService.listAllSongs();
            try
            {
                Console.Write("\nGüncelleme yapmak istediğiniz şarkının ID'sini yazınız: ");
                int songIdToUpdate;

                if (!int.TryParse(Console.ReadLine(), out songIdToUpdate)) // kullanıcının yazacağı ID alanı boş olursa kullanıcıya mesaj verildi.
                {
                    Console.WriteLine("Geçersiz giriş! Lütfen geçerli bir sayı giriniz.");
                }
                else
                {
                    Console.Write("Yeni şarkı adı: ");
                    var newSongName = Console.ReadLine();
                    Console.Write("Süresi (dk cinsinden belirtiniz, boş geçilemez): ");
                    double newSongDuration = double.Parse(Console.ReadLine());
                    Console.Write("Şarkının çıkış yılı (boş geçilemez): ");
                    int newSongReleaseYear = int.Parse(Console.ReadLine());

                    artistService.listAllArtist();
                    Console.Write("\nYeni sanatçı ID'sini giriniz: ");
                    var newArtistId = int.Parse(Console.ReadLine());

                    songService.updateSong(songIdToUpdate, newSongName.ToLower(), newSongDuration, newSongReleaseYear, newArtistId); // kullanıcıdan alınan veriler SongServise class'ında yazılan updateSong metoduna eklendi.
                    Console.WriteLine("Güncelleme yapıldı.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur \n {ex.Message}");
            }

        }

        static void DeleteSong() // şarkı silme
        {
            try
            {
                var serviceProvider = new ServiceCollection()
                 .AddDbContext<MusicStoreContext>()
                 .AddScoped<SongService>()
                 .BuildServiceProvider();

                var songService = serviceProvider.GetRequiredService<SongService>();

                songService.listAllSongs();

                Console.Write("\nSilmek istediğiniz şarkının ID'sini yazınız: ");
                var deleteSong = int.Parse(Console.ReadLine());

                songService.deleteSong(deleteSong);
                Console.WriteLine("Seçilen şarkı silinmiştir.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }

        }

        static void UpdateArtist() // sanatçı güncelleme
        {
            var context = new MusicStoreContext();

            var artistService = new ArtistService(context);

            artistService.listAllArtist(); // tüm artistler listelendi.

            try
            {
                Console.Write("\nGüncelleme yapmak istediğiniz sanatçının ID'sini giriniz: ");
                var choseArtist = int.Parse(Console.ReadLine());

                if (choseArtist != null && choseArtist != 0)
                {
                    Console.Write("Yeni sanatçı adı: ");
                    var newArtistName = Console.ReadLine();
                    Console.Write("Müzik türü: ");
                    var newGenre = Console.ReadLine();

                    artistService.updateArtist(choseArtist, newArtistName.ToLower(), newGenre.ToLower());
                    Console.WriteLine("Güncelleme yapılmıştır");
                }
                else
                {
                    Console.WriteLine("Geçersiz işlem");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }

        }

        static void DeleteArtist() // sanatçı silme
        {
            try
            {
                var context = new MusicStoreContext();

                var artistService = new ArtistService(context);

                artistService.listAllArtist();

                Console.Write("\nSilmek istediğiniz sanatçının ID'sini yazınız: ");
                var deleteArtist = int.Parse(Console.ReadLine()); // listeden seçilen sanaıtçı ID si alındı

                artistService.deleteArtist(deleteArtist); // alınan ID deleteArtist metoduna eklendi
                Console.WriteLine("Seçilen sanatçı silinmiştir.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }

        }

        static void GetPlaylistSong() // müzik listesi detayını görüntüleme
        {
            try
            {
                var context = new MusicStoreContext();

                var playlistService = new PlaylistService(context);
                var playlistSongService = new PlaylistSongService(context);

                playlistService.ListAllPlaylist(); // müzik listesi metodu çağırıldı.

                Console.Write($"\nHangi müzik listesinin detayını görmek istiyosunuz? Seçiminizi ID'ye göre yapınız: ");
                var playListId = int.Parse(Console.ReadLine()); // müzik listesi ID si seçildi.

                Console.WriteLine("Çalma listesi detayları:\n ");
                playlistSongService.getPlayListSong(playListId); // ID si ile seçilen müzik listesine eklenmiş müzikler listelendi.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }
            
        }

        static void AddNewPlaylist() // yeni öüzik listesi oluşturma
        {
            try
            {
                var context = new MusicStoreContext();
                var playlistService = new PlaylistService(context);

                Console.Write("Çalma listesinin adı: ");
                var playlistName = Console.ReadLine();
                Console.Write("Detayı: ");
                var description = Console.ReadLine();

                if (!string.IsNullOrEmpty(playlistName)) // Çalma listesi adı alanı tanımsız veya boş değilse çalma listesi oluşturulacak.
                {
                    playlistService.addNewPlaylist(playlistName.ToLower(), description.ToLower());
                    Console.WriteLine("Yeni çalma listesi oluşturuldu.");
                }
                else
                {
                    Console.WriteLine("Çalma listesi adı boş geçilemez. Kontrol ediniz.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }
            
        }

        static void UpdatePlaylist() // Müzik listesi güncelleme
        {
            try
            {
                var context = new MusicStoreContext();

                var playlistService = new PlaylistService(context);

                playlistService.ListAllPlaylist();

                Console.Write("\nDüzenlemek istediğiniz çalma listesinin ID'sini yazınız: ");
                var updatePlaylistId = int.Parse(Console.ReadLine());

                if (updatePlaylistId != 0 && updatePlaylistId != null)
                {
                    Console.Write("Yeni çalma listesi adı: ");
                    var playlistName = Console.ReadLine();
                    Console.Write("Yeni açıklama: ");
                    var description = Console.ReadLine();

                    playlistService.updatePlaylist(updatePlaylistId, playlistName.ToLower(), description.ToLower());
                    Console.WriteLine("Güncelleme yapılmıştır.");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştur. {ex.Message}");
            }


        }

        static void DeletePlaylist() // Müzik listesi silme
        {
            try
            {
                var context = new MusicStoreContext();

                var playlistService = new PlaylistService(context);

                playlistService.ListAllPlaylist();

                Console.Write("\nSilmek istediğiniz müzik listesinin ID'sini giriniz: ");
                var deleteplaylistId = int.Parse(Console.ReadLine());

                playlistService.deletePlaylist(deleteplaylistId);
                Console.WriteLine("Seçilen çalma listesi silinmiştir.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu. {ex.Message}");
            }

        }

        static void PlaylistSongAddSong() // Çalma listesine şarkı ekleme
        {
            try
            {
                var context = new MusicStoreContext();
                var playlist = new PlaylistService(context);
                var songs = new SongService(context);
                var playlistsongservice = new PlaylistSongService(context);

                playlist.ListAllPlaylist(); // Tüm çalma listesi 

                Console.Write("\nEklemek istediğiniz çalma listesini seçiniz (Seçeceğiniz şarkının da listede ekli olması gerekir!): ");
                var playlistId = int.Parse(Console.ReadLine()); // Şarkı eklemek için seçilen çalma listesi

                songs.listAllSongs(); // Tüm şarkılar

                Console.Write("\nEklemek istediğiniz şarkıyı seçiniz: ");
                var songId = int.Parse(Console.ReadLine()); // Çalma listesine eklenecek şarkı seçimi

                if (playlistId == null || songId == null)
                {
                    Console.WriteLine("Şarkı veya çalma listesi bulunamadı!");
                }

                playlistsongservice.playlistSongAddSong(songId, playlistId); // kullanıcıdan alınan veriler playlistSongAddSong metoduna eklendi.
                Console.WriteLine("Seçilen şarkı çalma listesine eklenmiştir.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu. {ex.Message}");
            }


        }

        static void PlaylistSongRemoveSong() // Çalma listesinden şarkı silme
        {
            try
            {
                var context = new MusicStoreContext();
                var playlist = new PlaylistService(context);
                var playlistSongService = new PlaylistSongService(context);
                playlist.ListAllPlaylist();

                Console.Write("\nŞarkıyı çalma listesinden silmek için önce şarkının hangi çalma listesi olduğunu seçiniz.(ID'ye göre): ");
                int playlistId = int.Parse(Console.ReadLine());

                int playlistID = playlistSongService.getPlayListSong(playlistId);

                if (playlistID != -1)
                {
                    Console.Write("\nSilmek istediğiniz şarkıyı seçiniz: ");
                    int songId = int.Parse(Console.ReadLine());

                    playlistSongService.playlistSongRemoveSong(songId); // kullanıcıdan alınan songId "playlistSongRemoveSong" metoduna eklendi.
                    Console.WriteLine("Seçilen şarkı çalma listesinden silinmiştir.");
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu. {ex.Message}");
            }

        }


    }
}