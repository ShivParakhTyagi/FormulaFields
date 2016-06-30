using System;
using Mobilize.Contract.CustomFields;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mobilize.Contract.MobilizeDataTypes;

namespace FormulaFieldsWorking_parent_child_handled.MobiFormDocs.JsonConverters
{
    public class FieldInfoJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FieldsInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            switch (item["MobilizeType"].ToObject<string>())
            {
                case "Text": return item.ToObject<Text>();
                case "Number": return item.ToObject<Number>();
                case "Note": return item.ToObject<Note>();
                case "Choice": return item.ToObject<Choice>();
                case "Lookup": return item.ToObject<Lookup>();
                case "LookupMulti": return item.ToObject<LookupMulti>();
                case "Boolean": return item.ToObject<Mobilize.Contract.CustomFields.Boolean>();
                case "DateTimeInfo": return item.ToObject<DateTimeInfo>();
                case "TimeInfo": return item.ToObject<TimeInfo>();
                case "DateTimeStamp": return item.ToObject<DateTimeStamp>();
                case "ImagePicker": return item.ToObject<ImagePicker>();
                case "ContactPicker": return item.ToObject<ContactPicker>();
                case "GeoStamp": return item.ToObject<GeoStamp>();
                case "GeoControl": return item.ToObject<GeoControl>();
                case "FilePicker": return item.ToObject<FilePicker>();
                case "Currency": return item.ToObject<Currency>();
                case "Email": return item.ToObject<Email>();
                case "Phone": return item.ToObject<Phone>();
                case "Formula": return item.ToObject<Formula>();
                case "SubForm": return item.ToObject<SubForm>();
                default: return item.ToObject<Mobilize.Contract.MobilizeDataTypes.FieldsInfo>();
            }
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
