using System;
using System.Collections.Generic;
using System.Text;
using BuildGraphQLModel.Extensions;
using System.IO;
using Sdl.Web.GraphQLClient;
using Sdl.Web.GraphQLClient.Schema;

namespace BuildGraphQLModel
{
    /// <summary>
    /// Generates strongly typed model from graphQL schema.
    /// 
    /// Example: 
    ///   run with cmdline args: -e http://localhost:8081/udp/content -ns sdl.web -o model.cs
    /// 
    /// Notes:
    ///   Feel free to modify so you can detect the extension of the output (.cs or .java) and
    ///   adjust the generated code as required so you can generate the model for Java also.
    /// </summary>
    class Program
    {

        // gen -e http://localhost:8081 -ns sdl.web -o model.cs -f cs
        // gen -e http://localhost:8081 -ns sdl.web -o model.java -f java
        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("BuildGraphQLModel -ns <namespace> -e <graphQL endpoint> -o <output file> -f <File language extension");
                Console.WriteLine("For Example : ");
                Console.WriteLine("BuildGraphQLModel -ns Sdl.Web.PublicContentApi.ContentModelDemo -e http://localhost:8081/udp/content -o c:\\fileoutput\\ContentModel.cs -f cs");
                string endpoint = null;
                string outputFile = null;
                string filelanguage = null;
                string ns = null;
                for (int i = 0; i < args.Length; i += 2)
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
                        case "-f":
                            filelanguage = args[i + 1];
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
                if (string.IsNullOrEmpty(filelanguage))
                {
                    Console.WriteLine("Specify file language(cs or java).");
                    return -1;
                }
                GraphQLClient client = new GraphQLClient(endpoint);
                GraphQLSchema schema = client.Schema;
                if (filelanguage == "cs")
                    GenerateSchemaClasses(schema, ns, outputFile);
                else if (filelanguage == "java")
                    GenerateJavaSchemaClasses(schema, ns, outputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured when generating classes.");
                return -1;
            }
            return 0;
        }

        static string Indent(int level) => new string('\t', level);


        #region   //Csharp file generator
        static void EmitComment(ref StringBuilder sb, string comment, int indent)
        {
            if (string.IsNullOrEmpty(comment)) return;
            sb.AppendLine($"{Indent(indent)}/// <summary>");
            sb.AppendLine($"{Indent(indent)}/// {comment}");
            sb.AppendLine($"{Indent(indent)}/// </summary>");
        }

        static void EmitFields(ref StringBuilder sb, List<GraphQLSchemaField> fields, int indent, bool isPublic)
        {
            if (fields == null) return;
            foreach (var field in fields)
            {
                sb.AppendLine("");
                EmitComment(ref sb, field.Description, indent);
                sb.AppendLine(
                    $"{Indent(indent)}{(isPublic ? "public " : "")}{field.Type.TypeName()} {field.Name.PascalCase()} {{ get; set; }}");
            }
        }


        static void EmitFields(ref StringBuilder sb, List<GraphQLSchemaEnum> enumValues, int indent)
        {
            if (enumValues == null) return;
            for (int i = 0; i < enumValues.Count - 1; i++)
            {
                sb.AppendLine("");
                EmitComment(ref sb, enumValues[i].Description, indent);
                sb.AppendLine($"{Indent(indent)}{enumValues[i].Name.PascalCase()},");
            }
            sb.AppendLine(
                $"\n{Indent(indent)}{enumValues[enumValues.Count - 1].Name.PascalCase()}");
        }

        static void GenerateClass(StringBuilder sb, GraphQLSchema schema, GraphQLSchemaType type, int indent)
        {
            if (type.Name.StartsWith("__")) return;
            if (type.Kind.Equals("SCALAR")) return;
            EmitComment(ref sb, type.Description, indent);
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
            sb.Append($"\n{Indent(indent)}{{");
            switch (type.Kind)
            {
                case "OBJECT":
                    EmitFields(ref sb, type.Fields, indent + 1, true);
                    break;
                case "INPUT_OBJECT":
                    EmitFields(ref sb, type.InputFields, indent + 1, true);
                    break;
                case "INTERFACE":
                    if (type.PossibleTypes != null)
                    {
                        EmitFields(ref sb, type.Fields, indent + 1, false);
                    }
                    break;
                case "ENUM":
                    EmitFields(ref sb, type.EnumValues, indent + 1);
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("oops");
                    break;
            }
            sb.AppendLine($"{Indent(indent)}}}\n");
        }

        static void GenerateSchemaClasses(GraphQLSchema schema, string ns, string outputFile)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"// This file was generated by a tool on {DateTime.Now}");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Newtonsoft.Json;");
            sb.AppendLine("using Newtonsoft.Json.Converters;\n");
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            foreach (var type in schema.Types)
            {
                GenerateClass(sb, schema, type, 1);
            }
            sb.AppendLine("}");
            string output = sb.ToString();
            if (File.Exists(outputFile)) File.Delete(outputFile);
            File.AppendAllText(outputFile, output);
        }



        #endregion


        #region Java file generator
        static void GenerateJavaSchemaClasses(GraphQLSchema schema, string ns, string outputFile)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"//This java file was generated by a tool on {DateTime.Now}");
            sb.AppendLine($"package {ns};");
            sb.AppendLine("import java.util.List;");
            sb.AppendLine("import java.util.Dictionary;");
            foreach (var type in schema.Types)
            {
                GenerateJavaClass(sb, schema, type, 1);
            }

            string output = sb.ToString();
            if (File.Exists(outputFile)) File.Delete(outputFile);
            File.AppendAllText(outputFile, output);
        }

        static void GenerateJavaClass(StringBuilder sb, GraphQLSchema schema, GraphQLSchemaType type, int indent)
        {
            if (type.Name.StartsWith("__")) return;
            if (type.Kind.Equals("SCALAR")) return;
            EmitComment(ref sb, type.Description, indent);
            if (type.Kind.Equals("ENUM"))
            {
                sb.AppendLine($"{Indent(indent)}/*[JsonConverter(typeof(StringEnumConverter))]*/");
            }
            sb.Append($"{Indent(indent)}{type.EmitTypeDecl()}");
            if (type.Interfaces != null && type.Interfaces.Count > 0)
            {
                sb.Append(" implements ");
                for (int i = 0; i < type.Interfaces.Count - 2; i++)
                {
                    sb.Append($"{type.Interfaces[i].TypeName()}, ");
                }
                sb.Append($"{type.Interfaces[type.Interfaces.Count - 1].TypeName()}");
            }

            sb.Append($"\n{Indent(indent)}{{");
            switch (type.Kind)
            {
                case "OBJECT":
                    EmitJavaFields(ref sb, type.Fields, indent + 1, false, true);
                    break;
                case "INPUT_OBJECT":
                    EmitJavaFields(ref sb, type.InputFields, indent + 1, false, true);
                    break;
                case "INTERFACE":
                    if (type.PossibleTypes != null)
                    {
                        EmitJavaFieldsForInterface(ref sb, type.Fields, indent + 1, false);
                    }
                    break;
                case "ENUM":
                    EmitFields(ref sb, type.EnumValues, indent + 1);
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("oops");
                    break;
            }
            sb.AppendLine($"{Indent(indent)}}}\n");
        }

        static void EmitJavaFieldsForInterface(ref StringBuilder sb, List<GraphQLSchemaField> fields, int indent, bool isPublic)
        {

            string methodPrefix = "get";

            if (fields == null) return;
            sb.AppendLine("");
            // foreach for interface variable declaration 
            foreach (var field in fields)
            {
                string strVariableDefaultValue = "null";
                string strDataType = "";
                string strVariable = "";

                HandleDataTypeAndVariablesForFieldDeclaration(field, ref strDataType, ref strVariableDefaultValue, ref strVariable);
                sb.AppendLine(
                    $"{Indent(indent)}{(isPublic ? "public" : "")}{strDataType} {strVariable + " = "}{strVariableDefaultValue + ";"}  ");

            }
            sb.AppendLine("");

            foreach (var field in fields)
            {
                string inputParameter = "";
                string strDataType = "";
                string strVariable = "";

                HandleDataTypeAndMethodNamesForGetterSetter(ref sb, field, ref strDataType, ref strVariable, ref inputParameter, ref methodPrefix);
                //   EmitComment(ref sb, field.Description, indent);
                sb.AppendLine(
              $"{Indent(indent)}{(isPublic ? "public " : "")}{strDataType} {methodPrefix + field.Name.PascalCase() + "();"}");

                sb.AppendLine(
               $"{Indent(indent)}{(isPublic ? "public " : "")}{"void"} {"set" + field.Name.PascalCase() + $"{"(" + strDataType} {inputParameter}" + ")"};");

            }
        }

        static void EmitJavaFields(ref StringBuilder sb, List<GraphQLSchemaField> fields, int indent, bool isPublic, bool isGetterSetterPublic)
        {

            string methodPrefix = "get";

            if (fields == null) return;
            sb.AppendLine("");
            foreach (var field in fields)
            {
                string strVariableDefaultValue = "null";
                string strDataType = "";
                string strVariable = "";

                HandleDataTypeAndVariablesForFieldDeclaration(field, ref strDataType, ref strVariableDefaultValue, ref strVariable);
                sb.AppendLine(
                    $"{Indent(indent)}{(isPublic ? "public " : "private ")}{strDataType} {strVariable + ";"}   ");
            }

            //  foreach for class getter and setter
            foreach (var field in fields)
            {
                string inputParameter = "";
                string strThisKeyword = "this.";
                string strVariable = "";
                string strDataType = "";

                HandleDataTypeAndMethodNamesForGetterSetter(ref sb, field, ref strDataType, ref strVariable, ref inputParameter, ref methodPrefix);

                sb.AppendLine(
                 $"{Indent(indent)}{(isGetterSetterPublic ? " public " : "private ")}{strDataType} {methodPrefix + strVariable.PascalCase() + "()\n"}" +
                 $"{Indent(indent)} {"{\n"}" +
                 $"{Indent(indent + 1)} { $"return {strVariable};\n"}" +
                 $"{Indent(indent)} {"}"}");

                sb.AppendLine(
                $"{Indent(indent)}{(isGetterSetterPublic ? " public " : "")}{"void"} {"set" + field.Name.PascalCase() + $"{"(" + strDataType} {inputParameter}" + ")"}\n" +
                $"{Indent(indent)} {"{\n"}" +
                $"{Indent(indent + 1)} {strThisKeyword}{strVariable} = {inputParameter};\n" +
                $"{Indent(indent)} {"}"}");
            }
        }

        static void HandleDataTypeAndVariablesForFieldDeclaration(GraphQLSchemaField field, ref string strDataType, ref string strVariableDefaultValue, ref string strVariable)
        {
            if (field.Type.TypeName(false).ToString() == "int")
            {
                strDataType = "int";
                strVariableDefaultValue = "0";
            }
            else if (field.Type.TypeName(false).ToString() == "bool")
            {
                strDataType = "boolean";
                strVariableDefaultValue = "false";
            }
            else if (field.Type.TypeName(true).ToString() == "List<int?>")
            {
                strDataType = "List<Integer>";
            }
            else if (field.Type.TypeName(false).ToString() == "IDictionary")
            {
                strDataType = "Dictionary";
            }
            else
            {
                strDataType = field.Type.TypeName(false).PascalCase();
            }


            if (field.Name == "abstract")
            {
                strVariable = "Abstract";
            }
            else
            {
                strVariable = field.Name;
            }
        }
        static void HandleDataTypeAndMethodNamesForGetterSetter(ref StringBuilder sb, GraphQLSchemaField field, ref string strDataType, ref string strVariable, ref string inputParameter, ref string methodPrefix)
        {
            const string javaAbstractReservedKeyword = "abstract";
            if (field.Type.TypeName(false).ToString() == "int")
                strDataType = "int";
            else if (field.Type.TypeName(false).ToString() == "bool")
            {
                strDataType = "boolean";
                methodPrefix = "is";
            }
            else if (field.Type.TypeName(true).ToString() == "List<int?>")
            {
                strDataType = "List<Integer>";
            }
            else if (field.Type.TypeName(false).ToString() == "IDictionary")
            {
                strDataType = "Dictionary";
            }
            else
                strDataType = field.Type.TypeName(false).PascalCase();

            if (field.Name == "abstract")
            {
                strVariable = "Abstract";
            }
            else
            {
                strVariable = field.Name;
            }

            sb.AppendLine("");
            //   EmitComment(ref sb, field.Description, indent);


            if (field.Name == javaAbstractReservedKeyword)
            {
                inputParameter = "an" + field.Name.PascalCase();
            }
            else
            {
                inputParameter = field.Name;
            }
        }
        #endregion

    }
}
