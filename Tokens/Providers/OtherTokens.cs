using System;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard;
using Orchard.Environment.Extensions;

namespace MainBit.General.Tokens.Providers
{
    [OrchardFeature("MainBit.General.Tokens")]
    public class OtherTokens : ITokenProvider {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        public OtherTokens(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context) {
            context.For("HttpContext", T("Http Context"), T("Http Context"))
                .Token("Counting:*", T("Counting:<name>"), T("Incremented value of specific name that starts from 1"))
            ;
        }

        public void Evaluate(EvaluateContext context) {
            if (_workContextAccessor.GetContext().HttpContext == null)
            {
                return;
            }

            context.For("HttpContext", _workContextAccessor.GetContext().HttpContext)
                .Token(
                    token => token.StartsWith("Counting:", StringComparison.OrdinalIgnoreCase) ? token.Substring("Counting:".Length) : null,
                    (tokenValue, httpContext) =>
                    {
                        var countingKey = string.Format("Tokens.Counting.{0}", tokenValue);
                        var countingValue = (int)(httpContext.Items[countingKey] ?? 0);
                        httpContext.Items[countingKey] = ++countingValue;
                        return countingValue;
                    });
        }
    }
}