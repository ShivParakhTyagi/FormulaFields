using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class SysDocuments
    {
        public SysDocuments()
        {
        }
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string FormName { get; set; }
        public string Data { get; set; }
        public bool ReadOnly { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("DocumentOrganization")]
        public int? OrganizationId { get; set; }
        public virtual Organization DocumentOrganization { get; set; }
    }
}
