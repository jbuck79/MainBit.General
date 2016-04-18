using System.Collections.Generic;
using Orchard.DynamicForms.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Tokens;
using DescribeContext = Orchard.Forms.Services.DescribeContext;
using Orchard.Environment.Extensions;

namespace MainBit.General.DynamicForms.Drivers
{
    [OrchardFeature("MainBit.General.DynamicForms")]
    public class HiddenFieldElementDriver : FormsElementDriver<HiddenField>
    {
        private readonly ITokenizer _tokenizer;
        public HiddenFieldElementDriver(IFormsBasedElementServices formsServices, ITokenizer tokenizer) : base(formsServices) {
            _tokenizer = tokenizer;
        }

        protected override void OnDisplaying(HiddenField element, ElementDisplayingContext context) {
            context.ElementShape.ProcessedValue = _tokenizer.Replace(element.RuntimeValue, context.GetTokenData());
        }
    }
}