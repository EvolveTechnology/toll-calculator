import React, { Fragment, Component } from "react";

import Results from "../../components/Results";
import Search from "../../components/Search";
import Filter from "../../components/Filter";

import Spinner from "../../components/Spinner";
import Placeholder from "../../components/Placeholder";

import { queryOne } from "../../api";
import { sortingOptions } from "../../constants";
import { isValidRegNum, sortingByTotalFees, upperCase } from "../../utils";

import "./landing.css";

const initialState = {
  showSpinner: false,
  results: [],
  regNum: "",
  type: "",
  allTimeTotalFee: 0,
  sorting: "None",
  isTollFree: false,
  error: false
};

class Landing extends Component {
  state = {
    ...initialState
  };

  _input = React.createRef();

  updateState = newState => this.setState({ ...newState });

  request = vehicle => queryOne(vehicle, this.updateState, initialState);

  onChangeFilter = type => e => {
    const update = e.target.value;
    return this.setState({ [type]: update });
  };

  search = e => {
    e.preventDefault();
    const { showSpinner } = this.state;
    const { value } = this._input.current;

    const regNum = upperCase(value);
    return isValidRegNum(regNum) && !showSpinner
      ? this.setState({ showSpinner: true }, () => this.request({ regNum }))
      : null;
  };

  render() {
    const {
      id,
      results,
      type,
      regNum,
      showSpinner,
      allTimeTotalFee,
      sorting,
      isTollFree,
      error
    } = this.state;

    const sorted = sortingByTotalFees(sorting, results);
    const hasResults = !!results.length;
    const isEmptySearch = id === null && isValidRegNum(regNum) && !hasResults;
    return (
      <Fragment>
        <div className="landing">
          <h1 className="lead title">Hello!</h1>
          <Search search={this.search} track={this._input} />
          {hasResults && (
            <Fragment>
              <Filter
                title="sorting"
                change={this.onChangeFilter("sorting")}
                options={sortingOptions}
              />
              <Results
                results={sorted}
                type={type}
                regNum={regNum}
                isTollFree={isTollFree}
                allTimeTotalFee={allTimeTotalFee}
              />
            </Fragment>
          )}
          {isEmptySearch && <Placeholder placeholder="empty" />}
          {error && <Placeholder placeholder="error" />}
        </div>
        <Spinner show={showSpinner} />
      </Fragment>
    );
  }
}

export default Landing;
