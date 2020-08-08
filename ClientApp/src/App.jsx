import React, { Component } from 'react';
import { Route } from 'react-router-dom';

import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css';
import CodeSubmissionForm from './features/codeSubmission/CodeSubmissionForm';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/code-submission' component={CodeSubmissionForm} />
                <Route exact path='/scores' component={Home} />
                <Route exact path='/' component={Home} />
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
            </Layout>
        );
    }
}
