Create Schema GCStoreData;
Go

drop table if exists Product;
drop table if exists Store;
drop table if exists Inventory;
drop table if exists Customer;
drop table if exists OrderOverview;
drop table if exists OrderItem;

Create Table Product(
	ProductId int identity primary key,
	ProductName nvarchar(30) unique not null,
	UnitPrice decimal(6,2) not null,
	Constraint pricecheck check ( UnitPrice > 0 )
);

Create Table Store(
	StoreId int identity primary key,
	StoreName nvarchar(20) unique not null,
	City nvarchar(30) not null,
	Postal nvarchar(5) not null,
);

Create table Inventory(
	InventoryId int identity primary key,
	StoreId int not null foreign key references Store(StoreId),
	ProductId int not null foreign key references Product(ProductId),
	Amount int not null,
	Constraint amountNumber check (amount >= 0)
);

Create Table Customer(
	CustomerId int identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	PhoneNumber nvarchar(13),
	FavoriteStore int foreign key references Store(StoreId),
	Constraint phoneformat check (PhoneNumber like '(%)%-%')

);

Create Table OrderOverView(
	OrderId int identity primary key,
	CustomerId int not null foreign key references Customer(CustomerId),
	StoreId int not null foreign key references Store(StoreId),
	OrderDate date not null,
	TotalPrice decimal(7,2) not null
);

Create Table OrderItem(
	OrderItemId int identity primary key,
	OrderId int not null foreign key references OrderOverView(OrderId),
	ProducutName nvarchar(30) not null foreign key references Product(ProductName),
	Amount int not null,
	Constraint amountcheck check ( Amount >= 0 )
);

EXEC sp_rename 'OrderOverview.Time', 'OrderDate';  
GO  

insert into Product values
	('NSwitch',199.99),
	('Xbox One',300),
	('Playstation 4 Pro',299.99);

insert into Store values
	('GCStore-Main','Arlinton','76011'),
	('GCStore-2','Dallas','76010'),
	('GCStore-3','Houston','76001');

insert into Inventory values
	(1,1,50),(1,2,50),(1,3,50),
	(2,1,40),(2,2,40),(2,3,40),
	(3,1,35),(3,2,35),(3,3,35);

insert into Customer values
	('Sam','Lin','(123)456-7890',1),
	('Tony','Brothers','(456)123-1568',2),
	('Gabe','Rei','(786)234-4838',3);

insert into OrderOverview values
	(2,1,'2019-01-31',200),
	(3,1,'2019-03-30',199.99),
	(1,3,'2019-05-01',199.99),
	(3,2,'2019-07-04',300);

insert into OrderItem values
	(1,'NSwitch',2),
	(1,'Xbox One',1),
	(2,'Playstation 4 Pro',1),
	(2,'Xbox One',1),
	(3,'NSwitch',1),
	(3,'Playstation 4 Pro',2);

select * from product;
select * from Store;

select * from Inventory;
select * from Customer;

select * from OrderOverView;
select * from OrderItem;

update Inventory 
	set Amount = 6
	where InventoryId = 1;
update Inventory 
	set Amount = 5
	where InventoryId = 2;
update Inventory 
	set Amount = 4
	where InventoryId = 3;

delete OrderItem
	where OrderId > 3;

delete OrderOverView
	where OrderId > 3;

delete customer
	where CustomerId > 3;