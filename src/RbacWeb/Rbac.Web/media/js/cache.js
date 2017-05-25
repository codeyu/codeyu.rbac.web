$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysDay").addClass('active');
    $("#selectUserInfoUser").bind("click", function () {
        selectUserInfoUser('UserInfoUserNameSpan', 'UserInfoUserIdList');
    })
    $("#selectDataPermissionUser").bind("click", function () {
        selectUserInfoUser('DataPermissionUserNameSpan', 'DataPermissionUserIdList');
    })
    $("#selectFieldPermissionUser").bind("click", function () {
        selectUserInfoUser('FieldPermissionUserNameSpan', 'FieldPermissionUserIdList');
    })
    $("#selectButtonPermissionUser").bind("click", function () {
        selectUserInfoUser('ButtonPermissionUserNameSpan', 'ButtonPermissionUserIdList');
    })
    $("#selectModulePermissionUser").bind("click", function () {
        selectUserInfoUser('ModulePermissionUserNameSpan', 'ModulePermissionUserIdList');
    })

})

//选择用户
var selectUserInfoUser = function (userNameSpan, userIdList) {
    parent.art.dialog.open("/Controls/Public/SelectHrmResource", {
        id: "showBoxDialog",
        title: "选择用户",
        //lock: true,
        width: 650,
        height: 500,
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
            $("#" + userNameSpan + "").text(chk_realName_value.join(','));
            $("#" + userIdList + "").val(chk_userId_value.join(','));
        }
    });
}

//更新缓存
var updateCache = function (cacheKey, userIdList) {
    if ($("#" + userIdList + "").val() == "") {
        showNotice('error', '您至少需要选择一个用户', 2000);
        return;
    }
    $.ajax({
        url: "/SysManage/BaseCache/Update",
        type: "post",
        dataType: "json",
        data: "userIdList=" + $("#" + userIdList + "").val() + "&cacheKey=" + cacheKey,
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
}