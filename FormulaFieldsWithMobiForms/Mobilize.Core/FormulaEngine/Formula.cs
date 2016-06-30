using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NCalc;

namespace Mobilize.Core.FormulaEngine
{
    public class Formula : IFormula
    {
        private Expression _expression;
        public string Expression { get; set; }

        public string ConvertedExpression { get; set; }
        public Formula(string formula)
        {
            this.Expression = formula;
            this.ParseFormula();
        }

        private void ParseFormula()
        {
            var regex = new Regex(@"\[(.*?)\]");

            var matches = regex.Matches(this.Expression);

            this.Tokens = new List<string>();
            this.Fields = new List<string>();
            foreach (var match in matches)
            {
                var temp = match.ToString();
                this.Tokens.Add(temp);

                temp = temp.Remove(0, 1);
                temp = temp.Remove(temp.Length - 1, 1);
                this.Fields.Add(temp);
            }

            this.ConvertedExpression = this.Expression;




            this.TokenMap = new Dictionary<string, string>();
            for (int i = 0; i < this.Tokens.Count; i++)
            {
                this.ConvertedExpression = this.ConvertedExpression.Replace(this.Tokens[i], $" var{i} ");
                this.TokenMap.Add(this.Tokens[i], $"var{i}");
            }
            //Code to Check Validity of Expression
            _expression = new Expression(this.ConvertedExpression);
            if (this.ConvertedExpression.Contains("[") || this.ConvertedExpression.Contains("]"))
            {
                this.IsValid = false;
            }
            else
            {
                if (_expression.HasErrors())
                {
                    this.IsValid = false;
                }
                else
                {
                    this.IsValid = true;
                }
            }
        }

        public virtual bool IsValid { get; set; }
        public virtual string Error { get; set; }
        public virtual List<string> Tokens { get; set; }
        public virtual Dictionary<string,string> TokenMap { get; set; }
        public virtual List<string> Fields { get; set; }

       
      
        public virtual void AcceptValues(Dictionary<string, double> valuesDic)
        {
            if (valuesDic == null || valuesDic.Count != this.Fields.Count)
            {
                throw new ArgumentException("List of values provided are not sufficient to evaluate the result of expression");
            }

            foreach (var field in this.Fields)
            {
                if (valuesDic.ContainsKey(field) == false)
                {
                    throw new ArgumentException("The provided field value is not in the expression");
                }
            }

            this.Values = valuesDic;
        }

        public virtual Dictionary<string, double> Values { get; set; }


        public object Evaluate()
        {
            if (this.IsValid==false)
            {
                   throw new InvalidOperationException("Expression is not valid");
            }
            if (this._expression == null)
            {
                throw new InvalidOperationException("There is an internal error in object");
            }

            foreach (var value in Values)
            {
                this._expression.Parameters[this.TokenMap[$"[{value.Key}]"]] = value.Value;
            }
            return this._expression.Evaluate();
        }
    }
}
