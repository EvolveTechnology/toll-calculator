import React from "react";
import sun from "../../assets/sunday.png";
import sat from "../../assets/saturday.png";

export default ({ isSaturday, isSunday }) => (
  <div className="fee-content flex-column">
    {isSaturday && <img src={sat} alt="saturday" width="48px" />}
    {isSunday && <img src={sun} alt="sunday" width="48px" />}
    <span>Weekend</span>
  </div>
);
