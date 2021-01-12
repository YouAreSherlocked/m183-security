import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Register extends Component {

  async register(e) {
    e.preventDefault()
    const user = e.target['user'].value
    const pw = e.target['password'].value

    await fetch(`${API_URL}/api/users/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({'nickname': user, 'password': pw})
    }).then(res => res.text()).then(res => this.setState({output: res}))
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>Register</h2>
        <form onSubmit={e => this.register(e)}>
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

export default Register;
