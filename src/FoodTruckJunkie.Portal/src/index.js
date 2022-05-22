import React from 'react';
// import ReactDOM from 'react-dom/client';
import ReactDOM from "react-dom";

import App from './App';

import './index.css';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle'

import AppConfig from './AppConfig'

import axios from 'axios';
axios.defaults.baseURL = AppConfig.BaseAPIUrl(); 
axios.defaults.headers.post['Content-Type'] = 'application/json';

// const root = ReactDOM.createRoot(document.getElementById('root'));
// root.render(
//   <React.StrictMode>
//     <App />
//   </React.StrictMode>
// );
ReactDOM.render(
  <App />,
document.getElementById("root"));
