$(document).ready(function () {
    $(".alert").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);

    });
});

$(function () {
    $(".btnSubmit").click(function () {
        var checked_checkboxes = $("#answers input[type=checkbox]:checked");
        var checkboxes = $("#answers  input[type=checkbox]")
        if (checkboxes > 0) {
            if (checked_checkboxes.length == 0) {
                $(".error").show();
                return false;
            }
            return true;
        }
        else return true;       
    });
});

$(function () {
    $('div.form-group .chkTrt').on('click', function () {
        $('div.form-group .chkTrt').not(this).prop('checked', false);
    });
});