﻿namespace Sdl.Web.GraphQL.Schema
{
    public class GraphQLSchemaField
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQLSchemaTypeInfo Type { get; set; }
    }
}
