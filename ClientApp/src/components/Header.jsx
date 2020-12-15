import React, { Component } from 'react';
import { NavLink } from 'react-router-dom'; 
class Header extends Component {

  render () {
    return (
      <header>
        <h1><a href="/">M183</a></h1>
        <nav>
            <NavLink to="/">Home</NavLink>
            <NavLink to="/ls">Shell Injection</NavLink>
            <NavLink to="/login">Login</NavLink>
            <NavLink to="/register">Register</NavLink>
        </nav>
      </header>
    );
  }
}

export default Header;
