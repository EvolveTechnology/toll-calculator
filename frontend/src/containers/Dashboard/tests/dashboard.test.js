import React from "react";
import axios from "axios";
import MockAdapter from "axios-mock-adapter";
import { mount } from "enzyme";
import Dashboard from "..";
import Spinner from "../../../components/Spinner";
import Search from "../../../components/Search";
import Filter from "../../../components/Filter";
import { HIGHEST, LOWEST, NONE } from "../../../constants";
import Summary from "../../../components/Summary";
import VehicleList from "../../../components/VehicleList";

import { endpoint } from "../../../endpoints";
import { mockData, expected } from "./mock";
import Type from "../../../components/Type";
import AnimatedProgress from "../../../components/AnimatedProgress";
import Button from "../../../components/Button";
import Placeholder from "../../../components/Placeholder";

// This sets the mock adapter on the default instance
const mockAxios = new MockAdapter(axios);

mockAxios.onPost(`${endpoint}/all`).replyOnce(function() {
  return [200, mockData];
});

describe("Dashboard", () => {
  const dashboard = mount(<Dashboard />, { disableLifecycleMethods: false });
  it("renders", () => {
    expect(dashboard.find("h1").text()).toEqual("Welcome!");
  });

  it("has 3 children", () => {
    expect(dashboard.children()).toHaveLength(3);
    expect(dashboard.find(Search)).toHaveLength(1);
    expect(dashboard.find(Filter)).toHaveLength(2);
  });

  it("loads the spinner", () => {
    expect(dashboard.find(Spinner)).toHaveLength(1);
  });

  it("has a load all button", () => {
    expect(dashboard.find(Button).prop("type")).toEqual("download");
  });

  it("sets the spinner when loading all", () => {
    expect(dashboard.state("vehicles")).toEqual([]);

    dashboard.find(Button).simulate("click");

    expect(dashboard.state("loadingAll")).toEqual(true);
    expect(dashboard.find(Spinner).prop("show")).toEqual(true);
  });

  it("renders the vehicles and two filters", () => {
    expect(dashboard.state("vehicles")).toEqual(expected);
    dashboard.update();

    expect(dashboard.find(Summary)).toHaveLength(1);
    expect(dashboard.find(VehicleList)).toHaveLength(1);
    expect(dashboard.find(VehicleList).children()).toHaveLength(1);
  });

  it("renders 2 vehicles", () => {
    // one children for the summary and one for the animated progress
    expect(dashboard.find(VehicleList).children()).toHaveLength(1);
    expect(dashboard.find(Type)).toHaveLength(2);
  });

  it("sorts them from Highest to Lowest", () => {
    dashboard
      .find(Filter)
      .find("select")
      .at(1)
      .simulate("change", { target: { value: HIGHEST } });

    expect(dashboard.state("sorting")).toEqual(HIGHEST);
    dashboard.update();

    expect(
      dashboard
        .find(Type)
        .at(0)
        .prop("totalFee")
    ).toEqual(31);
    expect(
      dashboard
        .find(Type)
        .at(1)
        .prop("totalFee")
    ).toEqual(26);
  });

  it("sorts them from Lowest to Highest", () => {
    dashboard
      .find(Filter)
      .find("select")
      .at(1)
      .simulate("change", { target: { value: LOWEST } });

    expect(dashboard.state("sorting")).toEqual(LOWEST);
    dashboard.update();

    expect(
      dashboard
        .find(Type)
        .at(0)
        .prop("totalFee")
    ).toEqual(26);
    expect(
      dashboard
        .find(Type)
        .at(1)
        .prop("totalFee")
    ).toEqual(31);
  });

  it("resets original order", () => {
    dashboard
      .find(Filter)
      .find("select")
      .at(1)
      .simulate("change", { target: { value: NONE } });

    expect(dashboard.state("sorting")).toEqual(NONE);
    dashboard.update();

    expect(
      dashboard
        .find(Type)
        .at(0)
        .prop("totalFee")
    ).toEqual(26);
    expect(
      dashboard
        .find(Type)
        .at(1)
        .prop("totalFee")
    ).toEqual(31);
  });

  it("filters based on type", () => {
    dashboard
      .find(Filter)
      .find("select")
      .at(0)
      .simulate("change", { target: { value: "Car" } });

    expect(dashboard.state("filterType")).toEqual("Car");
    dashboard.update();

    expect(dashboard.find(Type)).toHaveLength(1);
    expect(dashboard.find(Type).prop("type")).toEqual("Car");
  });

  it("resets filter based on type", () => {
    dashboard
      .find(Filter)
      .find("select")
      .at(0)
      .simulate("change", { target: { value: "All" } });

    expect(dashboard.state("filterType")).toEqual("All");
    dashboard.update();

    expect(dashboard.find(Type)).toHaveLength(2);
  });

  it("filters based on search regNum", () => {
    dashboard.find("input").instance().value = "ABC-123";
    dashboard
      .find("input")
      .simulate("change", { target: { value: "ABC-123" } });

    expect(dashboard.state("query")).toEqual("ABC-123");
    expect(dashboard.find(Type)).toHaveLength(1);
    expect(dashboard.find(Type).prop("regNum")).toEqual("ABC-123");
  });

  it("searches regNums that match the input", () => {
    dashboard.find("input").instance().value = "2";
    dashboard.find("input").simulate("change", { target: { value: "2" } });

    expect(dashboard.state("query")).toEqual("2");
    expect(dashboard.find(Type)).toHaveLength(1);
  });

  it("shows all vehicles when input is empty", () => {
    dashboard.find("input").instance().value = "";
    dashboard.find("input").simulate("change", { target: { value: "" } });

    expect(dashboard.state("query")).toEqual("");
    expect(dashboard.find(Type)).toHaveLength(2);
  });

  it("updates query when clicking an element, showing animated progress", () => {
    expect(dashboard.find(AnimatedProgress)).toHaveLength(0);
    const type = dashboard.find(Type);
    const clickableElement = type
      .at(0)
      .childAt(0)
      .childAt(0);
    clickableElement.simulate("click");
    expect(dashboard.find(AnimatedProgress)).toHaveLength(2);
  });

  it("clears the query when clicking Show All", () => {
    dashboard.find(Button).simulate("click");
    expect(dashboard.find(AnimatedProgress)).toHaveLength(0);
    expect(dashboard.find(Type)).toHaveLength(2);
  });

  it("shows empty box for non existent vehicle", () => {
    dashboard.find("input").instance().value = "QQQ-321";
    dashboard
      .find("input")
      .simulate("change", { target: { value: "QQQ-321" } });

    expect(dashboard.find(Placeholder)).toHaveLength(1);
    expect(dashboard.find(Placeholder).prop("placeholder")).toEqual("empty");
  });

  it("shows network error", () => {
    dashboard.setState({
      query: "",
      sorting: "None",
      filterType: "All",
      vehicles: [],
      loadingAll: false,
      error: true
    });

    expect(dashboard.find(Placeholder)).toHaveLength(1);
  });
});
