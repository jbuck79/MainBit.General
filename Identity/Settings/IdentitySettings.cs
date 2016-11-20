using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Environment.Extensions;
using System.Globalization;

namespace MainBit.General.Identity.Settings
{
    [OrchardFeature("MainBit.General.Identity")]
    public class IdentitySettings
    {
        public bool DisplayOnEdit { get; set; }
        public bool GenerateOnEdit { get; set; }

        public void Build(ContentTypePartDefinitionBuilder builder)
        {
            builder.WithSetting("IdentitySettings.DisplayOnEdit", DisplayOnEdit.ToString(CultureInfo.InvariantCulture));
            builder.WithSetting("IdentitySettings.GenerateOnEdit", GenerateOnEdit.ToString(CultureInfo.InvariantCulture));
        }
    }
}