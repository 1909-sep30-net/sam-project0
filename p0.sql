select * from Inventory;

update Inventory
	set Amount = 6
	where StoreId = 1 and productId = 1;
update Inventory
	set Amount = 5
	where StoreId = 1 and productId = 2;
update Inventory
	set Amount = 4
	where StoreId = 1 and productId = 3;

select * from customer;

select * from orderOverview;

select * from orderitem;