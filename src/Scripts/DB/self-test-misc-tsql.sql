select count(1) from foodtruckjunkie.foodtruckpermit;

select * from foodtruckjunkie.foodtruckpermit order by id desc;


LOAD DATA local INFILE 'C:/Weixian/Projects/FoodTruckJunkie/data/Mobile_Food_Facility_Permit_Reduced.csv' 
INTO TABLE foodtruckjunkie.foodtruckpermit
FIELDS TERMINATED BY ',' 
ENCLOSED BY '"'
LINES TERMINATED BY '\r\n'
IGNORE 1 ROWS
(Applicant, FacilityType, LocationDescription,Address, BlockLot,Block,Lot,Status,FoodItems,Latitude,Longitude);

drop table FoodTruckJunkie.FoodTruckPermit;

truncate table FoodTruckJunkie.FoodTruckPermit;

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
    `Latitude` nvarchar(50)  NULL,
    `Longitude`  nvarchar(50)   NULL,
     PRIMARY KEY ( id )
);




call SP_SearchLatitudeLongtitude(37.78813948, -122.3925795, 20, 10);



