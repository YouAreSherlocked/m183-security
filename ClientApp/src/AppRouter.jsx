import React, { Component } from 'react';
import { Switch, Route, BrowserRouter } from 'react-router-dom';

import Home from './components/Home';
import Login from './components/Login';
import Ls from './components/Ls';
import Register from './components/Register';
import Post from './components/Post';

class AppRouter extends Component {

    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route exact path="/" component={ Home }></Route>
                    <Route exact path="/ls" component={ Ls }></Route>
                    <Route exact path="/login" component={ Login }></Route>
                    <Route exact path="/register" component={ Register }></Route>
                    <Route exact path="/post" component={ Post }></Route>
                    <Route component={ Home }></Route>
                </Switch>
            </BrowserRouter>
        );
    }
}

export default AppRouter;