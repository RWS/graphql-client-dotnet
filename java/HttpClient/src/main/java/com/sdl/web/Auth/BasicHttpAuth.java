package com.sdl.web.Auth;

import com.sdl.web.Request.IHttpClientRequest;

public class BasicHttpAuth implements IAuthentication{

    public String Username;
    public String Password;

    public void setUsername(String username) {
        Username = username;
    }

    public String getUsername() {
        return Username;
    }

    public void setPassword(String password) {
        Password = password;
    }

    @Override
    public void ApplyManualAuthentication(IHttpClientRequest request) {

    }
}
