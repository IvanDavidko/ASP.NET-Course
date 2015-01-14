$(document).ready(function () {

    $('.photo-item').click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        } else {
            $(this).addClass("selected");
        }
    });

    $('#AddPhotoSubmit').click(function () {
        var selectedPhotoIds = $('.selected').map(function () { return this.id; }).get();
        var albumId = $('#UserPhotos').data('album-id');

        if (selectedPhotoIds.length > 0 && albumId > 0) {
            $.ajax({
                traditional: true,
                type: "POST",
                url: '/Album/AddPhotosToAlbum',
                data: {
                    albumId: albumId,
                    photoIds: selectedPhotoIds
                },
                success: function (response) {
                    if (response) {
                        alert("Photo(s) is/are added");
                    } else {
                        alert("Something went wrong");
                    }
                }
            });
        }
    });

    var albumsPage = $("#ListBlock");
    if (albumsPage.length != 0) {
        var albums = $(albumsPage).find(".album-item");
        $.each(albums, function (key, value) {
            var albumId = $(value).attr("id");
            $.ajax({
                type: "GET",
                url: "/Photo/GetTitlePhoto?albumId=" + albumId,
                success: function (response) {
                    if (response) {
                        $(value).prepend("<a href='/Photo/PhotoList?albumId=" + albumId + "'>" + response + "</a>");
                    }
                }
            });
        });
    }

    var userAlbumsPage = $("#UserAlbumListBlock");
    if (userAlbumsPage.length != 0) {
        var albums = $(userAlbumsPage).find(".album-item");
        $.each(albums, function (key, value) {
            var albumId = $(value).attr("id");
            $.ajax({
                type: "GET",
                url: "/Photo/GetTitlePhoto?albumId=" + albumId,
                success: function (response) {
                    if (response) {
                        $(value).prepend("<a href='/Photo/UserPhotoList?albumId=" + albumId + "'>" + response + "</a>");
                    }
                }
            });
        });
    }

    var photoPage = $("#PhotoListBlock");
    if (photoPage.length != 0) {
        var photos = $(photoPage).find(".photo-item");
        $.each(photos, function (key, value) {
            var id = $(value).attr("id");
            $.ajax({
                type: "GET",
                url: "/Photo/GetPhoto?id=" + id,
                success: function (response) {
                    if (response) {
                        $(value).prepend("<a href='/Photo/PhotoDetails?id=" + id + "'>" + response + "</a>");
                    }
                }
            });
        });
    }

    var userPhotoPage = $("#UserPhotoListBlock");
    if (userPhotoPage.length != 0) {

        var albumId = $('#UserPhotoListBlock').data('album-id');
        var maketitlebutton = false;
        if (albumId != null)
            maketitlebutton = true;

        var photos = $(userPhotoPage).find(".photo-item");
        $.each(photos, function (key, value) {
            var id = $(value).attr("id");

            $.ajax({
                type: "GET",
                url: "/Photo/GetUserPhoto?id=" + id + "&maketitlebutton=" + maketitlebutton,
                success: function (response) {
                    if (response) {
                        $(value).prepend("<a href='/Photo/PhotoDetails?id=" + id + "'>" + response + "</a>");
                    }
                }
            });
        });
    }

    var userPhotos = $("#UserPhotos");
    if (userPhotos.length != 0) {
        var photos = $(userPhotos).find(".photo-item");
        $.each(photos, function (key, value) {
            var id = $(value).attr("id");
            $.ajax({
                type: "GET",
                url: "/Photo/GetPhoto?id=" + id,
                success: function (response) {
                    if (response) {
                        $(value).prepend(response);
                    }
                }
            });
        });
    }
});

function MakePhotoTitle(obj) {
    if (confirm('Are you sure?')) {
        var albumId = $('#UserPhotoListBlock').data('album-id');
        var photoId = $(obj).attr('id');
        var url = "/Photo/MakePhotoTitle?albumId=" + albumId + "&photoId=" + photoId;
        $.ajax({
            type: "POST",
            url: url,
            success: function (data) {
                if (data) {
                    alert("Title photo was changed");
                } else {
                    alert("Something went wrong");
                }
            }
        })
    }
}

function RemoveAlbum(obj) {
    if (confirm('Are you sure?')) {
        var id = $(obj).attr('id');
        if (id != null) {
            var url = "/Album/Remove?id=" + id;
            $.ajax({
                type: "POST",
                url: url,
                data: id,
                success: function (data) {
                    if (data) {
                        $("div#" + id).remove();
                        alert("Album was removed successfully");
                    } else {
                        alert("Something went wrong");
                    }
                }
            });
        }
    }
}

function RemovePhoto(obj) {
    if (confirm('Are you sure?')) {
        var id = $(obj).attr('id');
        if (id != null) {
            var url = "/Photo/Remove?id=" + id;
            $.ajax({
                type: "POST",
                url: url,
                data: id,
                success: function (data) {
                    if (data) {
                        $("div#" + id).remove();
                        alert("Photo was removed successfully");
                    } else {
                        alert("Something went wrong");
                    }
                }
            });
        }
    }
}

function ChangeRole() {
    if (confirm('Are you sure?')) {
        $.ajax({
            type: "POST",
            url: "/account/Premium",
            success: function (data) {
                if (data) {
                    alert("Album was removed successfully");
                } else {
                    alert("Something went wrong");
                }
            }
        });
    }
}

function DeletePhotoFromAlbum(obj) {
    if (confirm('Are you sure?')) {
        var photoId = $(obj).attr('id');
        var albumId = $('#UserPhotoListBlock').data('album-id');
        if (photoId != null) {
            $.ajax({
                type: "POST",
                url: "/Photo/DeletePhotoFromAlbum",
                data: {
                    albumId: albumId,
                    photoId: photoId
                },
                success: function (data) {
                    if (data) {
                        $("div#" + photoId).remove();
                        alert("Photo was removed successfully");
                    } else {
                        alert("Something went wrong");
                    }
                }
            });
        }
    }
}





