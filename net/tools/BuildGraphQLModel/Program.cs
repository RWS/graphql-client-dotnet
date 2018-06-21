using System;
using System.Text;
using Sdl.Web.GraphQL.Schema;
using BuildGraphQLModel.Extensions;
using DxaContentApiClient.GraphQL;
using System.IO;

namespace BuildGraphQLModel
{
    /// <summary>
    /// Generates model from graphQL schema.
    /// 
    /// This tool is just to make life easier/quicker so you can quickly generate a strongly typed model
    /// from a graphQL schema.
    /// </summary>
    class Program
    {       
        static string Indent(int level) => new string('\t', level);

        static void GenerateClass(StringBuilder sb, GraphQLSchema schema, GraphQLSchemaType type, int indent)
        {
            if (type.Name.StartsWith("__")) return;
            if (type.Kind.Equals("SCALAR")) return;            
            if (!string.IsNullOrEmpty(type.Description))
            {
                sb.AppendLine($"{Indent(indent)}/// <summary>");
                sb.AppendLine($"{Indent(indent)}/// {type.Description}");
                sb.AppendLine($"{Indent(indent)}/// </summary>");
            }

            if (type.Kind.Equals("ENUM"))
            {
                sb.AppendLine($"{Indent(indent)}[JsonConverter(typeof(StringEnumConverter))]");
            }
            sb.Append($"{Indent(indent)}public {type.EmitTypeDecl()}");
            if (type.Interfaces != null && type.Interfaces.Count > 0)
            {
                sb.Append(" : ");
                for (int i = 0; i < type.Interfaces.Count - 2; i++)
                {
                    sb.Append($"{type.Interfaces[i].TypeName()}, ");
                }
                sb.Append($"{type.Interfaces[type.Interfaces.Count - 1].TypeName()}");
            }
            sb.AppendLine($"{Indent(indent)}{{");
            switch (type.Kind)
            {
                case "OBJECT":                
                    if (type.Fields != null)
                    {
                        foreach (var field in type.Fields)
                        {
                            if (!string.IsNullOrEmpty(field.Description))
                            {
                                sb.AppendLine($"{Indent(indent + 1)}/// <summary>");
                                sb.AppendLine($"{Indent(indent + 1)}/// {field.Description}");
                                sb.AppendLine($"{Indent(indent + 1)}/// </summary>");
                            }
                            sb.AppendLine(
                                $"{Indent(indent + 1)}public {field.Type.TypeName()} {field.Name.PascalCase()} {{ get; set; }}");
                        }
                    }                   
                    break;
                case "INPUT_OBJECT":
                    if (type.InputFields != null)
                    {
                        foreach (var field in type.InputFields)
                        {
                            if (!string.IsNullOrEmpty(field.Description))
                            {
                                sb.AppendLine($"{Indent(indent + 1)}/// <summary>");
                                sb.AppendLine($"{Indent(indent + 1)}/// {field.Description}");
                                sb.AppendLine($"{Indent(indent + 1)}/// </summary>");
                            }
                            sb.AppendLine(
                                $"{Indent(indent + 1)}public {field.Type.TypeName()} {field.Name.PascalCase()} {{ get; set; }}");
                        }
                    }
                    break;
                case "INTERFACE":
                    if (type.PossibleTypes != null)
                    {
                        foreach (var field in type.Fields)
                        {
                            if (!string.IsNullOrEmpty(field.Description))
                            {
                                sb.AppendLine($"{Indent(indent + 1)}/// <summary>");
                                sb.AppendLine($"{Indent(indent + 1)}/// {field.Description}");
                                sb.AppendLine($"{Indent(indent + 1)}/// </summary>");
                            }
                            sb.AppendLine(
                                $"{Indent(indent + 1)}{field.Type.TypeName()} {field.Name.PascalCase()} {{ get; set; }}");
                        }
                    }
                    break;
                case "ENUM":
                    if (type.EnumValues != null)
                    {
                        for (int i = 0; i < type.EnumValues.Count - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(type.EnumValues[i].Description))
                            {
                                sb.AppendLine($"{Indent(indent + 1)}/// <summary>");
                                sb.AppendLine($"{Indent(indent + 1)}/// {type.EnumValues[i].Description}");
                                sb.AppendLine($"{Indent(indent + 1)}/// </summary>");
                            }
                            sb.AppendLine($"{Indent(indent + 1)}{type.EnumValues[i].Name.PascalCase()},");
                        }
                        sb.AppendLine(
                            $"{Indent(indent + 1)}{type.EnumValues[type.EnumValues.Count - 1].Name.PascalCase()}");
                    }
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("oops");
                    break;
            }
            sb.AppendLine($"{Indent(indent)}}}");
        }

        static void GenerateSchemaClasses(GraphQLSchema schema, string ns, string outputFile)
        {          
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"// This file was generated by a tool on {DateTime.Now}");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Newtonsoft.Json;");
            sb.AppendLine("using Newtonsoft.Json.Converters;");            
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            foreach (var type in schema.Types)
            {
                GenerateClass(sb, schema, type, 1);
            }
            sb.AppendLine("}");
            string output = sb.ToString();
            if(File.Exists(outputFile)) File.Delete(outputFile);
            File.AppendAllText(outputFile, output);
        }

        // gen -e http://localhost:8081 -ns sdl.web -o model.cs
        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("BuildGraphQLModel -ns <namespace> -e <graphQL endpoint> -o <output file>");
                string endpoint = null;
                string outputFile = null;
                string ns = null;
                for (int i = 0; i < args.Length; i+=2)
                {
                    switch (args[i].ToLower())
                    {
                        case "-ns":
                            ns = args[i + 1];
                            break;
                        case "-e":
                            endpoint = args[i + 1];
                            break;
                        case "-o":
                            outputFile = args[i + 1];
                            break;
                    }
                }
                if (string.IsNullOrEmpty(endpoint))
                {
                    Console.WriteLine("Specify GraphQL endpoint address.");
                    return -1;
                }
                if (string.IsNullOrEmpty(outputFile))
                {
                    Console.WriteLine("Specify output file.");
                    return -1;
                }
                if (string.IsNullOrEmpty(ns))
                {
                    Console.WriteLine("Specify namespace.");
                    return -1;
                }
                GraphQLClient client = new GraphQLClient(endpoint);
                GraphQLSchema schema = client.Schema;
                GenerateSchemaClasses(schema, ns, outputFile);
            }
            catch
            {
                Console.WriteLine("An error occured when generating classes.");
                return -1;
            }
            return 0;
        }
    }
}
