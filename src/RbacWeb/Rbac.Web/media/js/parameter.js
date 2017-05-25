$(document).ready(function () {
    $("#SysManage").attr("class", "active");
    $("#SysDay").addClass('active');
    $.ajax({
        type: "POST",
        url: "/SysManage/BaseParameter/GetParameterList",
        dataType: "json",
        success: function (data) {
            $('#parameterTable tbody').html('');
            var html = '';
            var count = 0;
            $.each(data, function (index, comment) {
                html += '<tr>';
                html += '<td>';
                html += '<input type="hidden" name="ItemIndex" value="' + index.toString() + '"/>';
                html += '<input type="hidden" name="ParamId_' + index.toString() + '" id="ParamId_' + index.toString() + '" value="' + comment['Id'] + '"/>';
                if (comment['AllowUpdate'] == "1") {
                    html += '<input class="input"   value="' + comment['Code'] + '"  type="text" style="width:98%"  id="ParamCode_' + index.toString() + '" name=ParamCode_' + index.toString() + '" onchange=checkinput("ParamCode_' + index.toString() + '","ParamCode_' + index.toString() + '_Span") /><span id="ParamCode_' + index.toString() + '_Span"></span>';
                    html += '</td>';
                    html += '<td><input class="input"   value="' + comment['Value'] + '"  type="text" style="width:98%" id="ParamValue_' + index.toString() + '" name="ParamValue_' + index.toString() + '"  onchange=checkinput("ParamValue_' + index.toString() + '","ParamValue_' + index.toString() + '_Span") /><span id="ParamValue_' + index.toString() + '_Span"></span></td>';
                    html += '<td><input class="input" value="' + comment['Description'] + '"  type="text" style="width:98%" id="ParamDesc_' + index.toString() + '" name="ParamDesc_' + index.toString() + '"   /></td>';
                    html += '<td><select  style="width:100px" onchange=checkselect("AllowUpdate_' + index.toString() + '","AllowUpdate_' + index.toString() + '_Span")  id="AllowUpdate_' + index.toString() + '" name="AllowUpdate_' + index.toString() + '"  ><option value=""></option><option value="1" selected="selected">可以</option><option value="0">不可以</option></select><span id="AllowUpdate_' + index.toString() + '_Span"></span></td>';
                    html += '<td><input class="input" value="' + comment['SortOrder'] + '" onkeyup="checkSortCode(this)" oninput="checkSortCode(this)"  type="text" style="width:98%" id="ShowOrder_' + index.toString() + '" name="ShowOrder_' + index.toString() + '"   /></td>';
                    html += '<td><a murphybuttonname="BaseParameter.Delete"  href="javascript:deleteParameter(' + index.toString() + ');">删除</a></td>';
                } else {
                    html += '<input  disabled="disabled" class="input"   value="' + comment['Code'] + '"  type="text" style="width:98%"  id="ParamCode_' + index.toString() + '" name=ParamCode_' + index.toString() + '" onchange=checkinput("ParamCode_' + index.toString() + '","ParamCode_' + index.toString() + '_Span") /><span id="ParamCode_' + index.toString() + '_Span"></span>';
                    html += '</td>';
                    html += '<td><input disabled="disabled"  class="input"   value="' + comment['Value'] + '"  type="text" style="width:98%" id="ParamValue_' + index.toString() + '" name="ParamValue_' + index.toString() + '"  onchange=checkinput("ParamValue_' + index.toString() + '","ParamValue_' + index.toString() + '_Span") /><span id="ParamValue_' + index.toString() + '_Span"></span></td>';
                    html += '<td><input disabled="disabled"  class="input" value="' + comment['Description'] + '"  type="text" style="width:98%" id="ParamDesc_' + index.toString() + '" name="ParamDesc_' + index.toString() + '"   /></td>';
                    html += '<td><select disabled="disabled"  style="width:100px" onchange=checkselect("AllowUpdate_' + index.toString() + '","AllowUpdate_' + index.toString() + '_Span")  id="AllowUpdate_' + index.toString() + '" name="AllowUpdate_' + index.toString() + '"  ><option value=""></option><option value="1">可以</option><option value="0"  selected="selected">不可以</option></select><span id="AllowUpdate_' + index.toString() + '_Span"></span></td>';
                    html += '<td><input disabled="disabled"   class="input" value="' + comment['SortOrder'] + '"  type="text" style="width:98%" id="ShowOrder_' + index.toString() + '" name="ShowOrder_' + index.toString() + '"   /></td>';
                    html += '<td><a murphybuttonname="BaseParameter.Delete" href="javascript:void(0)">删除</a></td>';
                }

                html += '</tr>';
                count++;
            });
            $("#cur_number").text(count);
            $('#parameterTable tbody').html(html);
        }
    })
})
//只能输入整数
var checkSortCode = function (e) {
    var num = $(e).val();
    if (num != "") {
        if (!num.match($.regexpCommon('numberInt'))) {
            $(e).val("");
        }
    }
}

//创建参数
var createParameter = function () {
    var index = $("#parameterTable tbody tr").size() + 1;
    var tr = '<tr>';
    tr += '<td>';
    tr += '<input type="hidden" name="ItemIndex" value="' + index.toString() + '"/>';

    tr += '<input class="input"  type="text" style="width:98%"  id="ParamCode_' + index.toString() + '" name=ParamCode_' + index.toString() + '" onchange=checkinput("ParamCode_' + index.toString() + '","ParamCode_' + index.toString() + '_Span") /><span id="ParamCode_' + index.toString() + '_Span"><img src="/media/images/BacoError.gif"></span>';
    tr += '</td>';
    tr += '<td><input class="input" type="text" style="width:98%" id="ParamValue_' + index.toString() + '" name="ParamValue_' + index.toString() + '"  onchange=checkinput("ParamValue_' + index.toString() + '","ParamValue_' + index.toString() + '_Span") /><span id="ParamValue_' + index.toString() + '_Span"><img src="/media/images/BacoError.gif"></span></td>';
    tr += '<td><input class="input" type="text" style="width:98%" id="ParamDesc_' + index.toString() + '" name="ParamDesc_' + index.toString() + '"   /></td>';
    tr += '<td><select  style="width:80px" onchange=checkselect("AllowUpdate_' + index.toString() + '","AllowUpdate_' + index.toString() + '_Span")  id="AllowUpdate_' + index.toString() + '" name="AllowUpdate_' + index.toString() + '"  ><option value=""></option><option value="1">可以</option><option value="0">不可以</option></select><span id="AllowUpdate_' + index.toString() + '_Span"><img src="/media/images/BacoError.gif"></span></td>';
    tr += '<td><input class="input" type="text" style="width:98%" onkeyup="checkSortCode(this)" oninput="checkSortCode(this)"  id="ShowOrder_' + index.toString() + '" name="ShowOrder_' + index.toString() + '"   /></td>';
    tr += '<td><a href="javascript:deleteParameter(' + index.toString() + ');">删除</a></td>';
    tr += '</tr>';
    $("#parameterTable tbody").append(tr);
}

//删除参数
var deleteParameter = function (index) {
    $("#parameterTable tbody tr td input[type='hidden']").each(function () {
        //后端拼接的不能删除
        if ($(this).val() == index.toString()) {
            $(this).parent().parent().remove();
        }
    });
}

//保存参数
var saveParameter = function () {
    $("#submit_btn").button("loading");
    if ($("span>img").length > 0) {
        showNotice('error', '必要信息不完整', 2000);
        $("#submit_btn").button("complete");
        return;
    } 
    //var parameters = [];
    var parameter = "";
    var parameters = "";
    var parent = $("input[type='hidden'][name='ItemIndex']").each(function () {
        var index = $(this).val();
        var ParamCode = $('#ParamCode_' + index).val() || '';
        var ParamValue = $('#ParamValue_' + index).val() || '';
        var ParamDesc = $('#ParamDesc_' + index).val() || '';
        var AllowUpdate = $('#AllowUpdate_' + index).val() || '';
        var ShowOrder = $('#ShowOrder_' + index).val() || '';
        var ParamId = $('#ParamId_' + index).val() || '';
        //parameters.push(ParamCode + "|" + ParamValue + "|" + ParamDesc + "|" + AllowUpdate + "|" + ShowOrder + "|" + ParamId);
        parameter += ParamCode + "☻" + ParamValue + "☻" + ParamDesc + "☻" + AllowUpdate + "☻" + ShowOrder + "☻" + ParamId + "≌";
    });
    parameters += parameter;
    if (parameters.length <= 0) {
        showNotice('information', "请先创建系统参数", 2000);
        $("#submit_btn").button("complete");
    }
    //$.ajax({
    //    url: "/SysManage/BaseParameter/SaveParameter",
    //    type: "post",
    //    dataType: "json",
    //    data: "parameters=" + parameters.join(','),
    //    success: function (data) {
    //        if (data.Status == "+OK") {
    //            showNotice('success', data.Msg, 2000);
    //            //setTimeout(function () {
    //            //    window.location.reload(true);
    //            //}, (2000));
    //        } else {
    //            showNotice('error', data.Msg, 2000);
    //        }
    //        setTimeout(function () {
    //            $("#submit_btn").button("complete");
    //        }, (2000));
    //    }
    //});
    $.ajax({
        url: "/SysManage/BaseParameter/SaveParameter",
        type: "post",
        dataType: "json",
        data: "parameters=" + parameters,
        success: function (data) {
            if (data.Status == "+OK") {
                showNotice('success', data.Msg, 2000);
                //setTimeout(function () {
                //    window.location.reload(true);
                //}, (2000));
            } else {
                showNotice('error', data.Msg, 2000);
            }
            setTimeout(function () {
                $("#submit_btn").button("complete");
            }, (2000));
        }
    });
}
