'use strict'

$(function () {
    $(".pastpicker").datepicker({
        format: "mm/dd/yyyy",
        orientation: "auto bottom",
        autoclose: true,
        startDate: '01/01/1900',
        endDate: 'today'
    });

    $(".datepicker").datepicker({
        format: "mm/dd/yyyy",
        orientation: "auto bottom",
        autoclose: true
    });
});