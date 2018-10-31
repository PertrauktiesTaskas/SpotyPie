import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter} from 'react-router-dom';
import Home from './components/Home';
import registerServiceWorker from './registerServiceWorker';
/*
import 'font-awesome/css/font-awesome.min.css';
*/

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <Home/>
    </BrowserRouter>,
    rootElement);

registerServiceWorker();
