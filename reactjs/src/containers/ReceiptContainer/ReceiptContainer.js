import React, { useState, useEffect } from "react";
import Receipt from "../../components/Receipt/Receipt";
import "./ReceiptContainer.css";

const ReceiptContainer = () => {
  const [vehicleData, setVehicleData] = useState([]);
  const [error, setError] = useState(false);
  const [isLoaded, setIsLoaded] = useState(false);

  useEffect(() => {
    fetch("mockDB/data.json")
      .then((res) => res.json())
      .then(
        (res) => {
          setVehicleData(res[0]);
          setIsLoaded(true);
        },
        (error) => {
          setError(true);
        }
      );
  }, []);

  if (error) {
    return <div>Failed to load data</div>;
  } else if (!isLoaded) {
    return <div>Loading...</div>;
  } else {
    return (
      <div data-testid="receipt-container" className="receipt-container">
        <h2>Receipts</h2>
        <div className="receipts">
          {Object.keys(vehicleData.passages).map((date, index) => (
            <Receipt date={date} vehicle={vehicleData} key={index}></Receipt>
          ))}
        </div>
      </div>
    );
  }
};

export default ReceiptContainer;
