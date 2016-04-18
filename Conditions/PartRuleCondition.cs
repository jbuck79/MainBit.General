using System;
using System.Linq;
using Orchard.Widgets.Services;
using Orchard.Environment.Extensions;
using MainBit.Utility.Services;
using Orchard.Events;

namespace MainBit.General.Conditions
{
    public interface IConditionProvider : IEventHandler
    {
        void Evaluate(dynamic evaluationContext);
    }

    [OrchardFeature("MainBit.General.Conditions")]
    public class PartRuleCondition : IConditionProvider {
        private readonly ICurrentContentAccessor _currentContentAccessor;

        public PartRuleCondition(
            ICurrentContentAccessor currentContentAccessor)
        {
            _currentContentAccessor = currentContentAccessor;
        }

        public void Evaluate(dynamic evaluationContext) {

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