using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.Repository;
using PhotoManager.Service.Data.RepositoryInterface;
using PhotoManager.Service.Filters;
using PhotoManager.Service.Utilities;

namespace PhotoManager.Service
{
    public class PhotoService : PhotoManagerServiceBase, IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly UtilityMapper _utilityMapper;

        public PhotoService()
        {
            _photoRepository = new PhotoRepository();
            _utilityMapper = new UtilityMapper();
            MapperSetup();
        }

        private static void MapperSetup()
        {
            Mapper.CreateMap<Photo, PhotoResponse>()
                .ForSourceMember(c => c.AlbumPhotoes, option => option.Ignore())
                .ForSourceMember(c => c.User, option => option.Ignore());

            Mapper.CreateMap<PhotoResponse, Photo>();
        }

        public async Task<PhotoResponse> GetPhotoById(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                var photo = await _photoRepository.GetPhotoById(id);

                return photo != null ? _utilityMapper.ObjectMapper<PhotoResponse, Photo>(photo) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PhotoResponse> GetTitlePhoto(int albumId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);

                var photo = await _photoRepository.GetTitlePhoto(albumId);

                return photo != null ? _utilityMapper.ObjectMapper<PhotoResponse, Photo>(photo) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> MakePhotoTitle(int albumId, int photoId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                if (photoId < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                var a= await _photoRepository.MakePhotoTitle(albumId, photoId);
                return a;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int[]> GetPhotosByAlbumId(int albumId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                
                return await _photoRepository.GetPhotosByAlbumId(albumId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int[]> GetPhotosByAlbumUrl(string albumUrl)
        {
            try
            {
                if (String.IsNullOrEmpty(albumUrl))
                    throw new ArgumentException(_ALBUM_URL_INVALID_EXCEPTION);

                return await _photoRepository.GetPhotosByAlbumUrl(albumUrl);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int[]> GetPhotosByUserId(int userId)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _photoRepository.GetPhotosByUserId(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertPhoto(PhotoResponse photo)
        {
            try
            {
                if(photo == null)
                    throw new ArgumentException(_PHOTO_OBJECT_INVALID_EXCEPTION);
                if (photo.Image == null || photo.Image.Length == 0)
                    throw new ArgumentException(_PHOTO_IMAGE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(photo.ImageSize))
                    throw new ArgumentException(_PHOTO_SIZE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(photo.ImageType))
                    throw new ArgumentException(_PHOTO_TYPE_INVALID_EXCEPTION);
                if (photo.UserId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _photoRepository.InsertPhoto(_utilityMapper.ObjectMapper<Photo, PhotoResponse>(photo));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdatePhoto(PhotoResponse photo)
        {
            try
            {
                if (photo == null)
                    throw new ArgumentException(_PHOTO_OBJECT_INVALID_EXCEPTION);
                if (photo.Id < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);
                if (photo.Image == null || photo.Image.Length == 0)
                    throw new ArgumentException(_PHOTO_IMAGE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(photo.ImageSize))
                    throw new ArgumentException(_PHOTO_SIZE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(photo.ImageType))
                    throw new ArgumentException(_PHOTO_TYPE_INVALID_EXCEPTION);
                if (photo.UserId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _photoRepository.UpdatePhoto(_utilityMapper.ObjectMapper<Photo, PhotoResponse>(photo));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeletePhotoById(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                return await _photoRepository.DeletePhotoById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeletePhotosByAlbumId(int albumId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);

                return await _photoRepository.DeletePhotosByAlbumId(albumId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeletePhotoFromAlbum(int albumId, int photoId)
        {
            try
            {
                if(albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                if(photoId < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                return await _photoRepository.DeletePhotoFromAlbum(albumId, photoId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<int>> SearchPhoto(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException(_PHOTO_NAME_INVALID_EXCEPTION);

                return await _photoRepository.SearchPhoto(name);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<int>> ExtendedSearchPhoto(PhotoResponse source)
        {
            try
            {
                return await _photoRepository.ExtendedSearchPhoto(SearchFilter.GetFilters(source));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
