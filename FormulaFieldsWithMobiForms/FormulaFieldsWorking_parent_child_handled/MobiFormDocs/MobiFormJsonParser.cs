using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mobilize.Contract.CustomFields;
using Mobilize.Contract.MobilizeDataTypes;
using Mobilize.Contract.GetListItemsServices;
using Newtonsoft.Json;
using FormulaFieldsWorking_parent_child_handled.MobiFormDocs;

namespace FormulaFieldsWorking_parent_child_handled
{
    public class MobiFormDocumentBaseDecorator : MobiFormDocument
    {
        MobiFormDocument baseDocument = null;

        public MobiFormDocumentBaseDecorator(MobiFormDocument document) : base(document.OrgId)
        {
            baseDocument = document;
        }
    }

    public class MobiFormDocument
    {
        private int _orgId;
        public string OrgSchema
        {
            get { return $"o{_orgId}"; }
        }
        public int OrgId
        {
            get { return _orgId; }
        }
        public MobiFormDocument(int orgId)
        {
            this._orgId = orgId;
        }

        /// <summary>
        /// checks for if mobiForm object is not null and having some pages 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public virtual bool MobiForm_IsNullOrEmpty(MobiForm form)
        {
            if (form?.Pages == null || form?.Pages.Count == 0)
            {
                //no updated required in this mobiform this form is blank
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual List<MobiForm> GetAllForms()
        {
            return RepositoryFactory.CreateSysDocumentsRepository().GetAllForms(this._orgId) ?? new List<MobiForm>();
        }

        public virtual MobiForm GetMobiForm(string formName)
        {
            return RepositoryFactory.CreateSysDocumentsRepository().GetMobiForm(this._orgId, formName);
        }

        public virtual MobiForm GetSubMobiForm(string formName)
        {
            return RepositoryFactory.CreateSysDocumentsRepository().GetSubForm(this._orgId, formName);
        }

        /// <summary>
        /// this function adds client id, server id, flag field to a mobi form in memory object
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public virtual MobiForm AddSystemKeyFieldsToMobiForm(MobiForm form)
        {
            if (MobiForm_IsNullOrEmpty(form))
            {
                return form;
            }

            return form;

            if (form.Pages[0].FieldsInfo.All(x => x.MobilizeType != "ID"))
            {
                form.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "ServerId",
                    MobilizeType = "ID",
                    FieldUserFriendlyName = "ServerId",
                    Hidden = true,
                    ShowInListView = false,
                });
            }
            if (form.Pages[0].FieldsInfo.All(x => x.MobilizeType != "Flag"))
            {
                form.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "Flag",
                    MobilizeType = "Flag",
                    FieldUserFriendlyName = "Flag",
                    Hidden = true,
                    ShowInListView = false,
                });
            }
            if (form.Pages[0].FieldsInfo.All(x => x.MobilizeType != "Guid"))
            {
                form.Pages[0].FieldsInfo.Add(new FieldsInfo()
                {
                    FieldInternalName = "ClientId",
                    MobilizeType = "Guid",
                    FieldUserFriendlyName = "ClientId",
                    Hidden = true,
                    ShowInListView = false,
                });
            }
            return form;
        }

        /// <summary>
        /// Parent form will be assigned to Sub form
        /// Parent.Name will be added to Sub.Parent[]
        /// A parent cannot have 2 same subforms as Field
        /// </summary>
        /// <param name="subFormName"></param>
        /// <param name="parentFormName"></param>
        /// <returns></returns>
        public virtual bool SetParentOfSubForm(List<MobiForm> organizatonAllForms, string subFormName,
            string parentFormName)
        {
            var subForm = organizatonAllForms.FirstOrDefault(x => x.FormName == subFormName);
            if (subForm == null)
            {
                throw new Exception("Contact Support");
            }
            var parentForm = organizatonAllForms.FirstOrDefault(x => x.FormName == parentFormName);
            if (parentForm?.ParentForms == null || parentForm.ParentForms.Count == 0)
            {
                // if parent form is a new form or does not have any parent
            }
            else
            {
                // if parent form existe and is not a root form, may have some parents..

                if (DoesParentListOfParentFormHasSubFormName(organizatonAllForms, subFormName, parentForm.FormName))
                {
                    return false;
                }
                else
                {
                    //if there is no such parent of parentForm which contains sub form in 'ParentForms' List
                }
            }
            if (subForm.ParentForms == null || subForm.ParentForms.Count == 0)
            {
                subForm.ParentForms = new List<string>() { parentFormName };
            }
            else
            {
                if (subForm.ParentForms.Contains(parentFormName))
                {
                    //  return false as form is already asigned to Parent Form...
                    return false;
                }
                subForm.ParentForms.Add(parentFormName);
            }
            var indexOfSubForm = organizatonAllForms.IndexOf(subForm);
            organizatonAllForms[indexOfSubForm] = subForm;

            return true;
        }

        public virtual MobiForm RemoveParentOfSubFormInAllFormsList(List<MobiForm> organizatonAllForms, string subFormName,
            string parentFormName)
        {
            var parentForm = organizatonAllForms.FirstOrDefault(x => x.FormName == parentFormName);
            var subForm = organizatonAllForms.FirstOrDefault(x => x.FormName == subFormName);

            if (parentForm == null || subForm?.ParentForms == null || subForm.ParentForms.Count == 0 ||
                !subForm.ParentForms.Contains(parentForm.FormName))
            {
                throw new Exception("Contact Support");
            }

            subForm.ParentForms.Remove(parentForm.FormName);

            //var indexOfSubForm = organizatonAllForms.IndexOf(subForm);
            //organizatonAllForms[indexOfSubForm] = subForm;

            //return true;
            return subForm;
        }

        public virtual bool DoesParentListOfParentFormHasSubFormName(
            List<MobiForm> organizatonAllForms,
            string subFormName,
            string parentFormName
            )
        {
            var parentForm = organizatonAllForms.FirstOrDefault(x => x.FormName == parentFormName);

            if (parentForm == null)
            {
                throw new Exception("Contact Support");
            }

            if (parentForm.ParentForms == null || parentForm.ParentForms.Count == 0)
            {
                return false;
            }

            if (parentForm.ParentForms.Contains(subFormName))
            {
                return true;
            }

            foreach (var parents in parentForm.ParentForms)
            {
                if (DoesParentListOfParentFormHasSubFormName(organizatonAllForms, subFormName, parents))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual List<FieldInformation> GetMobiFormsKeys(string formName)
        {
            var mobiForm = GetMobiForm(formName) ?? GetSubMobiForm(formName);
            return GetKeysFromMobiForm(mobiForm);
        }

        public virtual List<FieldInformation> GetKeysFromMobiForm(MobiForm mobiForm, MobiForm parentMobiForm = null)
        {
            if (MobiForm_IsNullOrEmpty(mobiForm)) return null;
            //To add new fields in every page for internal purposes [offline scenario] and tracking of record [via ID]

            mobiForm = AddSystemKeyFieldsToMobiForm(mobiForm);
            List<FieldInformation> keys = new List<FieldInformation>();

            foreach (var page in mobiForm.Pages)
            {
                if (page?.FieldsInfo == null) continue;
                foreach (var field in page.FieldsInfo)
                {
                    if (field == null) continue;
                    switch (field.MobilizeType)
                    {
                        case "SubForm":
                            {
                                if (field is SubForm)
                                {
                                    var customField = (field as SubForm);
                                    var keyItem = new FieldInformation()
                                    {
                                        Label = field.Label,
                                        //Label = field.FieldUserFriendlyName,
                                        Formula = "",
                                        SubForm = customField.SubMobiForm,
                                        SubFormMode = customField.SubFormMode,
                                        Type = customField.MobilizeType,
                                        FilterType = customField.FilterType,
                                        Filterable = customField.Filterable,
                                        Hidden = customField.Hidden,
                                        InternalName = customField.FieldInternalName,
                                        ShowInListView = customField.ShowInListView,
                                        Sortable = customField.Sortable,
                                        ImpactedFields = GetImpactedFieldsList(field, parentMobiForm?? mobiForm),
                                        SubFormFields = null,
                                        FullName = customField.FullName,
                                    };

                                    if (customField != null)
                                    {
                                        if (keyItem.AdditionalInformation == null)
                                        {
                                            keyItem.AdditionalInformation = new Dictionary<string, object>();
                                        }
                                        //keyItem.AdditionalInformation.Add("SubFormMode", customField.SubFormMode);
                                    }

                                    var keysOfSubForm = GetKeysFromMobiForm(customField.MobiForm, mobiForm);

                                    if (keysOfSubForm != null)
                                    {
                                        keyItem.SubFormFields = keysOfSubForm;
                                    }

                                    keys.Add(keyItem);
                                }
                            }
                            break;
                        case "Formula":
                            {

                                if (field is Formula)
                                {
                                    var customField = (field as Formula);
                                    var keyItem = new FieldInformation()
                                    {
                                        Label = field.Label,
                                        //Label = field.FieldUserFriendlyName,
                                        Formula = customField.FormulaExpression,
                                        SubForm = null,
                                        SubFormMode = null,
                                        Type = field.MobilizeType,
                                        FilterType = field.FilterType,
                                        Filterable = field.Filterable,
                                        Hidden = field.Hidden,
                                        InternalName = field.FieldInternalName,
                                        ShowInListView = field.ShowInListView,
                                        Sortable = field.Sortable,
                                        ImpactedFields = GetImpactedFieldsList(field, parentMobiForm ?? mobiForm),
                                        SubFormFields = null,
                                        FullName = field.FullName,
                                    };

                                    if (customField != null)
                                    {
                                        if (keyItem.AdditionalInformation == null)
                                        {
                                            keyItem.AdditionalInformation = new Dictionary<string, object>();
                                        }
                                        keyItem.AdditionalInformation.Add("FormulaType", customField.FormulaType);
                                    }

                                    keys.Add(keyItem);
                                }
                            }
                            break;
                        case "DateTimeInfo":
                            {
                                var keyItem = new FieldInformation()
                                {
                                    Label = field.Label,
                                    //Label = field.FieldUserFriendlyName,
                                    Formula = "",
                                    SubForm = null,
                                    SubFormMode = null,
                                    Type = field.MobilizeType,
                                    FilterType = field.FilterType,
                                    Filterable = field.Filterable,
                                    Hidden = field.Hidden,
                                    InternalName = field.FieldInternalName,
                                    ShowInListView = field.ShowInListView,
                                    Sortable = field.Sortable,
                                    ImpactedFields = GetImpactedFieldsList(field, parentMobiForm ?? mobiForm),
                                    SubFormFields = null,
                                    FullName = field.FullName,
                                };

                                var customField = field as DateTimeInfo;
                                if (customField != null)
                                {
                                    if (keyItem.AdditionalInformation == null)
                                    {
                                        keyItem.AdditionalInformation = new Dictionary<string, object>();
                                    }
                                    keyItem.AdditionalInformation.Add("DateOnly", customField.DateOnly);
                                }

                                keys.Add(keyItem);
                            }
                            break;
                        default:
                            {
                                var keyItem = new FieldInformation()
                                {
                                    Label = field.Label,
                                    //Label = field.FieldUserFriendlyName,
                                    Formula = "",
                                    SubForm = null,
                                    SubFormMode = null,
                                    Type = field.MobilizeType,
                                    FilterType = field.FilterType,
                                    Filterable = field.Filterable,
                                    Hidden = field.Hidden,
                                    InternalName = field.FieldInternalName,
                                    ShowInListView = field.ShowInListView,
                                    Sortable = field.Sortable,
                                    ImpactedFields = GetImpactedFieldsList(field, parentMobiForm ?? mobiForm),
                                    SubFormFields = null,
                                    FullName = field.FullName,
                                };
                                if (keyItem.AdditionalInformation == null)
                                {
                                    keyItem.AdditionalInformation = new Dictionary<string, object>();
                                }
                                keys.Add(keyItem);
                            }
                            break;
                    }
                }
            }
            return keys;
        }

        public virtual List<SubForm> GetAllSubFormFieldsListInForm(MobiForm mobiForm)
        {
            List<SubForm> subForms = new List<SubForm>();
            if (mobiForm?.Pages == null || mobiForm.Pages.Count == 0)
            {
                return subForms;
            }
            foreach (var page in mobiForm.Pages)
            {
                if (page?.FieldsInfo == null || page.FieldsInfo.Count == 0)
                {
                    continue;
                }
                var subFormFields =
                    page.FieldsInfo.OfType<SubForm>().ToList();
                subForms.AddRange(subFormFields);
            }
            return subForms;
        }

        protected virtual List<string> GetImpactedFieldsList(FieldsInfo formField,MobiForm currentForm)
        {
            if (formField is SubForm )
            {
                return null;
            }

            List<string> fieldNames = new List<string>();

            if(formField?.ImpactedFieldsInfosList == null)
            {
                return fieldNames;
            }
            foreach (var field in formField.ImpactedFieldsInfosList)
            {
                //if (((field.ParentField as SubForm)?.MobiForm == null || (field.ParentField as SubForm).MobiForm.AllFields.Any(x => x.FullName == field.FullName)) && ((field.ParentField as SubForm)?.MobiForm?.InternalFormName == (formField.ParentField as SubForm)?.MobiForm?.InternalFormName))
                if ((field.ParentField as SubForm)?.MobiForm?.InternalFormName == (formField.ParentField as SubForm)?.MobiForm?.InternalFormName)
                {
                    fieldNames.Add($"this.{field.FieldInternalName}");//this
                }
                else
                {
                    bool isInParentForm =false;
                    foreach(var page in currentForm.Pages)
                    {
                        if (page?.FieldsInfo == null)
                        {
                            continue;
                        }
                        isInParentForm = page.FieldsInfo.Any(x => x != null && x.FieldInternalName == field.FieldInternalName && (x.ParentField as SubForm)?.MobiForm?.InternalFormName == (field.ParentField as SubForm)?.MobiForm?.InternalFormName);
                        if (isInParentForm)
                        {
                            break;
                        }
                    }
                    if(isInParentForm)
                    {
                        fieldNames.Add($"parent.{field.FieldInternalName}");//parent
                    }
                    else
                    {
                        fieldNames.Add($"{field.FullName}");//child

                    }
                }

                //if ((field.ParentField as SubForm)?.MobiForm?.InternalFormName == (formField.ParentField as SubForm)?.MobiForm?.InternalFormName)
                //{
                //    fieldNames.Add(field.FieldInternalName);
                //}
                //else
                //{
                //    fieldNames.Add($"parent.{field.FieldInternalName}");
                //}
            }
            return fieldNames;
        }
    }

    public class MobiFormDocumentDecorator : MobiFormDocumentBaseDecorator
    {
        public MobiFormDocumentDecorator(MobiFormDocument document) : base(document)
        {

        }

        //all forms in memory... for each instance...
        private List<MobiForm> _allForm, _allMobiForm, _allSubForm;

        public override List<MobiForm> GetAllForms()
        {
            if (_allForm != null && _allForm.Count > 0)
            {
                return _allForm;
            }
            _allForm = RepositoryFactory.CreateSysDocumentsRepository().GetAllForms(OrgId) ?? new List<MobiForm>();
            return _allForm;
        }

        public virtual List<MobiForm> GetAllMobiForm()
        {
            if (_allMobiForm != null && _allMobiForm.Count > 0)
            {
                return _allMobiForm;
            }
            _allMobiForm = RepositoryFactory.CreateSysDocumentsRepository().GetAllMobiForms(OrgId);
            return _allMobiForm;
        }

        public virtual List<MobiForm> GetAllSubForm()
        {
            if (_allSubForm != null && _allSubForm.Count > 0)
            {
                return _allSubForm;
            }
            _allSubForm = RepositoryFactory.CreateSysDocumentsRepository().GetAllSubForms(OrgId);
            return _allSubForm;
        }

        public virtual MobiForm GetCompleteMobiForm(string formName, SubForm parentField = null, bool isSubForm = false)
        {
            MobiForm form;
            if (isSubForm)
            {
                form = GetAllSubForm()?.FirstOrDefault(x => x.FormName == formName && x.Type == "SubMobiForm");
            }
            else
            {
                form = GetAllMobiForm()?.FirstOrDefault(x => x.FormName == formName && x.Type == "MobiForm");
            }
            if (form == null)
            {
                return null;
            }

            if (MobiForm_IsNullOrEmpty(form))
            {
                return form;
            }

            if (form.Pages != null)
            {
                foreach (var page in form.Pages)
                {
                    if (page?.FieldsInfo == null)
                    {
                        continue;
                    }
                    foreach (var field in page.FieldsInfo)
                    {
                        field.ParentField = parentField;
                        if (field is SubForm)
                        {
                            var subFormField = field as SubForm;

                            subFormField.MobiForm =
                                GetCompleteMobiForm(subFormField.SubMobiForm,
                                    subFormField, true);

                            foreach (var pa in subFormField.MobiForm.Pages)
                            {
                                foreach (var fieldIno in pa.FieldsInfo)
                                {
                                    fieldIno.ParentField = subFormField;
                                }
                            }
                        }
                    }
                }
            }

            return form;
        }

        public virtual MobiForm FormulaFieldsWithDependencies(MobiForm form, MobiForm parentForm= null)
        {
            if (MobiForm_IsNullOrEmpty(form))
            {
                return form;
            }
            foreach (var page in form.Pages)
            {
                if (page == null)
                {
                    continue;
                }
                foreach (var fieldsInfo in page.FieldsInfo)
                {
                    if (fieldsInfo is Formula)
                    {
                        var formula = fieldsInfo as Formula;
                        /* todo: evaluate expression...
                         * todo: replace expression with original expression...
                         * todo: assign dependencies to formula field...
                         */

                        FormulaExpressionResolver parser = new FormulaExpressionResolver(formula, form, parentForm ?? form);
                        if (parser.Valid)
                        {
                            formula.FormulaExpression = parser.SystemExpression;
                            if (formula.DependentFieldsInfosList == null)
                            {
                                formula.DependentFieldsInfosList = new List<FieldsInfo>();
                            }
                            formula.DependentFieldsInfosList.AddRange(parser.DependentFields);
                        }
                        else
                        {
                            throw new Exception("Invalid formula exists");
                        }
                    }

                    if (fieldsInfo is SubForm)
                    {
                        var subFormField = fieldsInfo as SubForm;
                        subFormField.MobiForm = FormulaFieldsWithDependencies(subFormField.MobiForm, form);
                    }
                }
            }
            return form;
        }

        public virtual MobiForm SetImpactedFormulaFields(MobiForm form, MobiForm parentForm = null)
        {
            List<Formula> allFormulaFields;
            if (parentForm != null)
            {
                allFormulaFields = AllFormulaFields(parentForm);
                //var allFormulaFieldsInParent = AllFormulaFields(parentForm);
                //var unique = allFormulaFieldsInParent.Where(x => x != null && allFormulaFields != null && !allFormulaFields.Contains(x));
                //allFormulaFields.AddRange(unique);
            }
            else
            {
                allFormulaFields = AllFormulaFields(form);
            }
            //var allFormulaFields = AllFormulaFields(form);

            if (allFormulaFields == null)
            {
                return form;
            }
            if (MobiForm_IsNullOrEmpty(form))
            {
                return form;
            }
            foreach (var page in form.Pages)
            {
                if (page == null)
                {
                    continue;
                }
                foreach (var fieldsInfo in page.FieldsInfo)
                {
                    if (fieldsInfo == null)
                        continue;
                    if (fieldsInfo.ImpactedFieldsInfosList == null)
                    {
                        fieldsInfo.ImpactedFieldsInfosList = new List<FieldsInfo>();
                    }
                    if (fieldsInfo is SubForm)
                    {
                        var subForm = fieldsInfo as SubForm;
                        subForm.MobiForm = SetImpactedFormulaFields(subForm.MobiForm, form);
                    }
                    var impactedFields = FieldsInFormulaDependencyList(allFormulaFields, fieldsInfo);
                    if (impactedFields == null)
                        continue;
                    fieldsInfo.ImpactedFieldsInfosList.AddRange(impactedFields);
                }
            }
            return form;
        }

        public virtual List<Formula> FieldsInFormulaDependencyList(List<Formula> allFormulaFields, FieldsInfo field)
        {
            if (allFormulaFields == null || allFormulaFields.Count == 0)
            {
                return null;
            }
            List<Formula> impactedFormulaFieldsList = new List<Formula>();

            foreach (var formula in allFormulaFields)
            {
                if (formula == null) continue;
                //this returns all the formula fields which have field in their dependency list
                var impactedFields = allFormulaFields
                    .Where(
                        x => x != null && x.DependentFieldsInfosList != null && x.DependentFieldsInfosList
                            .Any(
                                y => y.FullName == field.FullName && (y.ParentField as SubForm)?.MobiForm?.InternalFormName == (field.ParentField as SubForm)?.MobiForm?.InternalFormName
                            ))?.ToList();
                var unique = impactedFields.Where(x => x != null && impactedFormulaFieldsList != null && !impactedFormulaFieldsList.Contains(x)).ToList();
                impactedFormulaFieldsList
                    .AddRange(unique);
            }
            return impactedFormulaFieldsList;
        }

        public virtual List<Formula> AllFormulaFields(MobiForm form)
        {
            List<Formula> formulas = new List<Formula>();
            if (MobiForm_IsNullOrEmpty(form))
            {
                return formulas;
            }
            foreach (var page in form.Pages)
            {
                if (page?.FieldsInfo == null) continue;
                formulas.AddRange(page.FieldsInfo.OfType<Formula>().ToList());
                foreach (var field in page.FieldsInfo)
                {
                    var subFormField = field as SubForm;
                    if (subFormField?.MobiForm == null) continue;
                    var subFormulas = AllFormulaFields(subFormField.MobiForm);
                    if (subFormulas == null) continue;
                    formulas.AddRange(subFormulas);
                }
            }
            return formulas;
        }

        public virtual List<FieldInformation> GetKeysInPerfectMobiForm(string formName)
        {
            MobiForm completeMobiForm = GetCompleteMobiForm(formName);
            if (completeMobiForm == null) return null;
            completeMobiForm = FormulaFieldsWithDependencies(completeMobiForm);
            if (completeMobiForm == null) return null;
            completeMobiForm = SetImpactedFormulaFields(completeMobiForm);
            if (completeMobiForm == null) return null;

            if (MobiForm_IsNullOrEmpty(completeMobiForm))
            {
                return null;
            }

            MobiForm perfectForm = completeMobiForm;
            List<FieldInformation> keysInfos = GetKeysFromMobiForm(perfectForm);
            return keysInfos;
        }
    }

    public class FormulaFieldName
    {
        public string Name { get; set; }
        public List<string> NamesHierarchy { get; set; }
    }

    public class FormulaExpressionResolver
    {
        private string parentKeyWord = "parent";
        private MobiForm _mobiForm, _rootMobiForm;
        private Formula _formula;
        private bool _isValid;
        private string _systemExpression;
        private List<FieldsInfo> _dependentFieldsList;

        public bool Valid
        {
            get { return this._isValid; }
        }

        public string SystemExpression
        {
            get { return this._systemExpression; }
        }

        public List<FieldsInfo> DependentFields
        {
            get { return _dependentFieldsList; }
        }

        public FormulaExpressionResolver(Formula formula, MobiForm completeMobiForm, MobiForm rootMobiForm)
        {
            this._isValid = true;
            this._formula = formula;
            this._mobiForm = completeMobiForm;
            this._rootMobiForm = rootMobiForm;
            this._dependentFieldsList = new List<FieldsInfo>();
            this._systemExpression = RealExpressionEvaluation(this._formula.FormulaExpression, this._mobiForm, this._rootMobiForm);
            Init();
        }

        private void Init()
        {
        }

        private List<FormulaFieldName> GetFieldsList(string expression)
        {
            var formulaExpression = expression;
            if (string.IsNullOrEmpty(formulaExpression))
            {
                return null;
            }
            List<FormulaFieldName> fields = new List<FormulaFieldName>();

            var regex = new Regex(@"\[(.*?)\]");
            var matches = regex.Matches(formulaExpression);

            foreach (var match in matches)
            {
                var temp = match.ToString();
                if (string.IsNullOrEmpty(temp))
                {
                    _isValid = false;
                    fields = null;
                    break;
                }
                temp = temp.Remove(0, 1);
                temp = temp.Remove(temp.Length - 1, 1);
                if (string.IsNullOrEmpty(temp))
                {
                    _isValid = false;
                    fields = null;
                    break;
                }
                var values = temp.Split('.').ToList();
                if (values.Count > 2 || values.Any(string.IsNullOrEmpty))
                {
                    //  field names hierarchy must be till 1 child level...
                    _isValid = false;
                    fields = null;
                    break;
                }
                fields.Add(new FormulaFieldName()
                {
                    NamesHierarchy = values,
                    Name = temp,
                });
            }
            return fields;
        }

        private string RealExpressionEvaluation(string userExpression, MobiForm perfectMobiForm, MobiForm perfectParentMobiForm)
        {
            var systemExpression = $"{userExpression}";
            var allFieldsInExpression = GetFieldsList(userExpression);
            if (allFieldsInExpression == null || allFieldsInExpression.Count == 0)
            {
                this._isValid = false;
                return null;
            }
            foreach (var field in allFieldsInExpression)
            {
                if (field == null)
                {
                    continue;
                }
                if (!systemExpression.Contains(field.Name))
                {
                    this._isValid = false;
                    return null;
                }

                var fieldInternalName = GetFormulaFieldNameAndInitDependencyList_2(field.Name,
                    perfectMobiForm, perfectParentMobiForm);
                if (string.IsNullOrEmpty(fieldInternalName))
                {
                    this._isValid = false;
                    return null;
                }
                systemExpression = systemExpression.Replace(field.Name, fieldInternalName);
            }
            return systemExpression;
        }

        private string GetFormulaFieldNameAndInitDependencyList(List<string> hierarchialNamesList,
            MobiForm perfectForm, int index = 0)
        {
            var internalFieldName = $"";
            if (hierarchialNamesList.Count == 0)
            {
                return null;
            }
            if (hierarchialNamesList.Count <= index)
            {
                return null;
            }
            if (perfectForm?.Pages == null || perfectForm.Pages.Count == 0)
            {
                return null;
            }
            foreach (var page in perfectForm.Pages)
            {
                if (page.FieldsInfo == null || page.FieldsInfo.Count == 0)
                    continue;

                foreach (var field in page.FieldsInfo)
                {
                    if (field == null)
                        continue;
                    if (field.FieldUserFriendlyName != hierarchialNamesList[index])
                        continue;
                    internalFieldName = field.FieldInternalName;
                    AddFieldToDependencyList(field);
                    if (hierarchialNamesList.Count == 1)
                    {
                        //AddFieldToDependencyList(field);
                        return internalFieldName;
                    }
                    //this ensures that if a field is not subForm then continue or return null
                    if (!(field is SubForm))
                    {
                        if (hierarchialNamesList.Count > index + 1)
                        {
                            return null;
                        }
                        continue;
                    }
                    //if this is the last field in hierarchial names list
                    if (hierarchialNamesList.Count <= index + 1)
                    {
                        AddFieldToDependencyList(field);//todo:check if this is required
                        return internalFieldName;
                    }
                    var subForm = (field as SubForm);
                    if (subForm.MobiForm == null)
                    {
                        this._isValid = false;
                        return null;
                    }
                    var subFieldname =
                        GetFormulaFieldNameAndInitDependencyList(hierarchialNamesList,
                            subForm.MobiForm,
                            ++index);
                    if (string.IsNullOrEmpty(subFieldname))
                    {
                        return null;
                    }
                    internalFieldName = $"{internalFieldName}.{subFieldname}";

                    RemoveFieldFromDependencyList(field);//todo:check if this is required
                }
            }
            return internalFieldName;
        }

        private string GetFormulaFieldNameAndInitDependencyList_2(string fullUserFriendlyName,
            MobiForm perfectForm,
            MobiForm perfectParentForm)
        {
            if (perfectForm?.AllFields == null || perfectForm?.AllFields.Count == 0)
            {
                return null;
            }

            var origField = perfectForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && x.FieldUserFriendlyName == fullUserFriendlyName);

            if (origField != null && !(origField is SubForm))
            {
                AddFieldToDependencyList(origField);
                return origField.FieldInternalName;
            }

            origField = perfectForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && x.FullUserFriendlyName == fullUserFriendlyName && (x.ParentField as SubForm == null || (x.ParentField as SubForm)?.MobiForm.InternalFormName != perfectForm.InternalFormName));

            //&& (x.ParentField as SubForm)?.MobiForm?.InternalFormName == (x.ParentField as SubForm)?.MobiForm?.InternalFormName

            if (origField != null && !(origField is SubForm))
            {
                AddFieldToDependencyList(origField);
                return origField.FullName;
            }
            else
            {
                bool isInParent = false;
                origField = perfectParentForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && (x.FieldUserFriendlyName == fullUserFriendlyName));
                if (origField == null)
                {
                    origField = perfectParentForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && ($"{parentKeyWord}.{x.FieldUserFriendlyName}" == fullUserFriendlyName));
                    isInParent = origField != null;
                }

                if (origField == null)
                {
                    origField = perfectParentForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && x.FullUserFriendlyName == fullUserFriendlyName && (x.ParentField as SubForm == null || (x.ParentField as SubForm)?.MobiForm.InternalFormName != perfectForm.InternalFormName));
                    if (origField == null)
                    {
                        //origField = perfectParentForm?.AllFields?.FirstOrDefault(x => x != null && x.MobilizeType != "SubForm" && ($"{parentKeyWord}.{x.FieldUserFriendlyName}" == fullUserFriendlyName) && (x.ParentField as SubForm == null || (x.ParentField as SubForm)?.MobiForm.InternalFormName != perfectForm.InternalFormName));
                        isInParent = origField != null;
                    }
                }
                if (origField != null && !(origField is SubForm))
                {
                    AddFieldToDependencyList(origField);
                    if (isInParent)
                    {
                        return $"{parentKeyWord}.{origField.FullName}";
                    }
                    return origField.FullName;
                }
            }

            return null;
        }

        private void AddFieldToDependencyList(FieldsInfo field)
        {
            if (this._dependentFieldsList == null)
            {
                this._dependentFieldsList = new List<FieldsInfo>();
            }
            this._dependentFieldsList.Add(field);
        }

        private void RemoveFieldFromDependencyList(FieldsInfo field)
        {
            if (this._dependentFieldsList == null || !this._dependentFieldsList.Contains(field))
            {
                return;
            }
            this._dependentFieldsList.Remove(field);
        }
    }
}