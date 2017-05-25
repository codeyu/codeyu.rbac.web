//正则验证表单
(function ($) {
    $.regexpCommon = function (regexpDesc) {
        return $.regexpCommon.regexpPattern[regexpDesc].call();
    };

    $.regexpCommon.regexpPattern = {
        // numbers

        numberZhengInt: function () {
            return /^[0-9]*[1-9][0-9]*$/;
        },
        numberInteger: function () {
            return /^[-+]?[1-9]\d*\.?[0]*$/;
        },
        numberInt: function () {
            return /^[-+]?[0-9]+(\.[0-9]+)?$/;
        },
        numberFloat: function () {
            return /^[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?$/;
        },
        numberDecimal: function () {
            return /^\d+(\.\d{0,2})?$/;
        },
        numberDecimalFixed4: function () {
            return /^\d+(\.\d{0,4})?$/;
        },
        numberFu: function () {
            return /^(-?\d+)(\.\d{0,4})?$/;
        },
        // email
        email: function () {
            return /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
        },
        ssn: function () {
            return /^\d{3}-\d{2}-\d{4}$/;
        },
        url: function () {
            return /^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$/;
        },
        phoneNumberUS: function () {
            return /^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$/;
        },
        zipCodeUS: function () {
            return /^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$/;
        },
        currencyUS: function () {
            return /^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$/;
        },
        htmlHexCode: function () {
            return /^#([a-fA-F0-9]){3}(([a-fA-F0-9]){3})?$/;
        },
        dottedQuadIP: function () {
            return /^(\d|[01]?\d\d|2[0-4]\d|25[0-5])\.(\d|[01]?\d\d|2[0-4] \d|25[0-5])\.(\d|[01]?\d\d|2[0-4]\d|25[0-5])\.(\d|[01]?\d\d|2[0-4] \d|25[0-5])$/;
        },
        macAddress: function () {
            return /^([0-9a-fA-F]{2}:){5}[0-9a-fA-F]{2}$/;
        }
    };
})(jQuery);

//验证文本框
var checkinput = function (elementname, spanid) {
    var tmpvalue = document.getElementById(elementname).value;
    while (tmpvalue.indexOf(" ") >= 0) {
        tmpvalue = tmpvalue.replace(" ", "");
    }
    if (tmpvalue != "") {
        while (tmpvalue.indexOf("\r\n") >= 0) {
            tmpvalue = tmpvalue.replace("\r\n", "");
        }
        if (tmpvalue != "") {
            document.getElementById(spanid).innerHTML = "";
        } else {
            document.getElementById(spanid).innerHTML = "<img src='/media/images/BacoError.gif' align=absMiddle>";
        }
    } else {
        document.getElementById(spanid).innerHTML = "<img src='/media/images/BacoError.gif' align=absMiddle>";
    }
}

//验证下拉框
var checkselect = function (elementname, spanid) {
    var obj = document.getElementById(elementname);
    var index = obj.selectedIndex; // 选中索引
    var tmpvalue = obj.options[index].value; // 选中值
    while (tmpvalue.indexOf(" ") >= 0) {
        tmpvalue = tmpvalue.replace(" ", "");
    }
    if (tmpvalue != "") {
        while (tmpvalue.indexOf("\r\n") >= 0) {
            tmpvalue = tmpvalue.replace("\r\n", "");
        }
        if (tmpvalue != "") {
            document.getElementById(spanid).innerHTML = "";
        } else {
            document.getElementById(spanid).innerHTML = "<img src='/media/images/BacoError.gif' align=absMiddle>";
        }
    } else {
        document.getElementById(spanid).innerHTML = "<img src='/media/images/BacoError.gif' align=absMiddle>";
    }
}

//得到Get方式提交的查询字符串
var GetQuery = function (key) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == key) {
            return ar[1];
        }
    }
    return "";
}

//表格选中多条记录
var checkMultipleSelected = function (elName) {
    elName = (typeof (elName) === "undefined") ? "_id[]" : elName;
    var chk_value = [];
    $("input[name='" + elName + "']:checked").each(function () {
        chk_value.push($(this).val());
    });

    if (chk_value.length === 0) {
        showNotice('error', '您至少需要选择一条信息', 2000);
        return false;
    }
    return chk_value;
}

//提示信息
var showNotice = function (status, noticeText, timeout) {
    if (status === undefined || status === "") {
        status = "warning";
    }
    if (noticeText === undefined || noticeText === "") {
        noticeText = "正在处理你的请求，请稍候...";
    }
    if (timeout === undefined || timeout === "") {
        timeout = 3000;
    }
    var n = noty({
        text: noticeText,
        type: status,
        modal: false,
        layout: 'center',
        theme: 'defaultTheme',
        timeout: false
    });
    setTimeout(function () {
        n.close();
    }, (timeout));
    return n;
}

//安全退出系统
var outLogin = function () {
    art.dialog.confirm("确定退出当前系统 ?", function (r) {
        if (r) {
            window.location.href = "/Home/LoginUser";
        }
    });
}

//改变顶部菜单
var changeTopModule = function (moduleId) {
    $.post("/Home/ChangeTopModule", { moduleId: moduleId }, function () {
            
    })
}

$(document).ready(function () {

    //点击这里可以编辑用户信息提示
    $('#top-info a').mouseover(function () {
        $(this).tooltip('show');
    })

    //清空查询记录
    $("#clean").click(function () {
        $("#SearchForm input[type='text']").val("");
    });

    $("input[name='_id[]']").click(function () {
        if (this.checked === false) {
            $("#checkall").attr('checked', false);
        }
    });

    $("#checkall").click(function () {
        var checked_status = this.checked;
        $("input[name='_id[]']").each(function () {
            this.checked = checked_status;
        });
    });

    $("input[name='_id[]']").click(function () {
        if (this.checked === false) {
            $("#checkall").attr('checked', false);
        }
    });
})

