$(document).ready(function () {
    $("#captcha").click(function () {
        resetCaptcha();
    });
    $("#loginForm").submit(function () {
        var options = {
            url: "/Home/LoginUser",
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.Status === "+OK") {
                    location.replace(data.BackUrl);
                }
                else {
                    resetCaptcha();
                    $('#alert-txt').text(data.Msg)
                    $('.alert').show();
                }
            }
        };
        $(this).ajaxSubmit(options);
        return false;
    });

    $(document).bind("contextmenu", function () { return false; });
    $(document).bind("selectstart", function () { return false; });
  
   
})

function resetCaptcha() {
    if ($('#captcha').length > 0) {
        $("#captcha").attr('src', '/Home/ValidateCode?' + Math.round(Math.random() * 10000));
    }
}