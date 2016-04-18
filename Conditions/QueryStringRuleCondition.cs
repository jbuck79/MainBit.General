using Orchard.Environment.Extensions;
using Orchard.Events;
using Orchard.Mvc;
using Orchard.Widgets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.General.Conditions
{
    [OrchardFeature("MainBit.General.Conditions")]
    public class QueryStringRuleProvider : IConditionProvider
    {
        private readonly IHttpContextAccessor _hta;

        public QueryStringRuleProvider(IHttpContextAccessor hta)
        {
            _hta = hta;
        }

        public void Evaluate(dynamic evaluationContext)
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