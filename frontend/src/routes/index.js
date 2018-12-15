import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import Landing from "../containers/Landing";
import Dashboard from "../containers/Dashboard";
import NoMatch from "../containers/NoMatch";
import Nav from "../containers/Nav";
import Offline from "../containers/Offline";

export function Routes() {
  return (
    <BrowserRouter>
      <Offline>
        <Nav />
        <Switch>
          <Route exact path="/dashboard" component={Dashboard} />
          <Route exact path="/" component={Landing} />
          <Route component={NoMatch} />
        </Switch>
      </Offline>
    </BrowserRouter>
  );
}

export default Routes;
