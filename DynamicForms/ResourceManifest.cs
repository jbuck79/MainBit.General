using Orchard;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace MainBit.General.DynamicForms {
    [OrchardFeature("MainBit.General.DynamicForms")]
    public class ResourceManifest : IResourceManifestProvider {

        private readonly IWorkContextAccessor _wca;

        public ResourceManifest(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("MainBit.General.DynamicForms")
                .SetUrl(
                    "~/Modules/MainBit.General/DynamicForms/Scripts/mainbit-dynamic-form.min.js",
                    "~/Modules/MainBit.General/DynamicForms/Scripts/mainbit-dynamic-form.js")
                .SetDependencies("jQuery");

            manifest.DefineScript("jQuery-Validate")
                .SetUrl(
                    "~/Modules/Orchard.DynamicForms/Scripts/Lib.min.js",
                    "~/Modules/Orchard.DynamicForms/Scripts/Lib.js")
                .SetDependencies("jQuery");

            // http://stackoverflow.com/questions/28694131/orchard-localization-resourcemanifest-setcultures-override-path-pattern
            var currentCulture = _wca.GetContext().CurrentCulture;
            var validateLocalizationUrl = "~/Modules/MainBit.General/DynamicForms/Scripts/validation/messages_" + currentCulture.Split('-')[0] + ".js";
            manifest.DefineScript("jQuery-Validate-Localization").SetUrl(validateLocalizationUrl).SetDependencies("jQuery-Validate");

            //
        }
    }
}