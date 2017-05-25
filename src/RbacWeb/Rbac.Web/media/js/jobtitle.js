$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysBasic").addClass('active');


    $("#Name").blur(function () {
        $.post("/SysManage/BaseJobTitle/CheckJobTitleName", { name: $("#Name").val(), jobTitleId: $("#JobTitleId").val() }, function (data, status) {
            var dataJson = eval('(' + data + ')');
            if (dataJson.Status == "+ERROR") {
                $('#alert-txt').text(dataJson.Msg)
                $('.alert').show();
                $("#NameSpan").html("<img src='/media/images/BacoError.gif' align=absMiddle>");
            } else {
                $('.alert').hide();
            }
        })
    })

    $("#createForm").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/SysManage/BaseJobTitle/Create",
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
            url: "/SysManage/BaseJobTitle/Update",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status == "+OK") {
                    parent.showNotice('success', data.Msg, 2000);
                    setTimeout(function () {
                        window.location.href = "/SysManage/BaseJobTitle/JobTitleList?departmentId=" + $("#departmentId").val();
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

var backTrack = function () {
    window.location.href = "/SysManage/BaseJobTitle/JobTitleList?departmentId=" + $("#departmentId").val();
}

//创建岗位
var createJobTitle = function () {
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
            parent.$("#rightFrame").attr("src", "/SysManage/BaseJobTitle/Create?departmentId=" + obj[0].id + "");
        }
    }
};

//删除
var deleteJobTitle = function (id) {
    art.dialog.confirm("您确认要删除它吗 ?", function () {

        $.ajax({
            url: "/SysManage/BaseJobTitle/Delete",
            type: "post",
            dataType: "json",
            data: "id=" + id,
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
}

//只能输入整数
var checkSortCode = function (e) {
    var num = $(e).val();
    if (num != "") {
        if (!num.match($.regexpCommon('numberInt'))) {
            $(e).val("");
        }
    }
}