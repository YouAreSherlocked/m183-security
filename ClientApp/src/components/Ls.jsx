import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5000'

class Ls extends Component {

  constructor(props) {
    super(props)

    this.state= {
      output: null
    }

    this.submit = this.submit.bind(this)
  }

  async submit(e) {
    e.preventDefault()
    const cmd = e.target['cmd'].value
    await fetch(`${API_URL}/ls`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(cmd)
    }).then(res => this.setState({output: res.json()}))
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
        <p>{ this.state.output }</p>
      </Fragment>
    );
  }
}

export default Ls;
