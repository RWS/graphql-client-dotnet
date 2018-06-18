namespace Sdl.Web.PublicContentApi.ContentModel
{
    public class Pagination : IPagination
    {
        public int First { get; set; }
        public string After { get; set; }
    }
}
