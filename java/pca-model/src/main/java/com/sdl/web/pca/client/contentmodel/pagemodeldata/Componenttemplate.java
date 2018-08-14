package com.sdl.web.pca.client.contentmodel.pagemodeldata;
import java.util.Date;

public class Componenttemplate {

    private String Id;
    private Date RevisionDate;

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public Date getRevisionDate() {
        return RevisionDate;
    }

    public void setRevisionDate(Date revisionDate) {
        RevisionDate = revisionDate;
    }
}