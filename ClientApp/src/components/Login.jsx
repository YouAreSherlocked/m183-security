import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Login extends Component {

  async login(e) {
    e.preventDefault()
    const user = e.target['user'].value
    const pw = e.target['password'].value

    await fetch(`${API_URL}/api/users/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({'nickname': user, 'password': pw})
    }).then(res => res.json()).then(res => localStorage.setItem('auth', `Bearer ${res.token}`))
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>Login</h2>
        <form onSubmit={e => this.login(e)}>
            <label htmlFor="user">User</label>
            <input type="text" name="user" id="user"></input>
            <label htmlFor="password">Password</label>
            <input type="password" name="password" id="password"></input>
            <input type="submit" name="submit" id="submit" value="submit"></input>
        </form>
      </Fragment>
    );
  }
}

export default Login;
