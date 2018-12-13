import React, { Component } from "react";
import "./spinner.css";

export class Spinner extends Component {
  state = {
    mount: false
  };

  timer = null;

  componentDidUpdate(prevProps) {
    if (this.props.show && !prevProps.show) {
      clearTimeout(this.timer);
      return this.setState({ mount: true });
    }
    if (!this.props.show && prevProps.show) {
      clearTimeout(this.timer);
      this.timer = setTimeout(() => this.setState({ mount: false }), 1500);
      return null;
    }
    return null;
  }

  componentWillUnmount() {
    return clearTimeout(this.timer);
  }

  render() {
    const { mount } = this.state;
    return (
      mount && (
        <div className="spinner-wrapper">
          <div className="spinner" />
        </div>
      )
    );
  }
}

export default Spinner;
