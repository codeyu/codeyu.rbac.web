$(document).ready(function () {
    $("#SysDashboard").attr("class", "active");
    $("#AccountManage").addClass('active');

    //$("#updatePassword").submit(function () {
  
    //    $("#submit_btn").button("loading");
    //    var options = {
    //        url: "/InfoCenter/AccountManage/UpdatePassword",
    //        dataType: "json",
    //        cache: false,
    //        success: function (data) {
    //            if (data.Status == "+OK") {
    //                showNotice('success', data.Msg, 2000);
    //                setTimeout(function () {
    //                    window.location.href = "/Home/LoginUser";
    //                }, (2000));
    //            } else {
    //                showNotice('error', data.Msg, 2000);
    //            }
    //            setTimeout(function () {
    //                $("#submit_btn").button("complete");
    //            }, (2000));

    //        },
    //        beforeSubmit: function () {
    //            var c_newpassword = $("#newpassword").val();
    //            var c_comfirmpassword = $("#comfirmpassword").val();
    //            var c_oldpassword = $("#oldpassword").val();
    //            if (c_newpassword == "" || c_comfirmpassword == "" || c_oldpassword == "") {
    //                $('#alert-txt').text("所有的项都是必填的")
    //                $('.alert').show();
    //                $("#submit_btn").button("complete");
    //                return false;
    //            }
    //            if (c_newpassword == c_oldpassword) {
    //                $('#alert-txt').text("新密码和旧密码不能相同")
    //                $('.alert').show();
    //                $("#submit_btn").button("complete");
    //                return false;
    //            }
    //            if (c_comfirmpassword != c_newpassword) {
    //                $('#alert-txt').text("两次输入密码不一致")
    //                $('.alert').show();
    //                $("#submit_btn").button("complete");
    //                return false;
    //            }
    //        }
    //    };
    //    $(this).ajaxSubmit(options);
    //    return false;
    //});

    $("#updateProfile").submit(function () {
        $("#submit_btn").button("loading");
        var options = {
            url: "/InfoCenter/AccountManage/UpdateProfile",
            dataType: "json",
            cache: false,
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

            },
            beforeSubmit: function () {
                var realName = $("#RealName").val();
                var mobilePhone = $("#MobilePhone").val();
                var email = $("#Email").val();
                var gender = $('#Gender option:selected').text();
                if (realName == "" || mobilePhone == "" || email == "" || gender == "") {
                    $('#alert-txt').text("所有的项都是必填的")
                    $('.alert').show();
                    $("#submit_btn").button("complete");
                    return false;
                } else {
                    $('.alert').hide();
                    $("#submit_btn").button("complete");
                }
            }
        };
        $(this).ajaxSubmit(options);
        return false;
    });
})