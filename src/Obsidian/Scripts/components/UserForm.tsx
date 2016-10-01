import * as React from "react"
export const UserForm = (props) => (
    
    <div className="content-wrapper well">
        <form onSubmit={props.onSubmit}>
            Username: <input type="text" name="username" onChange={props.onInputChange} value={props.username} required></input>
            Password: <input type="password" name="password" onChange={props.onInputChange} value={props.password} required></input>
    <button className="btn btn-lg btn-success" type="submit">{props.action}</button>
    </form>
        {props.isComplete ?
            <div className="alert alert-dismissible alert-success">
                    <button type="button" className="close" data-dismiss="alert">×</button>
                    <strong>Success</strong><br/>
                    {props.action} success.
                </div>
            : null}
            {
                props.error ?
                <div className="alert alert-dismissible alert-danger">
                    <button type="button" className="close" data-dismiss="alert">×</button>
                    <strong>An error occured when {props.action}.</strong><br/>
                    {props.error}
                </div>:null
            }

    </div>
);