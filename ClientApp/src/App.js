import React, { Component } from 'react';
import AppRouter from './AppRouter';
import './custom.css';

class App extends Component {

  render() {
    return (
        <React.Fragment>
          <AppRouter />
        </React.Fragment>
    );
  }
}

export default App;