$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysBasic").addClass('active');

    $("#Name").blur(function () {
        $.post("/SysManage/BaseRole/CheckRoleName", { name: $("#Name").val(), roleId: $("#RoleId").val() }, function (data, status) {
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
            url: "/SysManage/BaseRole/Create",
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
            url: "/SysManage/BaseRole/Update",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.href = "/SysManage/BaseRole/Index";
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
    window.location.href = "/SysManage/BaseRole/Index";
}
//只能输入整数
var checkSortCode = function (e) {
    var num = $(e).val();
    if (num != "") {
        if (!num.match($.regexpCommon('numberInt'))) {
            $(e).val("");
        }
    }
}
//删除角色
var deleteRole = function (id) {
    art.dialog.confirm("您确认要删除它们吗 ?", function () {
        $.ajax({
            url: "/SysManage/BaseRole/Delete",
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