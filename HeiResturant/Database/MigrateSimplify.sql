-- Migrate existing database to simplified model (run once if DB already exists)
USE HeiRestaurant;
GO

-- Remove student user accounts (students login by student no)
IF COL_LENGTH('Students', 'UserId') IS NOT NULL
    UPDATE Students SET UserId = NULL WHERE UserId IS NOT NULL;
DELETE FROM Users WHERE RoleType = N'Student';

-- Sync card number with student number (one person one card)
UPDATE c SET c.CardNo = s.StudentNo
FROM Cards c
INNER JOIN Students s ON c.StudentId = s.StudentId
WHERE c.CardNo <> s.StudentNo;

-- Clear staff passwords for testing
UPDATE Users SET Password = N'' WHERE RoleType IN (N'Admin', N'Cashier', N'Restaurant');
GO
