import React, { useState, useEffect, Fragment } from "react";
import ReceiptContainer from "./containers/ReceiptContainer/ReceiptContainer";
import VehicleInfoContainer from "./containers/VehicleInfoContainer/VehicleInfoContainer";
import Header from "./components/Header/Header";
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
      <div className="App" data-testid="app">
        <Header></Header>
        <div className="App-data-error">
          No data found or missing reg number for vehicle
        </div>
      </div>
    );
  } else {
    return (
      <div className="App" data-testid="app">
        <Header></Header>
        {isLoaded && vehicleData ? (
          <Fragment>
            <VehicleInfoContainer
              vehicleData={vehicleData}
            ></VehicleInfoContainer>
            <ReceiptContainer vehicleData={vehicleData}></ReceiptContainer>
          </Fragment>
        ) : (
          <div className="App-loading">Loading...</div>
        )}
      </div>
    );
  }
}

export default App;
