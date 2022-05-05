import React, {useState} from 'react';
import {Route, Switch} from 'react-router-dom';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import {ApplicationPaths} from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import {MessengerPage} from "./components/messenger/MessengerPage";
import {NotFound} from './components/NotFound';
import {Token} from './components/Token';
import {FetchData} from "./components/FetchData";
import {ThemeProvider} from "styled-components";
import { Styles } from './styles';


export type AppTheme = 'light' | 'dark';

export default function App() {
    const [theme, setTheme] = useState<AppTheme>("dark")

    return (
        <ThemeProvider theme={{}}>
            <>
                <Styles/>
                <Layout>
                    <Switch>
                        <Route exact path='/' component={Home}/>
                        <Route path='/fetch-data' component={FetchData}/>
                        <AuthorizeRoute path='/messenger' component={MessengerPage}/>
                        <AuthorizeRoute path='/token' component={Token}/>
                        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes}/>

                        <Route component={NotFound}/>
                    </Switch>
                </Layout>
            </>
        </ThemeProvider>
    );
}

App.displayName = App.name;