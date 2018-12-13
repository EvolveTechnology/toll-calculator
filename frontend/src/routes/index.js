import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Landing from "../containers/Landing";
import Dashboard from "../containers/Dashboard";
import NoMatch from "../containers/NoMatch";

export function Routes() {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/dashboard" component={Dashboard} />
        <Route exact path="/" component={Landing} />
        <Route component={NoMatch} />
      </Switch>
    </BrowserRouter>
  );
}

export default Routes;
