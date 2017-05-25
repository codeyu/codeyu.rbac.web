$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysBasic").addClass('active');
    $('.tree').treegrid({
        'initialState': 'expanded',
        'saveState': true,
    });

    $("#selectModule").bind("click", function () {
        selectModule();
    })

    $("#Name").blur(function () {
        $.post("/SysManage/BaseModule/CheckModuleName", { name: $("#Name").val(), moduleId: $("#ModuleId").val() }, function (data, status) {
            var dataJson = eval('(' + data + ')');
            if (dataJson.Status == "+ERROR") {
                $('#alert-txt').text(dataJson.Msg)
                $('.alert').show();
                $("#NameSpan").html("<img src='/media/images/BacoError.gif' align=absMiddle>");
            } else {
                $('.alert').hide();
            }
        })
    })

    $("#createForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseModule/Create",
            dataType: "json",
            cache: false,
            success: function (data) {
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

    $("#updateForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseModule/Update",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.href = "/SysManage/BaseModule/Index";
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

var backTrack = function () {
    window.location.href = "/SysManage/BaseModule/Index";
}

//选择功能模块
var selectModule = function () {
    parent.art.dialog.open("/Controls/Public/SelectModule?moduleId=" + $("#ParentId").val(), {
        id: "showBoxDialog",
        title: "选择功能模块",
        //lock: true,
        width: 500,
        height: 550,
        init: function () {
            var iframe = this.iframe.contentWindow;
        },
        okVal: "确定",
        cancelVal: "取消",
        ok: function () {
            var node = this.iframe.contentWindow.zTreeObj.getCheckedNodes(true)[0];
            if (typeof (node) == 'undefined') {
                parent.showNotice('error', "你至少需要勾选一个节点", 2000);
                return false;
            }
            $("#ParentNameSpan").text(node.name);
            $("#ModuleSortCodeSpan").text(node.ModuleSortCode);
            $("#ParentId").val(node.id);
        }
    });
};

//删除菜单
var deleteModule = function (id, isleaf) {
    if (isleaf != '') {
        showNotice('error', "含有子节点", 2000);
        return;
    }
    art.dialog.confirm("您确认要删除它们吗 ?", function () {
        $.ajax({
            url: "/SysManage/BaseModule/Delete",
            type: "post",
            dataType: "json",
            data: "id=" + id,
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