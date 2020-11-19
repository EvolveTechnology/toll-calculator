import React from "react";
import ReceiptRow from "./../ReceiptRow/ReceiptRow";
import "./Receipt.css";

const Receipt = () => {
  return (
    <div data-testid="receipt" className="receipt">
      <h3 className="receipt-date">2020-11-19</h3>
      <ul className="receipt-passages-rows">
        <ReceiptRow time={"12:34:56"} fee={8}></ReceiptRow>
      </ul>
      <p className="receipt-total-fee">TOTAL: 8 SEK</p>
    </div>
  );
};

export default Receipt;
