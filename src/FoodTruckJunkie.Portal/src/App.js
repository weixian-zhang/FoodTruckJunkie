import React, { Component } from "react";
import './App.css';
import FoodTruckImage from './assets/images/foodtruck-main.png';
import UserMarkerIcon from './assets/images/user-marker.png';
import NearbyFoodTruckMarkerIcon from './assets/images/foodtruck-marker.png';
import SanFrancisco from './assets/images/sanfrans.png';

import { withScriptjs,
  withGoogleMap,
  GoogleMap,
  Marker,
  InfoWindow,  } from "react-google-maps";
 

import FoodTruckService from './Service/FoodTruckService';


class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      centerMap: {lat: 37.78813948, lng: -122.3925795},
      latitude: 37.78813948,
      longitude: -122.3925795,
      distantMiles: 20,
      noOfResult: 10,
      nearestFoodTrucks: [],
      selectedMarker: false
    };

    this.foodtruckService = new FoodTruckService();

    //this.getUserCurrentLocation();
  }

  render() {

    const labelSize = { 
      text: "You are here",
      color: "#4682B4",
      fontSize: "25px",
      fontWeight: "bold",
      x: '200',
      y: '0'
    };
  

    const FoodTruckMap = withScriptjs(withGoogleMap((props) =>{
                      
      return (
          <GoogleMap
            defaultZoom={17}
            onChange={this.onMapChange}
            center={this.state.centerMap}>
            
            
            {this.renderUserMapMarker()}

            {/* render food truck markers */}
            {
             
              this.state.nearestFoodTrucks.map(foodTruckInfo => {
                  const onClick = props.onClick.bind(this, foodTruckInfo)
                  return(
                    
                    <Marker
                        key= {foodTruckInfo.id}
                        onClick={onClick}
                        label= {{
                          text: foodTruckInfo.applicant,
                          color: "#4682B4",
                          fontSize: "20px",
                          fontWeight: "bold",
                          background: '#fff'
                        }}
                        icon= {{
                          url: NearbyFoodTruckMarkerIcon,
                          scaledSize: {width: 80, height: 90},
                          labelOrigin: {x: 40, y: -12}
                        }}
                        position={{ lat: foodTruckInfo.latitude, lng: foodTruckInfo.longitude }}>

                              {
                              props.selectedMarker === foodTruckInfo &&
                                <InfoWindow>
                                  <div style={{fontSize: '25px', fontFamily: 'Segoe UI'}}>
                                    <p style={{color: 'blue'}}>{foodTruckInfo.applicant}</p>
                                    <hr />
                                    <p>{foodTruckInfo.foodItems}</p>
                                    <hr />
                                    <p>{foodTruckInfo.address}</p>
                                    <hr />
                                    <p>{foodTruckInfo.locationDescription}</p>
                                  </div>
                                </InfoWindow>
                              }

                    </Marker>
                  )
                                  
              })
            }  
          </GoogleMap>
        );
      }
    ));

    return (
      <div class="container-fluid">
          <div class="row flex-nowrap">
              <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark">
                  <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">

                    <div class="row" style={{marginTop: '20px', marginBottom: '20px', fontSize: '25'}}>
                      <div style= {{position: "relative"}}>
                        <h2 style={{color: 'yellow', fontFamily: "Segoe UI", position: "absolute", marginTop: "10px"}}>
                          Food Truck Junkie
                        </h2>
                        <img src={SanFrancisco} style={{ width:"100%", height:"200px" }} alt="" />
                       </div>
                    </div>
                    <div class="row" style={{marginTop: '10px'}}>
                      <form>
                        <div class="form-group">
                          <label for="exampleInputEmail1">Latitude</label>
                          <input type="text" class="form-control"  placeholder="your current latitude"
                              onChange={this.latitudeChanged} value={this.state.latitude} />
                        </div>
                      </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                        <form>
                              <div class="form-group">
                              <label for="exampleInputEmail1">Longitude</label>
                                  <input type="text" class="form-control"  placeholder="your current longitude"
                                      onChange={this.longitudeChanged} value={ this.state.longitude } />
                              </div>
                        </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                      <form>
                        <div class="form-group">
                            <label for="exampleInputEmail1">Search within X miles</label>
                            <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="search within X miles" 
                            onChange={this.distantMilesChanged} value={ this.state.distantMiles }/>
                        </div>
                      </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                      <form>
                            <div class="form-group">
                              <label for="exampleInputEmail1">No. of result</label>
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="no. of result" 
                                    onChange={this.noOfResultChanged} value= { this.state.noOfResult } />
                            </div>
                        </form>
                    </div>
                    <br />
                    <br />
                    <div class="row" style={{marginTop: '10px', width: '100%'}}>
                      <button type="submit" class="btn btn-primary" style={{width: '100%'}} onClick={this.searchNearestFoodTrucks}> Search</button>
                    </div>  
                    <div class="row" style={{marginTop: '20px', width: '100%'}}>
                      <img src={FoodTruckImage} style={{ width:"100%", height:"150px" }} alt="" />
                    </div>                
                  </div>
              </div>
              <div class="col py-3">
                <FoodTruckMap
                  selectedMarker={this.state.selectedMarker}
                  onClick={this.handleMarkerClick}
                  markers={this.state.nearestFoodTrucks}
                  center={this.state.centerMap}
                  googleMapURL={`https://maps.googleapis.com/maps/api/js?key=AIzaSyC0rNODpJvayMKar-t5OC6OEDhmJZSaDKE&v=3.exp&libraries=geometry,drawing,places`}
                  loadingElement={<div style={{ height: `100%` }} />}
                  containerElement={<div style={{ height: `100%`, width: `100%` }} />}
                  mapElement={<div style={{ height: `100%` }} />}
                  
                />
              </div>
          </div>
      </div>
    );
  } //render

  handleMarkerClick = (marker, event) => {
    // console.log({ marker })
    this.setState({ selectedMarker: marker })
  }

  renderUserMapMarker() {

    return (
      this.state.latitude != '' && this.state.longitude != '' ?
      (
         <Marker
            key="999"      
            label= {{
              text: "You are here",
              color: "#4682B4",
              fontSize: "20px",
              fontWeight: "bold"
            }}
            icon= {{
              url: UserMarkerIcon,
              scaledSize: {width: 80, height: 90},
              labelOrigin: {x: 40, y: -12}
            }}
            position={{ lat: parseFloat(this.state.latitude), lng: parseFloat(this.state.longitude) }} />
      ) : 
      (
        ''
      )
    );
  }

//   getUserCurrentLocation = () => {
//     if (navigator.geolocation) {
//         navigator.geolocation.getCurrentPosition(this.userCurrentLocationHandler);
//       } 
//   }

//   userCurrentLocationHandler = (position) => {
//     var lat = position.coords.latitude;
//     var long = position.coords.longitude;

//     this.setState({
//       centerMap: {lat: parseFloat(lat), lng: parseFloat(long)}
//     })
//  }

latitudeChanged = (event) => {
    this.setState({
        latitude: event.target.value,
        centerMap: {lat: parseFloat(event.target.value), lng: parseFloat(this.state.longitude)}
    });

    this.setState({nearestFoodTrucks: []});

    this.renderUserMapMarker();
}

longitudeChanged = (event) => {
    this.setState({
        longitude: event.target.value,
        centerMap: {lat: parseFloat(this.state.latitude), lng: parseFloat(event.target.value)}
    });

    this.setState({nearestFoodTrucks: []});

    this.renderUserMapMarker();
}

distantMilesChanged = (event) => {
    this.setState({
        distantMiles: event.target.value
    })
}

noOfResultChanged = (event) => {
    this.setState({
        noOfResult: event.target.value
    })
}



searchNearestFoodTrucks = () => {
    if(!this.state.latitude || !this.state.longitude || 
      !this.state.distantMiles || !this.state.noOfResult) {
          alert('All fields need to be filled up');
          return;
      }

      this.renderUserMapMarker();

      this.setState({foodtruckResult: []}); //reset prev result

      var thisComp = this;
    
      this.foodtruckService.searchNearestFoodTruck
        (this.state.latitude, this.state.longitude, this.state.distantMiles, this.state.noOfResult,
            function(result) {
                if(result.hasError) {
                    alert('An error has occurred, likely input is incorrect. Try entering valid latitude and longitude');
                }
            
                thisComp.setState({nearestFoodTrucks:result.nearestFoodTrucks });                
            },
            function(error) {
                alert(error);
            });
}

}

export default App;
