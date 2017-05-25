var zTreeObj;
var setting = {
    view: {
        fontCss: getFont,
        nameIsHTML: true
    },
    check: {
        enable: true
    },
    data: {
        key: {
            checked: "isChecked"
        },
        simpleData: {
            enable: true
        }
    }
};
function getFont(treeId, node) {
    return node.font ? node.font : {};
}

$(document).ready(function () {
    $.ajax({
        url: "/SysManage/BaseUser/GetUserModulePermission?id=" + GetQuery("id"),
        type: "post",
        dataType: "json",
        success: function (zNodes) {
            $.fn.zTree.init($("#moduleTree"), setting, zNodes);
            zTreeObj = $.fn.zTree.getZTreeObj("moduleTree");
            zTreeObj.expandAll(true);
        }
    });
});

//功能模块权限全部收起
var packUpAllModule = function () {
    var moduleTreeObj = $.fn.zTree.getZTreeObj("moduleTree");
    moduleTreeObj.expandAll(false);
}
//功能模块权限全部展开
var expandAllModule = function () {
    var moduleTreeObj = $.fn.zTree.getZTreeObj("moduleTree");
    moduleTreeObj.expandAll(true);
}

//保存用户模块权限
var saveUserModulePermission = function (id) {
    $("#submit_btn").button("loading");
    var moduleCheckNodes = zTreeObj.getCheckedNodes(true);
    var accessValueArray = [];
    for (var i = 0; i < moduleCheckNodes.length; i++) {
        accessValueArray.push(moduleCheckNodes[i].id);
    }

    $.ajax({
        url: "/SysManage/BaseUser/UpdateUserModulePermission?accessValueArray=" + accessValueArray.join(',') + "&id=" + id,
        type: "post",
        dataType: "json",
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
        }
    });
}