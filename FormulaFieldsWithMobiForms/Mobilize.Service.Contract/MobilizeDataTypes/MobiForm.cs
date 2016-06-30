using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.CustomFields;
using Newtonsoft.Json;

namespace Mobilize.Contract.MobilizeDataTypes
{

    public class MobiForm
    {
        public MobiForm()
        {
            
        }

        public string id;

        public string _self;
        public string Organization { get; set; }

        public string FormName { get; set; }

        public string Type { get; set; } = "MobiForm"; // MobiForm or SubMobiForm

        public List<string> ParentForms { get; set; }

        public Conflict ConflictResolution { get; set; }

        public string PrimaryKeyColumn { get; set; }
        /// <summary>
        ///  unique name of the SQL db table
        /// </summary>
        public string InternalFormName { get; set; } 
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public List<Page> Pages { get; set; }
        public string Query { get; set; }


        //[JsonIgnore]
        public List<FieldsInfo> AllFields
        {
            get
            {
                var list = new List<FieldsInfo>();
                if (this.Pages == null) return list;
                foreach (var page in this.Pages)
                {
                    if (page?.FieldsInfo == null) continue;
                    foreach (var fieldsInfo in page.FieldsInfo)
                    {
                        var subField = fieldsInfo as SubForm;
                        if (subField == null)
                        {
                            list.Add(fieldsInfo);
                        }
                        else
                        {
                            foreach (var fi in subField.AllFields)
                            {
                                fi.ParentField = subField;
                            }
                            list.AddRange(subField.AllFields);
                        }
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// This method is used to get the internal name of the field using its user friendly name. 
        /// mainly used when a field is used as the source for another mobi form for lookups/lookupmulti fields
        /// </summary>
        /// <param name="fieldUserFriendlyName"></param>
        /// <returns></returns>
        public string GetFieldInternalName(string fieldUserFriendlyName)
        {
            if (string.IsNullOrEmpty(fieldUserFriendlyName))
            {
                throw new ArgumentException("Lookup source field name can not be null");
            }
            string fieldInternalName = string.Empty;
            if (this?.Pages == null || this.Pages.Count == 0)
            {
                throw new KeyNotFoundException("Source Mobi Form does not have any fields");
            }
            if (this.Type != "MobiForm")
            {
                throw new ArgumentException("form selected is either invalid or not a mobi form");
            }
            foreach (var page in this.Pages)
            {
                if (page?.FieldsInfo == null || page.FieldsInfo.Count == 0)
                {
                    continue;
                }
                foreach (var field in page.FieldsInfo)
                {
                    if (field.FieldUserFriendlyName.Equals(fieldUserFriendlyName))
                    {
                        fieldInternalName = field.FieldInternalName;
                    }
                }
            }
            if (string.IsNullOrEmpty(fieldInternalName))
            {
                throw new KeyNotFoundException("Lookup Field source not found in destination mobi form");
            }
            return fieldInternalName;
        }

        public MobiForm SortMobiFormPageFields()
        {
            if (this?.Pages == null)
            {
                return this;
            }
            foreach (var page in this.Pages)
            {
                page?.FieldsInfo?.Sort((field1, field2) => field1.Sequence.CompareTo(field2.Sequence));
            }
            return this;
        }
    }

    public class Page
    {
        public string ListName { get; set; }
        public List<FieldsInfo> FieldsInfo { get; set; }
    }
    /// <summary>
    /// This represents a field in a mobi form also known as Keys of a form
    /// 
    /// When adding a new type of field in mobi form,
    /// make sure it is also mentioned in _controlsFilter list in common settings 
    /// as well as in Json Converter FieldsConverter.cs 
    /// </summary>
    public class FieldsInfo
    {
        public virtual string Label { get; set; }
        //[JsonIgnore]
        public virtual FieldsInfo ParentField { get; set; }

        public virtual bool IsModified { get; set; }

        //[JsonIgnore]
        public virtual string FullName
        {
            get
            {
                if (this.ParentField==null)
                {
                    return this.FieldInternalName;
                }
                else
                {
                    return $"{this.ParentField.FieldInternalName}.{this.FieldInternalName}";
                }
            }
        }

        //[JsonIgnore]
        public virtual string FullUserFriendlyName
        {
            get
            {
                if (this.ParentField == null)
                {
                    return this.FieldUserFriendlyName;
                }
                else
                {
                    return $"{this.ParentField.FieldUserFriendlyName}.{this.FieldUserFriendlyName}";
                }
            }
        }
        public virtual string FieldInternalName { get; set; }
        public virtual string FieldUserFriendlyName { get; set; }
        public virtual string Description { get; set; }
        public virtual string MobilizeType { get; set; }
        public virtual string TypeStructure { get; set; }
        public virtual bool Required { get; set; }
        public virtual string RequiredErrorMessage { get; set; }
        public virtual bool Filterable { get; set; }
        public virtual string FilterType { get; set; }  //TODO: this should be changed to enum in future... currently UI app (WEB proj is able to set any random value)
        public virtual bool Sortable { get; set; }
        public virtual bool Hidden { get; set; }
        public virtual bool ShowInListView { get; set; } = true;
        public virtual int Sequence { get; set; }

        //[JsonIgnore]
        public virtual List<FieldsInfo> DependentFieldsInfosList { get; set; }
        //[JsonIgnore]
        public virtual List<FieldsInfo> ImpactedFieldsInfosList { get; set; }
    }
    
    public enum Conflict
    {
        First=0,
        Last=1
    }
    public enum FilterType
    {
        Contains = 0,
        Range = 1,
        MultipleChoice = 2
    }
}
