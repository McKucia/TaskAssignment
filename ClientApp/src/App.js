import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import TaskGroups from './components/TaskGroups';
import Group from './components/Group';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <div>
          <Route exact path='/task-groups' component={TaskGroups} />
          <Route path='/task/:id' component={Group} />
        </div>
    );
  }
}
