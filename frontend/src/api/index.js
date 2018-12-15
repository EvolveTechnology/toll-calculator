import axios from "axios";
import { objectTotalFeeAccumulator } from "../utils";
import { endpoint } from "../endpoints";

const headers = {
  "Content-Type": "application/json"
};

export const queryOne = (vehicle, callback, fallbackState) => {
  return axios
    .post(`${endpoint}/vehicle`, JSON.stringify(vehicle), {
      headers
    })
    .then(({ data: { fees, regNum, type, id } }) =>
      callback({
        id,
        fees,
        results: Object.keys(fees).map(day => ({ day, ...fees[day] })),
        isTollFree: Object.keys(fees).some(day => fees[day].isTollFreeVehicle),
        regNum,
        type,
        allTimeTotalFee: objectTotalFeeAccumulator(fees),
        showSpinner: false,
        error: false
      })
    )
    .catch(() => callback({ ...fallbackState, error: true }));
};

export const queryAll = (callback, fallbackState) => {
  return axios
    .post(`${endpoint}/all`, {
      headers
    })
    .then(({ data }) =>
      callback({
        vehicles: data.map(({ fees, ...vehicle }) => ({
          totalFee: objectTotalFeeAccumulator(fees),
          fees,
          ...vehicle
        })),
        loadingAll: false,
        error: false
      })
    )
    .catch(() => callback({ ...fallbackState, error: true }));
};
