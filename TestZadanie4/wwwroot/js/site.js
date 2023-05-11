// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(document).ready(function () {
    $('#myForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Book/AddBookAjax',
            method: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                alert(data);
            }
        });
    });
});
