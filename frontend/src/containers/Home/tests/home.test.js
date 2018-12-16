import React from "react";
import axios from "axios";
import MockAdapter from "axios-mock-adapter";
import { mount } from "enzyme";
import Home from "..";
import Spinner from "../../../components/Spinner";
import { endpoint } from "../../../endpoints";
import { mockData, expected } from "./mock";
import Filter from "../../../components/Filter";
import Results from "../../../components/Results";
import AnimatedProgress from "../../../components/AnimatedProgress";
import { HIGHEST } from "../../../constants";
import Placeholder from "../../../components/Placeholder";

// This sets the mock adapter on the default instance
const mock = new MockAdapter(axios);

mock
  .onPost(`${endpoint}/vehicle`)
  .replyOnce(() => {
    return [200, { ...mockData }];
  })
  .onPost(`${endpoint}/vehicle`)
  .replyOnce(() => {
    return [
      200,
      { id: null, fees: {}, dates: [], regNum: "QNX-473", type: "" }
    ];
  })
  .onPost(`${endpoint}/vehicle`)
  .reply(500);

describe("Home", () => {
  const home = mount(<Home />);
  it("renders", () => {
    expect(home.find("h1").text()).toEqual("Hello!");
  });

  it("has 3 children, search, fabs and spinner ", () => {
    expect(home.children()).toHaveLength(3);
  });

  it("loads the spinner", () => {
    expect(home.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(home.state("results")).toEqual([]);

    home.find("input").instance().value = "QNX-473";
    home.find("input").simulate("change", { target: { value: "" } });

    expect(home.state("showSpinner")).toEqual(true);
    expect(home.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders the results and one filter", () => {
    expect(home.state("results")).toEqual(expected);
    home.update();
    expect(home.find(Filter)).toHaveLength(1);
    expect(home.find(Results)).toHaveLength(1);
  });
  it("renders 2 results as animated progress, where the first element has value 13 (not sorted)", () => {
    // one children for the summary and one for the animated progress
    expect(home.find(Results).children()).toHaveLength(2);
    expect(home.find(AnimatedProgress)).toHaveLength(2);
    expect(
      home
        .find(AnimatedProgress)
        .at(0)
        .prop("value")
    ).toEqual(13);
    expect(
      home
        .find(AnimatedProgress)
        .at(1)
        .prop("value")
    ).toEqual(18);
  });

  it("sorts them according to selection", () => {
    home
      .find(Filter)
      .find("select")
      .simulate("change", { target: { value: HIGHEST } });

    expect(home.state("sorting")).toEqual(HIGHEST);
    home.update();
    expect(
      home
        .find(AnimatedProgress)
        .at(0)
        .prop("value")
    ).toEqual(18);
    expect(
      home
        .find(AnimatedProgress)
        .at(1)
        .prop("value")
    ).toEqual(13);
  });

  it("does nothing upon invalid regNum", () => {
    home.find("input").instance().value = "2";
    home.find("input").simulate("change", { target: { value: "" } });
    expect(home.state("showSpinner")).toEqual(false);
    expect(home.find(Spinner).prop("show")).toEqual(false);

    home.find("input").instance().value = undefined;
    home.find("input").simulate("change", { target: { value: undefined } });
    expect(home.state("showSpinner")).toEqual(false);
    expect(home.find(Spinner).prop("show")).toEqual(false);
  });
});

describe("Landing with a vehicle without any data", () => {
  const home = mount(<Home />);
  it("renders", () => {
    expect(home.find("h1").text()).toEqual("Hello!");
  });

  it("has 3 children, search, fabs and spinner", () => {
    expect(home.children()).toHaveLength(3);
  });

  it("loads the spinner", () => {
    expect(home.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(home.state("results")).toEqual([]);

    // this time the mock RETURNS null in a 200!!
    home.find("input").instance().value = "QNX-473";
    home.find("input").simulate("change", { target: { value: "" } });

    expect(home.state("showSpinner")).toEqual(true);
    expect(home.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders an empty box", () => {
    expect(home.state("error")).toEqual(false);
    expect(home.state("results")).toEqual([]);
    home.update();
    expect(home.find(Placeholder)).toHaveLength(1);
    expect(home.find(Placeholder).prop("placeholder")).toEqual("empty");

    expect(home.find(Results)).toHaveLength(0);
  });
});

describe("Landing with network error", () => {
  const home = mount(<Home />);
  it("renders", () => {
    expect(home.find("h1").text()).toEqual("Hello!");
  });

  it("has 3 children, search, fabs and spinner", () => {
    expect(home.children()).toHaveLength(3);
  });

  it("loads the spinner", () => {
    expect(home.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(home.state("results")).toEqual([]);

    // this time the mock FAILS with 500!!
    home.find("input").instance().value = "QNX-473";
    home.find("input").simulate("change", { target: { value: "" } });

    expect(home.state("showSpinner")).toEqual(true);
    expect(home.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders network error placeholder", () => {
    expect(home.state("results")).toEqual([]);
    expect(home.state("error")).toEqual(true);
    home.update();
    expect(home.find(Placeholder)).toHaveLength(1);
    expect(home.find(Placeholder).prop("placeholder")).toEqual("error");
    expect(home.find(Results)).toHaveLength(0);
  });
});
