$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysBasic").addClass('active');

    $("#selectOrganize").bind("click", function () {
        selectOrganize();
    })


    $("#Name").blur(function () {
        $.post("/SysManage/BaseOrganize/CheckOrganizeName", { name: $("#Name").val(), organizeId: $("#OrganizeId").val() }, function (data, status) {
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
            url: "/SysManage/BaseOrganize/Create",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.reload(true);
                        parent.leftFrame.window.location.reload(true);
                    }, (2000));
                } else {
                    parent.showNotice('error', data.Msg, 2000);
                }
                setTimeout(function () {
                    $("#submit_btn").button("complete");
                }, (2000));
            },
            beforeSubmit: function () {
                if ($("span>img").length > 0) {
                    parent.showNotice('error', '必要信息不完整', 2000);
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
            url: "/SysManage/BaseOrganize/Update",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        parent.leftFrame.window.location.reload(true);
                    }, (2000));
                } else {
                    parent.showNotice('error', data.Msg, 2000);
                }
                setTimeout(function () {
                    $("#submit_btn").button("complete");
                }, (2000));
            },
            beforeSubmit: function () {
                if ($("span>img").length > 0) {
                    parent.showNotice('error', '必要信息不完整', 2000);
                    $("#submit_btn").button("loading");
                    return false;
                }
            }
        };
        $(this).ajaxSubmit(options);
        return false;
    });
})

//只能输入整数
var checkSortCode = function (e) {
    var num = $(e).val();
    if (num != "") {
        if (!num.match($.regexpCommon('numberInt'))) {
            $(e).val("");
        }
    }
}

//选择组织机构
var selectOrganize = function () {
    parent.art.dialog.open("/Controls/Public/SelectOrganize?departmentId=" + $("#ParentId").val(), {
        id: "showBoxDialog",
        title: "选择组织机构",
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
            $("#ParentId").val(node.id);
        }
    });
};

//分配角色
var AllotRole = function () {
    var obj = parent.document.getElementById("leftFrame").contentWindow.zTreeObj.getSelectedNodes();
    if (obj.length == 0) {
        showNotice('error', "请先选择组织机构节点", 2000);
        return;
    }
    art.dialog.open("/SysManage/BaseOrganize/AllotRole?departmentId=" + obj[0].id + "&departmentName=" + encodeURI(obj[0].name), {
        id: "showBoxDialog",
        title: "分配角色",
        //lock: true,
        width: 500,
        height: 450,
        init: function () {
            var iframe = this.iframe.contentWindow;
        },
        okVal: "确定",
        cancelVal: "取消",
        ok: function () {
            var iframe = this.iframe.contentWindow;
            var roleIds = [];
            var departmentId = $(iframe.document).find("#departmentId").val();
            var parent = $(iframe.document).find("input[type='checkbox']:checked").each(function () {
                var index = $(this).val();
                roleIds.push(index);
            });

            $.ajax({
                url: "/SysManage/BaseOrganize/SaveAllotRole",
                type: "post",
                dataType: "json",
                data: "departmentId=" + departmentId + "&roleIds=" + roleIds.join(','),
                success: function (data) {
                    if (data.Status == "+OK") {
                        showNotice('success', data.Msg, 2000);
                        setTimeout(function () {
                            art.dialog.close();
                        }, (2000));
                    } else {
                        showNotice('error', data.Msg, 2000);

                    }
                }
            });
        }
    });
}