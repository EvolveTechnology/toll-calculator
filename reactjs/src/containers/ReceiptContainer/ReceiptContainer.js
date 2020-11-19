import React from "react";
import Receipt from "../../components/Receipt/Receipt";
import "./ReceiptContainer.css";

const ReceiptContainer = () => {
  return (
    <div data-testid="receipt-container" className="receipt-container">
      <h2>Receipts</h2>
      <div className="receipts">
        <Receipt></Receipt>
      </div>
    </div>
  );
};

export default ReceiptContainer;
