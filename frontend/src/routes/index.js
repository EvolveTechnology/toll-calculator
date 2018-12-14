import React, { Fragment } from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Landing from "../containers/Landing";
import Dashboard from "../containers/Dashboard";
import NoMatch from "../containers/NoMatch";
import Nav from "../containers/Nav";

export function Routes() {
  return (
    <BrowserRouter>
      <Fragment>
        <Nav />
        <Switch>
          <Route exact path="/dashboard" component={Dashboard} />
          <Route exact path="/" component={Landing} />
          <Route component={NoMatch} />
        </Switch>
      </Fragment>
    </BrowserRouter>
  );
}

export default Routes;
