import React from "react";
import { arrayTotalFeeAccumulator } from "../../utils";
import total from "../../assets/total.png";

export default ({ vehicles, sortedVehicles }) => (
  <div className="summary">
    <div className="total">
      <span className="lead">
        Showing: {sortedVehicles.length} / {vehicles.length}
      </span>
      <span className="lead">
        Total debt: {arrayTotalFeeAccumulator(sortedVehicles)} SEK
        <img src={total} alt="total" width="16px" />
      </span>
    </div>
  </div>
);
