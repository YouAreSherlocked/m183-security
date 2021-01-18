import React, { Component } from 'react';
import { NavLink } from 'react-router-dom'; 

class Header extends Component {
  logout() {
      localStorage.removeItem('auth')
      localStorage.setItem('sessionActive', 0)
      window.open('/', '_self')
  }

  render () {
    const loggedIn = parseInt(localStorage.getItem('sessionActive')) === 1
    return (
      <header>
        <h1><a href="/">M183</a></h1>
        <nav>
            <NavLink to="/">Home</NavLink>
            <NavLink to="/files">Shell Injection</NavLink>
            <NavLink to="/login">Login</NavLink>
            <NavLink to="/register">Register</NavLink>
            <NavLink to="/knights/all">Knights</NavLink>
            <NavLink to="/knights/new">New Knight</NavLink>
            <NavLink to="/users">Users</NavLink>
        </nav>
        { loggedIn ?
        <button className="btn btn-logout" onClick={this.logout}>Logout</button>
        : null }
      </header>
    );
  }
}

export default Header;
