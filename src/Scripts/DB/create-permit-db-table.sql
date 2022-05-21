CREATE DATABASE IF NOT EXISTS FoodTruckJunkie;

/* only necessary columns */
CREATE TABLE IF NOT EXISTS FoodTruckJunkie.FoodTruckPermit (
    `id` int NOT NULL AUTO_INCREMENT,
    `Applicant` nvarchar(500)  NOT NULL,
	`FacilityType` nvarchar(30)  NULL,
    `LocationDescription` nvarchar(500)  NULL,
    `Address` nvarchar(200)  NULL,
    `BlockLot` nvarchar(30)  NULL,
    `Block` nvarchar(30)  NULL,
    `Lot` nvarchar(30)  NULL,
    `Status` nvarchar(30)  NOT NULL,
    `FoodItems`  text NOT NULL,
    `X` nvarchar(50)  NULL,
    `Y` nvarchar(50)  NULL,
    `Latitude` nvarchar(50)  NULL,
    `Longitude`  nvarchar(50)   NULL,
     PRIMARY KEY ( id )
);

/* all columns
CREATE TABLE IF NOT EXISTS FoodTruckJunkie.FoodTruckPermit (
    `id` bigint(20) NOT NULL AUTO_INCREMENT,
    `LocationID` int NOT NULL,
    `Applicant` nvarchar(500)  NOT NULL,
	`FacilityType` nvarchar(30)  NOT NULL,
    `CNN` int  NOT NULL,
    `LocationDescription` nvarchar(500)  NOT NULL,
    `Address` nvarchar(200)  NOT NULL,
    `BlockLot` nvarchar(30)  NULL,
    `Block` nvarchar(30)  NULL,
    `Lot` nvarchar(30)  NULL,
    `Permit` nvarchar(50)  NOT NULL,
    `Status` nvarchar(30)  NOT NULL,
    `FoodItems`  text NOT NULL,
    `X` nvarchar(50)  NULL,
    `Y` nvarchar(50)  NULL,
    `Latitude` decimal(10,8)  NOT NULL,
    `Longitude`  decimal(11,8)   NOT NULL,
    `Schedule` nvarchar(2048)  NOT NULL,
    `DaysHours` nvarchar(50)  NULL,
    `NOISent` nvarchar(30)  NULL,
    `Approved` nvarchar(30) NULL,
    `Received` nvarchar(30) NOT NULL,
    `PriorPermit` int NOT NULL,
    `ExpirationDate` nvarchar(30) NULL,
    `Location` nvarchar(100)  NOT NULL,
    `FirePreventionDistricts` int NULL,
    `PoliceDistricts` int NULL,
    `SupervisorDistricts` int NULL,
    `ZipCodes` int NULL,
    `NeighborhoodsOld` int NULL,
     PRIMARY KEY ( id )
);

*/
