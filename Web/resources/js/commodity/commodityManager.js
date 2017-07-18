
var url = "../../ajax/getMerchant.aspx";
$(function () {
    getUserInfo();
    getPosInfo();

    $("#btnSearchPos").on('click', function () {
        getPosInfo($("#sel_userinfo").val());
    });
    $("#btn_print").on('click', function () {
        var hID = $("#hID").val();
        if (hID != "") {
            window.open("html/union/print.html?l_id=" + hID + "&idType=1");;
        }
    })
});


function getPosInfo(s_id) {
    var param = {
        Action: "getPrintCommodity",
        s_id: s_id
    }
    ajaxPost('../../ajax/getMerchant.aspx', param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
 
        $("#comUser_t").html(sheettwo(ret, "xf_listInfo", "l_id", false));
    });
}


function getUserInfo() {
    var param = { Action: "getUserInfoByParams" };
    ajaxPost('../../ajax/getUserinfo.aspx', param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
        if (ret.data != "" && ret.data != null) {
            if (ret.data.length > 0) {
                // localStorage.removeItem(localApi.getMerchantInfo);
                localStorage.setItem(localApi.getUserInfo, JSON.stringify(ret.data));
                selectSearch('sel_userinfo', ret);
            }
        }
    }, false, false);
}