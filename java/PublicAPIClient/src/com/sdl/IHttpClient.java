package com.sdl;

import com.sun.jndi.toolkit.url.Uri;

import java.util.Map;

public interface IHttpClient {

    void setBaseUri(Uri url);
    Uri getBaseUri();


    void setTimeout(int number);
    int getTimeout();

    String getUserAgent();
    void setUserAgent(String useragent);


    void setHeaders(Map<String, String> headers);
    Map<String, String> getHeaders();
}
