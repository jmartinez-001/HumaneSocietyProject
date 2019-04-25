<<<<<<< HEAD

ALTER TABLE DietPlans 
ALTER COLUMN FoodAmountInCups DECIMAL

INSERT INTO Categories VALUES ('Dog')
INSERT INTO Categories VALUES ('Cat')
INSERT INTO Categories VALUES ('Bird')
INSERT INTO Categories VALUES ('Reptile')
INSERT INTO Categories VALUES ('Small Mammal')

INSERT INTO DietPlans VALUES ( 'Big Dog Food' , 'Kibble' , 2)
INSERT INTO DietPlans VALUES ( 'Small Dog Food' , 'Kibble' , 1)
INSERT INTO DietPlans VALUES ( 'Cat Food' , 'Meow Mix' , .5)
INSERT INTO DietPlans VALUES ( 'Bird Seeds' , 'Sunflower Seeds' , .5)
INSERT INTO DietPlans VALUES ( 'Vegetables' , 'Carrots' , 1)
INSERT INTO DietPlans VALUES ( 'Bugs' , 'Crickets' , 1)

INSERT INTO Animals VALUES ('Neo' , 58 , 3 , 'Docile' , 1 , 0 , 'Male' , 'Available' , 1 , 1 , null ) 
INSERT INTO Animals VALUES ('Ledger' , 61 , 2 , 'Hyper' , 1 , 1 , 'Male' , 'Available' , 1 , 1 , null)
INSERT INTO Animals VALUES ('Puma' , 15 , 10 , 'Docile' , 1 , 0 , 'Female' , 'Available' , 2 , 3 , null)
INSERT INTO Animals VALUES ('Shadow' , 12 , 2 , 'Docile' , 1 , 0 , 'Female' , 'Available' , 2 , 3 , null)
INSERT INTO Animals VALUES ('Bubba' , 2 , 1 , 'Docile' , 1 , 0 , 'Male' , 'Available' , 5 , 5 , null)
INSERT INTO Animals VALUES ('Ratatouille' , 1 , 1 , 'Hyper' , 1 , 1 , 'Female' , 'Available' , 5 , 5 ,  null)
INSERT INTO Animals VALUES ('Shelly' , 2 , 5 , 'Docile' , 1 , 0 , 'Female' , 'Available' , 4 , 5 , null)
INSERT INTO Animals VALUES ('Brandy' , 60 , 2 , 'Docile' , 1 , 1 , 'Male' , 'Adopted' , 1 , 1 , 3)
INSERT INTO Animals VALUES ('Tacky' , 25 , 3 , 'Docile' , 1 , 1 , 'Male' , 'Adopted' , 2 , 1 , 2)

INSERT INTO Rooms VALUES (101 , 1)
INSERT INTO Rooms VALUES (102 , 2)
INSERT INTO Rooms VALUES (103 , 3)
INSERT INTO Rooms VALUES (104 , 4)
INSERT INTO Rooms VALUES (105 , 5)
INSERT INTO Rooms VALUES (201 , 6)
INSERT INTO Rooms VALUES (202 , 7)
INSERT INTO Rooms VALUES (203 , 8) 
INSERT INTO Rooms VALUES (204 , null)
INSERT INTO Rooms VALUES (205 , null)

INSERT INTO Employees VALUES ('Bill' , 'Kapri' , 'KodakBlack' , 'Password' , 11112 , 'KodakBlack@gmail.com')
INSERT INTO Employees VALUES ('Radric' , 'Davis' , 'GucciMane' , 'Password' , 11113 , 'Guccimane@gmail.com')
INSERT INTO Employees VALUES ('Clifford' , 'Harris' , 'T.I.' , 'Password' , 11114 , 'TIP@gmail.com')
INSERT INTO Employees VALUES ('Jamall' , 'Demons' , 'YNWMelly' , 'Password' , 11115 , 'YNWMelly@gmail.com')

INSERT INTO Clients VALUES ('Roger' , 'Waters' , 'Roger1' , 'Passwor d' 
INSERT INTO Clients VALUES ('Richard' , 'Wright' , 'Richard1' , 'Password'
INSERT INTO Clients VALUES ('David' , 'Gilmoure' , 'David1' , 'Password'
INSERT INTO Clients VALUES ('Nick' , 'Mason' , 'Nick1' , 'Password'

INSERT INTO Shots VALUES ('Distemper')
INSERT INTO Shots VALUES ('Bordetella')
INSERT INTO Shots VALUES ('Hepititis')
INSERT INTO Shots VALUES ('Parainfluenza')
INSERT INTO Shots VALUES ('Heartworm')

INSERT INTO Addresses VALUES ('1111 N 45th st' , 'Milwaukee' , 49 , '53215') 
INSERT INTO Addresses VALUES ('2222 N 55th st' , 'Milwaukee' , 49 , '52215')
INSERT INTO Addresses VALUES ('3333 N 65th st' , 'Milwaukee' , 49 , '53625')
INSERT INTO Addresses VALUES ('4444 N 75th st' , 'Milwaukee' , 49 , '53451')
INSERT INTO Addresses VALUES ('5555 N 85th st' , 'Milwaukee' , 49 , '53131')







=======
CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));
>>>>>>> 76b99b6d496fd5f12ebb25a5ab3abbb650fd608a
