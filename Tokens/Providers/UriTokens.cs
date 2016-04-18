using System;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard;
using Orchard.Settings;
using Orchard.Environment.Extensions;

namespace MainBit.General.Tokens.Providers
{
    [OrchardFeature("MainBit.General.Tokens")]
    public class UriTokens : ITokenProvider
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;

        public UriTokens(
            IWorkContextAccessor workContextAccessor,
            IContentManager contentManager,
            IOrchardServices orchardServices)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(DescribeContext context)
        {
            context.For("Site")
                .Token("BaseUri", T("BaseUri"), T("The base uri of the site."))
                ;
            context.For("Uri", T("Uri"), T("Uri"))
                .Token("Scheme", T("Scheme"), T("The scheme of the uri."))
                .Token("Host", T("Scheme"), T("The host of the uri."))
            ;
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For("Site", () => _orchardServices.WorkContext.CurrentSite)
                   .Token("BaseUri", content => new Uri(content.BaseUrl))
                   .Chain("BaseUri", "Uri", content => new Uri(content.BaseUrl));

            context.For<Uri>("Uri")
                .Token("Scheme", content => content.Scheme)
                .Token("Host", content => content.Host)
            ;
        }
    }
}