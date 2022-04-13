--create database Project1;
--use Project1;

drop table OrderItems;
drop table OrderDetails;
drop table ProductDetails;
drop table CustomerDetails;
drop table EmployeeDetails;

alter table EmployeeDetails drop constraint chk_empAge
alter table EmployeeDetails drop constraint chk_empFirstName
alter table EmployeeDetails drop constraint chk_empLastName
alter table EmployeeDetails drop constraint chk_empPhone
alter table EmployeeDetails drop constraint chk_empRole
alter table EmployeeDetails drop constraint chk_empSalary

alter table ProductDetails drop constraint chk_pName
alter table ProductDetails drop constraint chk_pPrice
alter table ProductDetails drop constraint chk_pQty
alter table ProductDetails drop constraint chk_pType



-- I done goofed and needed to drop the tables for a bigint conversion
/*
drop table CustomerDetails;
drop table EmployeeDetails;
drop table OrderDetails;
drop table OrderItems;
drop table ProductDetails; */

CREATE TABLE [CustomerDetails] (
  [cId] integer not null identity(1,1),
  [cFirstName] varchar(30) not null,
  [cLastName] varchar(30),
  [cEmail] varchar(30)not null,
  [cPhoneNo] bigint not null,
  [cUsername] varchar(20)not null,
  [cPassword] varchar(20)not null,

  constraint pk_cId
  PRIMARY KEY ([cId]),
  constraint chk_cFirstName check (len(cFirstName) >=3),
  constraint chk_cLastName check (len(cLastName) >=3),
  constraint chk_cEmail check (cEmail LIKE '%_@__%.__%'),
  constraint chk_cPhoneNo check (cPhoneNo between 1000000000 and 9999999999),
  constraint unk_cUsername unique(cUsername)
);

CREATE TABLE [EmployeeDetails] (
  [empId] integer not null identity(1,1),
  [empFirstName] varchar(30) not null,
  [empLastName] varchar(30) not null,
  [empRole] varchar(30) not null,
  [empAge] integer not null,
  [empHireDate] datetime,
  [empSalary] integer not null,
  [empPhone] bigint not null,
  [empUsername] varchar(20) not null,
  [empPassword] varchar(20) not null,

  constraint pk_empId PRIMARY KEY ([empId]),
  constraint chk_empFirstName check (len(empFirstName) >=3),
  constraint chk_empLastName check (len(empLastName) >=3),
  constraint chk_empRole check (empRole in ('Manager', 'Associate')),
  constraint chk_empAge check (empAge between 16 and 80),
  constraint chk_empSalary check (empSalary between 16000 and 60000),
  constraint chk_empPhone check (empPhone between 1000000000 and 9999999999),
  constraint unk_empUsername unique(empUsername)
);

CREATE TABLE [OrderDetails] (
  [oId] integer identity(1001, 1), --Be sure to check this out later. Unsure if an Alter table is needed later
  [Customer_ID] integer,
  [Date_Ordered] datetime,
  [Total_Cost] float,
  [Employee_Servicing_ID] integer,

  constraint pk_oId PRIMARY KEY ([oId]),
  CONSTRAINT [FK_OrderDetails.Customer_ID]
    FOREIGN KEY ([Customer_ID])
      REFERENCES [CustomerDetails]([cId]),
  CONSTRAINT [FK_OrderDetails.Employee_Servicing_ID]
    FOREIGN KEY ([Employee_Servicing_ID])
      REFERENCES [EmployeeDetails]([empId])
);

CREATE TABLE [ProductDetails] (
  [pId] integer not null identity(1,1),
  [pName] varchar(50) not null,
  [pType] varchar(50) not null,
  [pPrice] float not null,
  [pQty] integer not null,
  [isInStock] bit not null,

  constraint pk_pId PRIMARY KEY ([pId]),
  constraint chk_pName check (len(pName) >=3),
  constraint chk_pType check (pType in ('Leaf Tea', 'Iced Tea', 'Milk Tea')), --'Boba Tea', 'Jellies', 'Poppers', 'Boba' for future updates maybe
  constraint chk_pPrice check (pPrice between 0.25 and 10.00),
  constraint chk_pQty check (pQty >= 0)
  );

  --alter table ProductDetails Add Constraint chk_pType check (pType in ('Leaf Tea', 'Iced Tea', 'Milk Tea'));

  --Figure this one out.
  /*
  if @pQty > 0
	isInStock = true;
  else
	isInStock = false;
);*/

CREATE TABLE [OrderItems] (
  [oItemId] integer not null identity(1,1),
  [orderId] integer not null,
  [productId] integer not null,
  CONSTRAINT pk_oItemId PRIMARY KEY ([oItemId]),
  CONSTRAINT [FK_OrderItems.orderId]
    FOREIGN KEY ([orderId])
      REFERENCES [OrderDetails]([oId]),
  CONSTRAINT [FK_OrderItems.productId]
    FOREIGN KEY ([productId])
      REFERENCES [ProductDetails]([pId])
);


--This isn't being implemented right now due to time constraints.
/*
CREATE TABLE [Card Payment] (
  [Card_Number] biginteger,
  [Card_CCV] integer,
  [Card_Name] varchar(60),
  [HasFunds] bool,
  PRIMARY KEY ([Card_Number])
);

CREATE TABLE [Payment] (
  [payId] integer,
  [Cash_Paid] float,
  [Card_Paid] float,
  [Total Payment] float,
  [HasEnough] bool,
  PRIMARY KEY ([payId]),
  CONSTRAINT [FK_Payment.Card_Paid]
    FOREIGN KEY ([Card_Paid])
      REFERENCES [Card Payment]([Card_Number])
);*/

insert into CustomerDetails values 
('Aki', 'Ishimoto', 'hibackimaki@place.com', 5055551234, 'imAki', 'Ohnu'),
('Cyrus', 'Bishop', 'dontworryboutit@place.com', 5055551235, 'yoimcrazy', 'Bet')

--notes on datetime: it is YYYYMMDD HH:MM:SS AM/PM but for current time you can use CURRENT_TIMESTAMP

insert into EmployeeDetails values
('Samantha', 'Cortez', 'Manager', 24, '20161127 12:30:26 PM', 27000, 5055559876, 'SCortez02', 'Sunshine'),
('Kyle', 'Rodriguez', 'Associate', 19, '20210625 10:25:09 AM', 19000, 5055556969, 'KRod22', 'Bruh')

--need to add Size later...
insert into ProductDetails values 
--Leaf Teas
('Jasmine', 'Leaf Tea', 5.50, 100, 'true'),
('Matcha', 'Leaf Tea', 5.50, 100, 'true'),
('English Breakfast', 'Leaf Tea', 5.50, 100, 'true'),
('Irish Breakfast', 'Leaf Tea', 5.50, 100, 'true'),
('Scottish Afternoon', 'Leaf Tea', 5.50, 100, 'true'),
('Earl Grey', 'Leaf Tea', 6.25, 150, 'true'),
('Chai', 'Leaf Tea', 6.75, 200, 'true'),
('Darjeeling', 'Leaf Tea', 5.50, 100, 'true'),
('Tieguanyin (Iron Goddess)', 'Leaf Tea', 10.00, 50, 'true'),
('Chamomile', 'Leaf Tea', 5.50, 100, 'true'),
('Lavender', 'Leaf Tea', 5.50, 100, 'true'),
('Honeysuckle', 'Leaf Tea', 5.50, 100, 'true'),
('Mint', 'Leaf Tea', 5.50, 100, 'true'),
('Hibiscus', 'Leaf Tea', 5.50, 100, 'true'),
('Pumpkin Spice', 'Leaf Tea', 8.00, 0, 'false'),
--Iced Teas
('Blueberry', 'Iced Tea', 4.75, 100, 'true'),
('Kiwi', 'Iced Tea', 4.75, 100, 'true'),
('Lychee', 'Iced Tea', 4.75, 100, 'true'),
('Mango', 'Iced Tea', 4.75, 100, 'true'),
('Passion Fruit', 'Iced Tea', 4.75, 100, 'true'),
('Peach', 'Iced Tea', 4.75, 100, 'true'),
('Pineapple', 'Iced Tea', 4.75, 100, 'true'),
('Pomegranate', 'Iced Tea', 4.75, 100, 'true'),
('Raspberry', 'Iced Tea', 4.75, 100, 'true'),
('Strawberry', 'Iced Tea', 4.75, 100, 'true'),
('Watermelon', 'Iced Tea', 4.75, 100, 'true'),
--Milk Teas
('Coconut', 'Milk Tea', 6.50, 100, 'true'),
('HoneyDew', 'Milk Tea', 6.50, 100, 'true'),
('Lavender', 'Milk Tea', 6.50, 100, 'true'),
('Mango', 'Milk Tea', 6.50, 100, 'true'),
('Matcha', 'Milk Tea', 6.50, 100, 'true'),
('Pineapple', 'Milk Tea', 6.50, 100, 'true'),
('Strawberry', 'Milk Tea', 6.50, 100, 'true'),
('Taro', 'Milk Tea', 6.50, 100, 'true'),
('Thai Coffee', 'Milk Tea', 6.50, 100, 'true'),
('Thai Tea', 'Milk Tea', 6.50, 100, 'true'),
('Vanilla', 'Milk Tea', 6.50, 100, 'true')
--('pName', 'pType', price, Qty, true),