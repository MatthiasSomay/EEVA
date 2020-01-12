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

function valthisform()
{
    var checkboxs=document.getElementsByName("Answers");
    var okay=false;
    for(var i=0,l=checkboxs.length;i<l;i++)
    {
        if(checkboxs[i].checked)
        {
            okay=true;
            break;
        }
    }
    if (!okay) alert("Please choose at least one Answer");
}