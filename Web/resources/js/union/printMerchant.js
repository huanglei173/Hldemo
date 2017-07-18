var url = "../../ajax/getMerchant.aspx";

$(function () {

    getMerchantInfo();
    getUserInfo();
    $("#btn_print").click(function () {
        var temp = $("#sp_print_1").html();
        localStorage.setItem('print_panel', temp);
        window.open("html/union/print.html");
    });
    $("#btn_import_merchant").click(function () {
        $("#btn_model_UpFiles")[0].dataset.temp = 'merchant_up';
    });
    $("#txt_price").on('keyup', function () {
        $("#pos_price").html($(this).val());
    });
    $('.selectpicker').selectpicker({
        'selectedText': 'cat'
    });
    $('#sk_date').daterangepicker({
        singleDatePicker: true,
        singleClasses: "picker_2",
        format: 'YYYY-MM-DD',
        locale: {
            applyLabel: '确认',
            cancelLabel: '取消',
            fromLabel: '从',
            toLabel: '到',  
            weekLabel: 'zhdn',
            customRangeLabel: 'Custom Range',
            daysOfWeek: ["日", "一", "二", "三", "四", "五", "六"],
            monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        }
    }, function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
    });
    $("#btnSave_sk_list").on('click', function () {
        
        var param = {
            Action:"SaveSkList",
            s_id:$("#sel_userinfo").val(),
            m_id: $("#sel_merchant").find("option:selected").val(),
            sk_price: $("#txt_price").val(),
            sk_date: $("#sk_date").val() + " " + MathRandBetween(2, 10, 20) + ":" + MathRandBetween(2, 1, 59) +":"+ MathRandBetween(2,1,59),
        }
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data > 0) {
                var isTrue = confirm("保存成功是否打印");
                if (isTrue) {
                    console.log(ret.data);
                    window.open("html/union/print.html?l_id=" + ret.data + "&idType=merchantPrint"); 
                }
            }
        });
    });
});

 
 
function getMerchantInfo() {
    var param = { Action: "getMerchantInfo" };
    ajaxPost(url, param, function (ret) {
        if (!httpSuccess(ret)) {
            return;
        }
        if (ret.data != "" && ret.data != null) {
            if (ret.data.length > 0) {
                console.log(ret);
                console.log('----a');
                localStorage.setItem(localApi.getMerchantInfo, JSON.stringify(ret.data));
                var arr = [];
                
                arr.push('<option value="-1">请选择商户</option>');
                for (var i = 0; i < ret.data.length; i++) {
                    arr.push('<option value=' + ret.data[i].m_id + '>' + ret.data[i].m_name + '</option>');
                }
                $("#sel_merchant").html(arr.join(''));
                $("#sel_merchant").change(function (e) {
                    if ($(this).find("option:selected").val() == "-1")
                        return;
                    $("#pos_m_name").html($(this).find("option:selected").text());
                    printLoadInfo("pos_",$(this).find("option:selected").val(), "m_id", localApi.getMerchantInfo);
                });
            }
        }
    }, false, false);
}

//domId  文本框前缀
//m_id 需要匹配的字段
//cloumsId  local里的字段
//localName 
function printLoadInfo(domId,m_id,cloumsId,localName) {
    var local = localStorage.getItem(localName);
    if (local != null) {
       var worklist = JSON.parse(local);
       for (var i = 0; i < worklist.length; i++) {
           var objlist = worklist[i];
           if (objlist[cloumsId] == m_id) {
               for (var pos in objlist) {
                   $("#" + domId + pos).html(objlist[pos]);
               }
           }
       }
    }
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
                $("#sel_userinfo").change(function (e) {
                    if (e.target.value == "0")
                        return;
                    printLoadInfo("pos_", e.target.value, 's_id', localApi.getUserInfo);
                });

            }
        }
    }, false, false);
}