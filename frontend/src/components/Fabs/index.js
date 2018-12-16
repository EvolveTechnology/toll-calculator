import React, { Component } from "react";
import Button from "../Button";
import { activateButton } from "./helpers";
import { softTopScroll } from "../../utils";
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

  scrollTop = () => softTopScroll();

  render() {
    const { show } = this.state;
    const { children } = this.props;
    return (
      <div className="fabs-container">
        {show && (
          <div id="scrollTop">
            <Button type="slideup" onClick={this.scrollTop} />
          </div>
        )}
        <div id="fab-children">{children}</div>
      </div>
    );
  }
}

export default Fabs;
