import React, { Component } from "react";
import FoodTruckImage from './assets/images/foodtruck-main.png';

import FoodTruckService from './Service/FoodTruckService';

export default class SideBar extends Component {

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

            <div class="col">
                <div class="row flex-nowrap">
                    <div class="col-auto col-md-3 col-xl-2 px-sm-2 px-0 bg-dark">
                        <div class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
                            <a href="/" class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none">
                                <span class="fs-5 d-none d-sm-inline">Food Truck Junkie</span>
                            </a>
                            <ul class="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
                                <li class="nav-item">
                                    <a href="#" class="nav-link align-middle px-0">
                                        <i class="fs-4 bi-house"></i> <span class="ms-1 d-none d-sm-inline">Home</span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#submenu1" data-bs-toggle="collapse" class="nav-link px-0 align-middle">
                                        <i class="fs-4 bi-speedometer2"></i> <span class="ms-1 d-none d-sm-inline">Dashboard</span> </a>
                                    <ul class="collapse show nav flex-column ms-1" id="submenu1" data-bs-parent="#menu">
                                        <li class="w-100">
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Item</span> 1 </a>
                                        </li>
                                        <li>
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Item</span> 2 </a>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <a href="#" class="nav-link px-0 align-middle">
                                        <i class="fs-4 bi-table"></i> <span class="ms-1 d-none d-sm-inline">Orders</span></a>
                                </li>
                                <li>
                                    <a href="#submenu2" data-bs-toggle="collapse" class="nav-link px-0 align-middle ">
                                        <i class="fs-4 bi-bootstrap"></i> <span class="ms-1 d-none d-sm-inline">Bootstrap</span></a>
                                    <ul class="collapse nav flex-column ms-1" id="submenu2" data-bs-parent="#menu">
                                        <li class="w-100">
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Item</span> 1</a>
                                        </li>
                                        <li>
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Item</span> 2</a>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <a href="#submenu3" data-bs-toggle="collapse" class="nav-link px-0 align-middle">
                                        <i class="fs-4 bi-grid"></i> <span class="ms-1 d-none d-sm-inline">Products</span> </a>
                                        <ul class="collapse nav flex-column ms-1" id="submenu3" data-bs-parent="#menu">
                                        <li class="w-100">
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Product</span> 1</a>
                                        </li>
                                        <li>
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Product</span> 2</a>
                                        </li>
                                        <li>
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Product</span> 3</a>
                                        </li>
                                        <li>
                                            <a href="#" class="nav-link px-0"> <span class="d-none d-sm-inline">Product</span> 4</a>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <a href="#" class="nav-link px-0 align-middle">
                                        <i class="fs-4 bi-people"></i> <span class="ms-1 d-none d-sm-inline">Customers</span> </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    
                </div>
            </div>


            //top bar
            // <div>
            //     {/* <div class="row">
                    
            //     </div> */}
            //     <div class="row" style={{marginTop: '10px'}}>
            //         <div class="col-sm">
            //             <img src={FoodTruckImage} style={{ width:"400px", height:"300px" }} alt="" />
            //         </div>
            //         <div class="col-sm">
            //             <form>
            //                 <div class="form-group">
            //                     <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="latitude"
            //                         onChange={this.latitudeChanged} value={this.state.latitude} />
            //                 </div>
            //             </form>
            //             <br />
            //             <form>
            //                 <div class="form-group">
            //                     <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="longitude"
            //                         onChange={this.longitudeChanged} value={ this.state.longitude } />
            //                 </div>
            //             </form>
            //             <br />
            //             <form>
            //                 <div class="form-group">
            //                     <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="search within X miles" 
            //                     onChange={this.distantMilesChanged} value={ this.state.distantMiles }/>
            //                 </div>
            //             </form>
            //             <br />
            //             <form>
            //                 <div class="form-group">
            //                     <input type="text" class="form-control" aria-describedby="emailHelp" placeholder="number of food trucks as result" 
            //                         onChange={this.noOfResultChanged} value= { this.state.noOfResult } />
            //                 </div>
            //             </form>
            //         </div>
            //         <div class="col-sm">
            //             <button type="submit" class="btn btn-primary" onClick={this.searchNearestFoodTrucks}> Search</button>
            //             {/* style= {{ width: "100%", height: "100%" }}>Search</button> */}
            //         </div>
                    
            //     </div>
                
            // </div>
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