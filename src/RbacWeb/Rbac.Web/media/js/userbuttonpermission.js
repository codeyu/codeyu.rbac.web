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
        url: "/SysManage/BaseUser/GetUserButtonPermission?id=" + GetQuery("id"),
        type: "post",
        dataType: "json",
        success: function (zNodes) {
            $.fn.zTree.init($("#buttonTree"), setting, zNodes);
            zTreeObj = $.fn.zTree.getZTreeObj("buttonTree");
            zTreeObj.expandAll(true);
        }
    });
});

//操作按钮权限全部收起
var packUpAllButton = function () {
    var buttonTreeObj = $.fn.zTree.getZTreeObj("buttonTree");
    buttonTreeObj.expandAll(false);
}
//操作按钮权限全部展开
var expandAllButton = function () {
    var buttonTreeObj = $.fn.zTree.getZTreeObj("buttonTree");
    buttonTreeObj.expandAll(true);
}

//保存用户操作按钮
var saveUserButtonPermission = function (id) {
    $("#submit_btn").button("loading");
    var buttonCheckNodes = zTreeObj.getCheckedNodes(true);
    var accessValueArray = [];
    for (var i = 0; i < buttonCheckNodes.length; i++) {
        accessValueArray.push(buttonCheckNodes[i].id);
    }

    $.ajax({
        url: "/SysManage/BaseUser/UpdateUserButtonPermission?accessValueArray=" + accessValueArray.join(',') + "&id=" + id,
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