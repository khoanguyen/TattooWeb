var handleBootstrapMaxlength = function () {

    $('.maxlengthCheck').maxlength({
        limitReachedClass: "label label-danger",
        alwaysShow: true
    });

    $('input:text').maxlength({
        limitReachedClass: "label label-danger",
        alwaysShow: true
    });
};

var handleDatePickers = function () {

    if (jQuery().datepicker) {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            autoclose: true
        });
    }
};

jQuery(document).ready(function () {
    App.init(); // initlayout and core plugins

    if ($('span.messageBox').text() == '') {
        $('div.messageBox').addClass('hideMessage');
    } else if ($('div.messageBox') != undefined) {
        $('div.messageBox').removeClass('hideMessage');
    }
    handleBootstrapMaxlength();
    handleDatePickers();
});