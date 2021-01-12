import React, { Component, Fragment } from 'react';
import Header from './Header';
const API_URL = 'http://localhost:5001'

class Post extends Component {

    constructor(props) {
        super(props)

        this.state= {
            output: ''
        }

        this.submit = this.submit.bind(this)
    }

    async submit(e) {
        e.preventDefault()
        const image = e.target['image'].value
        const text = e.target['text'].value
        await fetch(`${API_URL}/api/...`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ 'image': image, 'text': text})
        }).then(res => res.text()).then(res => this.setState({output: res}))
    }

    render () {
    return (
        <Fragment>
        <Header></Header>
        <h2>Post new Knight</h2>
        <form onSubmit={ e => this.submit(e) }>
            <label htmlFor="image">Image (Url)</label>
            <input type="text" name="image" id="image"></input>
            <label htmlFor="text">Text</label>
            <input type="text" name="text" id="text"></input>
            <input type="submit" name="submit" id="submit" value="submit"></input>
        </form>
        </Fragment>
    );
    }
}

export default Post;
