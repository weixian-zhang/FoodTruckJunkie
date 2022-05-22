CREATE DEFINER=`foodtruckjunkie`@`%` PROCEDURE `SP_SearchLatitudeLongitude`(
	IN startLatitude decimal(10, 8),
    IN startLongtitude decimal(11, 8),
	IN distantMiles int,
    IN noOfResult int
)
BEGIN
    
    SELECT DISTINCT id, Applicant, FoodItems, Latitude, Longitude, Address,LocationDescription, (3956 * 2 * ASIN(SQRT( POWER(SIN(( startLatitude - (CAST(Latitude AS DECIMAL(10,8))) ) *  pi()/180 / 2), 2) +COS( startLatitude * pi()/180) * COS( (CAST(Latitude AS DECIMAL(10,8))) * pi()/180) * POWER(SIN(( startLongtitude - (CAST(Longitude AS DECIMAL(11,8))) ) * pi()/180 / 2), 2) ))) as distance  
	from foodtruckpermit  
    /* result should exclude parameters startLatitude and startLongtitude */
    where (CAST(Latitude AS DECIMAL(10,8))) != startLatitude and (CAST(Longitude AS DECIMAL(11,8))) != startLongtitude and Status = 'APPROVED' and FacilityType = 'Truck'
    having  distance <= distantMiles
	order by distance
    limit noOfResult;
END