import React, { Component } from "react";
import Placeholder from "../../components/Placeholder";

export class Offline extends Component {
  state = {
    online: true
  };

  componentDidMount() {
    window.addEventListener("offline", this.off);
    window.addEventListener("online", this.on);
    const online = window.navigator.onLine;
    return this.setState({ online });
  }

  componentWillUnmount() {
    window.removeEventListener("offline", this.off);
    window.removeEventListener("online", this.on);
  }

  on = () => this.setState({ online: true });
  off = () => this.setState({ online: false });

  render() {
    const { online } = this.state;
    const { children } = this.props;
    return !online ? <Placeholder placeholder="offline" /> : children;
  }
}

export default Offline;
