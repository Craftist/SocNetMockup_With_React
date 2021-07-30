import React, {Component} from 'react';
import {Route, Switch} from 'react-router-dom';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import {ApplicationPaths} from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import {MessengerPage} from "./components/messenger/MessengerPage";
import { NotFound } from './components/NotFound';
import { Token } from './components/Token';

export default class App extends Component {
    static displayName = App.name;
    
    render() {
        return (
            <Layout>
                <Switch>
                    <Route exact path='/' component={Home}/>
                    <AuthorizeRoute path='/messenger' component={MessengerPage} />
                    <AuthorizeRoute path='/token' component={Token} />
                    <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes}/>

                    <Route component={NotFound} />
                </Switch>
            </Layout>
        );
    }
}