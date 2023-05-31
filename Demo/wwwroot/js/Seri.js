


//-------------------------for Model w.r.t Brand----------------------------

$(function ()
{
    $("#BrandDropdown").on("change", function () {
       
        const brandid = $(this).val();
      

        $("#ModelDropdown").empty();
        $("#ModelDropdown").append(`<option selected disable>Select MODEL</option>`);
        $.ajax( 
            { 
                url: `/api/Proseri/GetModelDropdown?brandid=${brandid}`
                
            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#ModelDropdown").append(`<option value=${item.modelid}>${item.modelname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    }); 
})



//-------------------------for Brand w.r.t sub-category----------------------------


$(function ()
{
    $("#SubCategoryDropdown").on("change", function () {

        const subCategoryId = $(this).val();

        SubCatgInfo(subCategoryId);

        $("#BrandDropdown").empty();
        $("#BrandDropdown").append(`<option selected disable>Select BRAND</option>`);
        $.ajax(
            {
                url: `/api/Proseri/GetBrandDropdown?subCategoryId=${subCategoryId}`

            }).done(function (data) { 
                $.each(data, function (i, item) {
                    $("#BrandDropdown").append(`<option value=${item.brandid}>${item.brandname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
})
   //------------------------for hide model and serise button-------------------------------------
function SubCatgInfo(id)
{

    $.ajax(
        {
            url: `/api/Proseri/GetSubcategoryInfo?id=${id}`

        }).done(function (data) {
            
            if (data.isModelirserise == false) {
                $("#DivModelId").css('display', 'none')
                $("#DivSericeId").css('display', 'none')
            } else {
                $("#DivModelId").css('display', 'block')
                $("#DivSericeId").css('display', 'block')
            }
            
        }).fail(function (jqXHR, textStatus, error) {
            console.error(error);
        });

}

//-------------------------for sub-category w.r.t category----------------------------


$(function ()
{
    $("#CategoryDropdown").on("change", function () {

        const categoryid = $(this).val();


        $("#SubCategoryDropdown").empty();
        $("#SubCategoryDropdown").append(`<option selected disable>Select Sub-Category</option>`);
        $.ajax(
            {
                url: `/api/Proseri/GetSubCategoryDropdown?categoryid=${categoryid}`

            }).done(function (data) {
                $.each(data, function (i, item) {
                    $("#SubCategoryDropdown").append(`<option value=${item.subCategoryId}>${item.subCategoryname}</option>`);
                });
            }).fail(function (jqXHR, textStatus, error) {
                console.error(error);
            });


    });
})
