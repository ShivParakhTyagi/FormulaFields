using FormulaFieldsWorking_parent_child_handled.MobiFormDocs.JsonConverters;
using Mobilize.Contract.MobilizeDataTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FormulaFieldsWorking_parent_child_handled.MobiFormDataContainer;


namespace FormulaFieldsWorking_parent_child_handled.MobiFormDocs
{
    public interface ISysDocumentsRepository
    {
        List<MobiForm> GetAllMobiForms(int orgId);
        List<MobiForm> GetAllSubForms(int orgId);
        List<MobiForm> GetAllForms(int orgId);
        //List<SysDocuments> GetAllLayoutDocuments(int orgId, string type);
        //List<SysDocuments> GetLayoutDocuments(int orgId, List<string> formNames, string type);
        //SysDocuments GetMobiFormDocument(int orgId, string formName);
        //SysDocuments GetSubFormDocument(int orgId, string formName);
        //SysDocuments GetFormDocument(int orgId, string formName);
        MobiForm GetMobiForm(int orgId, string formName);
        MobiForm GetSubForm(int orgId, string formName);
        MobiForm GetForm(int orgId, string formName);
        //SysDocuments GetLayoutDocument(int orgId, string formName, string type);
        bool SaveMobiForm(int orgId, MobiForm form, string loginId);
        bool SaveSubForm(int orgId, MobiForm form, string loginId);
        bool SaveLayout(int orgId, string formName, string layout, string type, string loginId);
        bool DeleteFormDocuments(int orgId, string formName);
        bool DeleteMobiForm(int orgId, string formName);
        bool DeleteSubForm(int orgId, string formName);
        bool DeleteAllLayouts(int orgId, string formName);
        bool DeleteLayout(int orgId, string formName, string type);
    }

    public class SysDocumentsRepository : ISysDocumentsRepository
    {
        public SysDocumentsRepository()
        {
            //  constructor must be parameterless in repository pattern
        }

        public List<MobiForm> GetAllMobiForms(int orgId)
        {
            var db = new MobilizeFormsOrganizationData();
            {
                List<MobiForm> mobiForms = MobilizeFormsOrganizationData.GetAllForms()?.Where(x=>x.Type=="MobiForm")?.ToList();
                return mobiForms;
            }
        }

        public List<MobiForm> GetAllSubForms(int orgId)
        {
            var db = new MobilizeFormsOrganizationData();
            {
                List<MobiForm> subMobiForms = MobilizeFormsOrganizationData.GetAllForms()?.Where(x => x.Type == "SubMobiForm")?.ToList();
                return subMobiForms;
            }
        }

        public List<MobiForm> GetAllForms(int orgId)
        {
            var db = new MobilizeFormsOrganizationData();
            {
                List<MobiForm> forms = MobilizeFormsOrganizationData.GetAllForms()?.Where(x => x != null)?.ToList();
                return forms;
            }
        }

        //public List<SysDocuments> GetAllLayoutDocuments(int orgId, string type)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        List<SysDocuments> layouts = (from f in db.SysDocuments
        //                                      where f.OrganizationId == orgId && f.Type == type
        //                                      select f).ToList();
        //        return layouts;
        //    }
        //}
        //public List<SysDocuments> GetLayoutDocuments(int orgId, List<string> formNames, string type)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        List<SysDocuments> layouts = (from f in db.SysDocuments
        //                                      where f.OrganizationId == orgId && f.Type == type && formNames.Any(x => x == f.FormName)
        //                                      select f).ToList();
        //        return layouts;
        //    }
        //}


        //public SysDocuments GetMobiFormDocument(int orgId, string formName)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        SysDocuments document = (from f in db.SysDocuments
        //                                 where
        //                                     f.OrganizationId == orgId && f.FormName == formName &&
        //                                     (f.Type == "MobiForm" || f.Type == "SubMobiForm")
        //                                 select f).FirstOrDefault();

        //        return document;
        //    }
        //}

        //public SysDocuments GetSubFormDocument(int orgId, string formName)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        SysDocuments document = (from f in db.SysDocuments
        //                                 where
        //                                     f.OrganizationId == orgId && f.FormName == formName &&
        //                                     (f.Type == "MobiForm" || f.Type == "SubMobiForm")
        //                                 select f).FirstOrDefault();

        //        return document;
        //    }
        //}

        //public SysDocuments GetFormDocument(int orgId, string formName)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        SysDocuments document = (from f in db.SysDocuments
        //                                 where
        //                                     f.OrganizationId == orgId && f.FormName == formName &&
        //                                     (f.Type == "MobiForm" || f.Type == "SubMobiForm")
        //                                 select f).FirstOrDefault();

        //        return document;
        //    }
        //}

        public MobiForm GetMobiForm(int orgId, string formName)
        {
            var db = new MobilizeFormsOrganizationData();
            {
               return MobilizeFormsOrganizationData.GetMobiForm(formName);
            }
        }

        public MobiForm GetSubForm(int orgId, string formName)
        {
            var db = new MobilizeFormsOrganizationData();
            {
                return MobilizeFormsOrganizationData.GetSubForm(formName);
            }
        }

        public MobiForm GetForm(int orgId, string formName)
        {
            var db = new MobilizeFormsOrganizationData();
            {
                return MobilizeFormsOrganizationData.GetForm(formName);
            }
        }

        //public SysDocuments GetLayoutDocument(int orgId, string formName, string type)
        //{
        //    var db = new MobilizeFormsOrganizationData();
        //    {
        //        SysDocuments layouts = (from f in db.SysDocuments
        //                                where f.OrganizationId == orgId && f.Type == type && f.FormName == formName
        //                                select f).FirstOrDefault();
        //        return layouts;
        //    }
        //}

        public bool SaveMobiForm(int orgId, MobiForm form, string loginId)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(form?.FormName) || form.Type != "MobiForm")
            //    {
            //        return false;
            //    }
            //    var db = new MobilizeFormsOrganizationData();
            //    {
            //        form.Modified = DateTime.UtcNow;
            //        SysDocuments existingDocument = (from f in db.SysDocuments
            //                                         where
            //                                             f.OrganizationId == orgId && f.FormName == form.FormName &&
            //                                             f.Type == "MobiForm"
            //                                         select f).FirstOrDefault();

            //        if (existingDocument == null)
            //        {
            //            var document = new SysDocuments
            //            {
            //                FormName = form.FormName,
            //                OrganizationId = orgId,
            //                Type = form.Type,
            //                Data = JsonConvert.SerializeObject(form),
            //                Created = DateTime.UtcNow,
            //                Modified = DateTime.UtcNow,
            //                CreatedBy = loginId,
            //                UpdatedBy = loginId,
            //            };
            //            db.SysDocuments.Add(document);
            //        }
            //        else
            //        {
            //            existingDocument.Data = JsonConvert.SerializeObject(form);
            //            existingDocument.Modified = DateTime.UtcNow;
            //            existingDocument.UpdatedBy = loginId;
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool SaveSubForm(int orgId, MobiForm form, string loginId)
        {
            //try
            //{
            //    if (string.IsNullOrEmpty(form?.FormName) || form.Type != "SubMobiForm")
            //    {
            //        return false;
            //    }

            //    var db = new MobilizeFormsOrganizationData();
            //    {
            //        form.Modified = DateTime.UtcNow;
            //        SysDocuments existingDocument = (from f in db.SysDocuments
            //                                         where
            //                                             f.OrganizationId == orgId && f.FormName == form.FormName &&
            //                                             f.Type == "SubMobiForm"
            //                                         select f).FirstOrDefault();

            //        if (existingDocument == null)
            //        {
            //            var document = new SysDocuments
            //            {
            //                FormName = form.FormName,
            //                OrganizationId = orgId,
            //                Type = form.Type,
            //                Data = JsonConvert.SerializeObject(form),
            //                Created = DateTime.UtcNow,
            //                Modified = DateTime.UtcNow,
            //                CreatedBy = loginId,
            //                UpdatedBy = loginId,
            //            };
            //            db.SysDocuments.Add(document);
            //        }
            //        else
            //        {
            //            existingDocument.Data = JsonConvert.SerializeObject(form);
            //            existingDocument.Modified = DateTime.UtcNow;
            //            existingDocument.UpdatedBy = loginId;
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool SaveLayout(int orgId, string formName, string layout, string type, string loginId)
        {
            //try
            //{
            //    var db = new MobilizeFormsOrganizationData();
            //    {

            //        SysDocuments existingDocument = (from f in db.SysDocuments
            //                                         where f.OrganizationId == orgId && f.Type == type && f.FormName == formName
            //                                         select f).FirstOrDefault();
            //        if (existingDocument == null)
            //        {
            //            var document = new SysDocuments()
            //            {
            //                FormName = formName,
            //                OrganizationId = orgId,
            //                Type = type,
            //                Data = layout,
            //                Created = DateTime.UtcNow,
            //                Modified = DateTime.UtcNow,
            //                CreatedBy = loginId,
            //                UpdatedBy = loginId,
            //            };
            //            db.SysDocuments.Add(document);
            //        }
            //        else
            //        {
            //            existingDocument.Data = layout;
            //            existingDocument.Modified = DateTime.UtcNow;
            //            existingDocument.UpdatedBy = loginId;
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool DeleteFormDocuments(int orgId, string formName)
        {
            //try
            //{
            //    var db = new MobilizeFormsOrganizationData();
            //    {
            //        List<SysDocuments> entries = (from d in db.SysDocuments
            //                                      where d.FormName == formName
            //                                      select d)?.ToList();

            //        if (entries.Count == 0)
            //        {
            //            return false;
            //        }

            //        foreach (var document in entries)
            //        {
            //            db.SysDocuments.Remove(document);
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool DeleteMobiForm(int orgId, string formName)
        {
            //try
            //{
            //    var db = new MobilizeFormsOrganizationData();
            //    {
            //        List<SysDocuments> entries = (from d in db.SysDocuments
            //                                      where d.FormName == formName && d.Type == "MobiForm"
            //                                      select d)?.ToList();

            //        if (entries.Count == 0)
            //        {
            //            return false;
            //        }

            //        foreach (var document in entries)
            //        {
            //            db.SysDocuments.Remove(document);
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool DeleteSubForm(int orgId, string formName)
        {
            //try
            //{
            //    var db = new MobilizeFormsOrganizationData();
            //    {
            //        List<SysDocuments> entries = (from d in db.SysDocuments
            //                                      where d.FormName == formName && d.Type == "SubMobiForm"
            //                                      select d)?.ToList();

            //        if (entries.Count == 0)
            //        {
            //            return false;
            //        }

            //        foreach (var document in entries)
            //        {
            //            db.SysDocuments.Remove(document);
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        public bool DeleteAllLayouts(int orgId, string formName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLayout(int orgId, string formName, string type)
        {
            throw new NotImplementedException();
        }
    }
}