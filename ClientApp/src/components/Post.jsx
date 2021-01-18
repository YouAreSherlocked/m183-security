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
        const text = e.target['text'].value
        const imageUrl = e.target['image'].value
        await fetch(`${API_URL}/api/posts/create`, {
            method: 'POST',
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
            <h2>Post new Knight</h2>
            <form onSubmit={ e => this.submit(e) }>
                <label htmlFor="text">Text</label>
                <input type="text" name="text" id="text"></input>
                <label htmlFor="image">Image (Url)</label>
                <input type="text" name="image" id="image"></input>
                <input type="submit" name="submit" id="submit" value="submit"></input>
            </form>
        </Fragment>
    );
    }
}

export default Post;
