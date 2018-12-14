import React from "react";
import { shallow } from "enzyme";
import Progress from "..";
import TollFreeVehicle from "../../TollFreeVehicle";

describe("Progress", () => {
  const progress = shallow(
    <Progress freeDay={true}>
      <TollFreeVehicle />
    </Progress>
  );
  it("renders children for free day", () => {
    expect(progress.find(TollFreeVehicle)).toHaveLength(1);
  });
});
