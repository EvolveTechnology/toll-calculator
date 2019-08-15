import datetime
import json

from aiohttp import web

from toll_calculator.fee import get_toll_fee
from toll_calculator.vehicle import Vehicle


async def handle(request: web.Request) -> web.Response:
    try:
        data = await request.json()
    except json.decoder.JSONDecodeError:
        raise web.HTTPBadRequest(reason="Invalid JSON")

    try:
        passes = [datetime.datetime.fromisoformat(time) for time in data["passes"]]
    except (KeyError, ValueError):
        raise web.HTTPBadRequest(reason="Missing or invalid `passes`")

    try:
        vehicle = Vehicle(data["vehicle_type"])
    except (KeyError, ValueError):
        raise web.HTTPBadRequest(reason="Missing or invalid `vehicle_type`")

    try:
        fee = get_toll_fee(vehicle, passes)
    except ValueError as e:
        raise web.HTTPBadRequest(reason=f"Invalid data in `passes`: {e}")

    return web.json_response({"fee": fee})


if __name__ == "__main__":
    app = web.Application()
    app.router.add_post("/v1/fee", handle)
    web.run_app(app)
