import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.sdl.web.pca.client.GraphQLClient;
import com.sdl.web.pca.client.contentmodel.ContentQuery;
import graphql.introspection.IntrospectionQuery;
import org.apache.commons.io.IOUtils;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Formatter;
import java.util.List;

public class Program {
    public static void main(String[] args) {
        System.out.print("Hi");
        if (args.length > 0)
        {
            String endpoint = null;
            String outputFile = null;
            String ns = null;
            for (int i = 0; i < args.length; i+=2)
            {
                String argumt = args[i].toLowerCase();
                switch (argumt)
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
            if (isNullOrBlank(endpoint))
            {
                System.out.println("Specify GraphQL endpoint address.");
                return;
            }
            if (isNullOrBlank(outputFile))
            {
                System.out.println("Specify output file.");
                return;
            }
            if (isNullOrBlank(ns))
            {
                System.out.println("Specify namespace.");
                return;
            }

            GraphQLClient client = new GraphQLClient(endpoint, null);

            try {
                String query = IOUtils.toString(ContentQuery.class.getClassLoader().getResourceAsStream("queries/IntrospectionQuery.graphql"), "UTF-8");
                JsonObject body = new JsonObject();
                body.addProperty("query", query);


                String jsonresponse = client.execute(body.toString());
                JsonParser parser = new JsonParser();
                 JsonObject jsonObject = (JsonObject)parser.parse(jsonresponse);
                JsonObject dataObject = jsonObject.getAsJsonObject("data").getAsJsonObject("__schema");



                ObjectMapper objectMapper = new ObjectMapper();
                GraphQLSchema schema = objectMapper.readValue(dataObject.toString(), GraphQLSchema.class);

                GenerateSchemaClasses(schema, ns,outputFile);
                System.out.println(schema);

            }
            catch(JsonMappingException e)
            {
                e.getStackTrace();
            }
            catch(IOException e)
            {
            }
            catch(Exception e)
            {
                e.getStackTrace();
            }
            //GenerateSchemaClasses(schema, ns, outputFile);
        }
    }

    static void GenerateSchemaClasses(GraphQLSchema schema, String ns, String outputFile) throws IOException {

        for (GraphQLSchemaType type : schema.types){
            StringBuilder sb = new StringBuilder();
            sb = EmitHeader(sb, ns);
            StringBuilder sbuilder = GenerateClass(sb, schema, type, 1);
            createJavaFile(type, sbuilder,outputFile);
        }
    }

    static void createJavaFile(GraphQLSchemaType type, StringBuilder sb, String outputFilePath) throws IOException {
        if(type.name==null)
            return;
        File file = new File(outputFilePath+type.name+".java");
        if(file.exists())
            file.delete();

        File newfile = new File(outputFilePath+type.name+".java");
        BufferedWriter writer = null;
        try {
            writer = new BufferedWriter(new FileWriter(newfile));
            writer.append(sb);
        } finally {
            if (writer != null) writer.close();
        }
    }

    static void EmitComment(StringBuilder sb, String comment, int indentCount)
    {
        if (isNullOrBlank(comment)) return;
        String indentString = new String(new char[indentCount]).replace("\0", "\t");
        sb.append(indentString);
        sb.append("/**");
        sb.append("\n");
        sb.append(indentString);
        sb.append("*");
        sb.append(comment);
        sb.append("\n");
        sb.append(indentString);
        sb.append("*/");
        sb.append("\n");
        sb.append("\n");
    }

    static StringBuilder GenerateClass(StringBuilder sb, GraphQLSchema schema, GraphQLSchemaType type, int indentCount)
    {
        String indentString = new String(new char[indentCount]).replace("\0", "\t");
        if (type.name.startsWith("__")) return sb;
        if (type.kind.equalsIgnoreCase("SCALAR")) return sb;
        EmitComment(sb, type.description, indentCount);
        if (type.kind.equalsIgnoreCase("ENUM"))
        {
            sb.append(indentString);
            //sb.append([JsonConverter(typeof(StringEnumConverter))]");
        }
        sb.append(indentString);
        sb.append("public class "+type.name);
        sb.append("{");
        /* if (type.Interfaces != null && type.Interfaces.Count > 0)
        {
            sb.Append(" : ");
            for (int i = 0; i < type.Interfaces.Count - 2; i++)
            {
                sb.Append($"{type.Interfaces[i].TypeName()}, ");
            }
            sb.Append($"{type.Interfaces[type.Interfaces.Count - 1].TypeName()}");
        }
        sb.Append($"\n{Indent(indent)}{{");
        */
        switch (type.kind)
        {
            case "OBJECT":
                EmitFields( sb, type.fields, indentCount + 1, true);
                break;
            case "INPUT_OBJECT":
                EmitFields( sb, type.inputFields, indentCount + 1, true);
                break;
            case "INTERFACE":
                if (type.possibleTypes != null)
                {
                    EmitFields( sb, type.fields, indentCount + 1, false);
                }
                break;
            /*case "ENUM":
                EmitFields(sb, type.enumValues, indentCount + 1);
                break;*/
            default:
                System.out.println("oops");
                break;
        }
        sb.append(indentString);
        sb.append("\n");
        sb.append("}\n");

        return sb;
    }

    static void EmitFields(StringBuilder sb, List<GraphQLSchemaField> fields, int indentCount, Boolean isPublic)
    {
        String indentString = new String(new char[indentCount]).replace("\0", "\t");
        if (fields == null) return;
        for (GraphQLSchemaField field : fields)
        {
            sb.append(indentString);
            sb.append("\n");
            field.type = RemapFieldType(field);
            sb.append(indentString);
            sb.append("\n");
            if(field.type.name !=null) {
                String lowercaseFirstLetter = field.type.name.substring(0, 1).toLowerCase();
                sb.append("public " + field.type.name + " " + lowercaseFirstLetter + field.type.name.substring(1) + ";");
            }
        }
    }

    static GraphQLSchemaTypeInfo RemapFieldType(GraphQLSchemaField field)
    {
        // Just remap itemType and namespaceId(s) from int to use our enum
        // to make things a little nicer to work with.
        GraphQLSchemaTypeInfo graphQLSchemaTypeInfo = new GraphQLSchemaTypeInfo();
        switch (field.name)
        {
            case "namespaceIds":{
                GraphQLSchemaTypeInfo ofType = new GraphQLSchemaTypeInfo();
                ofType.kind = "ENUM";
                ofType.name = "ContentNamespace";

                graphQLSchemaTypeInfo.kind = "LIST";
                graphQLSchemaTypeInfo.ofType = ofType;
                return graphQLSchemaTypeInfo;
            }
            case "namespaceId":
            {
                graphQLSchemaTypeInfo.kind = "ENUM";
                graphQLSchemaTypeInfo.name = "ContentNamespace";
                return graphQLSchemaTypeInfo;
            }
            case "itemType":
            {
                graphQLSchemaTypeInfo.kind = "ENUM";
                graphQLSchemaTypeInfo.name = "Sdl.Web.PublicContentApi.ItemType";
                return graphQLSchemaTypeInfo;
            }
            default:
                return field.type;
        }
    }

    static StringBuilder EmitHeader( StringBuilder sb, String ns)
    {
        Formatter fmt = new Formatter(sb);
        fmt.format("package %s%n", ns);
        return sb;
    }

    public static boolean isNullOrBlank(String param) {
        return param == null || param.trim().length() == 0;
    }
}
