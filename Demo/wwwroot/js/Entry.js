




//-------------------------for Brand---------------------------------


$(function () {
    $("#BrandDropdown").on("change", function () {

        const brandid = $(this).val(); 


        $("#ModelDropdown").empty();
        $("#ModelDropdown").append(`<option selected disable>Select Model</option>`);
        $.ajax(
            {
                url: `/api/Entry/GetModelDropdown?brandid=${brandid}`

            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#ModelDropdown").append(`<option value=${item.modelid}>${item.modelname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
})


//-------------------------for Model----------------------------


$(function () {
    $("#ModelDropdown").on("change", function () {

        const modelid = $(this).val();


        $("#SeriseDropdown").empty();
        $("#SeriseDropdown").append(`<option selected disable>Select Serice</option>`);
        $.ajax(
            {
                url: `/api/Entry/GetSericeDropdown?modelid=${modelid}`

            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#SeriseDropdown").append(`<option value=${item.seriid}>${item.seirnumber}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });
    });
})


