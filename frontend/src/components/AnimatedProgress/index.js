import React, { Component } from "react";
import Progress from "../Progress";

export class AnimatedProgress extends Component {
  state = {
    percentage: 0
  };

  componentDidMount() {
    this.interval = setInterval(() => {
      const delta = this.state.percentage + 1;
      this.percentage(delta);

      if (delta >= (this.props.value / 60) * 100) {
        clearInterval(this.interval);
      }
    }, 50);
  }

  componentWillUnmount() {
    clearInterval(this.interval);
  }

  percentage = (value = 0) => {
    return this.setState({
      percentage: value % (100 + 1)
    });
  };

  render() {
    return <Progress {...this.state} {...this.props} />;
  }
}

export default AnimatedProgress;
