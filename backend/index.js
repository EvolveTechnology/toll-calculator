const express = require('express');
const Webtask = require('webtask-tools');
const bodyParser = require('body-parser');
const tollCalculatorService = require('./build');

const app = express();

const jsonParser = bodyParser.json();

app.use(jsonParser);
app.post('/vehicle', async (req, res) => {
  const { webtaskContext, body } = req;
  const { regNum } = body;
  const {
    secrets: { HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT },
  } = webtaskContext;
  try {
    const result = await tollCalculatorService(regNum, HOLIDAY_API_KEY, TOLL_DATA_ENDPOINT);
    return res.status(200).send(result);
  } catch (err) {
    return res.status(401).send({});
  }
});

module.exports = Webtask.fromExpress(app);
