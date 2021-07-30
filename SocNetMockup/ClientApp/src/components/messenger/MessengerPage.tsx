import * as React from "react";
import {PeerList} from "./PeerList";
import {PeerListItem} from "./PeerListItem";
import {ChatWindow} from "./ChatWindow";

export interface MessengerPageProps {}
export function MessengerPage(props: MessengerPageProps) {
    return (
        <div>
            <PeerList>
                <PeerListItem id="1234" name="First peer" />
                <PeerListItem id="5678" name="Second peer" />
                <PeerListItem id="90ab" name="Third peer" />
            </PeerList>
            <ChatWindow/>
        </div>
    )
}

