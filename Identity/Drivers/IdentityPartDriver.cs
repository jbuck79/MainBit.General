using MainBit.General.Identity.Settings;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace MainBit.General.Identity.Drivers
{
    [OrchardFeature("MainBit.General.Identity")]
    public class IdentityPartDriver : ContentPartDriver<IdentityPart> {
        public IdentityPartDriver() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix {
            get { return "Identity"; }
        }

        protected override DriverResult Editor(IdentityPart part, dynamic shapeHelper)
        {
            if(string.IsNullOrEmpty(part.Identifier))
            {
                return null;
            }

            var settings = part.TypePartDefinition.Settings.GetModel<IdentitySettings>();
            if(!settings.DisplayOnEdit)
            {
                return null;
            }

            return ContentShape("Parts_Identity_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts.Identity.Edit", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(IdentityPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }
    }
}