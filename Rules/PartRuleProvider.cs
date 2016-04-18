using System;
using System.Linq;
using Orchard.Widgets.Services;
using Orchard.Environment.Extensions;
using MainBit.Utility.Services;

namespace MainBit.General.Rules
{
    [OrchardFeature("MainBit.General.Rules")]
    public class PartRuleProvider : IRuleProvider {
        private readonly ICurrentContentAccessor _currentContentAccessor;

        public PartRuleProvider(
            ICurrentContentAccessor currentContentAccessor)
        {
            _currentContentAccessor = currentContentAccessor;
        }

        public void Process(RuleContext ruleContext) {

            if (!String.Equals(ruleContext.FunctionName, "part", StringComparison.OrdinalIgnoreCase))
                return;

            var contentItem = _currentContentAccessor.CurrentContentItem;
            if (contentItem == null)
            {
                ruleContext.Result = true;
                return;
            }

            var part = Convert.ToString(ruleContext.Arguments[0]);
            ruleContext.Result = contentItem.Parts.Any(p => p.PartDefinition.Name == part);
        }
    }
}