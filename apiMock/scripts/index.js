const fs = require("fs");
const data = require("./mock.json");

// This source code takes mock data and shapes it as required by the
// backend service
// mock.json -> script -> data.json

const regNums = data.reduce(
  (prev, curr) =>
    prev.includes(curr.reg_num) ? prev : prev.concat(curr.reg_num),
  []
);

if (regNums.length === data.length) {
  const modified = data.map(({ dates, ...vehicle }) => ({
    ...vehicle,
    dates: dates.map(({ pass }) => pass.replace(" UTC", ""))
  }));

  fs.writeFile("data.json", JSON.stringify(modified), () => {
    /* eslint-disable-next-line */
    console.log("Success");
  });
}

/* eslint-disable-next-line */
console.log("Not all reg nums are unique");
