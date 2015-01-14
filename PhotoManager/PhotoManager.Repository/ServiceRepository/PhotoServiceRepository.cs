
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Mapping;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Repository.ServiceRepository
{
    public class PhotoServiceRepository : IPhotoServiceRepository
    {
        private readonly UtilityMapper _utilityMapper;
        private readonly IValidator<Photo> _validator;
        private readonly IValidator<ExtendedSearch> _searchValidator;

        public PhotoServiceRepository(UtilityMapper utilityMapper,
            IValidator<Photo> validator,
            IValidator<ExtendedSearch> searchValidator)
        {
            _utilityMapper = utilityMapper;
            _validator = validator;
            _searchValidator = searchValidator;
        }

        #region Get
        public async Task<Photo> GetPhotoById(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetPhotoById(id);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Photo, PhotoResponse>(response) : new Photo();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Photo();
        }

        public async Task<Photo> GetTitlePhoto(int albumId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetTitlePhoto(albumId);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Photo, PhotoResponse>(response) : new Photo();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Photo();
        }

        public async Task<int> MakePhotoTitle(int albumId, int photoId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.MakePhotoTitle(albumId, photoId);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public async Task<int[]> GetPhotosByAlbumId(int albumId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetPhotosByAlbumId(albumId);
                ((ICommunicationObject)client).Close();

                return response ?? new int[0];
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new int[0];
        }

        public async Task<int[]> GetPhotosByAlbumUrl(string albumUrl)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetPhotosByAlbumUrl(albumUrl);
                ((ICommunicationObject)client).Close();

                return response ?? new int[0];
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new int[0];
        }

        public async Task<int[]> GetPhotosByUserId(int userId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetPhotosByUserId(userId);
                ((ICommunicationObject)client).Close();

                return response ?? new int[0];
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new int[0];
        }
        #endregion

        #region Add, Update, Delete
        public async Task<OperationResult<bool>> InsertPhoto(Photo photo)
        {
            var validationResult = _validator.Validate(photo);
            if (!validationResult.IsValid)
            {
                return new OperationResult<bool>
                {
                    IsValidationError = true,
                    ValidationResult = validationResult
                };
            }

            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.InsertPhoto(_utilityMapper.ObjectMapper<PhotoResponse, Photo>(photo));
                ((ICommunicationObject)client).Close();

                return new OperationResult<bool>
                {
                    Result = Convert.ToBoolean(response),
                    IsValidationError = false,
                };
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new OperationResult<bool>
            {
                IsValidationError = true
            };
        }

        public async Task<OperationResult<bool>> UpdatePhoto(Photo photo)
        {
            var validationResult = _validator.Validate(photo);
            if (!validationResult.IsValid)
            {
                return new OperationResult<bool>
                {
                    IsValidationError = true,
                    ValidationResult = validationResult
                };
            }

            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.UpdatePhoto(_utilityMapper.ObjectMapper<PhotoResponse, Photo>(photo));
                ((ICommunicationObject)client).Close();

                return new OperationResult<bool>
                {
                    Result = Convert.ToBoolean(response),
                    IsValidationError = false,
                };
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new OperationResult<bool>
            {
                IsValidationError = true
            };
        }

        public async Task<int> DeletePhotoById(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.DeletePhotoById(id);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public async Task<int> DeletePhotosByAlbumId(int albumId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.DeletePhotosByAlbumId(albumId);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }
        
        public async Task<int> DeletePhotoFromAlbum(int albumId, int photoId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.DeletePhotoFromAlbum(albumId, photoId);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }
        #endregion

        public async Task<List<int>> SearchPhoto(string name)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.SearchPhoto(name);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new List<int>();
        }

        public async Task<List<int>> ExtendedSearchPhoto(ExtendedSearch model)
        {
            var validationResult = _searchValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                return new List<int>();
            }

            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10200/PhotoService");
            var _channelFactory = new ChannelFactory<IPhotoService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IPhotoService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.ExtendedSearchPhoto(_utilityMapper.ObjectMapper<PhotoResponse, ExtendedSearch>(model));
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new List<int>();
        }
    }
}
