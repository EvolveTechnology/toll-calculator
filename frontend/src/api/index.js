import axios from "axios";
import { totalFeeAccumulator } from "../utils";

export const endpoint = "http://localhost:1337";

const headers = {
  "Content-Type": "application/json"
};

export const queryOne = (vehicle, callback, fallbackState) => {
  return axios
    .post(`${endpoint}/vehicle`, JSON.stringify(vehicle), {
      headers
    })
    .then(({ data: { fees, regNum, type } }) =>
      callback({
        fees,
        results: Object.keys(fees).map(day => ({ day, ...fees[day] })),
        isTollFree: Object.keys(fees).some(day => fees[day].isTollFreeVehicle),
        regNum,
        type,
        allTimeTotalFee: totalFeeAccumulator(fees),
        showSpinner: false
      })
    )
    .catch(() => callback({ ...fallbackState }));
};

export const queryAll = () => {
  return axios
    .post(`${endpoint}/all`, {
      headers
    })
    .then(({ data }) =>
      data.map(({ fees, ...vehicle }) => ({
        totalFee: totalFeeAccumulator(fees),
        fees,
        ...vehicle
      }))
    );
};
