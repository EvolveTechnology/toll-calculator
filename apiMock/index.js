// Local development server.
// Used to fake API calls
// and avoid using real endpoints
// which might have costs

const express = require("express");
const app = express();

const PORT = 9191;
const accessControlAllowOrigin = ["Access-Control-Allow-Origin", "*"];
const accessControlAllowHeaders = ["Access-Control-Allow-Headers", "*"];

const vehicleData = require("./vehicleData");
const { holidays2017, holidays2018 } = require("./holidays");

app.use((req, res, next) => {
  res.setHeader(...accessControlAllowOrigin);
  res.setHeader(...accessControlAllowHeaders);
  console.log(
    `****\nmethod: ${req.method}\nendpoint: ${req.originalUrl}\nsource: ${
      req.headers.origin
    }\n****`
  );
  next();
});

// API's queried by the backend
app.get("/holiday&year=2017", (req, res) => {
  return res.json(holidays2017);
});

app.get("/holiday&year=2018", (req, res) => {
  return res.json(holidays2018);
});

app.get("/vehicles", (req, res) => {
  return res.json(vehicleData);
});

app.listen(PORT, () => {
  console.log(`Local dev server running on PORT ${PORT}`);
});
