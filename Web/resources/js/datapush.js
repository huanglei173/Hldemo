
$(function () {

    $("#btn_exp_user").click(function () {
        $("#btn_model_UpFiles")[0].dataset.temp = 'user_up';
    })
    $("#btn_exp_merchant").click(function () {
        $("#btn_model_UpFiles")[0].dataset.temp = 'merchant_up';
    })
    $("#btn_exp_commodity").click(function () {
        $("#btn_model_UpFiles")[0].dataset.temp = 'commodity_up';
    })
    $("#btn_model_UpFiles").click(function (e) {
        var url = "ajax/UpDataByFiles.aspx";
        var param = {
            fileName: $("#fileName").val(),
            fileType: e.target.dataset.temp,
            action: 'SaveUserByFile'
        };
        ajaxPost(url, param, function (ret) {
            console.log(ret);
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data != "" && ret.data != null) {
                if (ret.data > 0) {
                    $("#modal_content").html("上传成功");
                    switch (e.target.dataset.temp) {
                        case "commodity_up":
                            printMyController.getCommodityInfo();
                            break;
                        case "user_up":
                            userMyController.getUserInfo();
                            break;
                        case "merchant_up":
                            printMyController.getMerchantInfo();
                            break;

                    }
                    if (e.target.dataset.temp == "user_up") {
                        userMyController.getUserInfo();
                    }
                }
            }
        }, true);

    });
})