import React, {useEffect, useState} from "react";
import authService from "./api-authorization/AuthorizeService";

export function Token() {
    const [token, setToken] = useState()

    useEffect(function() {
        getToken();
    }, []);

    async function getToken() {
        const tok = await authService.getAccessToken();
        setToken(tok);
    }

    return (
        <p style={{maxWidth: "100%", fontFamily: "monospace"}}>Token: {token}</p>
    )
}