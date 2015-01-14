using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.RepositoryInterface;
using System.Threading.Tasks;

namespace PhotoManager.Service.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        public async Task<Photo> GetPhotoById(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Photos.FindAsync(id);
            }
        }

        public async Task<Photo> GetTitlePhoto(int albumId)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Photos.FirstOrDefaultAsync(
                    p => p.AlbumPhotoes.Any(ap => ap.AlbumId == albumId && ap.IsTitlePhoto == true));
            }
        }

        public async Task<int> MakePhotoTitle(int albumId, int photoId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var titlePhoto = await context.AlbumPhotoes.Where(ap => (ap.AlbumId == albumId && ap.IsTitlePhoto == true))
                    .FirstOrDefaultAsync();

                if (titlePhoto != null)
                {
                    titlePhoto.IsTitlePhoto = false;
                    await context.SaveChangesAsync();
                }

                var photo = await context.AlbumPhotoes.Where(ap => (ap.AlbumId == albumId && ap.PhotoId == photoId))
                    .FirstOrDefaultAsync();

                if (photo == null) return 0;

                photo.IsTitlePhoto = true;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int[]> GetPhotosByAlbumId(int albumId)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.AlbumPhotoes.Where(ap => ap.AlbumId == albumId)
                    .Select(p => p.PhotoId)
                    .ToArrayAsync();
            }
        }

        public async Task<int[]> GetPhotosByAlbumUrl(string albumUrl)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.AlbumPhotoes.Where(ap => ap.Album.Url == albumUrl)
                    .Select(p => p.PhotoId)
                    .ToArrayAsync();
            }
        }

        public async Task<int[]> GetPhotosByUserId(int userId)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Photos.Where(photo => photo.UserId == userId)
                    .Select(p => p.Id)
                    .ToArrayAsync();
            }
        }

        public async Task<int> InsertPhoto(Photo photo)
        {
            using (var context = new PhotoManagerEntities())
            {
                context.Photos.Add(photo);

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdatePhoto(Photo info)
        {
            using (var context = new PhotoManagerEntities())
            {
                var photo = await context.Photos.FindAsync(info.Id);

                if (photo == null) return 0;

                photo.Image = info.Image;
                photo.ImageType = info.ImageType;
                photo.ImageSize = info.ImageSize;
                photo.Name = info.Name;
                photo.Description = info.Description;
                photo.PlaceCreated = info.PlaceCreated;
                photo.CameraModel = info.CameraModel;
                photo.Diaphragm = info.Diaphragm;
                photo.ShutterSpeed = info.ShutterSpeed;
                photo.FlashInUse = info.FlashInUse;
                photo.FocalLengthOfTheLens = info.FocalLengthOfTheLens;
                photo.ISO = info.ISO;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePhotoById(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                var photoDependencies = await context.AlbumPhotoes.Where(ap => ap.PhotoId == id).ToArrayAsync();
                var photo = await context.Photos.FindAsync(id);

                if (photo == null) return 0;

                context.AlbumPhotoes.RemoveRange(photoDependencies);
                context.Photos.Remove(photo);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePhotosByAlbumId(int albumId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var photos = await context.Photos.Where(
                    p => p.AlbumPhotoes.Any(ap => ap.AlbumId == albumId)).ToArrayAsync();
                context.Photos.RemoveRange(photos);

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePhotoFromAlbum(int albumId, int photoId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var albumPhoto = await context.AlbumPhotoes.FirstOrDefaultAsync(ap =>
                    (ap.AlbumId == albumId && ap.PhotoId == photoId));

                if (albumPhoto == null) return 0;

                context.AlbumPhotoes.Remove(albumPhoto);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<List<int>> SearchPhoto(string name)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await Task<List<int>>.Factory.StartNew(() =>
                {
                    var nullableIds = context.SearchPhoto(name).ToList();

                    return nullableIds.Any() ? nullableIds.Cast<int>().ToList() : new List<int>();
                });
            }
        }

        public async Task<List<int>> ExtendedSearchPhoto(IEnumerable<Expression<Func<Photo, bool>>> filters)
        {
            if (!filters.Any()) return new List<int>();

            using (var context = new PhotoManagerEntities())
            {
                var fullList = context.Photos as IQueryable<Photo>;
                foreach (var filter in filters)
                {
                    if(fullList != null)
                        fullList = fullList.Where(filter);
                }

                return fullList != null ? await fullList.Select(p => p.Id).ToListAsync() : new List<int>();
            }
        }
    }
}
