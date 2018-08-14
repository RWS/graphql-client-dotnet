
package com.sdl.web.pca.client.contentmodel.pagemodeldata;
import java.util.Date;


public class Xpmmetadata {

    private String PageID;
    private Date PageModified;
    private String PageTemplateID;
    private Date PageTemplateModified;

    public String getPageID() {
        return PageID;
    }

    public void setPageID(String pageID) {
        PageID = pageID;
    }

    public Date getPageModified() {
        return PageModified;
    }

    public void setPageModified(Date pageModified) {
        PageModified = pageModified;
    }

    public String getPageTemplateID() {
        return PageTemplateID;
    }

    public void setPageTemplateID(String pageTemplateID) {
        PageTemplateID = pageTemplateID;
    }

    public Date getPageTemplateModified() {
        return PageTemplateModified;
    }

    public void setPageTemplateModified(Date pageTemplateModified) {
        PageTemplateModified = pageTemplateModified;
    }
}