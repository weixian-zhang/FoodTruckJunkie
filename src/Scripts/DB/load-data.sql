LOAD DATA local INFILE 'C:/Weixian/Projects/FoodTruckJunkie/data/Mobile_Food_Facility_Permit_Reduced.csv' 
INTO TABLE foodtruckjunkie.foodtruckpermit
FIELDS TERMINATED BY ',' 
ENCLOSED BY '"'
LINES TERMINATED BY '\r\n'
IGNORE 1 ROWS
(Applicant, FacilityType, LocationDescription,Address, BlockLot,Block,Lot,Status,FoodItems,Latitude,Longitude);