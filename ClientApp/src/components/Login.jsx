import React, { Component, Fragment } from 'react';
import Header from './Header';
import  ReCAPTCHA  from 'react-recaptcha';
const API_URL = 'http://localhost:5001'

class Login extends Component {
  constructor(props) {
    super(props)

    this.state = {
      captchaVerified: false,
      sessionActive: false
    }

    this.handleCaptcha = this.handleCaptcha.bind(this)
  }

  componentDidMount() {
    console.log('o')
    const sessionActive = parseInt(localStorage.getItem('sessionActive'))
    if (sessionActive === 1) {
      this.setState({sessionActive: true})
    }
  }

  async login(e) {
    e.preventDefault()
    const user = e.target['user'].value
    const pw = e.target['password'].value

    if (this.state.captchaVerified) {
      await fetch(`${API_URL}/api/users/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'apikey': 'showdown'
        },
        body: JSON.stringify({'nickname': user, 'password': pw})
      }).then(res => res.json()).then(res => localStorage.setItem('auth', `Bearer ${res.token}`))
      localStorage.setItem('sessionActive', 1)
      this.setState({'sessionActive': true})
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
        <h2>Login</h2>
        {!this.state.sessionActive ?
          <Fragment>
            <form onSubmit={e => this.login(e)}>
                <label htmlFor="user">User</label>
                <input type="text" name="user" id="user"></input>
                <label htmlFor="password">Password</label>
                <input type="password" name="password" id="password"></input>
                <input type="submit" name="submit" id="submit" value="submit"></input>
            </form>
            <ReCAPTCHA
              sitekey="6LeR1i0aAAAAAP5MHqZP9WUzhTJpx5CkJEDARzBg"
              render='explicit'
              onloadCallback={console.log('loaded captcha')}
              verifyCallback={this.handleCaptcha}
            />
          </Fragment>
        : <p>You are logged in.</p> }
      </Fragment>
    );
  }
}

export default Login;
