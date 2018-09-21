package com.sdl.web.pca.client.util;

import com.google.common.base.Objects;
import com.google.common.base.Strings;
import org.slf4j.Logger;

import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static com.sdl.web.pca.client.util.ItemTypes.IDNULL;
import static com.sdl.web.pca.client.util.ItemTypes.getById;
import static java.lang.Integer.valueOf;
import static org.slf4j.LoggerFactory.getLogger;

/**
 * This class represents a CMURI object.
 * <p>
 * This object is a helper object used to parse and create Content Manager (TCM or ISH) URIs.
 */
public class CmUri {
    private static final Logger LOG = getLogger(CmUri.class);
    private static final String SEPARATOR = "-";
    private static final String URI_SEPARATOR = ":";
    private static final Pattern CM_URI_PATTERN = Pattern
            .compile("^(?<namespace>[a-zA-Z]+):(?<pubId>\\d+)-(?<itemId>\\d+)(?:-(?<itemType>\\d+))?(?:-v(?<version>\\d+))?$");

    private Namespace namespace;
    private int pubId;
    private int itemId;
    private int itemType;
    private Optional<Integer> version = Optional.empty();

    /**
     * Create a new URI from a string.
     *
     * @param uri String containing the URI to be parsed.
     */
    public CmUri(String uri) {
        this.load(uri);
    }

    private void load(String uriString) {
        Matcher matcher = CM_URI_PATTERN.matcher(uriString);
        if (!matcher.find()) {
            throw new IllegalArgumentException("Unable to parse CMURI: " + uriString);
        }

        this.namespace = Namespace.valueByName(matcher.group("namespace"));
        this.pubId = valueOf(matcher.group("pubId"));
        this.itemId = valueOf(matcher.group("itemId"));
        String itemTypeStr = matcher.group("itemType");
        if (Strings.isNullOrEmpty(itemTypeStr)) {
            itemType = ItemTypes.COMPONENT.getValue();
        } else {
            // assign itemType with validation
            itemType = getById(valueOf(itemTypeStr)).getValue();
        }

        String versionStr = matcher.group("version");
        if (!Strings.isNullOrEmpty(versionStr)) {
            version = Optional.of(valueOf(versionStr));
        }

    }

    /**
     * Overridden implementation of <code>toString()</code> in <code>Object</code>.
     *
     * @return A string representation of this <code>CMURI</code>.
     */
    public String toString() {
        String versionStr = version.isPresent()
                ? "-v" + getVersion()
                : "";
        return this.namespace.getName() + URI_SEPARATOR + this.pubId + SEPARATOR + this.itemId + SEPARATOR
                + this.itemType + versionStr;
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
     * Get the item Id for this URI.
     *
     * @return The item Id.
     */
    public int getItemId() {
        return this.itemId;
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
     * Get the version for this URI.
     *
     * @return The version.
     */
    public int getVersion() {
        return version.orElse(IDNULL.getValue());
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        CmUri cmUri = (CmUri) o;
        return pubId == cmUri.pubId &&
                itemId == cmUri.itemId &&
                itemType == cmUri.itemType &&
                namespace == cmUri.namespace &&
                Objects.equal(version, cmUri.version);
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(namespace, pubId, itemId, itemType, version);
    }

    /**
     * Represents namespace values.
     */
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
                throw new IllegalArgumentException("Unable to resolve namespace '" + name + "'");
            }
            return result;
        }
    }
}