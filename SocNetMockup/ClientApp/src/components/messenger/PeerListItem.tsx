import React from "react";

export interface PeerProps extends PeerListItemObject {
    id: string
    name: string
    lastMessage?: string
    image?: string

    selected: boolean

    onClick?: (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => void
}

export function PeerListItem(props: PeerProps) {
    return (
        <div className={"peer-list-item " + (props.selected ? "selected" : "")} onClick={props.onClick}>
            <div className="image"><img src="img/unknown-peer-chat.png"/> </div>
            <div className="name">{props.name}</div>
        </div>
    )
}

export class PeerListItemObject {
    id: string;
    name: string;
    lastMessage?: string;
    image?: string;

    constructor(id: string, name: string) {
        this.id = id;
        this.name = name;
    }

}