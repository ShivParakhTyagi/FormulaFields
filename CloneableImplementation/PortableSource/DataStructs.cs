using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PortableSource
{
    public class DataStructs
    {

        public class GeoLocation
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public override string ToString()
            {
                return $"{Latitude},{Longitude}";
            }
        }
        public class File
        {
            public string Name { get; set; }

            public long Size { get; set; }

            public string Content { get; set; }


            public string LocalLink { get; set; }


            public string ServerLink { get; set; }
            public string State { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public class Lookup
        {
            public Lookup(int id, string value)
            {
                this.Id = id;
                this.Value = value;
            }

            public int Id { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Value;
            }
        }

        class MultiLookupJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is MultiLookup)
                {
                    MultiLookup t = value as MultiLookup;
                    var str = JsonConvert.SerializeObject(t.SelectedValues);
                    writer.WriteRawValue(str);
                    Debug.WriteLine(str);
                }
                else
                {
                    var str = JsonConvert.SerializeObject(value);
                    writer.WriteRawValue(str);
                    Debug.WriteLine(str);
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                throw new NotImplementedException();
            }
        }
        [JsonConverter(typeof(MultiLookupJsonConverter))]
        public class MultiLookup
        {
            public MultiLookup(List<int> idList, List<string> valueList)
            {
                Values = valueList;
                Ids = idList;
            }

            [JsonIgnore]
            public List<string> Values { get; set; }
            [JsonIgnore]
            public List<int> Ids { get; set; }

            public Dictionary<int, string> SelectedValues
            {
                get
                {
                    if (this.Ids == null || this.Values == null || this.Ids.Count == 0 || this.Values.Count == 0)
                    {
                        return null;
                    }
                    var dic = new Dictionary<int, string>();
                    for (int i = 0; i < this.Values.Count; i++)
                    {
                        dic.Add(Ids[i], Values[i]);
                    }
                    return dic;
                }
            }

            [JsonIgnore]
            public string ValueString
            {
                get { return string.Join(",", Values); }
            }

            public override string ToString()
            {
                return string.Join(",", Values);
            }
        }

    }
}