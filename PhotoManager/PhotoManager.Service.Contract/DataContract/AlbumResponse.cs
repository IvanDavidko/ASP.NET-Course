using System;
using System.Runtime.Serialization;

namespace PhotoManager.Service.Contract.DataContract
{
    [DataContract]
    public class AlbumResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTimeOffset? DateCreated { get; set; }

        [DataMember]
        public DateTimeOffset? DateModified { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int UserId { get; set; }
    }
}
