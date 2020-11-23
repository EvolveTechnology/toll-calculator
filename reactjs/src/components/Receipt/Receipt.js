import React from "react";
import ReceiptRow from "./../ReceiptRow/ReceiptRow";
import {
  getFeeForTime,
  getTotalFeeForDate,
  notChargedPassagesForDate,
  isTollFreeDate,
  isTollFreeVehicle,
} from "../../tollcalculator/tollcalculator";
import { MAX_DAILY_FEE } from "../../constants/constants";
import "./Receipt.css";

const Receipt = ({ date, vehicle }) => {
  const passages = vehicle.passages[date];
  const totalFee = getTotalFeeForDate(vehicle, date);
  const notChargedTimes = notChargedPassagesForDate(passages, date);
  const tollFreeDate = isTollFreeDate(date);
  const tollFreeVehicle = isTollFreeVehicle(vehicle);

  const rows = passages.map((time, index) => {
    const fee = getFeeForTime(time);
    const greyOutRow = notChargedTimes.includes(time);
    return (
      <ReceiptRow
        time={time}
        fee={tollFreeDate || tollFreeVehicle ? 0 : fee}
        key={index}
        greyOut={tollFreeDate ? false : greyOutRow}
      ></ReceiptRow>
    );
  });

  return (
    <div data-testid="receipt" className="receipt">
      <h3 className="receipt-date">{date}</h3>
      {tollFreeDate && <p>Toll free date</p>}
      <ul className="receipt-passages-rows">{rows}</ul>
      {totalFee >= MAX_DAILY_FEE && <p>Max daily fee reached</p>}
      <p className="receipt-total-fee">TOTAL: {totalFee} SEK</p>
    </div>
  );
};

export default Receipt;
