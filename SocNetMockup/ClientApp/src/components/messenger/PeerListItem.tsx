import React from "react";

export interface PeerProps {
    id: string
    name: string
    lastMessage?: string
    image?: string
}

export function PeerListItem(props: PeerProps) {
    return (
        <div>
            <div>Peer ID: {props.id}</div>
            <div>Peer Name: {props.name}</div>
        </div>
    )
}

export class PeerListItemObject implements PeerProps {
    id: string;
    name: string;
    lastMessage?: string;
    image?: string;

    constructor(id: string, name: string) {
        this.id = id;
        this.name = name;
    }

}