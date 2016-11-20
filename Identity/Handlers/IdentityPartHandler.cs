using System;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using MainBit.General.Identity.Settings;
using Orchard.Environment.Extensions;

namespace MainBit.General.Identity.Handlers
{
    [OrchardFeature("MainBit.General.Identity")]
    public class IdentityPartHandler : ContentHandler {
        public IdentityPartHandler(
            IContentManager contentManager)
        {
            OnUpdating<IdentityPart>((ctx, part) => AssignIdentity(part));
        }

        protected void AssignIdentity(IdentityPart part) {
            if(!string.IsNullOrEmpty(part.Identifier))
            {
                return;
            }

            var settings = part.TypePartDefinition.Settings.GetModel<IdentitySettings>();
            if(!settings.GenerateOnEdit)
            {
                return;
            }

            part.Identifier = Guid.NewGuid().ToString("n");
        }
    }
}