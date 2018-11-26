using Sdl.Tridion.Api.IqQuery.Model.Search;

namespace Sdl.Tridion.Api.IqQuery.Model.Compile
{
    /// <summary>
    /// Query Compiler
    /// </summary>
    public interface IQueryCompiler
    {
        string Compile(SearchNode node);
    }
}
