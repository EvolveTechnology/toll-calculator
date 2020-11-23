import React from "react";
import Receipt from "../../components/Receipt/Receipt";
import "./ReceiptContainer.css";

const ReceiptContainer = ({ vehicleData }) => {
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
};

export default ReceiptContainer;
