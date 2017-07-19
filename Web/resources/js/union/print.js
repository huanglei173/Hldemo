 
$(function () {
    $("#print_marchant").hide();
    $("#print_commodity").hide();
    var l_id = getUrlParam('l_id');
    var idType = getUrlParam('idType');
    if (l_id != null && idType != null) {
        if (idType == "merchantPrint") {
            printMerchantInfo(l_id);
            $("#print_marchant").show();
        }
        else {
            printCommodityInfo(l_id);
            $("#print_commodity").show();
        }
    }
    else {
        alert("参数错误!");
       // window.close();
    }
    
    
});

function printMerchantInfo(l_id) {
    var param = {
        Action: "getPosInfo",
        l_id: l_id
    }
    ajaxPost('../../ajax/getMerchant.aspx', param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
        textFZ("pos_", ret);
    });
}

function printCommodityInfo(l_id) {
    var param = {
        Action: "getPrintCommodity",
        l_id: l_id
    }
    ajaxPost('../../ajax/getMerchant.aspx', param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
        if (ret.data) {
           
            var sp_total_1 = 0.00;
            var sp_list_price = "";
            for (var i = 0; i < ret.data.length; i++) {
                sp_list_price += '<div class="xf_t_list"  >';
                sp_list_price += '    <div class="xf_t_sx">' + ret.data[i].c_name + '</div>';
                sp_list_price += '    <div class="xf_t_sp">' + ret.data[i].commodity_num + '</div>';
                sp_list_price += '    <div class="xf_t_sp">' + ret.data[i].commodity_price + '</div>';
                sp_list_price += '</div>';
                sp_total_1 += parseFloat(ret.data[i].commodity_price) * parseInt(ret.data[i].commodity_num);
            }
            $("#sp_list_price_1").html(sp_list_price);
            $("#sp_total_1").html(sp_total_1);
            $("#sh_name_1").html(ret.data[0].m_name);
            var tox = ret.data[0].sk_date.replace(/\-/g, "/").split(/[- : \/]/);
            tox = new Date(tox[0], tox[1] - 1, tox[2], tox[3], tox[4], tox[5]);
            tox = getDateByTime(tox.getTime() + 48000, true);
            $("#xf_time_1").html(tox.replace(/\-/g, "."));
            $("#xf_syh_1").html(MathRand(5));
        }
        textFZ("pos_", ret);
    });
}
//domId  文本框前缀
//m_id 需要匹配的字段
//cloumsId  local里的字段
//localName 
function textFZ(domId, ret) {
    if (ret.data.length > 0) {
        var worklist = ret.data;
        for (var i = 0; i < worklist.length; i++) {
            var objlist = worklist[i];
            for (var pos in objlist) {
                $("#" + domId + pos).html(objlist[pos]);
            }
        }
    }
}