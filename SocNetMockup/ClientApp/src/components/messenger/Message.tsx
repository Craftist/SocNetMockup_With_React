import React from "react";
import {GroupChatDto, GroupChatMemberDto} from "./ChatWindow";

export interface MessageProps {
    id: string
    sender: GroupChatMemberDto
    chat?: GroupChatDto
    peer?: any // FIXME
    body: string
}

export function Message(props: MessageProps) {
    return (
        <div>Message</div>
    )
}

export class MessageObject implements MessageProps {
    body: string;
    chat?: GroupChatDto;
    id: string;
    peer?: any;
    sender: GroupChatMemberDto;


    constructor(id: string, body: string, sender: GroupChatMemberDto, chatOrPeer: { chat?: GroupChatDto, peer?: any }) {
        this.body = body;
        this.id = id;
        this.sender = sender;

        if (chatOrPeer.chat) {
            this.chat = chatOrPeer.chat;
        } else if (chatOrPeer.peer) {
            this.peer = chatOrPeer.peer;
        } else {
            throw new Error("Either chat or peer should be specified for the message object.");
        }
    }
}