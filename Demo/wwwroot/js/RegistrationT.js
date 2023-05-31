$(function () {
    $("#countryDropdown").on("change", function () {
        const countryID = $(this).val();

        $("#stateDropdown").empty();
        $("#stateDropdown").append(`<option selected disable>Select State</option>`);
        $.ajax(
            {
                url: `/api/RegistrationT/stateDropdown?countryID=${countryID}`
                /* url: "/api/RegistrationT/stateDropdown?countryID=" + countryID*/
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#stateDropdown").append(`<option value=${item.id}>${item.fullname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
}) 



$(function () {
    $("#stateDropdown").on("change", function () {
        const StateID = $(this).val();

        $("#cityDropdown").empty();
        $("#cityDropdown").append(`<option selected disable>Select State</option>`);
        $.ajax(
            {
                url: `/api/RegistrationT/cityDropdown?StateID=${StateID}`
                /* url: "/api/RegistrationT/cityDropdown?StateID=" + StateID*/
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#cityDropdown").append(`<option value=${item.id}>${item.fullname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
})


