const categories = document.querySelectorAll("#productCategory li");
const subcategories = document.querySelector("#subcategoryList");

categories.forEach((category) => { 
    category.addEventListener("click", async () => {
        const categoryId = category.dataset.categoryid;

        try {
            const data = await fetch(`/api/UI/GetProductSubcategories?categoryId=${categoryId}`).then(res => res.json());


            if (data.length > 0) {
                subcategories.innerHTML = '';

                for (const item of data) {
                    subcategories.insertAdjacentHTML("beforeend", `

                         <div class="col-lg-4 col-md-6 text-center">
                             <div class="single-product-item">
                                 <div class="product-image">
                                     <a asp-page="singleProduct"><img src="/images/${item.icon}" alt=""></a>
                                 </div>

                                 <h3>${item.name}</h3>
                                 <a href="cart" class="cart-btn"><i class="fas fa-shopping-cart"></i>choose subcategory</a>
                             </div>
                           
                         </div>`);
                }



            }


        } catch (e) {
            console.error(e);
        }

    })
});