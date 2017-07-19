

var userMyApi = {
    getUserManager: "../../ajax/getUserinfo.aspx",
}
var url = userMyApi.getUserManager;
var userMyController = {
    getUserInfo: function () {
         
        var param = {
            Action: "getUserManagerByParams"
        };
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data.length > 0) {
                var arr = [];
                
                arr.push("<thead style='white-space:nowrap;'><tr>");
                for (var key in ret.data[0]) {
                    arr.push("<th>");
                    if (key == "RowNumber")
                        arr.push("<input type='checkbox' id='ck_userManager'  class='flat'/>")
                    else
                        arr.push(key);
                    arr.push("</th>");
                }
                arr.push("</tr></thead><tbody>");
                for (var i = 0; i < ret.data.length; i++) {
                    arr.push("<tr>");
                    for (var key in ret.data[i]) {
                        arr.push("<td nowrap class='alt'>");
                        if (key == "RowNumber")
                            arr.push("<input type='checkbox' data-value='" + ret.data[i].会员ID + "' name='ck_u_list' class='flat'/> ")
                        else
                            arr.push("<a herf='#'>" + ret.data[i][key] + "</a>");
                        arr.push("</td>");
                    }
                    arr.push("</tr>");
                }
                arr.push("</tbody>");
                
                $("#user_t").html(arr.join('')); // 显示列表
            }
        });
    },
    SaveUserDate: function (id_list, type) {
        var param = {
            Action: "SaveUserDate",
            s_id:id_list,
            t_name: type,
            t_date:getCurrentTime(),
        };
        var htmlValue = userMyController.userCallbackMessage[type];
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data != "" && ret.data != null) {
                if (ret.data > 0) {
                    alert(htmlValue);
                    userMyController.getUserInfox();
                }
            }
        },true);
    },
    UpXuDate: function (id_list) {
        var param = {
            Action: "up_UserXF",
            s_id: id_list,
        };
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data != "" && ret.data != null) {
                if (ret.data > 0) {
                    alert('续费成功');
                    userMyController.getUserInfox();
                }
            }
        }, true);
    },
    getUserInfox: function () {
        var param = {
            Action: "getUserManagerByParams"
        };
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            $("#user_t").html(sheetUser(ret, "userinfo", "s_id"));
            $("#hID").val("");
        });
    },
    userCallbackMessage: {
        'xf_date': '续费成功',
        'fx_date': '返现成功,30日之后点击返码即可',
        'email_date': '发送邮件成功',
        'fm_date':'返码成功,7日之后即可收到码'
    }
}