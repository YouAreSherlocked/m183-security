import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Knights extends Component {

  constructor(props) {
      super(props)

      this.state = {
          knights: []
      }

  }
  async componentDidMount() {
    await fetch(`${API_URL}/api/posts/getall`, {
      method: 'GET'
    }).then(res => res.json()).then(res => this.setState({knights: res}))
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h1>Knights</h1>
        { this.state.knights ? 
            this.state.knights.map((knight, i) => (
                <Fragment>
                    <h2>{ knight.text }</h2>
                    <div className="knight-img" style={{backgroundImage: "url(" + knight.imageUrl + ")"}}></div>
                </Fragment>
            )) : null}
      </Fragment>
    );
  }
}

export default Knights;
