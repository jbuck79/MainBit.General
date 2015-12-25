using System;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard;
using Orchard.Settings;

namespace MainBit.General.Tokens.Providers
{
    public class UriTokens : ITokenProvider
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        public UriTokens(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context)
        {
            context.For("Uri")
                .Token("Scheme", T("Scheme"), T("Gets the scheme name for URI."))
                .Token("Scheme", T("Scheme"), T("Gets the host component of URI."))
            ;
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<ISite>("Site")
                   .Chain("BaseUrl", "Uri", content => content.BaseUrl);

            context.For<Uri>("Uri")
                .Token("Scheme", content => content.Scheme)
                .Token("Host", content => content.Host)
            ;
        }
    }
}