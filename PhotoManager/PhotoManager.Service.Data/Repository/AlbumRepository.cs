using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.RepositoryInterface;

namespace PhotoManager.Service.Data.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public async Task<Album> GetAlbumById(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Albums.FindAsync(id);
            }
        }

        public async Task<Album[]> GetAlbumsByUserId(int userId)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Albums.Where(a => a.UserId == userId).ToArrayAsync();
            }
        }

        public async Task<Album> GetAlbumByUrl(string url)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Albums.FirstOrDefaultAsync(a => a.Url == url);
            }
        }

        public async Task<Album[]> GetAllAlbums()
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Albums.ToArrayAsync();
            }
        }

        public async Task<int> InsertAlbum(Album album)
        {
            using (var context = new PhotoManagerEntities())
            {
                context.Albums.Add(album);

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateAlbum(Album info)
        {
            using (var context = new PhotoManagerEntities())
            {
                var album = await context.Albums.FindAsync(info.Id);

                if (album == null) return 0;

                album.DateModified = info.DateModified;
                album.Title = info.Title;
                album.Description = info.Description;
                album.Url = info.Url;
                
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAlbumById(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                var albumDependencies = await context.AlbumPhotoes.Where(ap => ap.AlbumId == id).ToArrayAsync();
                var album = await context.Albums.FindAsync(id);

                if (album == null) return 0;

                context.AlbumPhotoes.RemoveRange(albumDependencies);
                context.Albums.Remove(album);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddPhotoToAlbum(int photoId, int albumId)
        {
            using (var context = new PhotoManagerEntities())
            {
                context.AlbumPhotoes.Add(new AlbumPhoto()
                {
                    PhotoId = photoId,
                    AlbumId = albumId
                });

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> ChooseTitlePhoto(int photoId, int albumId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var albumphoto = await context.AlbumPhotoes.FirstOrDefaultAsync(ap => ap.AlbumId == albumId && ap.PhotoId == photoId);

                if (albumphoto == null) return 0;

                albumphoto.IsTitlePhoto = true;
                return await context.SaveChangesAsync();
            }
        }
    }
}
