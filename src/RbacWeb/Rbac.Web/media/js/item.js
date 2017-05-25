$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysDay").addClass('active');
    $("#createItemDetailForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseItem/CreateItemDetail",
            dataType: "json",
            cache: false,
            success: function (data) {
                console.log(data);
                if (data.Status == "+OK") {
                    showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.reload(true);
                    }, (2000));
                } else {
                    showNotice('error', data.Msg, 2000);
                }
                setTimeout(function () {
                    $("#submit_btn").button("complete");
                }, (2000));
            },
            beforeSubmit: function () {
                if ($("span>img").length > 0) {
                    showNotice('error', '必要信息不完整', 2000);
                    $("#submit_btn").button("complete");
                    return false;
                }
            }
        };
        $(this).ajaxSubmit(options);
        return false;
    });
    $("#updateItemDetailForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseItem/UpdateItemDetail",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.href = "/SysManage/BaseItem/ItemDetailList?itemId=" + $("#ItemId").val();
                    }, (2000));
                } else {
                    showNotice('error', data.Msg, 2000);
                }
                setTimeout(function () {
                    $("#submit_btn").button("complete");
                }, (2000));
            },
            beforeSubmit: function () {
                if ($("span>img").length > 0) {
                    showNotice('error', '必要信息不完整', 2000);
                    $("#submit_btn").button("complete");
                    return false;
                }
            }
        };
        $(this).ajaxSubmit(options);
        return false;
    });
})
//退回
var backTrack = function () {
    window.location.href = "/SysManage/BaseItem/ItemDetailList?ItemId=" + $("#ItemId").val();
}
//创建字典明细
var createItemDetail = function () {
    var obj = parent.document.getElementById("leftFrame").contentWindow.zTreeObj.getSelectedNodes();
    if (obj != "") {
        if (obj[0].name == "所有类别") {
            parent.showNotice('error', '根节点不可选', 2000);
        } else {
            window.location.href = "/SysManage/BaseItem/CreateItemDetail?itemId=" + obj[0].id;
        }
    } else {
        parent.showNotice('error', '您选中选择字典类别', 2000);
    }
}

//编辑字典明细
var updateItemDetail = function (id) {
    window.location.href = "/SysManage/BaseItem/UpdateItemDetail?itemDetailId=" + id;
}

//删除字典明细
var deleteItemDetail = function (id) {
    art.dialog.confirm("您确认要删除它吗 ?", function () {
        $.ajax({
            url: "/SysManage/SystemItem/DeleteItemDetail",
            type: "post",
            dataType: "json",
            data: "itemDetailId=" + id,
            success: function (data) {
                if (data.Status == "+OK") {
                    showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.reload(true);
                    }, (2000));
                } else {
                    showNotice('error', data.Msg, 2000);
                }
            }
        });

    }, function () {

    });
}