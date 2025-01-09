USE BTPBatimentPro;

-- Créer les données dans la table Employee
INSERT INTO Employees (FirstName, LastName, Role) VALUES 
('John', 'Doe', 'Manager'),
('Jane', 'Smith', 'Engineer'),
('Alice', 'Johnson', 'Technician');

-- Créer les données dans la table Project
INSERT INTO Projects (Name, Description, Address) VALUES 
('Building A', 'Construction of a 5-story office building', '123 Main St, Paris'),
('Building B', 'Renovation of a residential complex', '456 Oak Rd, Lyon');

-- Créer les données dans la table Assignment
-- Assignation des employés aux projets
INSERT INTO Assignments (EmployeeId, ProjectId) VALUES
(1, 1),  -- John Doe travaille sur Building A
(2, 1),  -- Jane Smith travaille sur Building A
(3, 2);  -- Alice Johnson travaille sur Building B

-- Créer les données dans la table Attendance
-- Enregistrement des présences
INSERT INTO Attendances (EmployeeId, Date, Status) VALUES
(1, '2025-01-09', 'Present'),
(2, '2025-01-09', 'Absent'),
(3, '2025-01-09', 'Present');

-- Créer les données dans la table Leave
-- Enregistrement des demandes de congé
INSERT INTO Leaves (EmployeeId, StartDate, EndDate, Status) VALUES
(1, '2025-01-10', '2025-01-15', 'Pending'),
(2, '2025-01-11', '2025-01-14', 'Approved'),
(3, '2025-01-12', '2025-01-13', 'Rejected');
