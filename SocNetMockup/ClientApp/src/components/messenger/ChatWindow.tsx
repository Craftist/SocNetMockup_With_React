import React, {useState} from "react";
import {MessageObject} from "./Message";
import {PeerListItemObject} from "./PeerListItem";

export function ChatWindow() {
    const [peer, setPeer] = useState<PeerListItemObject>();
    const [messages, setMessages] = useState<MessageObject[]>();

    return (
        <div id="ChatWindow">
            <div id="ChatWindowHeader">
                <div>Chat name: {peer?.name ?? "<Undefined Chat>"}</div>
            </div>
            {messages}
        </div>
    )
}