using Orchard.Conditions.Services;
using Orchard.Environment.Extensions;
using Orchard.Mvc;
using System;

namespace MainBit.General.Rules
{
    [OrchardFeature("MainBit.General.Conditions")]
    public class QueryStringCondition : IConditionProvider
    {
        private readonly IHttpContextAccessor _hta;

        public QueryStringCondition(IHttpContextAccessor hta)
        {
            _hta = hta;
        }

        public void Evaluate(ConditionEvaluationContext evaluationContext)
        {
            if (!String.Equals(evaluationContext.FunctionName, "querystring", StringComparison.OrdinalIgnoreCase))
                return;
            
            var key = Convert.ToString(evaluationContext.Arguments[0]);
            var value = evaluationContext.Arguments.Length > 1 ? Convert.ToString(evaluationContext.Arguments[1]) : null;

            var qsValue = _hta.Current().Request.QueryString[key];
            evaluationContext.Result = qsValue == null
                ? false
                : value == null
                    ? true
                    : qsValue.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}