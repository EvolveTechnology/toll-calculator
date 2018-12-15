import React, { Fragment, Component } from "react";

import withResultData from "../withResultData";
import Button from "../../components/Button";
import Filter from "../../components/Filter";
import Search from "../../components/Search";
import Summary from "../../components/Summary";
import VehicleList from "../../components/VehicleList";
import Results from "../../components/Results";
import Spinner from "../../components/Spinner";
import Placeholder from "../../components/Placeholder";

import Fabs from "../../components/Fabs";

import { queryAll } from "../../api";
import {
  partial,
  vehicleTypesAccumulator,
  isValidRegNum,
  sortingByTotalFees,
  upperCase,
  softTopScroll
} from "../../utils";
import {
  sortingOptions,
  SORTING,
  FILTER_TYPE,
  TYPE,
  LOAD_ALL,
  REFRESH,
  ALL
} from "../../constants";

import "./dashboard.css";

const SingleVehicle = partial(withResultData)(Results);

const initialState = {
  query: "",
  sorting: "None",
  filterType: "All",
  vehicles: [],
  loadingAll: false,
  error: false
};

export class Admin extends Component {
  state = {
    ...initialState
  };

  _input = React.createRef();

  search = () => {
    const { value } = this._input.current;
    const query = upperCase(value);

    return this.setState({ query: isValidRegNum(query) ? query : "" });
  };

  updateQuery = regNum => {
    this._input.current.value = regNum;
    return this.search();
  };

  clearQuery = () => {
    this.updateQuery("");
    return softTopScroll();
  };

  onChangeFilter = type => e => {
    const update = e.target.value;
    return this.setState({ [type]: update });
  };

  updateState = newState => this.setState({ ...newState });

  request = () => queryAll(this.updateState, initialState);

  loadAll = () => this.setState({ loadingAll: true }, this.request);

  render() {
    const {
      vehicles,
      sorting,
      filterType,
      query,
      loadingAll,
      error
    } = this.state;

    const sortedVehicles = sortingByTotalFees(sorting, vehicles)
      .filter(({ type }) => filterType === ALL || type === filterType)
      .filter(({ regNum }) => !query || query === regNum);

    const typeOptions = [ALL, ...vehicleTypesAccumulator(vehicles)];

    const hasVehicles = !!vehicles.length;
    const hasSortedVehicles = !!sortedVehicles.length;

    const showSingleView = hasSortedVehicles && isValidRegNum(query);
    const showEmptyBox =
      !hasSortedVehicles && isValidRegNum(query) && hasVehicles;

    return (
      <Fragment>
        <div className="admin-container">
          <h1 id="admin" className="lead title">
            Welcome!
          </h1>
          <div className="filters">
            <Search search={this.search} track={this._input} />
            <Filter
              title={TYPE}
              disabled={!!query}
              change={this.onChangeFilter(FILTER_TYPE)}
              options={typeOptions}
            />
            <Filter
              title={SORTING}
              change={this.onChangeFilter(SORTING)}
              options={sortingOptions}
            />
          </div>
          <Summary vehicles={vehicles} sortedVehicles={sortedVehicles} />
          {hasVehicles && (
            <VehicleList
              vehicles={sortedVehicles}
              vehicleClickAction={this.updateQuery}
            />
          )}
          {showSingleView && SingleVehicle(sorting, sortedVehicles)}
        </div>
        {showEmptyBox && <Placeholder placeholder="empty" />}
        {error && <Placeholder placeholder="error" />}
        <Fabs>
          {showSingleView ? (
            <Button text="Show All" onClick={this.clearQuery} />
          ) : (
            <Button
              text={hasVehicles ? REFRESH : LOAD_ALL}
              onClick={this.loadAll}
            />
          )}
        </Fabs>
        <Spinner show={loadingAll} />
      </Fragment>
    );
  }
}

export default Admin;
