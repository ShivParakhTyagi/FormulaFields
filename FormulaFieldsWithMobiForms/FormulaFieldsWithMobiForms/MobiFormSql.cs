using Mobilize.Contract.CustomFields;
using Mobilize.Contract.MobilizeDataTypes;
using Mobilize.Contract.ServerListItemsService;
using Mobilize.Contract.SyncListItemsService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mobilize.Contract.CreateListItemService;
using Mobilize.Contract.GetListItemsServices;
using Mobilize.Core;
using FormulaFieldsWithMobiForms.MobiFormDocs;

namespace Mobilize.DocDBPump
{
    public class MobiFormSql
    {
        private int _orgId;
        private string _connectionString;
        private string _loginId;
        private string _organizationName;

        private List<string> filesCreated, filesDeleted;

        public string OrgSchema
        {
            get { return $"o{_orgId}"; }
        }

        public MobiFormSql(int orgId, string connectionString, string loginId)
        {
            this._connectionString = connectionString;
            this._orgId = orgId;
            this._loginId = loginId;
            filesCreated = new List<string>();
            filesDeleted = new List<string>();
        }
        
        #region Get ALL Items

        /// <summary>
        /// _controlsFilter is used to limit the type of controls which are supported by the system.
        /// Adding values to it will support those field types like GUID,FLAG,Text,etc.
        /// This was created when mobi form was generated using sharepoint fields,
        /// because Sharepoint had several non-supported types.
        /// </summary>
        //private List<string> _controlsFilter = new List<string>(CommonSettings.XMLControlsFilter);

        public JObject GetFormItems(string formName,
            MobiForm mobiForm = null,
            string subFormFieldInternalName = null, int? recordId = null)
        {
            try
            {
                bool isSubForm = recordId != null;

                MobiFormDocument mobiFormDocument = new MobiFormDocument(_orgId);
                //MobiDocDBForms mobiForm;
                MobiFormDocumentDecorator decorator = new MobiFormDocumentDecorator(mobiFormDocument);
                //todo:get mobiform or subform accordingly
                if (recordId == null)
                {
                    mobiForm = decorator.GetCompleteMobiForm(formName);
                }

                //adding keys to form
                mobiForm = decorator.AddSystemKeyFieldsToMobiForm(mobiForm);

                if (mobiForm == null)
                {
                    return null;

                }

                List<FieldInformation> keysForJson;
                if (isSubForm)
                {
                    keysForJson = mobiFormDocument.GetKeysFromMobiForm(mobiForm);
                }
                else
                {
                    keysForJson = decorator.GetKeysInPerfectMobiForm(mobiForm.FormName);
                }

                List<string> fields = new List<string>();
                // to assign and get proper names of the fields in SQL table
                foreach (var keyInfo in keysForJson)
                {
                    switch (keyInfo.Type)
                    {
                        case "SubForm":
                            {
                                //skipped..
                            }
                            break;
                        case "ID":
                            {
                                //formated accordingly..
                                fields.Add("Id");
                            }
                            break;
                        case "Guid":
                            {
                                //skipped..
                            }
                            break;
                        case "Flag":
                            {
                                //skipped..
                            }
                            break;
                        case "FilePicker":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "ImagePicker":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "Lookup":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "LookupMulti":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        default:
                            {
                                //this looks good..
                                fields.Add(keyInfo.InternalName);
                            }
                            break;
                    }
                }
                //todo:create sql statement to get data
                var queryGetItems =
                    $"SELECT {string.Join(",", fields)} " +
                    $"FROM {OrgSchema}.{mobiForm.InternalFormName} ";

                if (recordId != null)
                {
                    // only in case of subform
                    queryGetItems += $" WHERE {subFormFieldInternalName} = {recordId} ";
                }
                else
                {
                    var xmlQuery = mobiForm.Query;
                    var whereClause = "";
                    if (!string.IsNullOrEmpty(xmlQuery))
                    {
                        //TODO: SQL Process Query here...

                        //SqlQueryProcessor queryProcessor = new SqlQueryProcessor(this._orgId, this._connectionString);
                        //whereClause = queryProcessor.GetWhereClause(
                        //    xmlQuery, formName, variables,
                        //    mobiFormDocument.GetAllForms());

                    }
                    queryGetItems += whereClause;
                }

                queryGetItems += " ; ";

                //SqlHelper helper = new SqlHelper();

                //todo: get data from source i.e. Sql Db
                List<Dictionary<string, object>> listItems =
                    new List<Dictionary<string, object>>();
                //await helper.GetDataAsync(this._connectionString, queryGetItems);

                if (listItems == null)
                {
                    return null;
                }

                //todo: Get table items here
                List<List<string>> values = GetValues(mobiForm, listItems);

                if (values == null)
                {
                    return null;
                }
                //todo: convert table data[values] and fields[keys] into key-value object
                return GenerateJsonResponse(keysForJson, values);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private List<List<string>> GetValues(MobiForm mobiForm,
            List<Dictionary<string, object>> itemsList)
        {
            if (mobiForm == null)
            {
                return null;
            }

            var values = new List<List<string>>();

            if (mobiForm.Pages[0].FieldsInfo.All(x => x.MobilizeType != "ID"))
            {
                mobiForm.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "ServerId",
                    MobilizeType = "ID",
                    FieldUserFriendlyName = "ServerId",
                    Hidden = true,
                    ShowInListView = false,
                });
            }
            if (mobiForm.Pages[0].FieldsInfo.All(x => x.MobilizeType != "Flag"))
            {
                mobiForm.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "Flag",
                    MobilizeType = "Flag",
                    FieldUserFriendlyName = "Flag",
                    Hidden = true,
                    ShowInListView = false,
                });
            }
            if (mobiForm.Pages[0].FieldsInfo.All(x => x.MobilizeType != "Guid"))
            {
                mobiForm.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "ClientId",
                    MobilizeType = "Guid",
                    FieldUserFriendlyName = "ClientId",
                    Hidden = true,
                    ShowInListView = false,
                });
            }

            if (mobiForm?.Pages == null || mobiForm.Pages.Count <= 0) return values;

            foreach (var page in mobiForm.Pages)
            {

                if (page?.FieldsInfo == null || page.FieldsInfo.Count <= 0) continue;

                //To add new fields in every page for internal purposes [ofline scenario] and tracking of record [via ID]
            }
            return values;
        }

        JObject GenerateJsonResponse(List<FieldInformation> keys, List<List<string>> values)
        {
            JObject responseJobject = new JObject();
            JToken keysToken = JToken.Parse(JsonConvert.SerializeObject(keys));
            JToken valuesToken = JToken.Parse(JsonConvert.SerializeObject(values));
            responseJobject.Add("Keys", keysToken);
            responseJobject.Add("Values", valuesToken);
            var resp = JsonConvert.SerializeObject(responseJobject);
            //return resp;
            return responseJobject;
        }


        #endregion

        #region ListItem

        public Result<bool, string> GetFormItem(int itemId, string formName)
        {
            try
            {
                MobiFormDocument mobiFormDocument = new MobiFormDocument(_orgId);
                MobiForm mobiForm;
                //todo:get mobiform or subform accordingly- in one list item it will be only mobi form
                mobiForm = mobiFormDocument.GetMobiForm(formName);

                if (mobiForm == null)
                {
                    return null;
                    //return string.Empty;
                }

                var xmlQuery = mobiForm.Query;

                if (!string.IsNullOrEmpty(xmlQuery))
                {
                    //TODO: SQL Process Query here...
                }
                MobiFormDocumentDecorator decorator = new MobiFormDocumentDecorator(mobiFormDocument);
                List<FieldInformation> keysForJson = decorator.GetKeysInPerfectMobiForm(mobiForm.FormName);
                List<string> fields = new List<string>();
                //todo: to assign and get proper names of the fields in SQL table
                foreach (var keyInfo in keysForJson)
                {
                    switch (keyInfo.Type)
                    {
                        case "SubForm":
                            {
                                //skipped..
                            }
                            break;
                        case "ID":
                            {
                                //formated accordingly..
                                fields.Add("Id");
                            }
                            break;
                        case "Guid":
                            {
                                //skipped..
                            }
                            break;
                        case "Flag":
                            {
                                //skipped..
                            }
                            break;
                        case "FilePicker":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "ImagePicker":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "Lookup":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        case "LookupMulti":
                            {
                                //formated accordingly..
                                fields.Add($"{keyInfo.InternalName}");
                            }
                            break;
                        default:
                            {
                                //this looks good..
                                fields.Add(keyInfo.InternalName);
                            }
                            break;
                    }
                }


                // todo: get record item from sql db using sql query
                var sqlQuery =
                    $"SELECT {string.Join(",", fields)} " +
                    $"FROM {OrgSchema}.{mobiForm.InternalFormName} " +
                    $"WHERE Id = {itemId} " +
                    $"; ";

                //SqlHelper helper = new SqlHelper();

                List<Dictionary<string, object>> itemsList =
                    new List<Dictionary<string, object>>();
                //await helper.GetDataAsync(this._connectionString, sqlQuery);

                if (itemsList == null)
                {
                    return new Result<bool, string>()
                    {
                        //Message = MessageStrings.RecordNotExistMessage,
                    };
                }

                //todo:get value from record into list of list of strings

                List<List<string>> values = GetValues(mobiForm, itemsList);

                if (values == null)
                {
                    return new Result<bool, string>();
                }

                var response = GenerateJsonResponse(keysForJson, values);

                var responseString = JsonConvert.SerializeObject(response);

                return new Result<bool, string>()
                {
                    Status = true,
                    Data = responseString,
                };
            }
            catch (Exception ex)
            {
                return new Result<bool, string>() { Status = false, Message = ex.Message };
            }
        }

        #endregion

    }
}