import React, { Component } from 'react';
import { SignIn } from './SignInPage/signIn';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <SignIn />
      </div>
    );
  }
}
