import * as React from "react";
import {PeerList} from "./PeerList";
import {PeerListItem, PeerListItemObject} from "./PeerListItem";
import {ChatWindow, ChatWindowProps} from "./ChatWindow";
import {FC, forwardRef, useEffect, useRef, useState} from "react";
import {Api} from "../api/Api";
import {Button} from "../Button";

export interface MessengerPageProps {}

export function MessengerPage(props: MessengerPageProps) {
    const [peers, setPeers] = useState<PeerListItemObject[]>([])
    const [selectedPeer, setSelectedPeer] = useState<PeerListItemObject>()

    const chatWindowRef: React.RefObject<typeof ChatWindow> = React.createRef();

    useEffect(() => {
        async function getPeersAsync() {
            const peerResponse = await Api.Chats.get();
            console.log('peerResponse', peerResponse);

            const newPeers: PeerListItemObject[] = [];
            for (const peer of peerResponse.response) {
                newPeers.push({
                    id: peer.id,
                    name: peer.title
                });
            }

            setPeers(newPeers);
        }

        getPeersAsync();
    }, []);

    return (
        <div id="MessengerPage">
            <PeerList>
                {peers.map(peerProps =>
                    <PeerListItem
                        {...peerProps}
                        selected={peerProps === selectedPeer}
                        onClick={ev => setSelectedPeer(peerProps)} />)}
                <Button className="btn-primary">Add chat</Button>
            </PeerList>
            <ChatWindow selectedPeer={selectedPeer} />
        </div>
    )
}