$(function () {
    $(".maskPanel").show();
    return;
    userMyController.getUserInfox();
    $("#btn_xf").click(function () {
        if ($('#hID').val() != "")
            userMyController.UpXuDate($('#hID').val());
        else
            alert("请选中要编辑的行");
    });
    $("#btn_fx").click(function () {
        if ($('#hID').val() != "")
            userMyController.SaveUserDate($('#hID').val(), "fx_date");
        else
            alert("请选中要编辑的行");
    });
    $("#btn_email").click(function () {
        if ($('#hID').val() != "")
            userMyController.SaveUserDate($('#hID').val(), "email_date");
        else
            alert("请选中要编辑的行");
    });
    $("#btn_fm").click(function () {
        if ($('#hID').val() != "")
            userMyController.SaveUserDate($('#hID').val(), "fm_date");
        else
            alert("请选中要编辑的行");
    });
});


 