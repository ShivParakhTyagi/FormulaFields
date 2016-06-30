using System.Collections.Generic;

namespace Mobilize.Core.FormulaEngine
{
    public interface IFormula
    {
        bool IsValid { get; set; }

        string Error { get; set; }

        List<string> Tokens { get; set; }


    }
}