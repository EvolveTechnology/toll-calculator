import React, { Component } from "react";
import { activateButton } from "./helpers";
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

  showButton = activateButton.bind(this);

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
