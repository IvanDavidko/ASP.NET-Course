using System;
using System.Runtime.Serialization;

namespace PhotoManager.Service.Contract.DataContract
{
    [DataContract]
    public class PhotoResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string ImageSize { get; set; }

        [DataMember]
        public string ImageType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTimeOffset? DateCreated { get; set; }

        [DataMember]
        public string PlaceCreated { get; set; }

        [DataMember]
        public string CameraModel { get; set; }

        [DataMember]
        public string FocalLengthOfTheLens { get; set; }

        [DataMember]
        public string Diaphragm { get; set; }

        [DataMember]
        public string ShutterSpeed { get; set; }

        [DataMember]
        public string ISO { get; set; }

        [DataMember]
        public bool FlashInUse { get; set; }

        [DataMember]
        public int UserId { get; set; }
    }
}
