import React, { Component, Fragment } from 'react';
import Recaptcha from 'react-recaptcha';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Register extends Component {
  constructor(props) {
    super(props)

    this.state = {
      captchaVerified: false
    }

    this.handleCaptcha = this.handleCaptcha.bind(this)
  }
  async register(e) {
    e.preventDefault()
    const user = e.target['user'].value
    const pw = e.target['Password'].value
    const cpw = e.target['ConfirmPassword'].value
    if (this.state.captchaVerified) {

      await fetch(`${API_URL}/api/users/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'apikey': 'showdown'
        },
        body: JSON.stringify({'Nickname': user, 'Password': pw, ConfirmPassword: cpw})
      }).then(res => res.text()).then(res => this.setState({output: res}))
      window.open('/register', '_self')

    } else {
      alert("please resolve ReCaptcha")
    }

  }

  handleCaptcha() {
    console.log('verified captcha')
    this.setState({captchaVerified: true})
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>Register</h2>
        <form onSubmit={e => this.register(e)}>
            <label htmlFor="user">User</label>
            <input type="text" name="user" id="user"></input>
            <label htmlFor="Password">Password</label>
            <input type="password" name="Password" id="Password"></input>
            <label htmlFor="ConfirmPassword">Confirm Password</label>
            <input type="password" name="ConfirmPassword" id="ConfirmPassword"></input>
            <input type="submit" name="submit" id="submit" value="submit"></input>
        </form>
        <Recaptcha
              sitekey="6LeR1i0aAAAAAP5MHqZP9WUzhTJpx5CkJEDARzBg"
              render='explicit'
              onloadCallback={console.log('loaded captcha')}
              verifyCallback={this.handleCaptcha}
            />
      </Fragment>
    );
  }
}

export default Register;
