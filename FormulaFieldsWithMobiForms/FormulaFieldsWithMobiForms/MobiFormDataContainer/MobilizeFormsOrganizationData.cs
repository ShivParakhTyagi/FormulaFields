using Mobilize.Contract.CustomFields;
using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaFieldsWithMobiForms.MobiFormDataContainer
{
    public class MobilizeFormsOrganizationData
    {
        public MobiForm MainForm = new MobiForm()
        {
            FormName = "A_f",
            InternalFormName = "mf_A_f",
            Type = "MobiForm",
            Pages = new List<Page>()
            {
                new Page()
                {
                    FieldsInfo=new List<FieldsInfo>()
                    {
                        new Number()
                        {
                            FieldUserFriendlyName = "A",
                            FieldInternalName ="mf_A",
                        },
                        new Number()
                        {
                            FieldUserFriendlyName = "B",
                            FieldInternalName ="mf_B",
                        },
                        new Formula()
                        {
                            FieldUserFriendlyName = "Sum",
                            FieldInternalName ="mf_Sum",
                            FormulaExpression = "[A]+[B]",
                        },
                        new SubForm()
                        {
                            FieldUserFriendlyName = "Sub",
                            FieldInternalName ="mf_Sub",
                            SubMobiForm = "A_sub_f",
                            SubMobiFormInternalName = "mf_A_sub_f",
                        },
                        new Formula()
                        {
                            FieldUserFriendlyName = "Sub_Sum",
                            FieldInternalName ="mf_Sub_Sum",
                            FormulaExpression = "SUM([Sub.Sum])",
                        },
                    }
                }
            }
        };
        public MobiForm SubForm = new MobiForm()
        {
            FormName = "A_sub_f",
            InternalFormName = "mf_A_sub_f",
            Type = "SubMobiForm",
            Pages = new List<Page>()
            {
                new Page()
                {
                    FieldsInfo=new List<FieldsInfo>()
                    {
                        new Number()
                        {
                            FieldUserFriendlyName = "A",
                            FieldInternalName ="mf_A",
                        },
                        new Number()
                        {
                            FieldUserFriendlyName = "B",
                            FieldInternalName ="mf_B",
                        },
                        new Formula()
                        {
                            FieldUserFriendlyName = "Sum",
                            FieldInternalName ="mf_Sum",
                            FormulaExpression = "[A]+[B]",
                        },
                        new SubForm()
                        {
                            FieldUserFriendlyName = "Sub",
                            FieldInternalName ="mf_Sub",
                            SubMobiForm = "A_sub2_f",
                            SubMobiFormInternalName = "mf_A_sub2_f",
                        },
                        new Formula()
                        {
                            FieldUserFriendlyName = "Sub_Sum",
                            FieldInternalName ="mf_Sub_Sum",
                            FormulaExpression = "SUM([Sub.Sum])",
                        },
                    }
                }
            }
        };
        public MobiForm SubSubForm = new MobiForm()
        {
            FormName = "A_sub2_f",
            InternalFormName = "mf_A_sub2_f",
            Type = "SubMobiForm",
            Pages = new List<Page>()
            {
                new Page()
                {
                    FieldsInfo=new List<FieldsInfo>()
                    {
                        new Number()
                        {
                            FieldUserFriendlyName = "A",
                            FieldInternalName ="mf_A",
                        },
                        new Number()
                        {
                            FieldUserFriendlyName = "B",
                            FieldInternalName ="mf_B",
                        },
                        new Formula()
                        {
                            FieldUserFriendlyName = "Sum",
                            FieldInternalName ="mf_Sum",
                            FormulaExpression = "[A]+[B]",
                        },
                    }
                }
            }
        };

        public static MobiForm GetForm(string formName)
        {
            return GetAllForms().FirstOrDefault(x => x.FormName == formName );
        }
        public static MobiForm GetMobiForm(string formName)
        {
            return GetAllForms().FirstOrDefault(x => x.FormName == formName && x.Type == "MobiForm");
        }
        public static MobiForm GetSubForm(string formName)
        {
            return GetAllForms().FirstOrDefault(x => x.FormName == formName && x.Type == "SubMobiForm");
        }

        public static List<MobiForm> GetAllForms()
        {
            var obj = new MobilizeFormsOrganizationData();
            return new List<MobiForm>() { obj.MainForm, obj.SubForm, obj.SubSubForm };
        }

        public int OrgId = 1;
    }
}