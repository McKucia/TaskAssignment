import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import TaskGroups from './components/TaskGroups';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Route exact path='/task-groups' component={TaskGroups} />
    );
  }
}
