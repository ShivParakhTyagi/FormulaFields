using FormSchemaWithSubFormSchema.Models;
using Mobilize.DocDBPump;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormSchemaWithSubFormSchema
{
    class Program
    {
        private static string JsonString { get; set; }
        private static string FormName { get; set; } = "A_f";
        private static void InitJsonFromServer()
        {
            MobiFormSql obj = new MobiFormSql(1, "my secret connection string", "user");
            var result = obj.GetFormItems(FormName);
            JsonString = JsonConvert.SerializeObject(result);
        }
        static void Main(string[] args)
        {
            try
            {
                InitJsonFromServer();

                JObject jObject = JObject.Parse(JsonString);
                var formSchema = FormSchema.CreateFormSchema(jObject, FormName, "MobiForm");
                int index = 0;
                var subJson = JsonConvert.SerializeObject(formSchema, Formatting.Indented);
                Console.WriteLine();
                Console.WriteLine($"Index[{index++}]:  {subJson}");
                FormItem formItem = FormItem.CreateFormItem(formSchema);


                var subFormItemKey = formItem.FieldKeys.First(x => x.Type == "SubForm");

                var subSchema = formItem.Schema[subFormItemKey.InternalName];

                var subJson_ = JsonConvert.SerializeObject(subSchema, Formatting.Indented);
                Console.WriteLine();
                Console.WriteLine($"Index[{index++}]:  {subJson_}");



                var _subFormItemKey = subSchema.FieldKeys.First(x => x.Type == "SubForm");
                var _subSchema = subSchema.Schema[_subFormItemKey.InternalName];

                var _subJson_ = JsonConvert.SerializeObject(_subSchema, Formatting.Indented);
                Console.WriteLine();
                Console.WriteLine($"Index[{index++}]:  {_subJson_}");


                //RecursiveSubFields(formSchema, index + 1);

                //var subFormKeys = formSchema.FieldKeys.Where(x => x.Type == "SubForm").ToList();

                //foreach (var subfield in subFormKeys)
                //{
                //    var subSchema = formSchema[subfield.InternalName];
                //    Console.WriteLine(subSchema);
                //    var subJson = JsonConvert.SerializeObject(subSchema);
                //    Console.WriteLine(subJson);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        static void RecursiveSubFields(FormSchema schema, int level = 0)
        {

            //var formSchema = FormSchema.CreateFormSchema(schema.FieldKeys, FormName, "MobiForm");

            var subFormKeys = schema.FieldKeys.Where(x => x.Type == "SubForm").ToList();

            foreach (var subfield in subFormKeys)
            {
                var subSchema = schema[subfield.InternalName];
                //Console.WriteLine(subSchema);
                var subJson = JsonConvert.SerializeObject(subSchema, Formatting.Indented);
                Console.WriteLine();
                Console.WriteLine($"Index[{level}]:  {subJson}");
                RecursiveSubFields(subSchema, level + 1);
            }
        }
    }
}