$(document).ready(function () {
    $('body').off('click', '.delete-btn').on('click', '.delete-btn', function (e) {
        e.preventDefault();
        var href = $(this).attr('href');
        bootbox.confirm("Are you sure you want to delete the record?", function (result) {
            window.location = href;
        });
    });
});