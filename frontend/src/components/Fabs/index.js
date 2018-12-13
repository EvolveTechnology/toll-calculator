import React, { Component } from "react";

import "./fabs.css";

export class Fabs extends Component {
  state = {
    show: false
  };

  componentDidMount() {
    window.addEventListener("scroll", this.showButton);
  }

  componentWillUnmount() {
    window.removeEventListener("scroll", this.showButton);
  }

  showButton = () => {
    const { show } = this.state;
    const fromTop = window.scrollY;

    if (fromTop > 100) {
      if (!show) {
        return this.setState({ show: true });
      }
    }
    if (fromTop <= 100) {
      if (show) {
        return this.setState({ show: false });
      }
    }
    return null;
  };

  scrollTop = () => window.scrollTo(0, 0);

  render() {
    const { show } = this.state;
    const { children } = this.props;
    return (
      <div className="fabs-container">
        {show && (
          <div id="scrollTop">
            <button
              className="btn btn-primary"
              onClick={this.scrollTop}
              onMouseDown={e => e.preventDefault()}
            >
              Top
            </button>
          </div>
        )}
        <div id="fab-children">{children}</div>
      </div>
    );
  }
}

export default Fabs;
