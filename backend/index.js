const express = require('express');
const Webtask = require('webtask-tools');
const bodyParser = require('body-parser');
const Services = require('./build');

const local = 'http://localhost:9191';

const calendarURI = 'https://www.calendarific.com/api/v2/holidays?country=SE';
const withKey = key => `${calendarURI}&api_key=${key}`;

const makeHolidayEP = ({ ENV }, key) => (ENV === 'DEV' ? `${local}/holiday` : withKey(key));
const makeTollDataEP = ({ ENV }, endpoint) => (ENV === 'DEV' ? `${local}/vehicles` : endpoint);

const app = express();

const jsonParser = bodyParser.json();

app.use(jsonParser);
app.use((req, res, next) => {
  const { webtaskContext } = req;
  const { meta } = webtaskContext;
  const allowedURIs = meta.ENV === 'DEV' ? 'http://localhost:3000' : 'https://nice-sky.surge.sh';

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
    meta,
    secrets: { HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT },
  } = webtaskContext;

  const holidayEndpoint = makeHolidayEP(meta, HOLIDAY_API_KEY);
  const tollDataEndpoint = makeTollDataEP(meta, TOLL_DATA_ENDPOINT);

  try {
    const result = await Services.allVehiclesCalculator(holidayEndpoint, tollDataEndpoint);
    return res.status(200).send(result);
  } catch (err) {
    return res.status(401).send({});
  }
});

app.post('/vehicle', async (req, res) => {
  const { webtaskContext, body } = req;
  const { regNum } = body;
  const {
    meta,
    secrets: { HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT },
  } = webtaskContext;

  const holidayEndpoint = makeHolidayEP(meta, HOLIDAY_API_KEY);
  const tollDataEndpoint = makeTollDataEP(meta, TOLL_DATA_ENDPOINT);

  try {
    const result = await Services.byVehicleCalculator(regNum, holidayEndpoint, tollDataEndpoint);
    return res.status(200).send(result);
  } catch (err) {
    return res.status(401).send(err);
  }
});

module.exports = Webtask.fromExpress(app);
