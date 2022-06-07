import React from 'react';
import { ApplicationInsights } from '@microsoft/applicationinsights-web'
import ReactDOM from "react-dom";

import App from './App';

import './index.css';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle'

import AppConfig from './AppConfig'

import axios from 'axios';
axios.defaults.baseURL = AppConfig.BaseAPIUrl(); 
axios.defaults.headers.post['Content-Type'] = 'application/json';


const appInsights = new ApplicationInsights({ config: {
  connectionString: 'InstrumentationKey=2138a114-8c3e-46b0-9471-b457cd6ff642;IngestionEndpoint=https://centralus-2.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/'
} });
appInsights.loadAppInsights();
appInsights.trackPageView(); // Manually call trackPageView to establish the current user/session/pageview

ReactDOM.render(
  <App />,
document.getElementById("root"));
