import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Knights extends Component {

  constructor(props) {
      super(props)

      this.state = {
          knights: [],
          unauthorized: false
      }

  }
  async componentDidMount() {
    await fetch(`${API_URL}/api/posts/getall`, {
      method: 'GET',
      headers: {
          'Authorization': localStorage.getItem('auth'),
          'apikey': 'showdown'
      }
    }).then(res => res.json()).then(res => this.setState({knights: res})).catch((error) => {
        this.setState({unauthorized: true})
      })
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h1>Knights</h1>
        { this.state.unauthorized ? <p>Please Log In to see the content.</p> : this.state.knights ? 
            this.state.knights.map((knight, i) => (
                <Fragment key={i}>
                    <h2>{ knight.text }</h2>
                    <img className="knight-img" src={knight.imageUrl}></img>
                </Fragment>
            )) : null}
      </Fragment>
    );
  }
}

export default Knights;
