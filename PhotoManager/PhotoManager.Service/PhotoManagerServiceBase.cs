
namespace PhotoManager.Service
{
    public class PhotoManagerServiceBase
    {
        //User
        protected const string _USERID_INVALID_EXCEPTION = "User Id argument is missing";
        protected const string _USER_NAME_INVALID_EXCEPTION = "user name argument is missing";
        protected const string _USER_PASSWORD_INVALID_EXCEPTION = "user password argument is missing";
        protected const string _USER_PASSWORD_SALT_INVALID_EXCEPTION = "user password salt argument is missing";
        protected const string _ROLEID_SALT_INVALID_EXCEPTION = "Role Id argument is missing";
        protected const string _ROLE_NAME_INVALID_EXCEPTION = "Role name argument is missing";

        //Photo
        protected const string _PHOTO_NAME_INVALID_EXCEPTION = "Photo name argument is missing";
        protected const string _PHOTO_OBJECT_INVALID_EXCEPTION = "Photo object argument is missing";
        protected const string _PHOTOID_INVALID_EXCEPTION = "Photo Id argument is missing";
        protected const string _PHOTO_IMAGE_INVALID_EXCEPTION = "Photo image argument is missing";
        protected const string _PHOTO_SIZE_INVALID_EXCEPTION = "Photo size argument is missing";
        protected const string _PHOTO_TYPE_INVALID_EXCEPTION = "Photo type argument is missing";

        //Album
        protected const string _ALBUM_OBJECT_INVALID_EXCEPTION = "Album object argument is missing";
        protected const string _ALBUMID_INVALID_EXCEPTION = "Album Id argument is missing";
        protected const string _ALBUM_TITLE_INVALID_EXCEPTION = "Album title argument is missing";
        protected const string _ALBUM_URL_INVALID_EXCEPTION = "Album url argument is missing";
        
    }
}