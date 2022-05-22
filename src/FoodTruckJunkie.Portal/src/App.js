import React, { Component } from "react";
import './App.css';
import SideBar from './SideBar';
import Main from './Main';


class App extends Component {

  constructor(props) {
    super(props);

    this.state = {};


  }

  render() {
    return (
      
      <div className="App" class = "container-fluid">
         <div class="row">
            <SideBar />
            <Main />
         </div>  
      </div>
    );
  }
}

export default App;
