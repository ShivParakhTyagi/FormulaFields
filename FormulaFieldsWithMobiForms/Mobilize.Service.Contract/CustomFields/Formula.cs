using System.Collections.Generic;
using Mobilize.Contract.MobilizeDataTypes;

namespace Mobilize.Contract.CustomFields
{
    public class Formula : FieldsInfo
    {
        public Formula()
        {
            MobilizeType = "Formula";
        }
        /// <summary>
        /// Actual representation of a formula to be computed by Computation engine
        /// </summary>
        public string FormulaExpression { get; set; }

        public string FormulaType { get; set; } = "string";
    }
}