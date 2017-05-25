$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysDay").addClass('active');
});

var showDetails = function (id) {
    art.dialog.open("/SysManage/BaseLog/Details?id=" + id, {
        id: "showBoxDialog",
        title: "操作日志明细",
        //lock: true,
        width: 700,
        height: 600,
        okVal: "确定",
        cancelVal: "取消",
        ok: true
    });
    return false;
}