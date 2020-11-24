import React from "react";
import {
  isTollFreeVehicle,
  totalFeeForDates,
  totalPassagesForDates,
} from "../../tollcalculator/tollcalculator";
import "./VehicleInfoContainer.css";

const VehicleInfoContainer = ({ vehicleData }) => {
  const totalFee = totalFeeForDates(vehicleData);
  const totalPassages = totalPassagesForDates(vehicleData);

  const tollFreeVehicle = isTollFreeVehicle(vehicleData);

  const dates = Object.keys(vehicleData.passages);
  const lastDate = dates[0];
  const firstDate = dates[dates.length - 1];

  return (
    <div data-testid="vehicleinfo-container" className="vehicleinfo-container">
      <div className="vehicleinfo-detail">
        <h2>{vehicleData.reg}</h2>
        <p className="vehicleinfo-type">
          Type: {vehicleData.type} {tollFreeVehicle ? "(Toll Free)" : ""}
        </p>
      </div>
      <hr></hr>
      <div className="vehicleinfo-detail summary-data">
        <h2>{totalFee} SEK</h2>
        <p>Total toll fee</p>
      </div>
      <hr></hr>
      <div className="vehicleinfo-detail summary-data">
        <h2>{totalPassages} passages</h2>
        <p>
          Between {firstDate} and {lastDate}
        </p>
      </div>
    </div>
  );
};

export default VehicleInfoContainer;
