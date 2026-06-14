-- HeiRestaurant Database Init Script
-- Run in SSMS with Windows Authentication

IF DB_ID(N'HeiRestaurant') IS NOT NULL
BEGIN
    ALTER DATABASE HeiRestaurant SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HeiRestaurant;
END
GO

CREATE DATABASE HeiRestaurant;
GO

USE HeiRestaurant;
GO

-- Staff only (students login by student number, no account)
CREATE TABLE Users (
    UserId       INT IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  NOT NULL UNIQUE,
    Password     NVARCHAR(100) NOT NULL DEFAULT N'',
    RoleType     NVARCHAR(20)  NOT NULL CHECK (RoleType IN (N'Admin', N'Cashier', N'Restaurant')),
    IsActive     BIT           NOT NULL DEFAULT 1,
    CreatedAt    DATETIME      NOT NULL DEFAULT GETDATE()
);

-- Single canteen
CREATE TABLE Restaurants (
    RestaurantId INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(100) NOT NULL,
    Location     NVARCHAR(200) NULL,
    ManagerUserId INT          NULL REFERENCES Users(UserId)
);

CREATE TABLE Students (
    StudentId    INT IDENTITY(1,1) PRIMARY KEY,
    StudentNo    NVARCHAR(20)  NOT NULL UNIQUE,
    Name         NVARCHAR(50)  NOT NULL,
    ClassName    NVARCHAR(50)  NULL,
    Phone        NVARCHAR(20)  NULL
);

-- One student one card; CardNo = StudentNo
CREATE TABLE Cards (
    CardId       INT IDENTITY(1,1) PRIMARY KEY,
    StudentId    INT           NOT NULL UNIQUE REFERENCES Students(StudentId),
    CardNo       NVARCHAR(30)  NOT NULL UNIQUE,
    Balance      DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (Balance >= 0),
    Status       NVARCHAR(20)  NOT NULL DEFAULT N'Active' CHECK (Status IN (N'Active', N'Lost', N'Frozen'))
);

CREATE TABLE FoodCategories (
    CategoryId   INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Foods (
    FoodId       INT IDENTITY(1,1) PRIMARY KEY,
    RestaurantId INT           NOT NULL REFERENCES Restaurants(RestaurantId),
    CategoryId   INT           NOT NULL REFERENCES FoodCategories(CategoryId),
    Name         NVARCHAR(100) NOT NULL,
    Price        DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    Stock        INT           NOT NULL DEFAULT 0 CHECK (Stock >= 0),
    IsAvailable  BIT           NOT NULL DEFAULT 1
);

CREATE TABLE Orders (
    OrderId      INT IDENTITY(1,1) PRIMARY KEY,
    CardId       INT           NOT NULL REFERENCES Cards(CardId),
    CashierId    INT           NOT NULL REFERENCES Users(UserId),
    RestaurantId INT           NOT NULL REFERENCES Restaurants(RestaurantId),
    TotalAmount  DECIMAL(10,2) NOT NULL,
    OrderTime    DATETIME      NOT NULL DEFAULT GETDATE(),
    Status       NVARCHAR(20)  NOT NULL DEFAULT N'Completed'
);

CREATE TABLE OrderDetails (
    DetailId     INT IDENTITY(1,1) PRIMARY KEY,
    OrderId      INT           NOT NULL REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FoodId       INT           NOT NULL REFERENCES Foods(FoodId),
    Quantity     INT           NOT NULL CHECK (Quantity > 0),
    UnitPrice    DECIMAL(10,2) NOT NULL,
    SubTotal     DECIMAL(10,2) NOT NULL
);

CREATE TABLE Transactions (
    TransId      INT IDENTITY(1,1) PRIMARY KEY,
    CardId       INT           NOT NULL REFERENCES Cards(CardId),
    TransType    NVARCHAR(20)  NOT NULL CHECK (TransType IN (N'Recharge', N'Consume', N'Refund')),
    Amount       DECIMAL(10,2) NOT NULL,
    BalanceAfter DECIMAL(10,2) NOT NULL,
    TransTime    DATETIME      NOT NULL DEFAULT GETDATE(),
    Remark       NVARCHAR(200) NULL
);
GO

-- Staff (no password for testing)
INSERT INTO Users (Username, Password, RoleType) VALUES
(N'admin',        N'', N'Admin'),
(N'cashier01',    N'', N'Cashier'),
(N'restaurant01', N'', N'Restaurant');

INSERT INTO Restaurants (Name, Location, ManagerUserId)
SELECT N'First Canteen', N'Campus', UserId FROM Users WHERE Username = N'restaurant01';

INSERT INTO FoodCategories (Name) VALUES
(N'Staple'), (N'Meat'), (N'Vegetable'), (N'Soup'), (N'Drink');

INSERT INTO Foods (RestaurantId, CategoryId, Name, Price, Stock, IsAvailable)
SELECT r.RestaurantId, c.CategoryId, v.Name, v.Price, v.Stock, 1
FROM Restaurants r
CROSS JOIN (VALUES
    (N'Staple',     N'Rice',      2.00, 500),
    (N'Staple',     N'Bun',       1.50, 300),
    (N'Meat',       N'Pork',     12.00, 100),
    (N'Meat',       N'Chicken',  10.00, 100),
    (N'Vegetable',  N'Vegetable', 6.00, 100),
    (N'Soup',       N'Soup',      3.00, 200),
    (N'Drink',      N'Cola',      3.00, 200),
    (N'Drink',      N'Water',     2.00, 300)
) AS v(CategoryName, Name, Price, Stock)
INNER JOIN FoodCategories c ON c.Name = v.CategoryName
WHERE r.RestaurantId = 1;

-- Students: card number = student number
INSERT INTO Students (StudentNo, Name, ClassName, Phone) VALUES
(N'2024001001', N'Zhang San', N'CS2401', N'13800000001'),
(N'2024001002', N'Li Si',     N'CS2401', N'13800000002');

INSERT INTO Cards (StudentId, CardNo, Balance, Status)
SELECT s.StudentId, s.StudentNo, v.Balance, N'Active'
FROM (VALUES
    (N'2024001001', 100.00),
    (N'2024001002',  50.00)
) AS v(StudentNo, Balance)
INNER JOIN Students s ON s.StudentNo = v.StudentNo;

INSERT INTO Transactions (CardId, TransType, Amount, BalanceAfter, Remark)
SELECT c.CardId, N'Recharge', c.Balance, c.Balance, N'Initial recharge'
FROM Cards c;
GO
