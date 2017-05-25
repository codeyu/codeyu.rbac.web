$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysDay").addClass('active');
});

var showFile = function (path) {
    art.dialog.open("/SysManage/BaseException/FileText?filePath=" + path, {
        id: "showBoxDialog",
        title: "查看日志明细",
        //lock: true,
        width: 600,
        height: 550,
        init: function () {
            var iframe = this.iframe.contentWindow;
        },
        okVal: "确定",
        cancelVal: "取消",
        ok: function () {
        }
    });
}