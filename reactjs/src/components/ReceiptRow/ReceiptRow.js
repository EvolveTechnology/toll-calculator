import React from "react";
import "./ReceiptRow.css";

const ReceiptRow = ({ time, fee }) => {
  return (
    <li data-testid="row">
      <span>{time}</span>
      <span>{fee} SEK</span>
    </li>
  );
};

export default ReceiptRow;
