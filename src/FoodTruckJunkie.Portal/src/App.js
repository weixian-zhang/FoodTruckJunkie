import React, { Component } from "react";
import './App.css';
import FoodTruckImage from './assets/images/foodtruck-main.png';
import UserMarkerIcon from './assets/images/user-marker.png';
import NearbyFoodTruckMarkerIcon from './assets/images/foodtruck-marker.png';

import { withScriptjs,
  withGoogleMap,
  GoogleMap,
  Marker,
  InfoWindow  } from "react-google-maps";
  

import FoodTruckService from './Service/FoodTruckService';


class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      latitude: 37.78813948,
      longitude: -122.3925795,
      distantMiles: 20,
      noOfResult: 10,
      foodtruckResult: {}
    };

    this.foodtruckService = new FoodTruckService();


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
    const marketIconSize = {
      width: 60, height: 80
    };

    const labelPadding = 8;

    const FoodTruckMap = withScriptjs(withGoogleMap((props) =>{
                      
      return (
          <GoogleMap
            defaultZoom={14} 
            google={this.props.google}
            center={{lat:this.state.latitude, lng: this.state.longitude}}
            >
            //user marker
            {
              this.state.latitude != undefined && this.state.longitude != undefined ?
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
                      scaledSize: {width: 60, height: 80}
                    }}
                    position={{ lat: this.state.latitude, lng: this.state.longitude }} />
              ) : 
              (
                ''
              )
            }
            
            //food truck markers
            {
              this.state.foodtruckResult.nearestFoodTrucks != undefined ?
              (
                this.state.foodtruckResult.nearestFoodTrucks.forEach((foodTruckInfo) => {
                    
                  <Marker
                    key= {foodTruckInfo.applicant }  
                    label= {{
                      text: foodTruckInfo.applicant,
                      color: "#4682B4",
                      fontSize: "20px",
                      fontWeight: "bold"
                    }}
                    icon= {{
                      url: NearbyFoodTruckMarkerIcon,
                      scaledSize: {width: 60, height: 80}
                    }}
                    position={{ lat: foodTruckInfo.latitude, lng: foodTruckInfo.longitude }} />
                  })
              ) :
              (
                ''
              )
            }

            {/* <Marker
            key="999"      
            label='You are here'    
            position={{ lat: this.state.latitude, lng: this.state.longitude }} /> */}
          </GoogleMap>
        );
      }
    ));

    return (
      <div class="container-fluid">
          <div class="row flex-nowrap">
              <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark">
                  <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">

                    <div class="row">
                      <img src={FoodTruckImage} style={{ width:"100%", height:"150px" }} alt="" />
                    </div>

                    <div class="row" style={{marginTop: '20px'}}>
                      <form>
                        <div class="form-group">
                          <input type="text" class="form-control"  placeholder="your current latitude"
                              onChange={this.latitudeChanged} value={this.state.latitude} />
                        </div>
                      </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                        <form>
                              <div class="form-group">
                                  <input type="text" class="form-control"  placeholder="your current longitude"
                                      onChange={this.longitudeChanged} value={ this.state.longitude } />
                              </div>
                        </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                      <form>
                              <div class="form-group">
                                  <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="search within X miles" 
                                  onChange={this.distantMilesChanged} value={ this.state.distantMiles }/>
                              </div>
                      </form>
                    </div>

                    <div class="row" style={{marginTop: '10px'}}>
                      <form>
                            <div class="form-group">
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="no. of result" 
                                    onChange={this.noOfResultChanged} value= { this.state.noOfResult } />
                            </div>
                        </form>
                    </div>

                    <div class="row" style={{marginTop: '10px', width: '100%'}}>
                      <button type="submit" class="btn btn-primary" style={{width: '100%'}} onClick={this.searchNearestFoodTrucks}> Search</button>
                    </div>                  
                  </div>
              </div>
              <div class="col py-3">
                <FoodTruckMap
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

  getUserCurrentLocation = () => {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(this.userCurrentLocationHandler);
      } 
  }

  userCurrentLocationHandler = (position) => {
    var lat = position.coords.latitude;
    var long = position.coords.longitude;

    this.setState({
        latitude: lat,
        longitude: long
    })
 }

latitudeChanged = (event) => {
    this.setState({
        latitude: event.target.value
    });

    this.renderUserLocationMarker();
}

longitudeChanged = (event) => {
    this.setState({
        longitude: event.target.value
    });

    this.renderUserLocationMarker();
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

renderUserLocationMarker() {

  return (
    <div>
      
    </div>
  );
}

searchNearestFoodTrucks = () => {
    if(!this.state.latitude || !this.state.longitude || 
      !this.state.distantMiles || !this.state.noOfResult) {
          alert('All fields need to be filled up');
          return;
      }

      this.setState({foodtruckResult: []}); //reset prev result

      var thisComp = this;
    
      this.foodtruckService.searchNearestFoodTruck
        (this.state.latitude, this.state.longitude, this.state.distantMiles, this.state.noOfResult,
            function(result) {
                if(result.hasError) {
                    alert('An error has occurred, likely input is incorrect. Try entering valid latitude and longitude');
                }
                thisComp.setState({foodtruckResult: result});
            },
            function(error) {
                alert(error);
            });
}

}

export default App;
