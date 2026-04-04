$(function () {

    let selectedDate = $('#ActiveDepartureDate').val();

    $('#ActiveDepartureDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),

        // ✅ Use model value if exists, else tomorrow
        startDate: selectedDate
            ? moment(selectedDate)
            : moment().add(1, 'days'),

        locale: {
            format: 'YYYY-MM-DD'
        }
    });

});