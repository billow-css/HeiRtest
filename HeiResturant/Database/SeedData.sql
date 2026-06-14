-- Seed data (when tables exist but empty)
USE HeiRestaurant;
GO

IF EXISTS (SELECT 1 FROM Users)
    RETURN;
GO

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
