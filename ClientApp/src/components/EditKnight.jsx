import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class EditKnight extends Component {

    constructor(props) {
        super(props)

        this.state= {
            output: '',
            text: '',
            imageUrl: ''
        }
        this.onInputchange = this.onInputchange.bind(this);
        this.submit = this.submit.bind(this)
    }

    componentDidMount() {
        this.setState({
            text: this.props.location.state.knight.text,
            imageUrl: this.props.location.state.knight.imageUrl
        })
    }

    onInputchange(event) {
        this.setState({
          [event.target.name]: event.target.value
        });
      }

    async submit(e) {
        e.preventDefault()
        const text = e.target['text'].value
        const imageUrl = e.target['imageUrl'].value
        await fetch(`${API_URL}/api/posts/update?id=${this.props.location.state.knight.id}`, {
            method: 'PATCH',
            headers: {
                'Authorization': localStorage.getItem('auth'),
                'Content-Type': 'application/json',
                'apikey': 'showdown'
            },
            body: JSON.stringify({ 'text': text, 'imageUrl': imageUrl})
        }).then(res => res.text()).then(res => this.setState({output: res}))
        window.open('/knights/all', '_self')
    }

  render () {
    return (
      <Fragment>
        <Header></Header>
        <h2>{ this.props.location.state.knight.text }</h2>
        <form onSubmit={ e => this.submit(e) }>
                <label htmlFor="text">Text</label>
                <input type="text" name="text" id="text" value={this.state.text} onChange={this.onInputchange}></input>
                <label htmlFor="imageUrl">Image (Url)</label>
                <input type="text" name="imageUrl" id="imageUrl" value={this.state.imageUrl} onChange={this.onInputchange}></input>
                <input type="submit" name="submit" id="submit" value="submit"></input>
            </form>
      </Fragment>
    );
  }
}

export default EditKnight;
