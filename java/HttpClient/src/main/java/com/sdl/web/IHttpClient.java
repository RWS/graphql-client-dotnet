package com.sdl.web;

import com.sun.jndi.toolkit.url.Uri;

public interface IHttpClient {
    Uri BaseUri = null;
    int Timeout = 0;
    String UserAgent = null;
   /* IHttpClientResponse<T> Execute<T>(IHttpClientRequest request);
    Task<IHttpClientResponse<T>> ExecuteAsync<T>(IHttpClientRequest request, CancellationToken cancellationToken);*/
}
