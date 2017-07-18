



function sheet(ret) {
    var arr = [];
    if (ret.data.length > 0) {
        arr.push(" <table class='table table-bordered'>")
        arr.push("<thead style='white-space:nowrap;'><tr>");
        for (var key in ret.data[0]) {
            arr.push("<th>");
            arr.push(key);
            arr.push("</th>");
        }
        arr.push("</tr></thead><tbody>");
        for (var i = 0; i < ret.data.length; i++) {
            arr.push("<tr >");
            for (var key in ret.data[i]) {
                arr.push("<td nowrap class='alt'>");
                arr.push("<a herf='#'>" + ret.data[i][key] + "</a>");
                arr.push("</td>");
            }
            arr.push("</tr>");
        }
        arr.push("</tbody>");
        arr.push("</table>");
    }
    return arr;
}

//下拉框带搜索功能
function selectSearch(domId, ret) {
    //多选<select id="id_select" class="selectpicker bla bla bli" multiple data-live-search="true">
    var arr = [];
    arr.push("<option value='0'>请选择</option>")
    for (var i = 0; i < ret.data.length; i++) {
        arr.push('<option value=' + ret.data[i].s_id + '>' + ret.data[i].u_name + '[' + ret.data[i].s_id + ']</option>');
    }
    $("#" + domId).html(arr.join(''));
    $('.selectpicker').selectpicker({
        'selectedText': 'cat'
    });
}




//ret数据集合
//listName需要显示的集合字段
//dataValue需要绑定checkbox的value
//单选或多选 true 是单选false 多选
function sheettwo(ret, listName, dataValue, cktype) {
    cktype = typeof (cktype) == "boolean" && cktype === false ? false : true;
    var field = fieldApi[listName];
    var arr = [];
    if (ret.data.length > 0) {
        arr.push("<thead style='white-space:nowrap;'><tr>");
        for (var key in field) {
            arr.push("<th>");
            if (key == "ck_box")
                arr.push("序号");
            else
                arr.push(field[key]);
            arr.push("</th>");
        }
        arr.push("</tr></thead><tbody>");
        for (var i = 0; i < ret.data.length; i++) {
            if (cktype)
                arr.push("<tr id='tr_" + ret.data[i][dataValue] + "' onclick=\"CheckTrue('" + ret.data[i][dataValue] + "')\">");
            else
                arr.push("<tr id='tr_" + ret.data[i][dataValue] + "' onclick=\"checkTrueOne('" + ret.data[i][dataValue] + "')\">");
            for (var key in field) {
                arr.push("<td nowrap class='alt'>");
                if (key == "ck_box")
                    arr.push("<input type='checkbox' id='checked_" + ret.data[i][dataValue] + "'  onclick=\"CheckTrue('" + ret.data[i][dataValue] + "')\" data-value='" + ret.data[i][dataValue] + "' name='ck_u_list' class='flat'/> ");
                else
                    arr.push("<a herf='#'>" + ret.data[i][key] + "</a>");
                arr.push("</td>");
            }
            arr.push("</tr>");
        }
        arr.push("</tbody>");
    }
    else {
        console.log('---');
        return "暂无数据";
    }
    return arr.join('');
}

function checkTrueOne(xuhao) {
    $("input[type='checkbox']").each(function (index, item) {
        $(item).parent().parent().removeClass("tractive");
        $(item).prop("checked", false);
    });
    $("#checked_" + xuhao).prop("checked", true);
    $("#tr_" + xuhao).attr("class", "tractive");
    $("#hID").val(xuhao);
}

function CheckTrue(xuhao) {
    if ($("#checked_" + xuhao).prop("checked")) {
        $("#checked_" + xuhao).prop("checked", false);
        $("#tr_" + xuhao).removeClass("tractive");

        var tempValue = $("#hID").val().split(';');
        $.each(tempValue, function (index, item) {
            if (item == xuhao)
                tempValue.splice(index, 1);
        });
        $("#hID").val(tempValue.join(';'));
    }
    else {
        $("#checked_" + xuhao).prop("checked", true);
        $("#tr_" + xuhao).attr("class", "tractive");
        $("#hID").val($("#hID").val() + xuhao + ";");
    }
}


//ret数据集合
//listName需要显示的集合字段
//dataValue需要绑定checkbox的value
//单选或多选 true 是单选 多选false
function sheetUser(ret, listName, dataValue, cktype) {
    cktype = typeof (cktype) == "boolean" && cktype === false ? false : true;
    var field = fieldApi[listName];
    var arr = [];
    if (ret.data.length > 0) {
        arr.push("<thead style='white-space:nowrap;'><tr>");
        for (var key in field) {
            arr.push("<th>");
            if (key == "ck_box")
                arr.push("序号");
            else
                arr.push(field[key]);
            arr.push("</th>");
        }
        arr.push("</tr></thead><tbody>");
        for (var i = 0; i < ret.data.length; i++) {
            if (cktype)
                arr.push("<tr id='tr_" + ret.data[i][dataValue] + "' onclick=\"CheckTrue('" + ret.data[i][dataValue] + "')\">");
            else
                arr.push("<tr id='tr_" + ret.data[i][dataValue] + "' onclick=\"checkTrueOne('" + ret.data[i][dataValue] + "')\">");
           
            for (var key in field) {
                var style = "";
                if (field[key] == "下次续费日期")
                    style = ret.data[i].next_xf_flag == "1" ? "backPink" : "";
                if (field[key] == "返码日期")  
                    style = ret.data[i].fm_date_flag == "1" ? "backPink" : "";
 
                if (field[key] == "发邮件日期")
                    style = ret.data[i].email_date_flag == "1" ? "backPink" : "";
                if (field[key] == "下次返现日期")  
                    style = ret.data[i].next_fx_flag == "1" ? "backPink" : "";

                
                arr.push("<td nowrap class='alt " + style + "' >");
                if (key == "ck_box")
                    arr.push("<input type='checkbox' id='checked_" + ret.data[i][dataValue] + "'  onclick=\"CheckTrue('" + ret.data[i][dataValue] + "')\" data-value='" + ret.data[i][dataValue] + "' name='ck_u_list' class='flat'/> ");
                else
                    arr.push("<a herf='#'>" + ret.data[i][key] + "</a>");
                arr.push("</td>");
            }
            arr.push("</tr>");
        }
        arr.push("</tbody>");
    }
    else {
        console.log('---');
        return "暂无数据";
    }
    return arr.join('');
}


