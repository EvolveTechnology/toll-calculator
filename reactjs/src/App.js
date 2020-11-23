import React, { useState, useEffect } from "react";
import ReceiptContainer from "./containers/ReceiptContainer/ReceiptContainer";
import "./App.css";

function App() {
  const [vehicleData, setVehicleData] = useState([]);
  const [error, setError] = useState(false);
  const [isLoaded, setIsLoaded] = useState(false);

  const urlParams = new URLSearchParams(window.location.search);
  const vehicleRegNumber = urlParams.get("reg")
    ? urlParams.get("reg").toUpperCase()
    : null;

  useEffect(() => {
    fetch(`mockDB/${vehicleRegNumber}.json`)
      .then((res) => res.json())
      .then(
        (res) => {
          setVehicleData(res);
          setIsLoaded(true);
        },
        (error) => {
          setError(true);
        }
      );
  }, [vehicleRegNumber]);

  if (error) {
    return (
      <div className="App">
        <h1>TollFee Calculator</h1>
        <div>No data found or missing reg number for vehicle</div>
      </div>
    );
  } else {
    return (
      <div className="App">
        <h1>TollFee Calculator</h1>
        {isLoaded && vehicleData ? (
          <ReceiptContainer vehicleData={vehicleData}></ReceiptContainer>
        ) : (
          <div>Loading...</div>
        )}
      </div>
    );
  }
}

export default App;
