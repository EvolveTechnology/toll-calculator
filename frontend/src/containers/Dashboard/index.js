import React, { Fragment, Component } from "react";

import Filter from "../../components/Filter";
import Search from "../../components/Search";
import Summary from "../../components/Summary";
import VehicleList from "../../components/VehicleList";
import Spinner from "../../components/Spinner";

import Fabs from "../../components/Fabs";

import { queryAll } from "../../api";
import {
  vehicleTypesAccumulator,
  isValidRegNum,
  sortingByTotalFees,
  upperCase
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

const initialState = {
  query: "",
  sorting: "None",
  filterType: "All",
  vehicles: [],
  loadingAll: false
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

  onChangeFilter = type => e => {
    const update = e.target.value;
    return this.setState({ [type]: update });
  };

  updateState = newState => this.setState({ ...newState });

  request = () => queryAll(this.updateState, initialState);

  loadAll = () => {
    return this.setState({ loadingAll: true }, this.request);
  };

  render() {
    const { vehicles, sorting, filterType, query, loadingAll } = this.state;

    const sortedVehicles = sortingByTotalFees(sorting, vehicles)
      .filter(({ type }) => filterType === ALL || type === filterType)
      .filter(({ regNum }) => !query || query === regNum);

    const typeOptions = [ALL, ...vehicleTypesAccumulator(vehicles)];

    return (
      <Fragment>
        <div className="admin-container">
          <h1 id="admin" className="lead admin-title">
            Welcome!
          </h1>
          <div className="filters">
            <Search search={this.search} track={this._input} />
            <Filter
              title={TYPE}
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
          {vehicles.length > 0 && <VehicleList vehicles={sortedVehicles} />}
        </div>
        <Fabs>
          <button
            className="btn btn-primary"
            onClick={this.loadAll}
            onMouseDown={e => e.preventDefault()}
          >
            {vehicles.length ? REFRESH : LOAD_ALL}
          </button>
        </Fabs>
        <Spinner show={loadingAll} />
      </Fragment>
    );
  }
}

export default Admin;
