using Sdl.Web.IQQuery.Model.Search;

namespace Sdl.Web.IQQuery.Model.Compile
{
    /// <summary>
    /// Query Compiler
    /// </summary>
    public interface IQueryCompiler
    {
        string Compile(SearchNode node);
    }
}
