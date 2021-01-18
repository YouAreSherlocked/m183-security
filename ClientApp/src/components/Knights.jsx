import React, { Component, Fragment } from 'react';
import { Link } from 'react-router-dom';
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

  async deleteKnight(id) {
    await fetch(`${API_URL}/api/posts/delete?id=${id}`, {
      method: 'DELETE',
      headers: {
          'Authorization': localStorage.getItem('auth'),
          'Content-Type': 'application/json',
          'apikey': 'showdown'
      },
    }).then(res => res.text()).then(res => this.setState({output: res}))
    window.open('/knights/all', '_self')
  }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h1>Knights</h1>
        { this.state.unauthorized ? <p>Please Log In to see the content.</p> : this.state.knights ? 
            this.state.knights.map((knight, i) => (
                <div className="knight-wrapper" key={i}>
                    <h2>{ knight.text }</h2>
                    <img className="knight-img" src={knight.imageUrl}></img>
                    <div>
                      <Link to={{ pathname: `/knights/update/${knight.id}`, state: {knight: knight} }}><button className="btn btn-edit" onClick={this.editKnight}>Edit</button></Link>
                      <button className="btn btn-delete" onClick={() => this.deleteKnight(knight.id)}>Delete</button>
                    </div>
                </div>
            )) : null}
      </Fragment>
    );
  }
}

export default Knights;
