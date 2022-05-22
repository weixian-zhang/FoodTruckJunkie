import React, { Component } from "react";
// import { Map, GoogleApiWrapper } from 'google-maps-react';
import { withScriptjs,
    withGoogleMap,
    GoogleMap,
    Marker,
    InfoWindow  } from "react-google-maps";

export default class MapContainer extends Component {

    constructor(props) {
        super(props);

        this.state = {};
     
    }

    

    render() {

        const center = {
            lat: -3.745,
            lng: -38.523
        };

        const FoodTruckMap = withScriptjs(withGoogleMap((props) =>{

            // const markers = props.doctors.map( doctor => <DoctorMarker
            //                   key={doctor.uid}
            //                   doctor={doctor}
            //                   location={center}
            //                 />);
                            
            return (
                <GoogleMap
                  defaultZoom={14}
                  center={ { lat:  42.3601, lng: -71.0589 } }
                  >
                  //loop array to create markers
                  <Marker
                    position={{ lat: 37.78813948, lng: -122.3925795 }}
                    />
                </GoogleMap>
              );
            }
          ))  
    
        return (
            <div>
               <FoodTruckMap
				doctors={this.props.doctors}
				googleMapURL={`https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY_HERE&v=3.exp&libraries=geometry,drawing,places`}
				loadingElement={<div style={{ height: `100%` }} />}
				containerElement={<div style={{ height: `600px`, width: `600px` }} />}
				mapElement={<div style={{ height: `100%` }} />} />
                
            </div>
        );
    }

}

// export default GoogleApiWrapper({
//     apiKey: 'AIzaSyC0rNODpJvayMKar-t5OC6OEDhmJZSaDKE'
//   })(MapContainer);
  