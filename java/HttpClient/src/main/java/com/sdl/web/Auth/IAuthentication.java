package com.sdl.web.Auth;

import com.sdl.web.Request.IHttpClientRequest;

public interface IAuthentication {
    void ApplyManualAuthentication(IHttpClientRequest request);
}
