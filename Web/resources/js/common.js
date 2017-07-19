

function httpSuccess(xhr) {
    var status = true;
    try {
        if (xhr.errorCode == -1) {//表示token验证失败
            status = false;
        }

        if (xhr.errorCode != 0) {
            
           alert(xhr.errorMessage, '温馨提示');
            status = false;
        }
    } catch (e) {
    }

    return status;
};



function ajaxPost(url, params, callback, async, xhrFields) {
    async = typeof (async) == "boolean" && async === false ? false : true;
    xhrFields = typeof (xhrFields) == "boolean" && xhrFields === false ? false : true;
    $.ajax({
        url: url,
        data: params,
        method: "POST",
        dataType: "json",
        cache: false,
        async: async,
        success: callback,
        beforeSend: function () {
            if (xhrFields)
                $(".maskPanel").show();
        },
        complete: function () {
            if (xhrFields)
             $(".maskPanel").hide();
        }
    });
}
 

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}



//随机生成ID
function MathRand(digit) {
    var Num = "";
    for (var i = 0; i < digit; i++) {
        Num += Math.floor(Math.random() * 10);
    }
    return Num;
}

function MathRandBetween(digit,beginNum, endNum) {
    var Num = "";
    while (Num == "") {
        for (var i = 0; i < digit; i++) {
            Num += Math.floor(Math.random() * 10);
        }
        if (Num > beginNum && Num < endNum)
            Num = Num.length < 2 ? p(Num) : Num;
        else
            Num = "";
    }
    return Num;
}

function p(s) {
    return s < 10 ? '0' + s : s;
}

function getCurrentTime(sms) {

    var myDate = new Date();
    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var h = myDate.getHours();       //获取当前小时数(0-23)
    var m = myDate.getMinutes();     //获取当前分钟数(0-59)
    var s = myDate.getSeconds();
    if (sms)
        return year + '-' + p(month) + "-" + p(date) + " " + p(h) + ':' + p(m) + ":" + p(s);

    return year + '-' + p(month) + "-" + p(date);
}



function getDateByTime(times,sms) {

    var myDate = new Date(times);
    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var h = myDate.getHours();       //获取当前小时数(0-23)
    var m = myDate.getMinutes();     //获取当前分钟数(0-59)
    var s = myDate.getSeconds();
    if (sms)
        return year + '-' + p(month) + "-" + p(date) + " " + p(h) + ':' + p(m) + ":" + p(s);

    return year + '-' + p(month) + "-" + p(date);
}