$(function ()
{
    $("#countryDropdown").on("change", function () {
        const countryID = $(this).val();

        $("#stateDropdown").empty();
        $("#stateDropdown").append(`<option selected disable>Select State</option>`);
        $.ajax(
            {
                url:`/api/AdminReg/GetStateDropdown?countryID=${countryID}`
               /* url: "/api/Admin/GetStateDropdown?countryID=" + countryID*/
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#stateDropdown").append(`<option value=${item.id}>${item.fullname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
}) 



$(function ()
{
        $("#stateDropdown").on("change", function()

        {
            const stateID = $(this).val();
            $("#cityDropdown").empty();
            $("#cityDropdown").append(`<option selected disable>Select City</option>`);
            $.ajax({
                url: `/api/AdminReg/GetCityDropdown?stateID=${stateID}`
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#cityDropdown").append(`<option value = ${item.id}>${item.fullname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });

        });

})

