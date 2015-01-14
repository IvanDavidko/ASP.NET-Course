using System;

namespace Domain.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }

        public bool PermissionAddAlbum { get; set; }
        public bool PermissionAddPhoto { get; set; }
    }
}
