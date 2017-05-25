$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/SysManage/BaseItem/GetItemList",
        dataType: "json",
        success: function (data) {
            $('#itemTable tbody').html('');
            var html = '';
            var count = 0;
            $.each(data, function (index, comment) {
                html += '<tr>';
                html += '<td>';
                html += '<input type="hidden" name="ItemId_' + index.toString() + '" id="ItemId_' + index.toString() + '" value="' + comment['Id'] + '"/>';
                html += '<input type="hidden" name="ItemIndex" value="' + index.toString() + '"/>';
                html += '<input class="input"  type="text" style="width:98%"  id="ItemCode_' + index.toString() + '" name=ItemCode_' + index.toString() + '" value="' + comment['Code'] + '"  onchange=checkinput("ItemCode_' + index.toString() + '","ItemCode_' + index.toString() + '_Span") /><span id="ItemCode_' + index.toString() + '_Span"></span>';
                html += '</td>';
                html += '<td><input class="input" type="text" style="width:98%" id="ItemValue_' + index.toString() + '" name="ItemValue_' + index.toString() + '" value="' + comment['Name'] + '"   onchange=checkinput("ItemValue_' + index.toString() + '","ItemValue_' + index.toString() + '_Span") /><span id="ItemValue_' + index.toString() + '_Span"></span></td>';
                html += '<td><input class="input" type="text" style="width:98%"  value="' + comment['Description'] + '"   id="ItemDesc_' + index.toString() + '" name="ItemDesc_' + index.toString() + '"   /></td>';
                html += '<td><input class="input" type="text"  style="width:98%" onkeyup="checkSortCode(this)" oninput="checkSortCode(this)"  value="' + comment['SortCode'] + '"   id="ShowOrder_' + index.toString() + '" name="ShowOrder_' + index.toString() + '"   /></td>';
                html += '<td><a href="javascript:deleteItem(' + index.toString() + ');">删除</a></td>';
                html += '</tr>';
                count++;
            });
            $("#cur_number").text(count);
            $('#itemTable tbody').html(html);
        }
    })
})
//新增字典类别
var createItem = function () {
    var index = $("#itemTable tbody tr").size() + 1;
    var tr = '<tr>';
    tr += '<td>';
    tr += '<input type="hidden" name="ItemIndex" value="' + index.toString() + '"/>';
    tr += '<input class="input"  type="text" style="width:80%"  id="ItemCode_' + index.toString() + '" name=ItemCode_' + index.toString() + '" onchange=checkinput("ItemCode_' + index.toString() + '","ItemCode_' + index.toString() + '_Span") /><span id="ItemCode_' + index.toString() + '_Span"><img src="/media/images/BacoError.gif"></span>';
    tr += '</td>';
    tr += '<td><input class="input" type="text" style="width:80%" id="ItemValue_' + index.toString() + '" name="ItemValue_' + index.toString() + '"  onchange=checkinput("ItemValue_' + index.toString() + '","ItemValue_' + index.toString() + '_Span") /><span id="ItemValue_' + index.toString() + '_Span"><img src="/media/images/BacoError.gif"></span></td>';
    tr += '<td><input class="input" type="text" style="width:98%" id="ItemDesc_' + index.toString() + '" name="ItemDesc_' + index.toString() + '"   /></td>';
    tr += '<td><input class="input" type="text" onkeyup="checkSortCode(this)" oninput="checkSortCode(this)"    style="width:98%" id="ShowOrder_' + index.toString() + '" name="ShowOrder_' + index.toString() + '"   /></td>';
    tr += '<td><a href="javascript:deleteItem(' + index.toString() + ');">删除</a></td>';
    tr += '</tr>';
    $("#itemTable tbody").append(tr);

}

//删除字典类别
var deleteItem = function (index) {
    $("#itemTable tbody tr td input[type='hidden']").each(function () {
        if ($(this).val() == index.toString()) {
            $(this).parent().parent().remove();
        }
    });
}

//保存字典类别
var saveItem = function () {
    $("#submit_btn").button("loading");
    if ($("span>img").length > 0) {
        showNotice('error', '必要信息不完整', 2000);
        $("#submit_btn").button("complete");
        return false;
    }

    var items = [];
    var parent = $("input[type='hidden'][name='ItemIndex']").each(function () {
        var index = $(this).val();
        var ItemCode = $('#ItemCode_' + index).val() || '';
        var ItemValue = $('#ItemValue_' + index).val() || '';
        var ItemDesc = $('#ItemDesc_' + index).val() || '';
        var ShowOrder = $('#ShowOrder_' + index).val() || '';
        var ItemId = $('#ItemId_' + index).val() || '';
        items.push(ItemCode + "|" + ItemValue + "|" + ItemDesc + "|" + ShowOrder + "|" + ItemId);
    });

    $.ajax({
        url: "/SysManage/BaseItem/SaveItem",
        type: "post",
        dataType: "json",
        data: "items=" + items.join(','),
        success: function (data) {
            if (data.Status == "+OK") {
                showNotice('success', data.Msg, 2000);
                top.parent.leftFrame.window.location.reload(true);
            } else {
                showNotice('error', data.Msg, 2000);
                $("#submit_btn").button("complete");
            }
            setTimeout(function () {
                $("#submit_btn").button("complete");
            }, (2000));
        }
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