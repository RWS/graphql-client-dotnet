package com.sdl.web.pca.client.response;

import java.util.Date;

public class GraphQLExtensions {

    private String Code;
    private Date Timestamp;

    public void setCode(String code) {
        Code = code;
    }

    public String getCode() {
        return Code;
    }

    public void setTimestamp(Date timestamp) {
        Timestamp = timestamp;
    }

    public Date getTimestamp() {
        return Timestamp;
    }
}
