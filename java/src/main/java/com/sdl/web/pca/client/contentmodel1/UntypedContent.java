package com.sdl.web.pca.client.contentmodel1;

import java.util.Dictionary;

public 	class UntypedContent implements IContent {
    private Dictionary data;
    private String id;
    private String type;

    public Dictionary getData()
    {
        return data;
    }
    public void setData(Dictionary data)
    {
        this.data = data;
    }

    public String getId()
    {
        return id;
    }
    public void setId(String id)
    {
        this.id = id;
    }

    public String getType()
    {
        return type;
    }
    public void setType(String type)
    {
        this.type = type;
    }
}
