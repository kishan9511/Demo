using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models;

public partial class DemoProjectContext : DbContext
{
    public DemoProjectContext()
    {
    }

    public DemoProjectContext(DbContextOptions<DemoProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<AboutBanner> AboutBanners { get; set; }

    public virtual DbSet<AboutService> AboutServices { get; set; }

    public virtual DbSet<AdminLogin> AdminLogins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<Frui> Fruis { get; set; }

    public virtual DbSet<GeneralNews> GeneralNews { get; set; }

    public virtual DbSet<IndexHome> IndexHomes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<OurTeam> OurTeams { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductBrand> ProductBrands { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductEntry> ProductEntries { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductSerise> ProductSerises { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<ProductTitle> ProductTitles { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M09BAAL;Database=DemoProject;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__about__3214EC070C37CA6C");

            entity.ToTable("about");

            entity.Property(e => e.AboutTitle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ServiceImage)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ServiceTitle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ShopAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.WeekdayHr)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WeekendHr)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("weekendHr");
        });

        modelBuilder.Entity<AboutBanner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aboutBan__3214EC07A1401A01");

            entity.ToTable("aboutBanner");

            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MainTitle)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.SubTitle)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.SubTitleDiscount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubTitleValue)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(900)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<AboutService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aboutSer__3214EC07B7E85A10");

            entity.ToTable("aboutService");

            entity.Property(e => e.Details)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Isdeleted).HasColumnName("ISdeleted");
            entity.Property(e => e.Logo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AdminLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminLog__3214EC27F249314B");

            entity.ToTable("AdminLogin");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart__3214EC070C990770");

            entity.ToTable("cart");

            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Price)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Quantity)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Total)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC2724894103");

            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__City__StateID__3D5E1FD2");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Colorid).HasName("PK__color__70A94BA5D274FD08");

            entity.ToTable("color");

            entity.Property(e => e.Colorid).HasColumnName("colorid");
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("colorName");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contactu__3214EC0798F06D70");

            entity.ToTable("contactus");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC27329CBC2C");

            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Deal__3214EC0772FF473D");

            entity.ToTable("Deal");

            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Percentage)
                .HasMaxLength(90)
                .IsUnicode(false)
                .HasColumnName("percentage");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Frui>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__frui__3214EC0701A9E9B0");

            entity.ToTable("frui");

            entity.Property(e => e.Details)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeneralNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__generalN__3214EC073BBD16A9");

            entity.ToTable("generalNews");

            entity.Property(e => e.Author)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("details");
            entity.Property(e => e.HealLine)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IndexHome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__indexHom__3214EC07B3BC8CFD");

            entity.ToTable("indexHome");

            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("details");
            entity.Property(e => e.Logo)
                .HasMaxLength(900)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.SubTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("subTitle");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__login__3214EC276DC14E18");

            entity.ToTable("login");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__News__3214EC079ACEE585");

            entity.Property(e => e.Details)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OurTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ourTeam__3214EC07C7183117");

            entity.ToTable("ourTeam");

            entity.Property(e => e.ShopOwnerSlidshow)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SlidShowLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("slidShowLogo");
            entity.Property(e => e.SlidshowWithName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("slidshowWithName");
            entity.Property(e => e.TeamMamberName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("teamMamberName");
            entity.Property(e => e.TeamMamberProfile)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("teamMamberProfile");
            entity.Property(e => e.TeamMemberLogo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("teamMemberLogo");
            entity.Property(e => e.TeamTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("teamTitle");
            entity.Property(e => e.TeanDetails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("teanDetails");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3214EC076B41A3F3");

            entity.ToTable("product");

            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.ProdecuName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("prodecuName");
            entity.Property(e => e.ProductCart)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("productCart");
            entity.Property(e => e.ProductDetails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("productDetails");
            entity.Property(e => e.ProductPrice)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productPrice");
        });

        modelBuilder.Entity<ProductBrand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__ProductB__DAD4F05E46226B6D");

            entity.ToTable("ProductBrand");

            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.ProductBrands)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__ProductBr__Produ__06CD04F7");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ProductBrands)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__ProductBr__SubCa__07C12930");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC27D1DAC68A");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Productcolorid).HasName("PK__ProductC__1AFE835409D1F4DE");

            entity.ToTable("ProductColor");

            entity.Property(e => e.Colorid).HasColumnName("colorid");
            entity.Property(e => e.ProductEntryid).HasColumnName("productEntryid");

            entity.HasOne(d => d.Color).WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.Colorid)
                .HasConstraintName("FK__ProductCo__color__656C112C");

            entity.HasOne(d => d.ProductEntry).WithMany(p => p.ProductColors)
                .HasForeignKey(d => d.ProductEntryid)
                .HasConstraintName("FK__ProductCo__produ__6477ECF3");
        });

        modelBuilder.Entity<ProductEntry>(entity =>
        {
            entity.HasKey(e => e.ProductEntryid).HasName("PK__ProductE__6033052CD3D7892A");

            entity.ToTable("ProductEntry");

            entity.Property(e => e.ProductEntryid).HasColumnName("productEntryid");
            entity.Property(e => e.ActualPrice).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.Descriptions).IsUnicode(false);
            entity.Property(e => e.Discount)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

            entity.HasOne(d => d.Brand).WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__ProductEn__Brand__5AEE82B9");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__ProductEntry__ID__59FA5E80");

            entity.HasOne(d => d.Model).WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.Modelid)
                .HasConstraintName("FK__ProductEn__Model__5BE2A6F2");

            entity.HasOne(d => d.Serise).WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.Seriseid)
                .HasConstraintName("FK__ProductEn__Seris__5CD6CB2B");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ProductEntries)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__ProductEn__SubCa__05D8E0BE");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageid).HasName("PK__ProductI__AD7F0DA00F3A4D61");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageid).HasColumnName("productImageid");
            entity.Property(e => e.ImageName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProductEntryid).HasColumnName("productEntryid");

            entity.HasOne(d => d.ProductEntry).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductEntryid)
                .HasConstraintName("FK__ProductIm__produ__68487DD7");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.Modelid).HasName("PK__ProductM__E8D4A544306F01BB");

            entity.ToTable("ProductModel");

            entity.Property(e => e.ModelName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

            entity.HasOne(d => d.Brand).WithMany(p => p.ProductModels)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__ProductMo__Brand__5070F446");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ProductModels)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__ProductMo__SubCa__03F0984C");
        });

        modelBuilder.Entity<ProductSerise>(entity =>
        {
            entity.HasKey(e => e.Seriseid).HasName("PK__ProductS__01AE89629260E13A");

            entity.ToTable("ProductSerise");

            entity.Property(e => e.BrandId).HasColumnName("brandID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.SeriseNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

            entity.HasOne(d => d.Brand).WithMany(p => p.ProductSerises)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__ProductSe__brand__09A971A2");

            entity.HasOne(d => d.Model).WithMany(p => p.ProductSerises)
                .HasForeignKey(d => d.Modelid)
                .HasConstraintName("FK__ProductSe__Model__534D60F1");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.ProductSerises)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__ProductSe__Produ__08B54D69");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ProductSerises)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__ProductSe__SubCa__04E4BC85");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.ProductSizeid).HasName("PK__ProductS__9DAEF9697E299736");

            entity.ToTable("ProductSize");

            entity.Property(e => e.ProductEntryid).HasColumnName("productEntryid");
            entity.Property(e => e.Sizeid).HasColumnName("sizeid");

            entity.HasOne(d => d.ProductEntry).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.ProductEntryid)
                .HasConstraintName("FK__ProductSi__produ__5FB337D6");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.Sizeid)
                .HasConstraintName("FK__ProductSi__sizei__60A75C0F");
        });

        modelBuilder.Entity<ProductTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__productT__3214EC07CD9E5B06");

            entity.ToTable("productTitle");

            entity.Property(e => e.ProductDetails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("productDetails");
            entity.Property(e => e.ProductMainTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productMainTitle");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registra__3214EC276CC3E0FD");

            entity.ToTable("Registration");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Gender)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PanCardId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Uidnumber).HasColumnName("UIDNumber");
            entity.Property(e => e.VoteId)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Registrat__CityI__48CFD27E");

            entity.HasOne(d => d.Country).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__Registrat__Count__46E78A0C");

            entity.HasOne(d => d.Role).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Registrat__RoleI__49C3F6B7");

            entity.HasOne(d => d.State).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__Registrat__State__47DBAE45");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A0DAF448C");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Isdeleted).HasColumnName("ISdeleted");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Sizeid).HasName("PK__size__55B2E17F66ADF3CF");

            entity.ToTable("size");

            entity.Property(e => e.Sizeid).HasColumnName("sizeid");
            entity.Property(e => e.SizeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3214EC27BFCEF23C");

            entity.ToTable("State");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__State__CountryID__3A81B327");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3213E83FCF06DF31");

            entity.ToTable("SubCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__SubCatego__Produ__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
