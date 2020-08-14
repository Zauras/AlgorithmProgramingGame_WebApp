import React, { Component } from 'react';
import { Route } from 'react-router-dom';

import { Layout } from './components/Layout';

import './custom.css';
import CodeSubmissionForm from './features/codeSubmission/CodeSubmissionForm';
import TopScoresList from './features/scoreList/TopScoresList';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={TopScoresList} />
                <Route exact path='/code-submission' component={CodeSubmissionForm} />
            </Layout>
        );
    }
}
