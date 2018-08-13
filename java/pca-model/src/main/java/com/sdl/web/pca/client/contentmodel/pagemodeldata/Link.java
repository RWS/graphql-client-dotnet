
package com.sdl.web.pca.client.contentmodel.pagemodeldata;

public class Link {

    private String $type;
    private String linkText;
    private Internallink internalLink;

    public String get$type() {
        return $type;
    }

    public void set$type(String $type) {
        this.$type = $type;
    }

    public String getLinkText() {
        return linkText;
    }

    public void setLinkText(String linkText) {
        this.linkText = linkText;
    }

    public Internallink getInternalLink() {
        return internalLink;
    }

    public void setInternalLink(Internallink internalLink) {
        this.internalLink = internalLink;
    }
}