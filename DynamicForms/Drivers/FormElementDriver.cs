using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.DynamicForms.Elements;
using Orchard.DynamicForms.Helpers;
using Orchard.DynamicForms.Services;
using Orchard.Forms.Services;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Tokens;
using DescribeContext = Orchard.Forms.Services.DescribeContext;
using Orchard.UI.Resources;
using Orchard.Environment;

namespace MainBit.General.DynamicForms.Drivers
{
    public class FormElementDriver : ElementDriver<Form>
    {
        private readonly Work<IResourceManager> _resourceManager;

        public FormElementDriver(
            Work<IResourceManager> resourceManager)
        {
            _resourceManager = resourceManager;
        }

        protected override void OnDisplaying(Form element, ElementDisplayContext context)
        {
            if (context.DisplayType == "Design") {
                return;
            }

            _resourceManager.Value.Require("script", "jQuery-Validate");
            _resourceManager.Value.Require("script", "jQuery-Validate-Localization");

            if (element.Action != null && element.Action.EndsWith("/MainBit.General/Form/Submit", StringComparison.OrdinalIgnoreCase))
            {
                _resourceManager.Value.Require("script", "MainBit.General.DynamicForms");
            }
        }
    }
}