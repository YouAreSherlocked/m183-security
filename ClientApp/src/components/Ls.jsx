import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'https://localhost:5001'

class Ls extends Component {

  constructor(props) {
    super(props)

    this.state= {
      output: ''
    }

    this.submit = this.submit.bind(this)
  }

  async submit(e) {
    e.preventDefault()
    const cmd = e.target['cmd'].value
    await fetch(`${API_URL}/api/files?cmd=${cmd}`, {
      method: 'GET'
    }).then(res => res.text()).then(res => this.setState({output: res}))
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>Shell Injection</h2>
        <form onSubmit={ e => this.submit(e) }>
            <input type="text" name="cmd" id="cmd"></input>
            <input type="submit" name="submit" id="submit" value="submit"></input>
        </form>
        <h4>Output:</h4>
        <p>{ this.state.output }</p>
      </Fragment>
    );
  }
}

export default Ls;
