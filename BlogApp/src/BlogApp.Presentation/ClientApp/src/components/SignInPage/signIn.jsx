import React, { Component } from "react";
import { Logo } from "../Logo/logo";

export const SignIn = () => {
  return (
    <div className="signin-page">
      <div className="blog-logo">
        <Logo />
      </div>
      <div>Sign In</div>
      <form>
        <div>
          <label>Mail</label>
          <input/>
        </div>
        <div>
          <label>Name</label>
          <input/>
        </div>
      </form>
    </div>
  );
};
