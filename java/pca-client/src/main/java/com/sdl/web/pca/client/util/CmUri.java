package com.sdl.web.pca.client.util;

import org.slf4j.Logger;

import java.text.ParseException;
import java.util.HashMap;
import java.util.Map;
import java.util.Objects;
import java.util.StringTokenizer;

import static com.sdl.web.pca.client.util.ItemTypes.COMPONENT;
import static com.sdl.web.pca.client.util.ItemTypes.IDNULL;
import static com.sdl.web.pca.client.util.ItemTypes.NULL;
import static java.util.Comparator.comparing;
import static org.slf4j.LoggerFactory.getLogger;

/**
 * This class represents a CMURI object.
 * <p>
 * This object is a helper object used to parse and create Content Manager (TCM or ISH) URIs.
 */
public class CmUri implements Comparable<CmUri> {

    private static final String SEPARATOR = "-";
    private static final String URI_SEPARATOR = ":";
    private static final Namespace DEFAULT_NAMESPACE = Namespace.SITES;
    private static final Logger LOG = getLogger(CmUri.class);


    /**
     * Null URI. Defaulting to TCM namespace.
     */
    public static final CmUri NULL_URI = new CmUri(DEFAULT_NAMESPACE.getName(), 0, 0, 0, 0);

    private Namespace namespace;
    private int itemType;
    private int itemId;
    private int pubId;
    private int version;

    /**
     * Create a new URI based on namespace, publication Id, item Id, item type and version.
     *
     * @param namespace     namespace for this URI.
     * @param publicationId publicationId for this URI.
     * @param itemId        itemId for this URI.
     * @param itemType      itemType for this URI.
     * @param version       version for this URI.
     */
    public CmUri(String namespace, int publicationId, int itemId, int itemType, int version) {
        this.namespace = Namespace.valueByName(namespace.toLowerCase());
        this.itemType = itemType;
        this.itemId = itemId;
        this.pubId = publicationId;
        this.version = version;
    }

    /**
     * Create a new URI from a string.
     *
     * @param uri String containing the URI to be parsed.
     * @throws ParseException Thrown if the String does not contain a valid <code>CMURI</code>
     */
    public CmUri(String uri) throws ParseException {
        this.namespace = null;
        this.itemType = NULL.getValue();
        this.itemId = IDNULL.getValue();
        this.pubId = IDNULL.getValue();
        this.version = IDNULL.getValue();
        this.load(uri);
    }

    /**
     * This method is used to create a <code>CMURI</code> based on a String URI.
     *
     * @param uriString String representation of the URI.
     * @throws ParseException Thrown if the String does not contain a valid <code>CMURI</code>
     */
    protected void load(String uriString) throws ParseException {
        if (uriString != null) {
            if (!uriString.contains(URI_SEPARATOR)) {
                throw new ParseException("Separator character in uri [" + uriString +
                        "] is not found. Cannot determine namespace.", 0);
            }

            if (namespace == null) {
                // in case namespace is not already set by the subclasses
                this.namespace = Namespace.valueByName(uriString.substring(0, uriString.indexOf(URI_SEPARATOR)));
            }
            int namespacePlusColonLength = namespace.getName().length() + 1;
            int uriStringLength = uriString.length();
            int currentPosition = uriStringLength; //current position during parsing, used for reporting errors

            String uri = uriString.substring(namespacePlusColonLength, uriStringLength);
            StringTokenizer st = new StringTokenizer(uri, SEPARATOR);
            if (st.countTokens() < 2) {
                throw new ParseException("URI string " + uriString + " does not contain enough ID's", currentPosition);
            }
            try {
                String token = st.nextToken();
                currentPosition += token.length();
                this.pubId = Integer.parseInt(token);

                token = st.nextToken();
                currentPosition += token.length();
                this.itemId = Integer.parseInt(token);

                if (!st.hasMoreTokens()) { //if we only have two tokens, then assume component
                    this.itemType = COMPONENT.getValue();
                } else {
                    token = st.nextToken();
                    currentPosition += token.length();
                    if (!token.startsWith("v")) {  //item type for non-component
                        this.itemType = Integer.parseInt(token);
                    } else { //version for component
                        this.version = Integer.parseInt(token.substring(1, token.length()));
                        this.itemType = COMPONENT.getValue();
                    }

                    if (st.hasMoreTokens()) { //version for non component
                        token = st.nextToken();
                        currentPosition += token.length();
                        this.version = Integer.parseInt(token.substring(1, token.length()));
                    }
                }
            } catch (NumberFormatException e) {
                throw new ParseException("Invalid ID (not an integer) in URI string " + uriString, currentPosition);
            }
        } else {
            throw new ParseException("Invalid CMURI String, string cannot be null", 0);
        }
    }

    /**
     * Overridden implementation of <code>toString()</code> in <code>Object</code>.
     *
     * @return A string representation of this <code>CMURI</code>.
     */
    public String toString() {
        return this.namespace.getName() + URI_SEPARATOR + this.pubId + SEPARATOR + this.itemId + SEPARATOR + this.itemType;
    }

    public void setNamespace(String namespace) {
        this.namespace = Namespace.valueByName(namespace);
    }

    public String getNamespace() {
        return namespace.getName();
    }

    public int getNamespaceId() {
        return namespace.getId();
    }

    /**
     * Get the item type for this URI.
     *
     * @return The type identifier.
     */
    public int getItemType() {
        return this.itemType;
    }

    /**
     * Sets the itemType.
     *
     * @param itemType The new itemType.
     */
    public void setItemType(int itemType) {
        if (isNullUri() && itemType != 0) {
            throw new IllegalStateException("Changing the item type of the NULL_URI is not allowed!!!");
        }

        this.itemType = itemType;
    }

    /**
     * Get the item Id for this URI.
     *
     * @return The item Id.
     */
    public int getItemId() {
        return this.itemId;
    }

    /**
     * Sets the itemId.
     *
     * @param itemId The new itemId
     */
    public void setItemId(int itemId) {
        if (isNullUri() && itemId != 0) {
            throw new IllegalStateException("Changing the item id '" + itemId + "' of the NULL_URI is not allowed!!!");
        }

        this.itemId = itemId;
    }

    /**
     * Get the publicationId for this URI.
     *
     * @return the publication Id.
     */
    public int getPublicationId() {
        return this.pubId;
    }

    /**
     * Sets the publicationId.
     *
     * @param thePubId The new publicationId.
     */
    public void setPublicationId(int thePubId) {
        if (isNullUri() && thePubId != 0) {
            throw new IllegalStateException("Changing the publication id '" + thePubId + "' of the NULL_URI is not allowed!!!");
        }
        this.pubId = thePubId;
    }

    /**
     * Get the version for this URI.
     *
     * @return The version.
     */
    public int getVersion() {
        return this.version;
    }

    /**
     * Sets the version.
     *
     * @param version the new version.
     */
    public void setVersion(int version) {
        if (isNullUri() && version != 0) {
            throw new IllegalStateException("Changing the version of the NULL_URI is not allowed!!!");
        }

        this.version = version;
    }

    /**
     * Check if this object is NULL_URI.
     *
     * @return true if this object is NULL_URI, false otherwise.
     */
    protected boolean isNullUri() {
        return this == NULL_URI;
    }

    /**
     * Compare two URI instances.
     *
     * @param uri <code>CMURI</code> to compare to.
     * @return a boolean indicating if the other URI is equal.
     */
    private boolean equals(String uri) {
        try {
            return this.equals(new CmUri(uri));
        } catch (ParseException e) {
            LOG.debug("Unable to parse uri: {}. Assuming it doesn't equal to {}", uri, this.toString());
            return false;
        }
    }

    /**
     * Compare two URI instances.
     *
     * @param uri <code>CMURI</code> to compare to.
     * @return a boolean indicating if the other URI is equal.
     */
    private boolean equals(CmUri uri) {
        return stringEquals(this.getNamespace(), uri.getNamespace())
                && this.getItemType() == uri.getItemType()
                && this.getItemId() == uri.getItemId()
                && this.getPublicationId() == uri.getPublicationId();
    }

    /**
     * Overridden implementation of <code>equals()</code> method in <code>Object</code>.
     *
     * @param object The reference object with which to compare.
     * @return <code>true</code> if this object is the same as passed <code>Object</code>; <code>false</code> otherwise.
     */
    public boolean equals(Object object) {
        if (object instanceof CmUri) {
            return this.equals((CmUri) object);
        } else if (object instanceof String) {
            return this.equals((String) object);
        }
        return false;
    }

    /**
     * Overridden implementation of <code>hashCode()</code> in <code>Object</code>.
     *
     * @return a hash code value for this object.
     */
    public int hashCode() {
        return this.toString().hashCode();
    }

    @Override
    public int compareTo(CmUri other) {
        return comparing(CmUri::getNamespace)
                .thenComparing(CmUri::getPublicationId)
                .thenComparing(CmUri::getItemType)
                .thenComparing(CmUri::getItemId).compare(this, other);
    }

    public static boolean stringEquals(String str1, String str2) {
        return Objects.isNull(str1) ? Objects.isNull(str2) : str1.equals(str2);
    }

    public enum Namespace {
        SITES("tcm", 1),
        DOCS("ish", 2);

        private static final Map<String, Namespace> namespaceByName = new HashMap<>();

        static {
            for (Namespace namespace : Namespace.values()) {
                namespaceByName.put(namespace.name, namespace);
            }
        }

        private final String name;
        private final int id;

        Namespace(String name, int id) {
            this.name = name;
            this.id = id;
        }

        public String getName() {
            return name;
        }

        public int getId() {
            return id;
        }

        public static Namespace valueByName(String name) {
            Namespace result = namespaceByName.get(name);
            if (result == null) {
                throw new IllegalArgumentException("Unable to resolve namespace " + name);
            }
            return result;
        }
    }

}