import React, {ReactNode, useState} from "react";
import {PeerListItem, PeerListItemObject} from "./PeerListItem";

export interface PeerListProps {
    children?: ReactNode
}

export function PeerList(props: PeerListProps) {
    const [peers, setPeers] = useState<PeerListItemObject[]>([])

    return (
        <div id="PeerList">
            {peers.map(peer => <PeerListItem {...peer}/>)}
            {props.children}
        </div>
    )
}