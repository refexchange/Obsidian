﻿import * as React from "react";

import { Router, Route, IndexRoute, browserHistory } from "react-router";
import { PortalContentWrapper } from "../components/PortalContentWrapper";
import { UserManagementContainer } from "../containers/UserManagementContainer";
import { Portal } from "../components/Portal";
import { UserCreationContainer } from "../containers/UserCreationContainer"

export const routes = (
    <Router history={ browserHistory }>
        <Route path="/manage" component={ PortalContentWrapper }>
            <IndexRoute component={ Portal }/>
            <Route path="/manage/users" component={ UserManagementContainer } />
            <Route path="/manage/users/create" component={ UserCreationContainer } />
        </Route>
    </Router>
);

