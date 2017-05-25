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
        url: "/SysManage/BaseRole/GetRoleDataPermission?id=" + GetQuery("id"),
        type: "post",
        dataType: "json",
        success: function (zNodes) {
            $.fn.zTree.init($("#organizeTree"), setting, zNodes);
            zTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
            zTreeObj.expandAll(true);
        }
    });
});

var showOrganize = function (status) {
    if (status == 1) {
        $("#organizeDiv").show();
    } else {
        $("#organizeDiv").hide();
    }
}

//明细数据权限全部收起
var packUpAllOrganize = function () {
    var organizeTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
    organizeTreeObj.expandAll(false);
}
//明细数据权限全部展开
var expandAllOrganize = function () {
    var organizeTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
    organizeTreeObj.expandAll(true);
}

//保存用户明细数据权限
var SaveUserDataPermission = function (id) {
    $("#submit_btn").button("loading");
    var organizeCheckNodes = zTreeObj.getCheckedNodes(true);
    var accessValueArray = [];
    for (var i = 0; i < organizeCheckNodes.length; i++) {
        accessValueArray.push(organizeCheckNodes[i].id);
    }
    var constraint = $("input[name='optionsRadios']:checked").val();

    $.ajax({
        url: "/SysManage/BaseRole/UpdateRoleDataPermission?accessValueArray=" + accessValueArray.join(',') + "&id=" + id + "&constraint=" + constraint,
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