$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysBasic").addClass('active');


    $("#selectOrganize").bind("click", function () {
        selectOrganize();
    })


    //得到角色
    getRoleList();
    //得到岗位
    getJobTitleList();

    $("#selectManager").bind("click", function () {
        selectUser();
    })
   

    $("#UserName").blur(function () {
        $.post("/SysManage/BaseUser/CheckUserName", { userName: $("#UserName").val(), userId: $("#UserId").val() }, function (data, status) {
            var dataJson = eval('(' + data + ')');
            if (dataJson.Status == "+ERROR") {
                $('#alert-txt').text(dataJson.Msg)
                $('.alert').show();
                $("#UserNameSpan").html("<img src='/media/images/BacoError.gif' align=absMiddle>");
            } else {
                $('.alert').hide();
            }
        })
    })

    $("#createForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseUser/CreateUser",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.reload(true);
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
            url: "/SysManage/BaseUser/UpdateUser",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.href = "/SysManage/BaseUser/UserList?departmentId=" + $("#departmentId").val();
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
})
//返回
var backTrack = function () {
    window.location.href = "/SysManage/BaseUser/UserList?departmentId=" + $("#departmentId").val();
}

//得到角色
var getRoleList = function () {

    $.ajax({
        url: "/SysManage/BaseUser/GetRoleListByDepartmentId",
        type: "post",
        dataType: "json",
        data: "departmentId=" + $("#departmentId").val(),
        success: function (data) {
            $("#RoleId").html('');
            var html = "<option value=''></option>";
            $.each(data, function (index, comment) {
                html += "<option value='" + data[index].Id + "'>" + data[index].Name + "</option>";
            });

            $("#RoleId").html(html);
            var selectedRoleId = $("#SelectedRoleId").val();
            $("#RoleId option[value='" + selectedRoleId + "']").attr("selected", "selected");
        }
    });
}

//得到岗位
var getJobTitleList = function () {
    $.ajax({
        url: "/SysManage/BaseUser/GetJobTitleListByDepartmentId",
        type: "post",
        dataType: "json",
        data: "departmentId=" + $("#departmentId").val(),
        success: function (data) {
            $("#JobTitle").html('');
            var html = "<option value=''></option>";
            $.each(data, function (index, comment) {
                html += "<option value='" + data[index].Id + "'>" + data[index].Name + "</option>";
            });
            $("#JobTitle").html(html);
            var SelectedJobTitle = $("#SelectedJobTitle").val();
            $("#JobTitle option[value='" + SelectedJobTitle + "']").attr("selected", "selected");
        }
    });
}

//创建用户
var createUser = function () {
    var obj = parent.document.getElementById("leftFrame").contentWindow.zTreeObj.getSelectedNodes();

    if (obj.length == 0) {
        parent.showNotice('error', "请先选择组织机构节点", 2000);
    } else {
        if (obj[0].type == 2) {
            parent.showNotice('error', "分部节点下不能创建用户", 2000);
        }
        else if (obj[0].type == 1) {
            parent.showNotice('error', "单位节点下不能创建用户", 2000);
        } else {
            parent.$("#rightFrame").attr("src", "/SysManage/BaseUser/CreateUser?departmentId=" + obj[0].id + "");
        }
    }
}
//编辑用户
var updateUser = function (id) {
    parent.$("#rightFrame").attr("src", "/SysManage/BaseUser/UpdateUser?id=" + id + "");
}
//冻结、激活
var changeUserStatus = function (status, id) {
    var notify_txt = '您确认要审核通过它吗';
    if (status == 0) {
        notify_txt = '您确认要屏蔽它吗 ?';
    }
    parent.art.dialog.confirm(notify_txt, function () {

        $.ajax({
            url: "/SysManage/BaseUser/ChangeUserStatus",
            type: "post",
            dataType: "json",
            data: 'status=' + status + "&id=" + id,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.reload(true);
                    }, (2000));
                } else {
                    parent.showNotice('error', data.Msg, 2000);
                }
            }
        });

    }, function () {

    });
    return false;
}
//用户授权
var userPermission = function (id) {
    window.location.href = "/SysManage/BaseUser/UserPermission?id=" + id;
}
//选择组织机构
var selectOrganize = function () {
    parent.art.dialog.open("/Controls/Public/SelectOrganize?departmentId=" + $("#departmentId").val(), {
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
            $("#departmentSpan").text(node.name);
            $("#departmentId").val(node.id);
            getRoleList();
            getJobTitleList();
        }
    });
};

//选择用户
var selectUser = function () {
    parent.art.dialog.open("/Controls/Public/SelectHrmResource", {
        id: "showBoxDialog",
        title: "选择上级领导",
        //lock: true,
        width: 600,
        height: 550,
        init: function () {
            var iframe = this.iframe.contentWindow;
        },
        okVal: "确定",
        cancelVal: "取消",
        ok: function () {
            var iframe = this.iframe.contentWindow;
            var chk_realName_value = [];
            var chk_userId_value = [];
            $(iframe.document).find("input[name='_id[]']:checked").each(function () {
                chk_userId_value.push($(this).val());
                chk_realName_value.push($(this).attr("realname"));
            });
            if (chk_userId_value.length === 0) {
                parent.showNotice('error', '您至少需要选择一条信息', 2000);
                return false;
            }
            if (chk_userId_value.length > 1) {
                parent.showNotice('error', '您只能选择一条信息', 2000);
                return false;
            }
            $("#managerSpan").text(chk_realName_value.join(','));
            $("#managerId").val(chk_userId_value.join(','));
        }
    });
}

//只能输入整数
var checkSecurityLevel = function (e) {
    var num = $(e).val();
    if (num != "") {
        if (!num.match($.regexpCommon('numberInt'))) {
            $(e).val("");
        }
    }
}


//分配角色
var AllotRole = function (userId, realName) {
    art.dialog.open("/SysManage/BaseUser/AllotRole?userId=" + userId + "&realName=" + encodeURI(realName), {
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
            var userId = $(iframe.document).find("#userId").val();
            var parent = $(iframe.document).find("input[type='checkbox']:checked").each(function () {
                var index = $(this).val();
                roleIds.push(index);
            });

            $.ajax({
                url: "/SysManage/BaseUser/SaveAllotRole",
                type: "post",
                dataType: "json",
                data: "userId=" + userId + "&roleIds=" + roleIds.join(','),
                success: function (data) {
                    if (data.Status == "+OK") {
                        showNotice('success', data.Msg, 2000);
                    } else {
                        showNotice('error', data.Msg, 2000);
                    }
                }
            });
        }
    });
}


//删除角色
var confirmUserPassword = function (id) {
    art.dialog.confirm("您确认要用户密码 ?", function () {
        $.ajax({
           // url: "/SysManage/BaseUser/ConfirmUserPassword",
            type: "post",
            dataType: "json",
            data: "id=" + id,
            success: function (data) {
                //if (data.Status == "+OK") {
                //    showNotice('success', data.Msg, 2000);
                //    setTimeout(function () {
                //        window.location.reload(true);
                //    }, (2000));
                //} else {
                //    showNotice('error', data.Msg, 2000);
                //}
            }
        });
    }, function () {

    });
}