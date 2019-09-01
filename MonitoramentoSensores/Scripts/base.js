$(function () {
    $(document).on('ready', function () {
        $('[data-toggle="tooltip"]').tooltip();
    })

    $(document).on('mousemove', '[data-toggle="tooltip"]', function () {
        var title = $(this).attr('data-title');
        $(this).attr('title', title);
    });
});