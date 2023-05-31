create database DemoProject
use DemoProject

create table Country
(
ID int primary key identity(1,1),
CountryCode varchar(4),
ShortName char(10),
FullName char(50),
IsDeleted bit
);

select * from Country

create table State
(
ID int primary key identity(1,1),
CountryID int foreign key references Country(ID),
ShortName char(10),
FullName char(50),
IsDeleted bit
);

select * from State

create table City
(
ID int primary key identity(1,1),
StateID int  foreign key references State(ID),
FullName char(10),
Pincode int,
IsDeleted bit
);

select * from City

create table Registration
(
ID int primary key identity(1,1),

CountryID int foreign key references Country(ID),
StateID int foreign key references State(ID),
CityID int foreign key references City(ID),

FirstName char(50),
LastName char(50),
Gender char(3),
PhoneNumber varchar(10),
Email varchar(50),
Password varchar (50),
Occupation varchar(50),
DOB date,
UIDNumber int,
VoteId varchar(20),
PassportNumber varchar(50),
PanCardId varchar(20),
Qualification varchar(100),
Address varchar(100),
IsActive bit,
Image varchar(100),
IsDeleted bit
);


drop table Registration

select * from Country
select * from Registration
select * from Role

create table Role
(
RoleID int primary key identity(1,1),
RoleName varchar(50),
ISdeleted bit
);
select * from Role
drop table Role

alter table Registration
add  RoleID int foreign key 
references Role(RoleID);


create table ProductCategory
(
ID int primary key identity(1,1),
Name varchar(150),
Isdeleted bit
);


Create table ProductBrand
(
BrandId int primary key identity(1,1),
BrandName varchar(50),
Isdeleted bit
);

create table ProductModel
(
Modelid int primary key identity(1,1),
BrandId int foreign key references ProductBrand(BrandId),
ModelName varchar(50),
Isdeleted bit
);

create table ProductSerise
(
Seriseid int primary key identity(1,1),
Modelid int foreign key references ProductModel(Modelid),
SeriseNumber varchar(50),
Isdeleted bit
);


create table size
(
sizeid int primary key identity(1,1),
SizeName varchar(50),
Isdeleted bit
);

create table color
(
colorid int primary key identity(1,1),
colorName varchar(50),
Isdeleted bit
);


create table ProductEntry
(
productEntryid int primary key identity(1,1),
ID int foreign  key references ProductCategory(ID),
BrandId int foreign key references ProductBrand(BrandId),
Modelid int foreign key references ProductModel(Modelid),
Seriseid int foreign key references  ProductSerise(Seriseid),

ProductName varchar(50),
ActualPrice decimal(10,3),
CurrentPrice decimal(10,3),
Discount varchar(100),
Descriptions varchar(max),
IsAvailable bit,
Isdeleted bit
);

alter table ProductEntry add IsAvailable bit

create table ProductSize
(
ProductSizeid int primary key identity(1,1),
 productEntryid  int foreign key references productEntry(productEntryid ),
sizeid int foreign key references size(sizeid),
Isdeleted bit
);


create table ProductColor
(
Productcolorid int primary key identity(1,1),
 productEntryid  int foreign key references productEntry(productEntryid ),
colorid  int foreign key references color(colorid),
Isdeleted bit
);


create table ProductImage
(
productImageid int primary key identity(1,1),
 productEntryid  int foreign key references productEntry(productEntryid ),
 ImageName varchar(150),
 Isdeleted bit
);



drop table color  

drop table size

drop table ProductSerise
drop table  ProductModel
drop table  ProductBrand
drop table ProductCategory



select * from ProductEntry


create table contactus
(
Id int primary key identity(1,1),
Name varchar(50),
Email varchar (50),
PhoneNumber varchar(10),
Subject varchar(30),
Message varchar(300),
Isdeleted bit
);

select * from contactus



create table about
(
Id int primary key identity(1,1),
ShopAddress varchar(250),
ShopeHours varchar(70),
Contact varchar(100),
Isdeleted bit


);
select * from about

---about--- weekday hr,  weekend hr, mobile , phone, email, fax, about title, service title, service image

--new tbl - aboutservice--  title, logo, details

--- tbl about banner------- image, maintitle, subtitle, subtitle val, subtitle discount, url (500).


alter table about add WeekdayHr varchar(15),
 weekendHr varchar(15),
 Mobile varchar(10),
 phone varchar(10),
 Email varchar(30),
 Fax varchar(30),
 AboutTitle varchar(30),
 ServiceTitle varchar(30),
 ServiceImage varchar(50);




 
alter table about
drop column WeekdayHr,
weekendHr ;

alter table about add WeekdayHr varchar(200), weekendHr varchar(200);


alter table about
drop column ShopeHours,
Contact ;

alter table about
drop column ServiceImage;


alter table about add ServiceImage varchar(200);

create table aboutService
(
Id int primary key identity(1,1),

Title varchar(30),
Logo varchar(50),
Details varchar(100),
ISdeleted bit
);



alter table aboutService
drop column Details;


alter table aboutService add Details varchar(500);

alter table aboutService
drop column Logo;

alter table aboutService add Logo varchar(200);



create table aboutBanner
(
Id int primary key identity(1,1),

Image varchar(90),
MainTitle varchar(80),
SubTitle varchar(80),
SubTitleValue varchar(60),
SubTitleDiscount varchar(50),
url varchar(900),
Isdeleted bit
);



alter table aboutBanner
drop column Image;

alter table aboutBanner add Image varchar(200);

select * from aboutService
select * from aboutBanner



create table ourTeam
(
Id int primary key identity(1,1),
teamTitle varchar(100),
teanDetails varchar(500),
teamMemberLogo varchar(200),
ShopOwnerSlidshow varchar(500),
slidShowLogo varchar(500),
Isdeleted bit
);



select * from ourTeam

alter table ourTeam add
teamMamberName varchar(200),
teamMamberProfile varchar(200),
slidshowWithName varchar(200);



create table indexHome
(
Id int primary key identity(1,1),
subTitle varchar(200),
details varchar(500),
logo varchar(900),
Isdeleted bit
);

select * from indexHome


create table productTitle
(
Id int primary key identity(1,1),
productMainTitle varchar(100),
productDetails varchar(500),
Isdeleted bit
);

select * from productTitle



create table product
(
Id int primary key identity(1,1),
prodecuName varchar(200),
productPrice varchar(100),
productDetails varchar(500),
image varchar (500),
productCart varchar(1000),
Isdeleted bit
);

select * from product


create table Deal
(
Id int primary key identity(1,1),
Title varchar(200),
Subtitle varchar(200),
Details varchar(500),
percentage varchar(90),
Image varchar(500),
Isdeleted bit
);

select * from Deal


create table News
(
Id int primary key identity(1,1),
Title varchar(200),
Details varchar(200),
Isdeleted bit
);


create table generalNews
(
Id int primary key identity(1,1),
HealLine varchar(200),
Author varchar(200),
Date varchar(200),
Logo varchar(200),
Isdeleted bit
);

alter table generalNews
add  details varchar(500);

select * from generalNews


create  table frui
(
Id int  primary key identity(1,1),
Title varchar(200),
Details varchar(200),
Year varchar(20),
Isdeleted bit
);


create table cart
(
Id int primary key identity(1,1),
Name varchar(200),
Price varchar(200),
Image varchar(200),
Quantity varchar(200),
Total varchar(200),
Isdeleted bit
);


create table SubCategory
(
id int primary key identity(1,1),
ProductCategoryID int foreign  key references ProductCategory(ID),
Name varchar(500),
Icon varchar(500),
IsModelOrSerise bit,
Isdeleted bit
);


alter table SubCategory
add IsModelOrSerise bit;




select * from SubCategory


alter table ProductModel
add SubCategoryID int foreign key
references SubCategory(id);



alter table ProductSerise
add SubCategoryID int foreign key
references SubCategory(id);


alter table ProductEntry
add SubCategoryID int foreign key
references SubCategory(id);


alter table ProductBrand
add ProductCategoryID int foreign key
references ProductCategory(ID);


alter table ProductBrand
add SubCategoryID int foreign key
references SubCategory(id);

alter table ProductSerise
add ProductCategoryID int foreign key
references ProductCategory(ID);


select * from ProductBrand


BrandId


alter table ProductSerise
add brandID int foreign key
references ProductBrand(BrandId);

Select * from ProductSerise


create table login
(
ID int primary key identity(1,1),
userName varchar(200),
Password varchar(200),
Isdeleted bit
);


create table AdminLogin
(
ID int primary key identity(1,1),
userName varchar(200),
Password varchar(200),
Isdeleted bit
);


select * from login