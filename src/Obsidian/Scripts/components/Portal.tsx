﻿import * as React from "react";
import { Motion, spring } from "react-motion";
import { NotificationCenterContainer } from "../containers/NotificationCenterContainer";
import { PortalHeader } from "./PortalElements";

export let Portal = (props) => (
    <Motion defaultStyle={{ opacity: 0 }} style={{ opacity: spring(1) }}>
        {(style) =>
            (
                <div style={style} className="layout-top-nav wrapper skin-purple">
                    {props.children}
                </div>
            )}
    </Motion>
);
