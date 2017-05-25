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
        url: "/SysManage/BaseItem/GetItemTree",
        type: "post",
        dataType: "json",
        success: function (zNodes) {
            $.fn.zTree.init($("#itemTree"), setting, zNodes);
            zTreeObj = $.fn.zTree.getZTreeObj("itemTree");
            zTreeObj.expandAll(true);
        }
    });
})

//全部收起
var packUpAll = function () {
    var itemTreeObj = $.fn.zTree.getZTreeObj("itemTree");
    itemTreeObj.expandAll(false);
}
//全部展开
var expandAll = function () {
    var itemTreeObj = $.fn.zTree.getZTreeObj("itemTree");
    itemTreeObj.expandAll(true);
}