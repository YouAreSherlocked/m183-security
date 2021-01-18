import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Users extends Component {
    constructor(props) {
        super(props)
  
        this.state = {
            users: [],
            unauthorized: false
        }
  
    }
    async componentDidMount() {
        await fetch(`${API_URL}/api/users/getall`, {
            method: 'GET',
            headers: {
            'apikey': 'showdown',
            'Authorization': localStorage.getItem('auth')
            }
        }).then(res => res.json()).then(res => this.setState({users: res})).catch((error) => {
            this.setState({unauthorized: true})
        });
    }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h1>Users</h1>
        { this.state.unauthorized ? <p>Please Log In as Admin to see the content.</p> : this.state.users ? 
            <ul>
            {this.state.users.map((user, i) => (
                <Fragment key={i}>
                    <li>{ user.nickname }</li>
                </Fragment>
            ))} </ul> : null}
      </Fragment>
    );
  }
}

export default Users;
