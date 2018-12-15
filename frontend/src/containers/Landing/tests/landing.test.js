import React from "react";
import axios from "axios";
import MockAdapter from "axios-mock-adapter";
import { mount } from "enzyme";
import Landing from "..";
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

describe("Landing", () => {
  const landing = mount(<Landing />);
  it("renders", () => {
    expect(landing.find("h1").text()).toEqual("Hello!");
  });

  it("has 2 children", () => {
    expect(landing.children()).toHaveLength(2);
  });

  it("loads the spinner", () => {
    expect(landing.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(landing.state("results")).toEqual([]);

    landing.find("input").instance().value = "QNX-473";
    landing.find("input").simulate("change", { target: { value: "" } });

    expect(landing.state("showSpinner")).toEqual(true);
    expect(landing.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders the results and one filter", () => {
    expect(landing.state("results")).toEqual(expected);
    landing.update();
    expect(landing.find(Filter)).toHaveLength(1);
    expect(landing.find(Results)).toHaveLength(1);
  });
  it("renders 2 results as animated progress, where the first element has value 13 (not sorted)", () => {
    // one children for the summary and one for the animated progress
    expect(landing.find(Results).children()).toHaveLength(2);
    expect(landing.find(AnimatedProgress)).toHaveLength(2);
    expect(
      landing
        .find(AnimatedProgress)
        .at(0)
        .prop("value")
    ).toEqual(13);
    expect(
      landing
        .find(AnimatedProgress)
        .at(1)
        .prop("value")
    ).toEqual(18);
  });

  it("sorts them according to selection", () => {
    landing
      .find(Filter)
      .find("select")
      .simulate("change", { target: { value: HIGHEST } });

    expect(landing.state("sorting")).toEqual(HIGHEST);
    landing.update();
    expect(
      landing
        .find(AnimatedProgress)
        .at(0)
        .prop("value")
    ).toEqual(18);
    expect(
      landing
        .find(AnimatedProgress)
        .at(1)
        .prop("value")
    ).toEqual(13);
  });

  it("does nothing upon invalid regNum", () => {
    landing.find("input").instance().value = "2";
    landing.find("input").simulate("change", { target: { value: "" } });
    expect(landing.state("showSpinner")).toEqual(false);
    expect(landing.find(Spinner).prop("show")).toEqual(false);

    landing.find("input").instance().value = undefined;
    landing.find("input").simulate("change", { target: { value: undefined } });
    expect(landing.state("showSpinner")).toEqual(false);
    expect(landing.find(Spinner).prop("show")).toEqual(false);
  });
});

describe("Landing with a vehicle without any data", () => {
  const landing = mount(<Landing />);
  it("renders", () => {
    expect(landing.find("h1").text()).toEqual("Hello!");
  });

  it("has 2 children", () => {
    expect(landing.children()).toHaveLength(2);
  });

  it("loads the spinner", () => {
    expect(landing.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(landing.state("results")).toEqual([]);

    // this time the mock RETURNS null in a 200!!
    landing.find("input").instance().value = "QNX-473";
    landing.find("input").simulate("change", { target: { value: "" } });

    expect(landing.state("showSpinner")).toEqual(true);
    expect(landing.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders an empty box", () => {
    expect(landing.state("error")).toEqual(false);
    expect(landing.state("results")).toEqual([]);
    landing.update();
    expect(landing.find(Placeholder)).toHaveLength(1);
    expect(landing.find(Placeholder).prop("placeholder")).toEqual("empty");

    expect(landing.find(Results)).toHaveLength(0);
  });
});

describe("Landing with network error", () => {
  const landing = mount(<Landing />);
  it("renders", () => {
    expect(landing.find("h1").text()).toEqual("Hello!");
  });

  it("has 2 children", () => {
    expect(landing.children()).toHaveLength(2);
  });

  it("loads the spinner", () => {
    expect(landing.find(Spinner)).toHaveLength(1);
  });

  it("sets the spinner upon valid regNum", () => {
    expect(landing.state("results")).toEqual([]);

    // this time the mock FAILS with 500!!
    landing.find("input").instance().value = "QNX-473";
    landing.find("input").simulate("change", { target: { value: "" } });

    expect(landing.state("showSpinner")).toEqual(true);
    expect(landing.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders network error placeholder", () => {
    expect(landing.state("results")).toEqual([]);
    expect(landing.state("error")).toEqual(true);
    landing.update();
    expect(landing.find(Placeholder)).toHaveLength(1);
    expect(landing.find(Placeholder).prop("placeholder")).toEqual("error");
    expect(landing.find(Results)).toHaveLength(0);
  });
});
