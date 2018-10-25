using Sdl.Tridion.Api.IQQuery.Model.Search;

namespace Sdl.Tridion.Api.IQQuery.Model.Compile
{
    /// <summary>
    /// Query Compiler
    /// </summary>
    public interface IQueryCompiler
    {
        string Compile(SearchNode node);
    }
}
