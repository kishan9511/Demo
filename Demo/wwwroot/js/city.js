$(function () {
    $("#countryDropdown").on("change", function () {
        const countryID = $(this).val();

        $("#stateDropdown").empty();
        $("#stateDropdown").append(`<option selected disable>Select State</option>`);
        $.ajax(
            {
                url: `/api/city/GetstateDropdown?countryID=${countryID}`
                /* url: "/api/city/GetstateDropdown?countryID=" + countryID*/
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#stateDropdown").append(`<option value=${item.id}>${item.fullname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
})

