USE BTPBatimentPro;

-- Insertion des rôles dans la table AspNetRoles
INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES 
('1', 'Admin', 'ADMIN'),
('2', 'User', 'USER');

-- Insertion des employés dans la table Employees
INSERT INTO Employees (FirstName, LastName, Role) VALUES 
('John', 'Doe', 'Manager'),
('Jane', 'Smith', 'Engineer'),
('Alice', 'Johnson', 'Technician');

-- Insertion des utilisateurs dans la table AspNetUsers en utilisant l'EmployeeId
-- (Les employés deviennent les utilisateurs)
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, EmployeeId)
VALUES
('1', 'johndoe', 'JOHNDOE', 'john.doe@example.com', 'JOHN.DOE@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEO9K4KA9uuZBezI9zv8hB9Dd6vIysM4Ggqk1OL7ybjnotylTPDufZUiLiNsxN+59Mw==', 'security_stamp_1', 1, 0, 1, 0, 1),  -- John Doe lié à l'EmployeeId 1
('2', 'janesmith', 'JANESMITH', 'jane.smith@example.com', 'JANE.SMITH@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEDca87VhJXt3+PdjTalAM8wDNjhL6w74J7IzOXUY2+ZPrKZlRF+stZTEQXxtkfpqeA==', 'security_stamp_2', 1, 0, 1, 0, 2),  -- Jane Smith lié à l'EmployeeId 2
('3', 'alicejohnson', 'ALICEJOHNSON', 'alice.johnson@example.com', 'ALICE.JOHNSON@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAEOW+oTzAwDzpt5+AQAAAAIAAYagAAAAEH+Jle+yVZcwFCF1BvPOrDZPyujePNgrFwqGMWe9uCjfvXowA6iQekAoOmya/3LdfQ==', 'security_stamp_3', 1, 0, 1, 0, 3); -- Alice Johnson lié à l'EmployeeId 3

-- Lier l'utilisateur 'johndoe' avec le rôle 'Admin'
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('1', '1');  -- Admin

-- Lier l'utilisateur 'janesmith' avec le rôle 'User'
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('2', '2');  -- User

-- Lier l'utilisateur 'alicejohnson' avec le rôle 'User'
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('3', '2');  -- User

-- Insertion des projets dans la table Projects
INSERT INTO Projects (Name, Description, Address) VALUES 
('Building A', 'Construction of a 5-story office building', '123 Main St, Paris'),
('Building B', 'Renovation of a residential complex', '456 Oak Rd, Lyon');

-- Insertion des assignations dans la table Assignments
INSERT INTO Assignments (EmployeeId, ProjectId) VALUES
(1, 1),  -- John Doe travaille sur Building A
(2, 1),  -- Jane Smith travaille sur Building A
(3, 2);  -- Alice Johnson travaille sur Building B
