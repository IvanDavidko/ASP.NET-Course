
using System;

namespace Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string ImageSize { get; set; }
        public string ImageType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public string PlaceCreated { get; set; }
        public string CameraModel { get; set; }
        public string FocalLengthOfTheLens { get; set; }
        public string Diaphragm { get; set; }
        public string ShutterSpeed { get; set; }
        public string ISO { get; set; }
        public bool FlashInUse { get; set; }
        public int UserId { get; set; }
    }
}
