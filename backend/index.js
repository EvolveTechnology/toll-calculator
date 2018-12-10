const express = require('express');
const Webtask = require('webtask-tools');
const bodyParser = require('body-parser');
const Services = require('./build');

const app = express();

const jsonParser = bodyParser.json();

app.use(jsonParser);
app.use((req, res, next) => {
  const allowedURIs = 'http://localhost:3000';

  const accessControlAllowOrigin = ['Access-Control-Allow-Origin', allowedURIs];
  const accessControlAllowHeaders = ['Access-Control-Allow-Headers', 'Content-Type'];
  res.setHeader(...accessControlAllowOrigin);
  res.setHeader(...accessControlAllowHeaders);
  next();
});

// TODO: add authentication to this route
app.post('/all', async (req, res) => {
  const { webtaskContext } = req;
  const {
    secrets: { HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT },
  } = webtaskContext;
  try {
    const result = await Services.allVehiclesCalculator(HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT);
    return res.status(200).send(result);
  } catch (err) {
    return res.status(401).send({});
  }
});

app.post('/vehicle', async (req, res) => {
  const { webtaskContext, body } = req;
  const { regNum } = body;
  const {
    secrets: { HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT },
  } = webtaskContext;
  try {
    const result = await Services.byVehicleCalculator(regNum, HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT);
    return res.status(200).send(result);
  } catch (err) {
    return res.status(401).send({});
  }
});

module.exports = Webtask.fromExpress(app);
