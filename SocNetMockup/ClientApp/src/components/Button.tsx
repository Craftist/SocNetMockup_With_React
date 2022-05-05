import React, {MouseEventHandler} from "react";

export interface ButtonProps {
    children?: string;
    onClick?: MouseEventHandler<HTMLDivElement>;
    color?: string;
    className?: string;
}

export function Button(props: ButtonProps) {
    return (
        <div className={"btn " + (props.className ?? '')} onClick={props.onClick}>
            {props.children}
        </div>
    );
}