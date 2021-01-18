import React, { Component } from 'react';
import { Switch, Route, BrowserRouter } from 'react-router-dom';

import Home from './components/Home';
import Login from './components/Login';
import Files from './components/Files';
import Register from './components/Register';
import Post from './components/Post';
import Knights from './components/Knights';
import Users from './components/Users';
import EditKnight from './components/EditKnight';

class AppRouter extends Component {

    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route exact path="/" component={ Home }></Route>
                    <Route exact path="/files" component={ Files }></Route>
                    <Route exact path="/login" component={ Login }></Route>
                    <Route exact path="/register" component={ Register }></Route>
                    <Route exact path="/knights/all" component={ Knights }></Route>
                    <Route exact path="/knights/new" component={ Post }></Route>
                    <Route exact path="/knights/update/:id" component={ EditKnight }></Route>
                    <Route exact path="/users" component={ Users }></Route>
                    <Route component={ Home }></Route>
                </Switch>
            </BrowserRouter>
        );
    }
}

export default AppRouter;