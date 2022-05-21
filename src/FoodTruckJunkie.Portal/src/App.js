import React, { Component } from "react";
import './App.css';
import TopBar from './TopBar';
import Main from './Main';


class App extends Component {

  constructor(props) {
    super(props);

    this.state = {};


  }

  render() {
    return (
      <div className="App" class = "container">
          <TopBar />
          <Main />
      </div>
    );
  }
}

export default App;
