var $fromWhere = $('#fromWhere');
var $toWhere = $('#toWhere');

var $hdnCookieDate = $('#hdnCookieDate');

$fromWhere.autocomplete({
    minLength: 1,
    autoFocus: true,
    delay: 500,
    source: function (request, response) {
        $.ajax({
            cache: false,
            url: '/Home/GetBusLocations/',
            data: { term: request.term },
            dataType: "json",
            type: "POST",
            success: function (data) {
                response($.map(data.data, function (item) {
                    return {
                        id: item.id,
                        value: item.name
                    }
                }));
            }
        })
    },
    select: function (event, ui) {
        $fromWhere.attr("data-id", ui.item.id);
    },
});

$toWhere.autocomplete({
    minLength: 1,
    autoFocus: true,
    delay: 500,
    source: function (request, response) {
        $.ajax({
            cache: false,
            url: '/Home/GetBusLocations/',
            data: { term: request.term },
            dataType: "json",
            type: "POST",
            success: function (data) {
                response($.map(data.data, function (item) {
                    return {
                        id: item.id,
                        value: item.name
                    }
                }));
            }
        })
    },
    select: function (event, ui) {
        $toWhere.attr("data-id", ui.item.id);
    },
});

var $datePicker = $("#datePicker");

$datePicker.datepicker({
    dateFormat: "d MM yy DD"
});

var cookieDate = $hdnCookieDate.val();
if (cookieDate == "") {
    $datePicker.datepicker('setDate', '1');
}
else {
    $datePicker.datepicker("setDate", cookieDate);
}


SetToday = function () {
    $datePicker.datepicker('setDate', '0');
}

SetTomorrow = function () {
    $datePicker.datepicker('setDate', '1');
}

ChangeLocation = function () {
    var fromWhereVal = $fromWhere.val();
    var fromWhereId = $fromWhere.attr("data-id");

    var toWhereVal = $toWhere.val();
    var toWhereId = $toWhere.attr("data-id");

    $fromWhere.val(toWhereVal);
    $fromWhere.attr("data-id", toWhereId);

    $toWhere.val(fromWhereVal);
    $toWhere.attr("data-id", fromWhereId);
}

FindTicket = function () {
    var msgList = FindTicketValidation();
    if (msgList.length > 0) {
        MessageFunc("Hata", msgList, "danger");
        return;
    }

    var fromWhere = $fromWhere.val();
    var toWhere = $toWhere.val();

    var fromWhereId = $fromWhere.attr("data-id");
    var toWhereId = $toWhere.attr("data-id");
    var dateVal = $datePicker.datepicker("getDate");

    var date = $.datepicker.formatDate("dd/mm/yy", dateVal);

    window.location.href = "Home/FindJourney?fromWhereId=" + fromWhereId + "&toWhereId=" + toWhereId + "&date=" + date + "&fromWhere=" + fromWhere + "&toWhere=" + toWhere;
}

FindTicketValidation = function () {
    var messageList = [];

    var fromWhereId = $fromWhere.attr("data-id");
    var toWhereId = $toWhere.attr("data-id");

    if (fromWhereId == toWhereId)
        messageList.push("Hem çıkış hem de varış noktası olarak aynı konum seçilemez.");

    var datePickerVal = $datePicker.datepicker("getDate");

    var today = new Date();
    today.setHours(0, 0, 0, 0);

    if (datePickerVal < today)
        messageList.push("Hareket tarihi için minimum geçerlilik tarihi Bugün'dür.");

    return messageList;
}

MessageFunc = function (title, textList, type) {
    var content = "<h4>" + title + "</h4>";

    for (var i = 0; i < textList.length; i++) {
        content += "<p>" + textList[i] + "</p>"
    }

    $.showNotification({
        body: content,
        type: type,
        duration: 400000,
        maxWidth: "320px",
    })
};