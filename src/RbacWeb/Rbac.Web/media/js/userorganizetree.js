var zTreeObj;
var setting = {
    view: {
        fontCss: getFont,
        nameIsHTML: true
    },
    check: {
        enable: false
    },
    data: {
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
        url: "/SysManage/BaseUser/GetUserOrganizeTree",
        type: "post",
        dataType: "json",
        success: function (zNodes) {
            $.fn.zTree.init($("#organizeTree"), setting, zNodes);
            zTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
            zTreeObj.expandAll(true);
        }
    });
})

//全部收起
var packUpAll = function () {
    var organizeTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
    organizeTreeObj.expandAll(false);
}
//全部展开
var expandAll = function () {
    var organizeTreeObj = $.fn.zTree.getZTreeObj("organizeTree");
    organizeTreeObj.expandAll(true);
}