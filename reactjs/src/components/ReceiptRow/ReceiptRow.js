import React from "react";
import "./ReceiptRow.css";

const ReceiptRow = ({ time, fee, greyOut }) => {
  return (
    <li data-testid="row" className={greyOut ? "greyed-out" : ""}>
      <span>{time}</span>
      <span>{fee} SEK</span>
    </li>
  );
};

export default ReceiptRow;
