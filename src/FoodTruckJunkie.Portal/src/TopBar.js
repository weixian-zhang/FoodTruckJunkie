import React, { Component } from "react";
import FoodTruckImage from './assets/images/foodtruck-main.png';

import FoodTruckService from './Service/FoodTruckService';

export default class TopBar extends Component {

    constructor(props) {
        super(props);

        this.state = {
            latitude: '',
            longitude: '',
            distantMiles: 20,
            noOfResult: 10
        };

        this.foodtruckService = new FoodTruckService();
     
    }

    render() {
        return (
            <div>
                {/* <div class="row">
                    
                </div> */}
                <div class="row" style={{marginTop: '10px'}}>
                    <div class="col-sm">
                        <img src={FoodTruckImage} style={{ width:"400px", height:"300px" }} alt="" />
                    </div>
                    <div class="col-sm">
                        <form>
                            <div class="form-group">
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="latitude"
                                    onChange={this.latitudeChanged} value={this.state.latitude} />
                            </div>
                        </form>
                        <br />
                        <form>
                            <div class="form-group">
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="longitude"
                                    onChange={this.longitudeChanged} value={ this.state.longitude } />
                            </div>
                        </form>
                        <br />
                        <form>
                            <div class="form-group">
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="search within X miles" 
                                onChange={this.distantMilesChanged} value={ this.state.distantMiles }/>
                            </div>
                        </form>
                        <br />
                        <form>
                            <div class="form-group">
                                <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="number of food trucks as result" 
                                    onChange={this.noOfResultChanged} value= { this.state.noOfResult } />
                            </div>
                        </form>
                    </div>
                    <div class="col-sm">
                        <button type="submit" class="btn btn-primary" onClick={this.searchNearestFoodTrucks}> Search</button>
                        {/* style= {{ width: "100%", height: "100%" }}>Search</button> */}
                    </div>
                    
                </div>
                
            </div>
        );
    }

    getUserCurrentLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(this.userCurrentLocationHandler);
          } 
    }

    userCurrentLocationHandler(position) {
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
        })
    }

    longitudeChanged = (event) => {
        this.setState({
            longitude: event.target.value
        })
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
        
          this.foodtruckService.searchNearestFoodTruck
            (this.state.latitude, this.state.longitude, this.state.distantMiles, this.state.noOfResult,
                function(result) {
                    if(result.hasError) {
                        alert('An error has occurred, likely input is incorrect. Try entering valid latitude and longitude');
                    }
                    alert('successful!');
                },
                function(error) {
                    alert(error);
                });
    }

}