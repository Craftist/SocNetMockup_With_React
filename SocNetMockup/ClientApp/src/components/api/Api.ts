import authService from "../api-authorization/AuthorizeService";

//region [[Api.Chats]]
export interface ApiChatEntity {
    id: string
    title: string
    creationDate: string
    owner: ApiChatMemberEntity
}

export interface ApiChatMemberEntity {
    id: string
    userName: string
}

export interface ApiChatsGetByIdResponse {
    success: boolean
    count: number
    response: ApiChatEntity
}

export interface ApiChatsGetAllResponse {
    success: boolean
    count: number
    response: ApiChatEntity[]
}

export interface ApiChats {
    get(): Promise<ApiChatsGetAllResponse>
    get(id: string): Promise<ApiChatsGetByIdResponse>
}
//endregion

//region [[Api.Messages]]
export interface ApiMessageEntity {

}

export interface ApiMessagesGetInChatResponse {
    success: boolean
    count: number
    response: ApiMessageEntity[]
}

export interface ApiMessagesGetByIdResponse {
    success: boolean
    response: ApiMessageEntity
}

export interface ApiMessages {
    getInChat(id: string): Promise<ApiMessagesGetInChatResponse>
}
//endregion

export class Api {
    static Users = Object.freeze({

    });

    static Chats: ApiChats = Object.freeze({
        async get(id?: string) {
            if (typeof id === 'string') {
                return Api._get(`chats/${id}`);
            }
            return Api._get('chats');
        },

        async create(title: string, initialMembers: string[] = []) {
            return Api._post('chats', { title, initialMembers });
        }
    });

    static Messages: ApiMessages = Object.freeze({
        async getInChat(chatId: string): Promise<ApiMessagesGetInChatResponse> {
            return Api._get(`messages/${chatId}`);
        },

        async getById(chatId: string, messageId: string): Promise<ApiMessagesGetByIdResponse> {
            return Api._get(`messages/${chatId}/${messageId}`);
        }
    });

    private static async _get(endpoint: string, body?: any) {
        return fetch('api/' + endpoint, {
            method: 'get',
            body: body != null ? JSON.stringify(body) : null,
            headers: {
                'Authorization': 'Bearer ' + await authService.getAccessToken()
            }
        }).then(async x => {
            const response = await x.text();
            console.table({requestUrl: x.url, responseText: response});
            return JSON.parse(response);
        });
    }

    private static async _post(endpoint: string, body?: any) : Promise<any> {
        return fetch('api/' + endpoint, {
            method: 'post',
            body: body != null ? JSON.stringify(body) : null,
            headers: {
                'Authorization': 'Bearer ' + await authService.getAccessToken()
            }
        }).then(async x => {
            const response = await x.text();
            console.table({requestUrl: x.url, responseText: response});
            return JSON.parse(response);
        });
    }
}

