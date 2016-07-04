using Mobilize.Contract.GetListItemsServices;
using Mobilize.DocDBPump;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaFieldsWorking_parent_child_handled
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool go = true;
                while (go)
                {
                    //Console.Clear();
                    //test();
                    try
                    {
                        MobiFormSql obj = new MobiFormSql(1, "my secret connection string", "user");
                        //var result = obj.GetFormItems("B_sub_f");
                        var result = obj.GetFormItems("A_f");
                        var json = JsonConvert.SerializeObject(result);

                        Console.WriteLine(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    go = !Console.ReadLine().Contains("N");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void test()
        {


            List<FieldInformation> keysForJson;
            MobiFormDocument mobiFormDocument = new MobiFormDocument(1);
            //MobiDocDBForms mobiForm;
            MobiFormDocumentDecorator decorator = new MobiFormDocumentDecorator(mobiFormDocument);
            keysForJson = decorator.GetKeysInPerfectMobiForm("A_f");
            keysForJson = decorator.GetKeysInPerfectMobiForm("B_sub_f");

        }
    }
}