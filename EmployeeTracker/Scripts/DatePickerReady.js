if (!Modernizr.inputtypes.date) {
    $(function () {
        // initialize datepicker and set it to display the selected date
        $(".datefield").datepicker("setDate", $(this).val());
    })
}