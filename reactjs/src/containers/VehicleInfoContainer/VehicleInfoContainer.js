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

  return (
    <div data-testid="vehicleinfo-container" className="vehicleinfo-container">
      <div className="info">
        <h3>Data for: {vehicleData.reg}</h3>
        <p className="vehicleinfo-type">
          Type: {vehicleData.type} {tollFreeVehicle ? "(Toll Free)" : ""}
        </p>
        <p>Total toll fee: {totalFee} SEK</p>
        <p>Total passages: {totalPassages} </p>
      </div>
    </div>
  );
};

export default VehicleInfoContainer;
