
using System;
using System.Threading.Tasks;
using AutoMapper;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.Repository;
using PhotoManager.Service.Data.RepositoryInterface;
using PhotoManager.Service.Utilities;

namespace PhotoManager.Service
{
    public class AlbumService : PhotoManagerServiceBase, IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly UtilityMapper _utilityMapper;

        public AlbumService()
        {
            _albumRepository = new AlbumRepository();
            _utilityMapper = new UtilityMapper();
            MapperSetup();
        }

        private static void MapperSetup()
        {
            Mapper.CreateMap<Album, AlbumResponse>()
                .ForSourceMember(c => c.AlbumPhotoes, option => option.Ignore())
                .ForSourceMember(c => c.User, option => option.Ignore());

            Mapper.CreateMap<AlbumResponse, Album>();

            //Mapper.CreateMap<UserResponse, User>()
            //    .ForMember(c => c.Photos, option => option.Ignore())
            //    .ForMember(c => c.Albums, option => option.Ignore())
            //    .ForMember(c => c.Role, option => option.Ignore());
        }

        public async Task<AlbumResponse> GetAlbumById(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);

                var album = await _albumRepository.GetAlbumById(id);

                return album != null ? _utilityMapper.ObjectMapper<AlbumResponse, Album>(album) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AlbumResponse[]> GetAlbumsByUserId(int userId)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                var albums = await _albumRepository.GetAlbumsByUserId(userId);

                return albums != null ? _utilityMapper.ObjectMapper<AlbumResponse[], Album[]>(albums) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AlbumResponse> GetAlbumByUrl(string url)
        {
            try
            {
                if (String.IsNullOrEmpty(url))
                    throw new ArgumentException(_ALBUM_URL_INVALID_EXCEPTION);

                var album = await _albumRepository.GetAlbumByUrl(url);

                return album != null ? _utilityMapper.ObjectMapper<AlbumResponse, Album>(album) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AlbumResponse[]> GetAllAlbums()
        {
            try
            {
                var albums = await _albumRepository.GetAllAlbums();

                return albums != null ? _utilityMapper.ObjectMapper<AlbumResponse[], Album[]>(albums) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertAlbum(AlbumResponse album)
        {
            try
            {
                if(album == null)
                    throw new ArgumentException(_ALBUM_OBJECT_INVALID_EXCEPTION);
                if(String.IsNullOrEmpty(album.Title))
                    throw new ArgumentException(_ALBUM_TITLE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(album.Url))
                    throw new ArgumentException(_ALBUM_URL_INVALID_EXCEPTION);
                if (album.UserId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _albumRepository.InsertAlbum(_utilityMapper.ObjectMapper<Album, AlbumResponse>(album));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateAlbum(AlbumResponse album)
        {
            try
            {
                if (album == null)
                    throw new ArgumentException(_ALBUM_OBJECT_INVALID_EXCEPTION);
                if (album.Id < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(album.Title))
                    throw new ArgumentException(_ALBUM_TITLE_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(album.Url))
                    throw new ArgumentException(_ALBUM_URL_INVALID_EXCEPTION);
                if (album.UserId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _albumRepository.UpdateAlbum(_utilityMapper.ObjectMapper<Album, AlbumResponse>(album));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteAlbumById(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);

                return await _albumRepository.DeleteAlbumById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> AddPhotoToAlbum(int photoId, int albumId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                if (photoId < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                return await _albumRepository.AddPhotoToAlbum(photoId, albumId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ChooseTitlePhoto(int photoId, int albumId)
        {
            try
            {
                if (albumId < 1)
                    throw new ArgumentException(_ALBUMID_INVALID_EXCEPTION);
                if (photoId < 1)
                    throw new ArgumentException(_PHOTOID_INVALID_EXCEPTION);

                return await _albumRepository.ChooseTitlePhoto(photoId, albumId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
