$(document).ready(function () {
    $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);

    });
});

$(function () {
    $('div.form-group .chkTrt').on('click', function () {
        $('div.form-group .chkTrt').not(this).prop('checked', false);
    });
});