import React, { Component } from "react";
import Progress from "../Progress";
import { deltaSteps } from "./helpers";

export class AnimatedProgress extends Component {
  state = {
    percentage: 0
  };

  componentDidMount() {
    this.interval = setInterval(this.steps, 100);
  }

  componentWillUnmount() {
    clearInterval(this.interval);
  }

  steps = deltaSteps.bind(this);

  percentage = value =>
    this.setState({
      percentage: value % (100 + 1)
    });

  render() {
    return <Progress {...this.state} {...this.props} />;
  }
}

export default AnimatedProgress;
