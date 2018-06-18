namespace Sdl.Web.PublicContentApi.ContentModel
{
    public interface IPagination
    {
        int First { get; set; }
        string After { get; set; }
    }
}
