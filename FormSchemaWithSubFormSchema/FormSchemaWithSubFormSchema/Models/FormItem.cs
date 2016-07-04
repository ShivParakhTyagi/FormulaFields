using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSchemaWithSubFormSchema.Models
{
    public class FormItem : FormSchema, IDictionary<string, object>, INotifyPropertyChanged
    {
        [JsonIgnore]
        public FormItem ParentFormItem { get; set; }

        [JsonIgnore]
        public string ParentFieldName { get; set; }


        [JsonIgnore]
        public Guid ClientId
        {
            get
            {
                object val = null;
                if (TryGetValue("ClientId", out val))
                {
                    if (val == null || string.IsNullOrEmpty(val.ToString()) || val.ToString() == "null")
                    {
                        return Guid.Empty;
                    }
                    if (val is string)
                    {
                        return Guid.Parse((string)val);
                    }
                    return (Guid)val;
                }
                return Guid.Empty;
            }
            set
            {
                if (ContainsKey("ClientId"))
                {
                    this["ClientId"] = value;
                }
                else
                {
                    this.Add("ClientId", value);
                }
            }
        }

        [JsonIgnore]
        public string ServerId
        {
            get
            {
                object val = null;
                if (TryGetValue("ServerId", out val))
                {
                    return val.ToString();
                }
                return "";
            }
            set
            {
                if (ContainsKey("ServerId"))
                {
                    this["ServerId"] = value;
                }
                else
                {
                    this.Add("ServerId", value);
                }
            }
        }


        [JsonIgnore]
        public string State
        {
            get
            {
                object val = null;
                if (TryGetValue("Flag", out val))
                {
                    return val.ToString();
                }
                return "";
            }
            set
            {
                if (ContainsKey("Flag"))
                {
                    this["Flag"] = value;
                }
                else
                {
                    this.Add("Flag", value);
                }
            }
        }


        private Dictionary<string, object> _fieldValues;

        public Dictionary<string, object> FieldValues
        {
            get
            {
                if (_fieldValues == null)
                {
                    _fieldValues = new Dictionary<string, object>();
                }
                return _fieldValues;
            }
            set { _fieldValues = value; }
        }

        //Making Constructor Private to Disallow Creation of Items
        private FormItem()
        {

        }

        public static FormItem CreateFormItem(FormSchema formSchema)
        {
            var formItem = new FormItem();
            formItem.FormName = formSchema?.FormName;
            formItem.FormType = formSchema?.FormType;
            formItem.FieldKeys = formSchema?.FieldKeys;

            formItem = InitFormItem(formItem);

            formItem.State = "C";
            formItem.ClientId = Guid.NewGuid();
            formItem.ServerId = formItem.ClientId.ToString();

            return formItem;
        }
        private static FormItem InitFormItem(FormItem formItem)
        {
            try
            {
                if (formItem?.FieldKeys == null)
                {
                    return formItem;
                }


                if (formItem?.FieldValues == null)
                {
                    formItem.FieldValues = new Dictionary<string, object>();
                }

                foreach (var key in formItem.FieldKeys)
                {
                    if (key == null) { continue; }
                    switch (key.Type)
                    {
                        case "Flag": { } break;
                        case "ClientId": { } break;
                        case "ServerId": { } break;

                        //case "Number":
                        //    {
                        //        if (formItem.FieldValues.ContainsKey(key.InternalName))
                        //        {
                        //            formItem.FieldValues[key.InternalName] = default(double);
                        //        }
                        //        else
                        //        {
                        //            formItem.FieldValues.Add(key.InternalName, default(double));
                        //        }
                        //    }
                        //    break;
                        //case "Currency":
                        //    {
                        //        if (formItem.FieldValues.ContainsKey(key.InternalName))
                        //        {
                        //            formItem.FieldValues[key.InternalName] = default(double);
                        //        }
                        //        else
                        //        {
                        //            formItem.FieldValues.Add(key.InternalName, default(double));
                        //        }
                        //    }
                        //    break;
                        default:
                            {
                                if (formItem.FieldValues.ContainsKey(key.InternalName))
                                {
                                    formItem.FieldValues[key.InternalName] = null;
                                }
                                else
                                {
                                    formItem.FieldValues.Add(key.InternalName, null);
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception At:-FormItem.cs-InitFormItem()-" + ex.Message);
            }
            return formItem;
        }

        private static FormItem CreateFormItem(List<FieldInformation> keys, JToken jValues, string formName, string formType, string parentFieldName, FormItem parent)
        {
            try
            {
                object value = null;

                var formItem = new FormItem();

                formItem.FieldKeys = keys;
                formItem.FormName = formName;
                formItem.FormType = formType;
                formItem.State = "O";
                formItem.ClientId = Guid.NewGuid();
                formItem.ParentFormItem = parent;
                formItem.ParentFieldName = parentFieldName;

                for (int index = 0; index < keys.Count(); index++)
                {
                    var label = keys[index].Label;
                    var type = keys[index].Type;
                    var internalName = keys[index].InternalName;
                    if (type.Equals("SubForm"))
                    {
                        var temp = jValues[index].ToString();
                        var subFormItems = CreateFormItems(temp, keys[index].SubForm, "SubMobiForm", internalName, formItem);
                        if (subFormItems != null)
                        {
                            value = subFormItems;
                        }
                    }
                    else if (type.Equals("Number") || type.Equals("Currency"))
                    // add more type from json to support sorting on numeric field.
                    {
                        var temp = jValues[index].ToString();
                        if (!string.IsNullOrEmpty(temp))
                        {
                            value = Convert.ToDouble(temp);
                        }
                        else
                        {
                            value = temp;
                        }
                    }

                    else if (type.Equals("DateTimeInfo"))
                    {
                        try
                        {
                            var dateTimeValue = jValues[index].ToObject<object>();
                            if (dateTimeValue != null && (string)dateTimeValue != "" &&
                                (string)dateTimeValue != "null")
                            {
                                var dateTimeObj = JsonConvert.DeserializeObject<DateTime>(dateTimeValue as string,
                                new JsonSerializerSettings()
                                {
                                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                    DateParseHandling = DateParseHandling.DateTime,
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                                }
                                );
                                //var dateTimeLocale = dateTimeObj.ToLocalTime();
                                //value = dateTimeLocale;
                                var dateTimeUtc = dateTimeObj.ToUniversalTime();
                                value = dateTimeUtc;

                            }
                            else
                            {
                                value = null;
                            }
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(
                                "Exception At:-ListView.xaml.cs-CreateKeyValueList()-else if (type.Equals(\"DateTimeInfo\")" +
                                exception.Message);
                            value = null;

                        }

                    }
                    else if (type.Equals("DateTimeStamp"))
                    {
                        try
                        {
                            var dateTimeValue = jValues[index].ToObject<object>();
                            if (dateTimeValue != null && (string)dateTimeValue != "" &&
                                (string)dateTimeValue != "null")
                            {
                                var dateTimeObj = //jValues[index].ToObject<DateTime>();
                                JsonConvert.DeserializeObject<DateTime>(dateTimeValue as string,
                                new JsonSerializerSettings()
                                {
                                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                    DateParseHandling = DateParseHandling.DateTime,
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                                }
                                );
                                //var dateTimeLocale = dateTimeObj.ToLocalTime();
                                var dateTimeUtc = dateTimeObj.ToUniversalTime();
                                value = dateTimeUtc;

                            }
                            else
                            {
                                value = null;
                            }
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(
                                "Exception At:-ListView.xaml.cs-CreateKeyValueList()-else if (type.Equals(\"DateTimeStamp\")" +
                                exception.Message);
                            value = null;

                        }

                    }

                    else if (type.Equals("FilePicker"))
                    {
                        var temp = jValues[index].ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            value = null;
                        }
                        else
                        {
                            var filedata = temp.Split('|');
                            if (filedata.Length == 3)
                            {
                                value = new File()
                                {
                                    Name = filedata[0],
                                    //LocalLink = $"{CommonSettings.BasePathString}{filedata[1]}",
                                    State = filedata[2],
                                };
                            }
                            else
                            {
                                value = null;
                            }
                        }
                    }
                    else if (type.Equals("ImagePicker"))
                    {
                        var temp = jValues[index].ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            value = null;
                        }
                        else
                        {
                            var filedata = temp.Split('|');
                            if (filedata.Length == 3)
                            {
                                value = new File()
                                {
                                    Name = filedata[0],
                                    //LocalLink = $"{CommonSettings.BasePathString}{filedata[0]}",
                                    State = filedata[2],
                                };
                            }
                            else
                            {
                                value = null;
                            }
                        }
                    }

                    else if (type.Equals("Lookup"))
                    {
                        var temp = jValues[index].ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            value = null;
                        }
                        else
                        {
                            var array = temp.Split(',');
                            int id = Int32.Parse(array[0]);
                            string lookupValue = array[1];
                            value = new Lookup(id, lookupValue);
                        }
                    }

                    else if (type.Equals("LookupMulti"))
                    {
                        var temp = jValues[index].ToString();
                        var array = temp.Split(';');
                        List<string> valueList = new List<string>();
                        List<int> idList = new List<int>();
                        foreach (var item in array)
                        {
                            if (string.IsNullOrEmpty(item))
                            {
                                continue;
                            }
                            var idValue = item.Split(',');
                            if (idValue.Length != 2)
                            {
                                continue;
                            }
                            var mId = idValue[0];
                            var mValue = idValue[1];
                            idList.Add(Int32.Parse(mId));
                            valueList.Add(mValue);
                        }
                        value = new MultiLookup(idList, valueList);

                    }

                    else if (type.Equals("GeoControl"))
                    {

                        var temp = jValues[index]?.ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            value = null;
                        }
                        else
                        {
                            string[] val = temp.Split(',');
                            if (val.Length == 2)
                            {
                                value = new GeoLocation
                                {
                                    Latitude = val[0],
                                    Longitude = val[1],
                                };
                            }
                            else
                            {
                                value = null;
                            }
                        }
                    }

                    else if (type.Equals("GeoStamp"))
                    {

                        var temp = jValues[index]?.ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            value = null;
                        }
                        else
                        {
                            string[] val = temp.Split(',');
                            if (val.Length == 2)
                            {
                                value = new GeoLocation
                                {
                                    Latitude = val[0],
                                    Longitude = val[1],
                                };
                            }
                            else
                            {
                                value = null;
                            }
                        }
                    }
                    else if (type.Equals("Flag"))
                    {
                        var flagValue = jValues[index].ToString();
                        formItem.State = flagValue;
                        value = jValues[index].ToString();

                    }

                    else if (type.Equals("ID"))
                    {
                        var idValue = jValues[index].ToString();
                        formItem.ServerId = idValue;
                        value = jValues[index].ToString();

                    }

                    else if (type.Equals("Guid"))
                    {
                        var idValue = jValues[index].ToString();
                        Guid guidValue;
                        Guid.TryParse(idValue, out guidValue);
                        formItem.ClientId = guidValue;
                        value = jValues[index].ToString();
                    }

                    else
                    {
                        value = jValues[index].ToString();
                    }

                    if (formItem.ContainsKey(internalName))
                    {
                        formItem[internalName] = value;
                    }
                    else
                    {
                        formItem.Add(internalName, value); //creating dictionary of single item    
                    }
                }
                return formItem;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception At:-FormItem.cs-CreateFormItem()-" + exception.Message);
                return null;
            }
        }

        public static List<FormItem> CreateFormItems(string jsonString, string formName, string formType, string parentFieldName = "", FormItem parent = null) //Create List  from keys and values of json 
        {

            try
            {
                if (string.IsNullOrEmpty(jsonString))
                {
                    return null;
                }

                JObject jObject = JObject.Parse(jsonString);
                return CreateFormItems(jObject, formName, formType, parentFieldName, parent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception At:-ListViewPage.xaml.cs-CreateKeyValueList()-" + ex.Message);
                return null;
            }
        }

        public static List<FormItem> CreateFormItems(JObject jObject, string formName, string formType, string parentFieldName = "", FormItem parent = null) //Create List  from keys and values of json 
        {
            try
            {
                if (jObject == null)
                {
                    return null;
                }
                List<FieldInformation> keys = jObject["Keys"].ToObject<List<FieldInformation>>();
                var formItems = new List<FormItem>();

                //Creating label value dictionary
                for (int item = 0; item < jObject["Values"].Count(); item++)
                {
                    formItems.Add(CreateFormItem(keys, jObject["Values"][item], formName, formType, parentFieldName, parent));
                }
                return formItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception At:-ListViewPage.xaml.cs-CreateKeyValueList()-" + ex.Message);
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.FieldKeys != null && this.FieldValues != null && this.FieldKeys.Count == this.FieldValues.Count)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.FieldValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.FieldValues.GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
            //this.Compute(item.Key);
        }



        public void Clear()
        {
            this.FieldValues.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.FieldValues.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            object itemVal = null;
            if (this.FieldValues.TryGetValue(item.Key, out itemVal))
            {
                if (itemVal == item.Value)
                {
                    this.FieldValues.Remove(item.Key);
                    return true;
                }
            }

            return false;
        }

        public int Count
        {
            get { return this.FieldValues.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(string key, object value)
        {
            if (this.FieldValues.ContainsKey(key))
            {
                this.FieldValues[key] = value;
            }
            else
            {
                this.FieldValues.Add(key, value);
            }

            OnPropertyChanged(key);
        }

        public bool ContainsKey(string key)
        {
            return this.FieldValues.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return this.FieldValues.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return this.FieldValues.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get
            {
                if (this.FieldValues.ContainsKey(key))
                {
                    return this.FieldValues[key];
                }
                else
                {
                    return null;
                }
            }
            set { this.Add(key, value); }
        }

        public ICollection<string> Keys
        {
            get { return this.FieldValues.Keys; }
        }

        public ICollection<object> Values
        {
            get { return this.FieldValues.Values; }
        }


        //public FormItem UpdateFormItemIds(ResponseFormItem responseFormItem)
        //{
        //    this.State = "O";

        //    this.ServerId = $"{responseFormItem.Id}";
        //    foreach (var subFormItemResponse in responseFormItem.SubFormItems)
        //    {
        //        var subFormItemsValue = this.FieldValues[subFormItemResponse.FieldInternalName];
        //        if (!(subFormItemsValue is List<FormItem>))
        //        {
        //            continue;
        //        }
        //        this.FieldValues[subFormItemResponse.FieldInternalName] =
        //            UpdateFormItemFromResponse(subFormItemsValue as List<FormItem>,
        //                subFormItemResponse.ResponseFormItems);
        //    }
        //    return this;
        //}

        //private List<FormItem> UpdateFormItemFromResponse(List<FormItem> formItems,
        //    List<ResponseFormItem> responseFormItems)
        //{
        //    foreach (var responseFormItem in responseFormItems)
        //    {
        //        int index = -1;
        //        if (responseFormItem.Flag == "C")
        //        {
        //            index = formItems.FindIndex(x => x.ClientId.Equals(responseFormItem.Guid));
        //        }
        //        else
        //        {
        //            index = formItems.FindIndex(x => x.ServerId == $"{responseFormItem.Id}");
        //        }
        //        if (index == -1)
        //        {
        //            continue;
        //        }
        //        formItems[index].UpdateFormItemIds(responseFormItem);
        //    }
        //    return formItems;
        //}


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            //var key = this.FieldKeys.FirstOrDefault(x => x.InternalName == propertyName);
            //if (key?.ImpactedFields != null)
            //{
            //    var impactedFields = key.ImpactedFields;
            //    foreach (var impactedField in impactedFields)
            //    {
            //        var impactedFieldsBreakup = impactedField.Split('.');
            //        if (impactedFieldsBreakup.Length > 1)
            //        {
            //            if (impactedFieldsBreakup[0] == "parent")
            //            {
            //                this.ParentFormItem.Calculate(impactedFieldsBreakup[1]);
            //            }
            //            else
            //            {
            //                var subFormField =
            //                    this.FieldKeys.FirstOrDefault(x => x.InternalName == impactedFieldsBreakup[0]);
            //                if (subFormField != null)
            //                {
            //                    var formItems = this[impactedFieldsBreakup[0]] as List<FormItem>;
            //                    if (formItems != null)
            //                    {
            //                        foreach (var subFormItem in formItems)
            //                        {
            //                            subFormItem.Calculate(impactedFieldsBreakup[1]);
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //        else
            //        {
            //            Calculate(impactedField);
            //        }
            //    }
            //}
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Calculate(string fieldName)
        {
            //var field = this.FieldKeys.FirstOrDefault(x => x.InternalName == fieldName);
            //if (field != null)
            //{
            //    var formula = field.Formula;
            //    var formulaProcessor = new Mobilize.Core.FormulaEngine.Formula(formula);
            //    if (formulaProcessor.IsValid)
            //    {
            //        var acceptValues = new Dictionary<string, double>();
            //        foreach (var operand in formulaProcessor.Fields)
            //        {
            //            var operandValue = this.FieldValues.ContainsKey(operand) ? this.FieldValues[operand] : 0;
            //            double val = 0D;
            //            double.TryParse(operandValue?.ToString(), out val);
            //            acceptValues.Add(operand, val);
            //        }
            //        formulaProcessor.AcceptValues(acceptValues);
            //        if (formulaProcessor.IsValid)
            //        {
            //            this[fieldName] = formulaProcessor.Evaluate();
            //        }
            //    }
            //}
        }
    }

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


    //[JsonConverter(typeof(MultiLookupJsonConverter))]
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
