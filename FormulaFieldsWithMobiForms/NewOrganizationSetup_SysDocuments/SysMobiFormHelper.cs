using Mobilize.Contract.CustomFields;
using Mobilize.Contract.MobilizeDataTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.DocDBPump.Helpers.SystemForms
{
    public class SysMobiFormHelper
    {



        public MobiForm UserMobiForm { get; set; }

        #region Internal Names and Create sql statements
        public static string GetFormInternalName(string userFriendlyName)
        {
            return $"mf_{userFriendlyName.Replace(' ', '_')}";
        }
        public static string GetFieldInternalName(string userFriendlyName)
        {
            return $"mf_{userFriendlyName.Replace(' ', '_')}";
        }
        public static string GetFieldInternalNameWithId(string userFriendlyName)
        {
            return $"{GetFieldInternalName(userFriendlyName)}_id";
        }
        private static string getIdFieldCreateSQL()
        {
            var str = new StringBuilder();

            str.Append($" Id ");
            str.Append($" INT ");
            str.Append($" PRIMARY KEY ");

            var sqlString = str.ToString();
            return sqlString;
        }
        private static string getCreatedByFieldCreateSQL()
        {
            var maxLength = 100;
            var str = new StringBuilder();

            str.Append($" CreatedBy ");
            str.Append($" nvarchar({maxLength}) ");
            str.Append($" NOT NULL ");

            var sqlString = str.ToString();
            return sqlString;
        }
        private static string getUpdatedByFieldCreateSQL()
        {
            var maxLength = 100;
            var str = new StringBuilder();

            str.Append($" UpdatedBy ");
            str.Append($" nvarchar({maxLength}) ");
            str.Append($" NOT NULL ");

            var sqlString = str.ToString();
            return sqlString;
        }

        private static string getCreateDateFieldCreateSQL()
        {
            var str = new StringBuilder();

            str.Append($" CreateDate ");
            str.Append($" DATETIME ");
            str.Append($" NOT NULL ");

            var sqlString = str.ToString();
            return sqlString;
        }

        private static string getUpdateDateFieldCreateSQL()
        {
            var str = new StringBuilder();

            str.Append($" UpdateDate ");
            str.Append($" DATETIME ");
            str.Append($" NOT NULL ");

            var sqlString = str.ToString();
            return sqlString;
        }

        private static string getTimeStampFieldCreateSQL()
        {
            var str = new StringBuilder();

            str.Append($" TimeStamp ");
            str.Append($" timestamp ");

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string GetTextFieldCreateSql(Text field)
        {
            const int defaultMaxlength = 255;

            var str = new StringBuilder();
            str.Append($" {field.FieldInternalName} ");
            int maxLength;
            if (!string.IsNullOrEmpty(field.MaxLength) &&
                int.TryParse(field.MaxLength, out maxLength))
            {
                if (maxLength == 0 || maxLength > defaultMaxlength)
                {
                    str.Append($" nvarchar({defaultMaxlength}) ");
                }
                else
                {
                    str.Append($" nvarchar({maxLength}) ");
                }
            }
            else
            {
                str.Append($" nvarchar({defaultMaxlength}) ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }
            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }
            var sqlString = str.ToString();
            return sqlString;
        }

        public static string GetContactPickerFieldCreateSql(ContactPicker field)
        {
            const int defaultMaxlength = 255;

            var str = new StringBuilder();
            str.Append($" {field.FieldInternalName} ");
            int maxLength;
            if (!string.IsNullOrEmpty(field.MaxLength) &&
                int.TryParse(field.MaxLength, out maxLength))
            {
                if (maxLength == 0 || maxLength > defaultMaxlength)
                {
                    str.Append($" nvarchar({defaultMaxlength}) ");
                }
                else
                {
                    str.Append($" nvarchar({maxLength}) ");
                }
            }
            else
            {
                str.Append($" nvarchar({defaultMaxlength}) ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }
            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }
            var sqlString = str.ToString();
            return sqlString;
        }

        public static string GetEmailFieldCreateSql(Email field)
        {
            const int defaultMaxlength = 255;

            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" nvarchar({defaultMaxlength}) ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }
            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }
            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getPhoneFieldCreateSQL(Phone field)
        {
            const int defaultMaxlength = 255;

            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            int maxLength;
            if (!string.IsNullOrEmpty(field.MaxLength) &&
                int.TryParse(field.MaxLength, out maxLength))
            {
                if (maxLength == 0 || maxLength > defaultMaxlength)
                {
                    str.Append($" nvarchar({defaultMaxlength}) ");
                }
                else
                {
                    str.Append($" nvarchar({maxLength}) ");
                }
            }
            else
            {
                str.Append($" nvarchar({defaultMaxlength}) ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }
            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }
            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getNoteFieldCreateSQL(Note field)
        {
            const string defaultMaxlength = "max";

            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" nvarchar({defaultMaxlength}) ");

            if (field.NumberOfLines <= 0)
            {
                throw new Exception("number of lines in note field can not be less than or equal to zero");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getNumberFieldCreateSQL(Number field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" decimal(18,9) ");

            double defaultValue;
            var hasDefaultvalue = double.TryParse(field.DefaultValue, out defaultValue);

            double minValue, maxValue;
            if (!string.IsNullOrEmpty(field.MinimumValue) && !string.IsNullOrEmpty(field.MaximumValue))
            {
                if (double.TryParse(field.MinimumValue, out minValue) &&
                    double.TryParse(field.MaximumValue, out maxValue))
                {
                    if (maxValue < minValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} number field has some invalid data");
                    }
                    if (hasDefaultvalue)
                    {
                        if (defaultValue > maxValue || defaultValue < minValue)
                        {
                            throw new Exception(
                                $"{field?.FieldUserFriendlyName ?? string.Empty} number field has some invalid data");
                        }
                    }
                    str
                        .Append(
                            $" check( {field.FieldInternalName}>={minValue} and {field.FieldInternalName}<={maxValue} ) ");
                }
            }
            else if (!string.IsNullOrEmpty(field.MinimumValue) && double.TryParse(field.MinimumValue, out minValue))
            {
                if (hasDefaultvalue)
                {
                    if (defaultValue < minValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} number field has some invalid data");
                    }
                }
                str
                    .Append(
                        $" check( {field.FieldInternalName}>={minValue} ) ");
            }
            else if (!string.IsNullOrEmpty(field.MaximumValue) && double.TryParse(field.MaximumValue, out maxValue))
            {
                if (hasDefaultvalue)
                {
                    if (defaultValue > maxValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} number field has some invalid data");
                    }
                }
                str
                    .Append(
                        $" check( {field.FieldInternalName}<={maxValue} ) ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getCurrencyFieldCreateSQL(Currency field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" decimal(18,9) ");

            double defaultValue;
            var hasDefaultvalue = double.TryParse(field.DefaultValue, out defaultValue);

            double minValue, maxValue;
            if (!string.IsNullOrEmpty(field.MinimumValue) && !string.IsNullOrEmpty(field.MaximumValue))
            {
                if (double.TryParse(field.MinimumValue, out minValue) &&
                    double.TryParse(field.MaximumValue, out maxValue))
                {
                    if (maxValue < minValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} currency field has some invalid data");
                    }
                    if (hasDefaultvalue)
                    {
                        if (defaultValue > maxValue || defaultValue < minValue)
                        {
                            throw new Exception(
                                $"{field?.FieldUserFriendlyName ?? string.Empty} currency field has some invalid data");
                        }
                    }
                    str
                        .Append(
                            $" check( {field.FieldInternalName}>={minValue} and {field.FieldInternalName}<={maxValue} ) ");
                }
            }
            else if (!string.IsNullOrEmpty(field.MinimumValue) && double.TryParse(field.MinimumValue, out minValue))
            {
                if (hasDefaultvalue)
                {
                    if (defaultValue < minValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} currency field has some invalid data");
                    }
                }
                str
                    .Append(
                        $" check( {field.FieldInternalName}>={minValue} ) ");
            }
            else if (!string.IsNullOrEmpty(field.MaximumValue) && double.TryParse(field.MaximumValue, out maxValue))
            {
                if (hasDefaultvalue)
                {
                    if (defaultValue > maxValue)
                    {
                        throw new Exception(
                            $"{field?.FieldUserFriendlyName ?? string.Empty} currency field has some invalid data");
                    }
                }
                str
                    .Append(
                        $" check( {field.FieldInternalName}<={maxValue} ) ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getBooleanFieldCreateSQL(Contract.CustomFields.Boolean field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" bit ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getChoiceFieldCreateSQL(Choice field)
        {
            const string defaultMaxlength = "max";
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" nvarchar({defaultMaxlength}) ");

            var choices = field.Choices.Split('\n');
            if (choices == null || choices.Length == 0)
            {
                throw new ArgumentException(
                    $"{field?.FieldUserFriendlyName ?? string.Empty} field: Invalid arguments are passed, there should be at least one choice");
            }
            if (!string.IsNullOrEmpty(field.DefaultValue) && !choices.Contains(field.DefaultValue))
            {
                throw new ArgumentException(
                    $"{field?.FieldUserFriendlyName ?? string.Empty} field:Invalid arguments are passed, default choices must be one from of the choices");
            }
            var choicesIn = "";
            foreach (var choice in choices)
            {
                choicesIn += $"N\'{choice}\',";
            }
            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }
            else
            {
                choicesIn += $"NULL,";
            }

            choicesIn = choicesIn.Remove(choicesIn.Length - 1, 1);
            str.Append($" CHECK ( {field.FieldInternalName} IN({choicesIn}) ) ");


            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getDateTimeInfoFieldCreateSQL(DateTimeInfo field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            if (field.DateOnly)
            {
                str.Append($" DATE ");
            }
            else
            {
                str.Append($" DATETIME ");
            }

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getTimeInfoFieldCreateSQL(TimeInfo field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" TIME(4) ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getDateTimeStampFieldCreateSQL(DateTimeStamp field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" DATETIME ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getGeoControlFieldCreateSQL(GeoControl field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" geography ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getGeoStampFieldCreateSQL(GeoStamp field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" geography ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getLookupFieldCreateSQL(Lookup field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" INT ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getLookupMultiFieldCreateSQL(LookupMulti field)
        {
            var str = new StringBuilder();
            str.Append($" {field.FieldInternalName} ");

            str.Append($" nvarchar(max) ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            if (field.EnforceUniqueValues)
            {
                //throw new NotImplementedException("LookupMulti can not have unique constrain");
                str.Append($" UNIQUE ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getFilePickerFieldCreateSQL(FilePicker field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" INT ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        public static string getImagePickerFieldCreateSQL(ImagePicker field)
        {
            var str = new StringBuilder();

            str.Append($" {field.FieldInternalName} ");

            str.Append($" INT ");

            if (field.Required)
            {
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }

        //public static string GetSubFormFieldCreateSql(SubForm field)
        //{
        //    var str = new StringBuilder();

        //    str.Append($"ALTER TABLE {OrgSchema}.{field.SubMobiFormInternalName} ADD ");
        //    str.Append($" {field.FieldInternalName} ");

        //    str.Append($" INT ");

        //    if (field.Required)
        //    {
        //        str.Append($" NOT NULL ");
        //    }
        //    var sqlString = str.ToString();
        //    return sqlString;
        //}

        public static string getFormulaFieldCreateSQL(Formula field)
        {
            const int defaultMaxlength = 255;

            var str = new StringBuilder();
            str.Append($" {field.FieldInternalName} ");

            if (field.FormulaType == "string")
            {
                str.Append($" nvarchar({defaultMaxlength}) ");
            }
            else if (field.FormulaType == "number")
            {
                str.Append($" decimal(18,9) ");
            }
            else
            {
                throw new Exception(
                    $"{field?.FieldUserFriendlyName ?? string.Empty} Formula field has invalid FormulaType");
            }

            if (field.Required)
            {
                throw new Exception($"{field?.FieldUserFriendlyName ?? string.Empty} Formula field can not be required");
                str.Append($" NOT NULL ");
            }

            var sqlString = str.ToString();
            return sqlString;
        }
        #endregion

        public static class UserMobiFormHelper
        {
            public static string SQLStatement { get; set; }

            public static string MobiFormName = "User";

            public static string Name = "Name";
            public static string Email = "Email";
            public static string CountryCode = "CountryCode";
            public static string Phone = "PhoneNo";
            public static string IsAdmin = "IsAdmin";
            public static string CanSavePassword = "CanSavePassword";
            public static string IsDisabled = "IsDisabled";
            public static string Login = "UserLogin";
            public static string UserRoles = "UserRoles";

            static string name_Label = "Name";
            static string email_Label = "Email";
            static string countryCode_Label = "Country Code";
            static string phone_Label = "Phone No";
            static string isAdmin_Label = "Is Admin";
            static string canSavePassword_Label = "Can Save Password";
            static string isDisabled_Label = "Is Disabled";
            static string login_Label = "User Login";
            static string userRoles_Label = "User Roles";

            static string roleForm_FieldName = RoleMobiFormHelper.Name;

            public static MobiForm Form = new MobiForm()
            {
                FormName = MobiFormName,
                InternalFormName = GetFormInternalName(MobiFormName),
                ConflictResolution = Conflict.First,
                Pages = new List<Page>()
                {
                    new Page()
                    {
                        FieldsInfo=new List<FieldsInfo>()
                        {
                            new Text()
                            {
                                FieldUserFriendlyName = Name,
                                Label = name_Label,
                                FieldInternalName = GetFieldInternalName(Name),
                                Sequence=0,
                                ShowInListView = true,
                            },

                            new Email()
                            {
                                FieldUserFriendlyName = Email,
                                Label = email_Label,
                                FieldInternalName = GetFieldInternalName(Email),
                                Sequence=1,
                                ShowInListView = true,
                            },

                            new Text()
                            {
                                FieldUserFriendlyName = CountryCode,
                                Label = countryCode_Label,
                                FieldInternalName = GetFieldInternalName(CountryCode),
                                Sequence=2,
                                ShowInListView = true,
                            },

                            new Phone()
                            {
                                FieldUserFriendlyName = Phone,
                                Label = phone_Label,
                                FieldInternalName = GetFieldInternalName(Phone),
                                Sequence=3,
                                ShowInListView = true,
                            },

                            new Contract.CustomFields.Boolean()
                            {
                                FieldUserFriendlyName = IsAdmin,
                                Label = isAdmin_Label,
                                FieldInternalName = GetFieldInternalName(IsAdmin),
                                Sequence=4,
                                ShowInListView = true,
                            },

                            new Contract.CustomFields.Boolean()
                            {
                                FieldUserFriendlyName = CanSavePassword,
                                Label = canSavePassword_Label,
                                FieldInternalName = GetFieldInternalName(CanSavePassword),
                                Sequence = 5,
                                ShowInListView = true,
                            },

                            new Contract.CustomFields.Boolean()
                            {
                                FieldUserFriendlyName = IsDisabled,
                                Label = isDisabled_Label,
                                FieldInternalName = GetFieldInternalName(IsDisabled),
                                Sequence = 6,
                                ShowInListView = true,
                            },

                            new Text()
                            {
                                FieldUserFriendlyName = Login,
                                Label = login_Label,
                                FieldInternalName = GetFieldInternalName(Login),
                                Sequence = 7,
                                ShowInListView = true,
                            },

                            new LookupMulti()
                            {
                                FieldUserFriendlyName = UserRoles,
                                Label = userRoles_Label,
                                FieldInternalName = GetFieldInternalNameWithId(UserRoles),
                                Sequence = 8,
                                LookupList = RoleMobiFormHelper.Form.FormName,
                                LookupField = GetFieldInternalName(roleForm_FieldName),
                                FormName = RoleMobiFormHelper.Form.InternalFormName,
                                FieldName =roleForm_FieldName,
                                Hidden = true,
                                ShowInListView = true,
                            }
                        }
                    }
                }
            };
        }

        public static class RoleMobiFormHelper
        {
            public static string SQLStatement { get; set; }

            public static string MobiFormName = "Role";

            public static string Name = "Name";
            public static string Description = "Description";
            public static string UserRoles = "UserForms";

            static string name_Label = "Name";
            static string description_Label = "Description";
            static string userRoles_Label = "User Forms";

            static string sysDocumentForm_FieldName = SysDocumentsMobiFormHelper.Name;

            public static MobiForm Form = new MobiForm()
            {
                FormName = MobiFormName,
                InternalFormName = GetFormInternalName(MobiFormName),
                ConflictResolution = Conflict.First,
                Pages = new List<Page>()
                {
                    new Page()
                    {
                        FieldsInfo=new List<FieldsInfo>()
                        {
                            new Text()
                            {
                                FieldUserFriendlyName = Name,
                                Label = name_Label,
                                FieldInternalName = GetFieldInternalName(Name),
                                Sequence=0,
                                ShowInListView = true,
                            },

                            new Text()
                            {
                                FieldUserFriendlyName = Description,
                                Label = description_Label,
                                FieldInternalName = GetFieldInternalName(Description),
                                Sequence = 1,
                                Hidden = true,
                                ShowInListView = true,
                            },

                            new LookupMulti()
                            {
                                FieldUserFriendlyName = UserRoles,
                                Label = userRoles_Label,
                                FieldInternalName = GetFieldInternalNameWithId(UserRoles),
                                Sequence = 2,
                                LookupList = SysDocumentsMobiFormHelper.Form.InternalFormName,
                                LookupField = GetFieldInternalName(sysDocumentForm_FieldName),
                                FormName = SysDocumentsMobiFormHelper.Form.FormName,
                                FieldName = sysDocumentForm_FieldName,
                                Hidden = true,
                                ShowInListView = true,
                            }
                        }
                    }
                }
            };
        }

        public static class SysDocumentsMobiFormHelper
        {
            public static string SQLStatement { get; set; }

            public static string MobiFormName = "SysDocuments";

            public static string Name = "FormName";
            public static string Data = "Data";
            public static string FormType = "FormType";


            static string name_Label = "Form Name";
            static string data_Label = "Data";
            static string formType_Label = "Form Type";

            public static MobiForm Form = new MobiForm()
            {
                FormName = MobiFormName,
                InternalFormName = GetFormInternalName(MobiFormName),
                ConflictResolution = Conflict.First,
                Pages = new List<Page>()
                {
                    new Page()
                    {
                        FieldsInfo=new List<FieldsInfo>()
                        {
                            new Text()
                            {
                                FieldUserFriendlyName = Name,
                                Label = name_Label,
                                FieldInternalName = GetFieldInternalName(Name),
                                Sequence=0,
                                ShowInListView = true,
                            },

                            new Note()
                            {
                                FieldUserFriendlyName = Data,
                                Label = data_Label,
                                FieldInternalName = GetFieldInternalName(Data),
                                NumberOfLines = 1,
                                Sequence = 1,
                                Hidden = true,
                                ShowInListView = true,
                            },

                            new Text()
                            {
                                FieldUserFriendlyName = FormType,
                                Label = formType_Label,
                                FieldInternalName = GetFieldInternalName(FormType),
                                Sequence=2,
                                Hidden = true,
                                ShowInListView = true,
                            },

                        }
                    }
                }
            };
        }

        public static List<MobiForm> OrgForms = new List<MobiForm>()
        {
            UserMobiFormHelper.Form,
            RoleMobiFormHelper.Form,
            SysDocumentsMobiFormHelper.Form,
        };

        public static string GenearateFormSql(MobiForm mobiForm, string OrgSchema)
        {
            List<string> sqlTableFieldCommandsList = new List<string>();
            sqlTableFieldCommandsList.Add($" {getIdFieldCreateSQL()}  ");

            foreach (var page in mobiForm.Pages)
            {
                if (page?.FieldsInfo != null && page.FieldsInfo.Count > 0)
                {
                    //// atleast one field must be available which is not hidden
                    //var allFieldsAreHidden =
                    //    page.FieldsInfo.All(x => x.Hidden);
                    ////var allFieldsAreHidden =
                    ////    !page.FieldsInfo.Any(x => x.MobilizeType != "SubForm" && x.Hidden == false);
                    //if (allFieldsAreHidden)
                    //{
                    //    throw new Exception("Error: All fields of a Mobi Form Page can not be set as hidden.");
                    //}
                    //// atleast one field must be available which is shown in list view
                    //var noFieldsShowInListView =
                    //    page.FieldsInfo.All(x => x.ShowInListView != true);

                    ////var noFieldsShowInListView =
                    ////    !page.FieldsInfo.Any(x => x.MobilizeType != "SubForm" && x.ShowInListView == true);

                    //if (noFieldsShowInListView)
                    //{
                    //    throw new Exception("Error: At least one field must be shown in the list view.");
                    //}


                    foreach (var field in page.FieldsInfo)
                    {
                        if (field == null) continue;
                        switch (field.MobilizeType)
                        {
                            case "Text":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {GetTextFieldCreateSql(field as Text)}  ");
                                break;
                            case "ContactPicker":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {GetContactPickerFieldCreateSql(field as ContactPicker)}  ");
                                break;
                            case "Email":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {GetEmailFieldCreateSql(field as Email)}  ");
                                break;
                            case "Phone":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getPhoneFieldCreateSQL(field as Phone)}  ");
                                break;
                            case "Note":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getNoteFieldCreateSQL(field as Note)}  ");
                                break;
                            case "Formula":
                                {
                                    //if (field is Formula)
                                    //{
                                    //    field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                    //    sqlTableFieldCommandsList.Add($" {getFormulaFieldCreateSQL(field as Formula)}  ");

                                    //    var formulaField = field as Formula;

                                    //    var formulaEngine =
                                    //        new Core.FormulaEngine.Formula(formulaField.FormulaExpression);
                                    //    if (!formulaEngine.IsValid)
                                    //    {
                                    //        throw new Exception("Formula field contains an invalid formula expression");
                                    //    }
                                    //}
                                }
                                break;
                            case "Number":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getNumberFieldCreateSQL(field as Number)}  ");
                                break;
                            case "Currency":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getCurrencyFieldCreateSQL(field as Currency)}  ");
                                break;
                            case "Boolean":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getBooleanFieldCreateSQL(field as Contract.CustomFields.Boolean)}  ");
                                break;
                            case "Choice":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getChoiceFieldCreateSQL(field as Choice)}  ");
                                break;
                            case "DateTimeInfo":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getDateTimeInfoFieldCreateSQL(field as DateTimeInfo)}  ");
                                break;
                            case "TimeInfo":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getTimeInfoFieldCreateSQL(field as TimeInfo)}  ");
                                break;
                            case "DateTimeStamp":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getDateTimeStampFieldCreateSQL(field as DateTimeStamp)}  ");
                                break;
                            case "GeoControl":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getGeoControlFieldCreateSQL(field as GeoControl)}  ");
                                break;
                            case "GeoStamp":
                                field.FieldInternalName = GetFieldInternalName(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getGeoStampFieldCreateSQL(field as GeoStamp)}  ");
                                break;
                            case "Lookup":
                                {
                                    if (field is Lookup)
                                    {
                                        var lookupField = field as Lookup;

                                        //MobiForm lookupForm =
                                        //    OrgForms.FirstOrDefault(x => x.FormName == lookupField.FormName);
                                        //if (lookupForm == null)
                                        //{
                                        //    throw new Exception(MessageStrings.MobiFormNotFoundMessage);
                                        //}
                                        //lookupField.LookupList = lookupForm.InternalFormName;
                                        //lookupField.LookupField = lookupForm.GetFieldInternalName(lookupField.FieldName);

                                        field.FieldInternalName = GetFieldInternalNameWithId(field.FieldUserFriendlyName);
                                        sqlTableFieldCommandsList.Add($" {getLookupFieldCreateSQL(field as Lookup)}  ");
                                    }

                                }
                                break;
                            case "LookupMulti":
                                {
                                    if (field is LookupMulti)
                                    {
                                        var lookupField = field as LookupMulti;

                                        //MobiForm lookupForm =
                                        //    OrgForms.FirstOrDefault(x => x.FormName == lookupField.FormName);
                                        //if (lookupForm == null)
                                        //{
                                        //    throw new Exception(MessageStrings.MobiFormNotFoundMessage);
                                        //}
                                        //lookupField.LookupList = lookupForm.InternalFormName;

                                        //lookupField.LookupField = lookupForm.GetFieldInternalName(lookupField.FieldName);

                                        field.FieldInternalName = GetFieldInternalNameWithId(field.FieldUserFriendlyName);
                                        sqlTableFieldCommandsList.Add($" {getLookupMultiFieldCreateSQL(field as LookupMulti)}  ");
                                    }
                                }
                                break;
                            case "FilePicker":
                                field.FieldInternalName = GetFieldInternalNameWithId(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getFilePickerFieldCreateSQL(field as FilePicker)}  ");
                                break;
                            case "ImagePicker":
                                field.FieldInternalName = GetFieldInternalNameWithId(field.FieldUserFriendlyName);
                                sqlTableFieldCommandsList.Add($" {getImagePickerFieldCreateSQL(field as ImagePicker)}  ");
                                break;
                            case "SubForm":
                                {
                                    //if (field is SubForm)
                                    //{
                                    //    var subField = field as SubForm;

                                    //    subField.FieldInternalName =
                                    //        GetFieldInternalNameWithSubFormId(subField.FieldUserFriendlyName,
                                    //            mobiForm.InternalFormName);
                                    //    subField.ShowInListView = false;
                                    //    MobiFormDocument mobiFormDocument = new MobiFormDocument(_orgId);
                                    //    var subMobiForm = mobiFormDocument.GetSubMobiForm(subField.SubMobiForm);
                                    //    if (subMobiForm == null)
                                    //    {
                                    //        return new Result<bool, MobiForm>()
                                    //        {
                                    //            Status = false,
                                    //            Message = MessageStrings.SubMobiFormNotFoundMessage,
                                    //        };
                                    //    }
                                    //    var assigned =

                                    //        mobiFormDocument.SetParentOfSubForm(allForms, subMobiForm.FormName,
                                    //            mobiForm.FormName);
                                    //    if (assigned == false)
                                    //    {
                                    //        return new Result<bool, MobiForm>()
                                    //        {
                                    //            Message = MessageStrings.InconsistentSubFormAssignment(subMobiForm.FormName, subField.FieldUserFriendlyName),
                                    //            //$"to prevent data inconsistency, this form can not have '{subMobiForm.FormName}' as a Sub Form for SubForm Field '{subField.FieldUserFriendlyName}' "
                                    //        };
                                    //    }
                                    //    //to update these forms
                                    //    allUpdatedForms.Add(
                                    //        allForms.FirstOrDefault(x => x.FormName == subMobiForm.FormName));

                                    //    subField.SubMobiFormInternalName = subMobiForm.InternalFormName;
                                    //    subFormFieldCommandsList.Add($" {GetSubFormFieldCreateSql(subField)}  ");
                                    //}
                                }
                                break;
                            default:
                                throw new ArgumentException();
                        }
                    }
                }
            }



            sqlTableFieldCommandsList.Add($" {getCreateDateFieldCreateSQL()}  ");
            sqlTableFieldCommandsList.Add($" {getUpdateDateFieldCreateSQL()}  ");
            sqlTableFieldCommandsList.Add($" {getCreatedByFieldCreateSQL()}  ");
            sqlTableFieldCommandsList.Add($" {getUpdatedByFieldCreateSQL()}  ");
            sqlTableFieldCommandsList.Add($" {getTimeStampFieldCreateSQL()}  ");

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append($"Create table {OrgSchema}.{mobiForm.InternalFormName} ");
            sqlCommand.Append("(");
            sqlCommand.Append(string.Join(" , ", sqlTableFieldCommandsList));
            sqlCommand.Append(");");
            var createSqlTableCommand = sqlCommand.ToString();
            return createSqlTableCommand;
        }

        public static void Run()
        {
            string orgSchema = "{OrgSchema}";

            List<string> sql = new List<string>();
            List<string> formsJson = new List<string>();

            foreach (var form in OrgForms)
            {
                sql.Add(GenearateFormSql(form, orgSchema));
                formsJson.Add(JsonConvert.SerializeObject(form));
            }

            string sqls = string.Join("\n\n--\n\n", sql);
            string formsJsons = string.Join("\n\n--\n\n", formsJson);

            Console.WriteLine(sqls);
            Console.WriteLine(formsJsons);

            Console.ReadLine();
            Console.Clear();

        }

    }
}