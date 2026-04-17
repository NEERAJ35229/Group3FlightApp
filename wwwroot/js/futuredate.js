jQuery.validator.addMethod("futuredate", function (value, element, params) {
    if (!value) return true;

    let selectedDate = new Date(value);
    let today = new Date();
    today.setHours(0, 0, 0, 0);

    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + parseInt(params));

    return selectedDate > today && selectedDate <= maxDate;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("futuredate", "years");