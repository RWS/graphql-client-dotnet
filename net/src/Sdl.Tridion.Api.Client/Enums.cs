namespace Sdl.Tridion.Api.Client
{
    public enum ContentIncludeMode
    {
        IncludeData,
        IncludeDataAndRender,
        IncludeJson,
        IncludeJsonAndRender,
        Exclude
    }

    public enum TcdlLinkRendering
    {
        Absolute,
        Relative
    }

    public enum ModelServiceLinkRendering
    {
        Absolute,
        Relative
    }

    // Strategy of DCP template resolving is template ID is missing in request.
    public enum DcpType
    {
        DEFAULT, // If template is not set, then load a default DXA Data Presentation.         
        HIGHEST_PRIORITY // If template is not set, then load a Component Presentation with the highest priority.
    }

    public enum ContentType
    {
        RAW, // RAW will perform no conversion and return what is in the Broker
        MODEL,
    }

    public enum DataModelType
    {
        R2,  // Return R2 data model
        DD4T // Return DD4T data model format
    }

    public enum PageInclusion
    {
        INCLUDE, // Page regions should be included.        
        EXCLUDE  // Page regions should be excluded.
    }  
}
