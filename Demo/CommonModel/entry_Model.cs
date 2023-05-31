namespace Demo.CommonModel
{
    public class entry_Model
    {
       public int? productEntryid { get; set; }
       public int? productBrandid { get; set; }
       public int? productModelid { get; set; }
       public int? productSericeid { get; set; }
       public int? productCategoryid { get; set; }
        public int? subCategoryId { get; set; }

        public string? productEntryName { get; set; }
        public string? productBrandName { get; set; }
        public string? productModelName { get; set; }
        public string? productSericenumber { get; set; }
        public string? productCategoryName { get; set; }

        public string? subCategoryname { get; set; }


        public int? price { get; set; }
        public bool? Available { get; set; }

    }
}
