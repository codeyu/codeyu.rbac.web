$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysGenerator").addClass('active');
})
//生成代码
var createCode = function () {
    var obj = parent.document.getElementById("leftFrame").contentWindow.zTreeObj.getSelectedNodes();
    if (obj.length == 0) {
        parent.showNotice('error', "请先选择数据表节点", 2000);
    } else {
        if (obj[0].type == 2) {
            parent.showNotice('error', "数据库节点不可选", 2000);
        }
        else if (obj[0].type == 1) {
            parent.showNotice('error', "根节点不可选", 2000);
        } else {
            parent.$("#rightFrame").attr("src", "/Generator/DbServer/CreateCode?dataBaseName=" + obj[0].pname + "&tableName=" + obj[0].name + "");
        }
    }
}