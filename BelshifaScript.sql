CREATE TABLE Product (
    ProdID NUMBER PRIMARY KEY,
    ProdName VARCHAR2(20),
    Price NUMBER,
    ProdCategory VARCHAR2(20),
    PharmacyName VARCHAR2(20),
    Quantity NUMBER,
    Offer NUMBER
);

CREATE TABLE Users (
    UserID NUMBER PRIMARY KEY,
    UserName VARCHAR2(50),
    Email VARCHAR2(50)unique,
    UPassword VARCHAR2(50),
    Address VARCHAR2(50),
    PhoneNum NUMBER,
    UType VARCHAR2(50)
);
CREATE TABLE Orders (
    OrderID NUMBER PRIMARY KEY,
    CustomerID NUMBER,
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID) ON DELETE CASCADE
);
CREATE TABLE OrderDetails (
    OrderDetailID NUMBER PRIMARY KEY,
    OrderID NUMBER,
    ProdID NUMBER,
    Quantity NUMBER,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProdID) REFERENCES Product(ProdID) ON DELETE CASCADE

INSERT INTO Product VALUES (1, 'Abilify', 75, 'medicine', 'elezaby',10, 0);
INSERT INTO Product VALUES (2, 'Abiraterone', 150, 'medicine', 'elezaby',20, 10);
INSERT INTO Product VALUES (3, 'Actemra', 100, 'medicine', 'elezaby',50, 0);
INSERT INTO Product VALUES (4, 'Clary Shampoo', 250, 'Hair Care', 'Elemam',10, 20);
INSERT INTO Product VALUES (5, 'Blankie', 100, 'Skin Care', 'elezaby',15, 15);
INSERT INTO Product VALUES (6, 'telofill sunscren', 370, 'Skin Care', 'Elemam',15, 25);

INSERT INTO Users VALUES (1, 'Hagar', 'Hagar@gmail.com', '123456789', 'October', 01145864768, 'admin');
INSERT INTO Users VALUES (2, 'Logain', 'Logain@gmail.com', '987654321', 'Abbaseya', 01278453547, 'user');
INSERT INTO Users VALUES (3, 'Latifa', 'Latifa@gmail.com', '369258147', 'El-Marg', 015784157845, 'admin');
INSERT INTO Users VALUES (4, 'Menna', 'Menna@gmail.com', '147258369', 'Dokki', 010865468799, 'user');
INSERT INTO Users VALUES (5, 'Mustafa', 'Mustafa@gmail.com', '147852369', 'October', 01189765687, 'user');
INSERT INTO Users VALUES (6, 'Ahmed', 'Ahmed@gmail.com', '852369741', 'Dokki', 01087854875, 'user');

INSERT INTO Orders VALUES (1, 2);
INSERT INTO Orders VALUES (2, 4);
INSERT INTO Orders VALUES (3, 6);
INSERT INTO OrderDetails VALUES (1, 1, 4, 2);
INSERT INTO OrderDetails VALUES (2, 1, 5, 1);
INSERT INTO OrderDetails VALUES (3, 1, 1, 5);
INSERT INTO OrderDetails VALUES (4, 2, 3, 1);
INSERT INTO OrderDetails VALUES (5, 2, 2, 3);
INSERT INTO OrderDetails VALUES (6, 3, 6, 2);
commit;

Create or Replace Procedure OrdersMaxID
(MID out number)
as 
begin 
SELECT Max(OrderID) into MID
from Orders;
end;

Create or Replace Procedure OrderDetailsMaxID
(DID out number)
as 
begin 
SELECT Max(OrderDetailID) into DID
from OrderDetails;
end;
create or replace Procedure SelectUser
(mail in VARCHAR2 , pass in VARCHAR2, Userid out number , utype out VARCHAR2)
as 
begin
SELECT UserID, UType 
into Userid , utype  
From Users 
where Email = mail and UPassword = pass;
end;
create or replace
Procedure ShowUserOrders
(Userid in NUMBER , UOrders out sys_refcursor)
as
begin
open uorders for
select p.ProdName
from Orders o , OrderDetails od , Product p
where o.CustomerID = Userid
and od.OrderID = o.OrderID
and  p.ProdID = od.ProdID;
end;
