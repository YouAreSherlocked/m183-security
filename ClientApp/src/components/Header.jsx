import React, { Component } from 'react';
import { NavLink } from 'react-router-dom'; 
const API_URL = 'http://localhost:5001'

class Header extends Component {
  logout() {
      localStorage.removeItem('auth')
      window.open('/', '_self')
  }

  render () {
    return (
      <header>
        <h1><a href="/">M183</a></h1>
        <nav>
            <NavLink to="/">Home</NavLink>
            <NavLink to="/files">Shell Injection</NavLink>
            <NavLink to="/login">Login</NavLink>
            <NavLink to="/register">Register</NavLink>
            <NavLink to="/knights">Knights</NavLink>
            <NavLink to="/post">New Knight</NavLink>
        </nav>
        <button className="btn btn-logout" onClick={this.logout}>Logout</button>
      </header>
    );
  }
}

export default Header;
