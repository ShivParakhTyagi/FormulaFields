using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSchemaWithSubFormSchema.Models
{

    public class FormSchema
    {
        [JsonIgnore]
        public string FormType { get; set; }

        [JsonIgnore]
        public string FormName { get; set; }

        public List<FieldInformation> _fieldKeys;

        [JsonIgnore]
        public List<FieldInformation> FieldKeys
        {
            get
            {
                if (_fieldKeys == null)
                {
                    _fieldKeys = new List<FieldInformation>();
                }
                return _fieldKeys;
            }
            set
            {
                _fieldKeys = value;
            }
        }

        public static FormSchema CreateFormSchema(List<FieldInformation> keys, string formName, string formType)
        {
            FormSchema schema = new FormSchema();
            schema.FormName = formName;
            schema.FieldKeys = keys;
            schema.FormType = formType;
            return schema;
        }

        public static FormSchema CreateFormSchema(JObject jObject, string formName, string formType)
        {
            if (jObject == null)
            {
                return null;
            }
            FormSchema schema = new FormSchema();
            schema.FieldKeys = jObject["Keys"].ToObject<List<FieldInformation>>();
            schema.FormName = formName;
            schema.FormType = formType;
            return schema;
        }

        protected virtual FormSchema GetSubFormSchema(FormSchema schema, string fieldName)
        {
            if (schema?.FieldKeys == null)
            {
                return schema;
            }
            var fieldsInfo = schema.FieldKeys.FirstOrDefault(x => x.InternalName == fieldName);
            return CreateFormSchema(fieldsInfo?.SubFormFields, fieldsInfo?.SubForm, "SubMobiForm");
        }

        [JsonIgnore]
        public FormSchema this[string internalName]
        {
            get
            {
                return GetSubFormSchema(this, internalName);
            }
        }
    }


}
