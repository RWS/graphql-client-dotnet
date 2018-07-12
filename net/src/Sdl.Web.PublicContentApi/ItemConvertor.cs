using System;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi
{
    public class ItemConvertor : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(IItem) == objectType;       

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var target = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
            var expConverter = new ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(target.ToString(), expConverter);
            IItem item = null;
            int itemType = (int) obj.itemType;
            switch (itemType)
            {
                case (int) ItemType.Publication:
                    item = new Publication();
                    break;
                case (int) ItemType.Component:
                    item = new Component();
                    break;
                case (int) ItemType.Category:
                case (int)ItemType.Keyword:
                    item = new Keyword();
                    break;
                case (int) ItemType.Page:
                    item = new Page();
                    break;
                case (int) ItemType.StructureGroup:
                    item = new StructureGroup();
                    break;
                case (int) ItemType.ComponentTemplate:
                    item = new Template();
                    break;
            }
            if (item == null) return null;
            serializer.Populate(target.CreateReader(), item);
            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
