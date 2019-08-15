# Toll Fee Calculator 2.0

A calculator for vehicle toll fees.

## Background

Our city has decided to implement toll fees in order to reduce traffic
congestion during rush hours. This is the current draft of requirements:
 
- Fees will differ between 8 SEK and 18 SEK, depending on the time of day 
- Rush-hour traffic will render the highest fee
- The maximum fee for one day is 60 SEK
- A vehicle should only be charged once an hour
  - In the case of multiple fees in the same hour period, the highest one
    applies.
- Some vehicle types are fee-free
- Weekends and holidays are fee-free

## API

**Endpoint:** `POST /v1/fee`

- `passes`: Sorted list of ISO 8601 formatted timestamps of fee station passes
  for one day.
- `vehicle_type`: One of `"CAR"`, `"MOTORBIKE"`, `"TRACTOR"`, `"EMERGENCY"`, 
  `"DIPLOMAT"`, `"FOREIGN"`, `"MILITARY"`.

Example request body:

```json
{
  "passes": ["2019-08-15T12:41:45.386869"],
  "vehicle_type": "CAR"
}
```

Example response body:

```json
{
  "fee": 8
}
```

## Development

Install dependencies

```bash
pip install pip-tools
pip-sync
```

Run code formatters

```bash
sorti .
black .
```

Run linters

```bash
flake8
mypy .
```

Run test suite

```bash
pytest
```

Start server

```bash
python app.py
```

Send a test request to the server

```bash
curl -X POST -d '{"passes": ["2019-08-15T12:41:45"], "vehicle_type": "CAR"}' http://localhost:8080/v1/fee
```
