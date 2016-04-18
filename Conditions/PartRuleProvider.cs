using System;
using System.Linq;
using Orchard.Environment.Extensions;
using MainBit.Utility.Services;
using Orchard.Conditions.Services;

namespace MainBit.General.Rules
{
    [OrchardFeature("MainBit.General.Conditions")]
    public class PartCondition : IConditionProvider
    {
        private readonly ICurrentContentAccessor _currentContentAccessor;

        public PartCondition(
            ICurrentContentAccessor currentContentAccessor)
        {
            _currentContentAccessor = currentContentAccessor;
        }

        public void Evaluate(ConditionEvaluationContext evaluationContext)
        {
            if (!String.Equals(evaluationContext.FunctionName, "part", StringComparison.OrdinalIgnoreCase))
                return;

            var contentItem = _currentContentAccessor.CurrentContentItem;
            if (contentItem == null)
            {
                evaluationContext.Result = true;
                return;
            }

            var part = Convert.ToString(evaluationContext.Arguments[0]);
            evaluationContext.Result = contentItem.Parts.Any(p => p.PartDefinition.Name == part);
        }
    }
}