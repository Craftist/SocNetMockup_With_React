import React, {FC, forwardRef, useEffect, useState} from "react";
import {MessageObject} from "./Message";
import {PeerListItemObject} from "./PeerListItem";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import authService from "../api-authorization/AuthorizeService";
import {Api} from "../api/Api";

export interface ChatWindowProps {
    children?: never

    selectedPeer?: PeerListItemObject
}

export interface MessageDto {
    /*
    public Guid Id { get; set; }
    public GroupChatMemberDto Sender { get; set; }
    public GroupChatDto? Chat { get; set; }
    public GroupChatDto? Peer { get; set; }
    */

    id: string
    sender: GroupChatMemberDto
    chat?: GroupChatDto
    peer?: GroupChatDto
}

export interface GroupChatMemberDto {
    /*
    public Guid Id { get; set; }
    public string UserName { get; set; }
    */

    id: string
    userName: string
}

export interface GroupChatDto {
    /*
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public GroupChatMemberDto Owner { get; set; }
    */

    id: string
    title: string
    creationDate: string
    owner: GroupChatMemberDto
}

export const ChatWindow: FC<ChatWindowProps> = (props: ChatWindowProps) => {
    const [messages, setMessages] = useState<MessageObject[]>([]);
    const [hubConnection, setHubConnection] = useState<HubConnection>();

    useEffect(() => {
        const establishConnection = async () => {
            const token = await authService.getAccessToken() as string;
            console.log(`Access token: '${token}'`)
            const signalRConnection = new HubConnectionBuilder()
                .withUrl("/chat", { accessTokenFactory: () => token })
                .withAutomaticReconnect()
                .build();

            setHubConnection(signalRConnection);
        };

        establishConnection();
    }, []);

    useEffect(() => {
        if (hubConnection) {
            hubConnection.start().then(() => {
                console.log(`Hub connected, connection id: '${hubConnection.connectionId}'`);
                hubConnection.on("OnMessage", (messageDto: MessageDto) => {
                    console.log('Received message:', messageDto);
                });
                hubConnection.on("OnMessageSimple", (message: string) => {
                    console.log('Received simple message:', message);
                });
            });
        }
    }, [hubConnection]);

    useEffect(() => {
        if (!props.selectedPeer) return; // ignore peer if it's falsy

        const messages = Api.Messages.Get();
    }, [props.selectedPeer])

    const sendMessage = (peerId: string, message: string) => {
        if (hubConnection) {
            console.log(`Before sending message '${message}' to peer '${peerId}'`);
            hubConnection.invoke<boolean>("OnMessageSimple", peerId, message).then((result) => {
                console.log('Sent simple message:', message);
                console.log('  Result:', result)
            });
        }
    };

    return props.selectedPeer ? (
        <div id="ChatWindow">
            <div id="ChatWindowHeader">
                {props.selectedPeer.name}
            </div>
            <div id="ChatHistory">
                {messages.map(msg => <div>
                    <div><b>Chat or peer ID:</b> <span>{msg.chat?.id ?? msg.peer?.id ?? "none"}</span></div>
                    <div><b>Body:</b> <span>{msg.body}</span></div>
                </div>)}
            </div>
            <div id="ChatWindowFooter">
                <textarea id="ChatWindowFooter-MessageTextArea" cols={80} rows={3} onKeyDown={
                    (event) => {
                        const target = event.target as HTMLTextAreaElement;
                        if (event.key === "Enter" && props.selectedPeer) {
                            event.preventDefault();
                            sendMessage(props.selectedPeer.id, target.value);
                            target.value = "";
                        }
                    }
                } />
            </div>
        </div>
    ) : <></>
};