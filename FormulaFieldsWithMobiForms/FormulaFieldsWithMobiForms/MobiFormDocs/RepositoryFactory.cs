using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaFieldsWithMobiForms.MobiFormDocs
{
    public static class RepositoryFactory
    {
        public static ISysDocumentsRepository CreateSysDocumentsRepository()
        {
            return new SysDocumentsRepository();
        }
    }
}
