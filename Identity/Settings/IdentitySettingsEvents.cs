using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Localization;
using Orchard.UI.Notify;
using Orchard.Environment.Extensions;

namespace MainBit.General.Identity.Settings
{
    [OrchardFeature("MainBit.General.Identity")]
    public class TechnicalNameSettingsHooks : ContentDefinitionEditorEventsBase
    {
        private readonly INotifier _notifier;

        public TechnicalNameSettingsHooks(INotifier notifier)
        {
            _notifier = notifier;
        }

        public Localizer T { get; set; }

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "IdentityPart")
                yield break;

            var settings = definition.Settings.GetModel<IdentitySettings>();

            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "IdentityPart")
                yield break;

            var settings = new IdentitySettings();

            if (updateModel.TryUpdateModel(settings, "IdentitySettings", null, null))
            {
                settings.Build(builder);
            }

            yield return DefinitionTemplate(settings);
        }
    }
}
