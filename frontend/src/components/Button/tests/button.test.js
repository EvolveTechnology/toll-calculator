import React from "react";
import { shallow } from "enzyme";
import Button from "..";

describe("Button", () => {
  const onClick = jest.fn();
  const btn = shallow(<Button text="test" onClick={onClick} />);

  it("passes the onClick to native button", () => {
    btn.simulate("click");
    expect(onClick).toHaveBeenCalled();
  });
});
