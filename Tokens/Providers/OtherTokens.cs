using System;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard;

namespace MainBit.General.Tokens.Providers
{
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
            context.For("Request")
                .Token("UrlReferrer", T("Url referrer"), T("The url referrer"))
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