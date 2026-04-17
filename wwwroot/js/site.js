$(function () {

    let selectedDate = $('#ActiveDepartureDate').val();

    $('#ActiveDepartureDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        startDate: selectedDate
            ? moment(selectedDate)
            : moment().add(1, 'days'),
        locale: {
            format: 'YYYY-MM-DD'
        }
    });

});
$("#FlightCode, #Date").on("change blur", function () {
    $("#FlightCode").valid();
    $("#Date").valid();
});