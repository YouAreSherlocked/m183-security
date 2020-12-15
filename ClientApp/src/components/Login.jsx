import React, { Component, Fragment } from 'react';
import Header from './Header';

class Login extends Component {

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>Login</h2>
        <form>
            <label for="user">User</label>
            <input type="text" name="user" id="user"></input>
            <label for="password">Password</label>
            <input type="text" name="password" id="password"></input>
            <input type="submit" name="submit" id="submit" value="submit"></input>
        </form>
      </Fragment>
    );
  }
}

export default Login;
