import typeSelector from "../helpers";

describe("typeSelector", () => {
  const types = [
    "Car",
    "Bus",
    "Military",
    "Emergency",
    "Truck",
    "Diplomat",
    "Foreign",
    "Motorbike",
    "Tractor",
    "Unknown"
  ];

  const expected = [
    "car.png",
    "bus.png",
    "military.png",
    "emergency.png",
    "truck.png",
    "diplomat.png",
    "foreign.png",
    "motorbike.png",
    "tractor.png",
    "wheel.png"
  ];

  it("selects the appropiate images", () => {
    expect(types.map(typeSelector)).toEqual(expected);
  });
});
