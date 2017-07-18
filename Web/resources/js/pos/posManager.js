
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
            window.open("html/union/print.html?l_id=" + hID + "&idType=merchantPrint");
        }
        else
            alert("请选择行");
    });
    $("#btn_print_xf").on('click', function () {
        var hID = $("#hID").val();
        if (hID != "") {
            window.open("html/union/print.html?l_id=" + hID + "&idType=1");
        }
        else
            alert("请选择行");
    })
});


function getPosInfo(s_id) {
    var param = {
        Action: "getPosInfo",
        s_id: s_id
    }
    ajaxPost(url, param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
        $("#pos_t").html(sheettwo(ret, "posinfo", "l_id", false));
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