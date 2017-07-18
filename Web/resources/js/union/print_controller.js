
var printMyApi = {
    getMerchantURL: "../../ajax/getMerchant.aspx",
}
var url = printMyApi.getMerchantURL;

var printMyController = {
    getCommodityInfo: function (m_name) {
        var param = { Action: "getCommodityInfo",m_name:m_name };
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data != "" && ret.data != null) {
                if (ret.data.length > 0) {
                    var arr = [];
                    for (var i = 0; i < ret.data.length; i++) {
                        arr.push("<tr>");
                        arr.push("<td nowrap class='alt'>");
                        arr.push("<input type='checkbox' id='ck_sp_" + ret.data[i].c_id + "' data-price='" + ret.data[i].单价 + "' data-spname='" + ret.data[i].商品名称 + "' data-num='1' data-value='" + ret.data[i].c_id + "' name='ck_merchant_list' class='flat'/>");
                        arr.push("</td>")
                        arr.push("<td nowrap class='alt'>");
                        arr.push(ret.data[i].商品名称);
                        arr.push("</td>")
                        arr.push("<td nowrap class='alt'>");
                        arr.push("<input type='text' name='text_sp_price' data-value=" + ret.data[i].c_id + " class='form-control-p'  value=" + ret.data[i].单价 + ">");
                        arr.push("</td>")
                        arr.push("<td nowrap class='alt'>");
                        //arr.push('<div class="input-group input-group-sm">');
                        //arr.push('  <label class="input-group-addon" data-value=' + ret.data[i].c_id + ' name="sp_num_jian" for="dataX">-</label>');
                        //arr.push(' <input type="text" class="form-control" value="1" name="sp_num" placeholder="x">');
                        //arr.push(' <span class="input-group-addon" data-value=' + ret.data[i].c_id + ' name="sp_num_jia">+</span>');
                        //arr.push(' </div>');
                        arr.push('<div class="input-group">');
                        arr.push('<span class="input-group-btn">');
                        arr.push('  <button type="button" data-value=' + ret.data[i].c_id + ' name="sp_num_jian" class="btn btn-primary">-</button>');
                        arr.push('</span>');
                        arr.push('  <input type="text" disabled="disabled" class="form-control-p" value="1">');
                        arr.push('<span class="input-group-btn">');
                        arr.push('  <button type="button" data-value=' + ret.data[i].c_id + '  name="sp_num_jia" class="btn btn-primary">+</button>');
                        arr.push('</span>');
                        arr.push('</div>');
                        arr.push("</td>")
                       
                        arr.push("</tr>");
                    }
                    $("#div_commodity").html(arr.join('')); // 显示列表

                    $('[name=ck_merchant_list]').change(function (e) {
                        if (e.target.checked == true)
                            printMyController.addPrintList("p_sp_list", e);
                        else
                            printMyController.delPrintList("p_sp_list", e.target.dataset.spname);
                    });
                    $("[name=sp_num_jian]").click(function (e) {
                        var sp_num = $(this).parent().next();
                        if ($(sp_num).val() <= 1)
                            return;
                        else
                        {   
                            $(sp_num).val(parseInt($(sp_num).val()) - 1);
                            $("#ck_sp_" + e.target.dataset.value)[0].dataset.num = $(sp_num).val();
                        }
                    });
                    $("[name=sp_num_jia]").click(function (e) {
                        var sp_num = $(this).parent().prev();
                        $(sp_num).val(parseInt($(sp_num).val()) + 1);
                        $("#ck_sp_" + e.target.dataset.value)[0].dataset.num = $(sp_num).val();
                    });
                    $("[name=text_sp_price]").keyup(function (e) {
                        $("#ck_sp_" + e.target.dataset.value)[0].dataset.price = $(this).val();
                    });
                }
            }
        });
    },
    getMerchantInfo: function () {
        console.log('getMerchantInfo');
        var param = { Action: "getMerchantInfo" };
        ajaxPost(url, param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            if (ret.data != "" && ret.data != null) {
                if (ret.data.length > 0) {

                    var arr = [];
                    arr.push('<select class="select2_single form-control" id="sel_merchant" tabindex="-1">');
                    for (var i = 0; i < ret.data.length; i++) {
                        arr.push('<option value=' + ret.data[i].m_id + '>' + ret.data[i].商户名 + '</option>');
                    }
                    arr.push('</select>');
                    
                    $("#div_merchant").html(arr.join(''));
                    $("#sel_merchant").change(function (e) {
                        printMyController.getCommodityInfo($(this).find("option:selected").text());
                        $("#sh_name_1").html($(this).find("option:selected").text());
                    });
                }
            }
        },false,false);
    },
    getUserInfo: function () {
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
                        //printLoadInfo("pos_", e.target.value, 's_id', localApi.getUserInfo);
                        printMyController.getMercharInfoBySid(e.target.value);
                    });

                }
            }
        }, false, false);
    },
    //localStorage  printSPList
    addPrintList: function (localName, e) {
        var merchant_Model = {
            sname: e.target.dataset.spname,
            sprice: e.target.dataset.price,
            s_num: e.target.dataset.num
        };
       
        var local = localStorage.getItem(localName);
        if (local != null) {
            var temptotal = 0;
            var obj = JSON.parse('[' + local + ']');
            for (var i = 0; i < obj.length; i++) {
                var temprice = obj[i].sprice;
                var tempnum = obj[i].s_num;
                temptotal += parseFloat(temprice) * parseInt(tempnum);
            }

            temptotal += parseFloat(merchant_Model.sprice) * parseInt(merchant_Model.s_num);
            if (temptotal > parseFloat($("#txt_price").val())) {
                alert("选中的商品金额总价大于此次消费的总额!");
                e.target.checked = false;
                return;
            }
            var jsonstr = JSON.stringify(merchant_Model);
            var newlocal = local + ',' + jsonstr;
            localStorage.setItem(localName, newlocal);
        }
        else {
            if (merchant_Model.sprice * merchant_Model.s_num > $("#txt_price").val()) {
                alert("选中的商品金额总价大于此次消费的总额!");
                e.target.checked = false;
                return;
            }
            var jsonstr = JSON.stringify(merchant_Model);
            localStorage.setItem(localName, jsonstr);
            
        }
        printMyController.sp_htmlShow(localName);
    },
    delPrintList: function (localName,del_name) {
        var local = localStorage.getItem(localName);
        var obj = JSON.parse('[' + local + ']');
        if (obj.length == 1)
            localStorage.removeItem(localName);
        else {
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].sname == del_name) {
                    obj.splice(i, 1);
                }
            }
            var objlist = JSON.stringify(obj);
            objlist = objlist.substring(1, objlist.length - 1);
            localStorage.setItem(localName, objlist);
        }
        printMyController.sp_htmlShow(localName);
    },
    sp_htmlShow: function (localName) {
        
        var local = localStorage.getItem(localName);
        if (local != null) {
            var worklist = JSON.parse('[' + local + ']');
            var sp_list = "";
            var sp_list_price = "";
            var total = 0.00;
            for (var i = 0; i < worklist.length; i++) {
                sp_list += ' <div class="sp_title">' + worklist[i].sname + '</div>';
                sp_list_price += '<div class="xf_t_list"  >';
                sp_list_price += '    <div class="xf_t_sp">' + worklist[i].sprice + '</div>';
                sp_list_price += '    <div class="xf_t_sp">' + worklist[i].s_num + '</div>';
                sp_list_price += '    <div class="xf_t_sp">' + parseFloat(worklist[i].sprice) * parseInt(worklist[i].s_num) + '</div>';
                sp_list_price += '</div>';
                total += parseFloat(worklist[i].sprice) * parseInt(worklist[i].s_num);
            }
            $("#sp_list_1").html(sp_list);
            $("#sp_list_price_1").html(sp_list_price);
            $("#sp_total_1").html(total);
            
        }
        else {
            $("#sp_list_1").html("");
            $("#sp_list_price_1").html("");
            $("#sp_total_1").html("0");
        }
        console.log();
        $("#la_total").html(parseFloat($("#txt_price").val()) - parseFloat($("#sp_total_1").html()));
    },
    getMercharInfoBySid:function(s_id) {
        var param = {
            Action: "getPosInfo",
            s_id: s_id,
            type:'xl',
        }
        ajaxPost('../../ajax/getMerchant.aspx', param, function (ret) {
            if (!httpSuccess(ret)) {
                return;
            }
            var arr = [];
            arr.push('<option value="-1">请选择消费记录</option>');
            for (var i = 0; i < ret.data.length; i++) {
                arr.push('<option data-price=' + ret.data[i].sk_price + ' data-mdate=' + ret.data[i].sk_date + ' data-value=' + ret.data[i].m_name + ' value=' + ret.data[i].l_id + '>' + ret.data[i].m_name + '-' + ret.data[i].sk_date.substring(0, 10) + '--' + ret.data[i].sk_price + '</option>');
            }
            $("#sel_merchantByinfo").html(arr.join(''));
        }, false, false);
    
        $("#sel_merchantByinfo").change(function (e) {
            printMyController.getCommodityInfo($(this).find("option:selected")[0].dataset.value);
            $("#sh_name_1").html($(this).find("option:selected")[0].dataset.value);
            $("#xf_time_1").html($(this).find("option:selected")[0].dataset.mdate);
            $("#txt_price").val($(this).find("option:selected")[0].dataset.price);
            localStorage.removeItem('p_sp_list');
        });
    },
    saveUserComList: function () {
        var paramArr = [];
        var temptotal = 0.00;
        $("[name=ck_merchant_list]:checked").each(function (index, item) {
            var c = {
                s_id: $("#sel_userinfo").val(),
                merchant_id: $("#sel_merchantByinfo").find("option:selected").val(),
                commodity_id: $(item)[0].dataset.value,
                commodity_price: $(item)[0].dataset.price,
                commodity_num: $(item)[0].dataset.num,
            }
            temptotal += parseFloat($(item)[0].dataset.price) * parseInt($(item)[0].dataset.num);
            paramArr.push(c);
        });
        if (temptotal == parseFloat($("#txt_price").val())) {
            var param = {
                Action: "saveUserComList",
                c: JSON.stringify(paramArr),
            }
            ajaxPost(url, param, function (ret) {
                if (!httpSuccess(ret)) {
                    return;
                }
                if (ret.data > 0) {
                    var isTrue = confirm("保存成功是否打印");
                    if (isTrue) {
                        window.open("html/union/print.html?l_id=" + $("#sel_merchantByinfo").find("option:selected").val() + "&idType=1");
                    }
                }
            });
        }
        else {
            alert("消费的金额与实际打印的不一致!");
        }
    },
}

$(function () {
    localStorage.removeItem('p_sp_list');
    printMyController.getUserInfo();
    $("#btnSave_sk_list").click(function () {
        printMyController.saveUserComList();
    });
})


 
 