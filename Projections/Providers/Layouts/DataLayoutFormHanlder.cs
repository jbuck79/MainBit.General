using Orchard.DisplayManagement;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Forms.Services;
using Orchard.Localization;
using Orchard.Tokens;
using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MainBit.General.Projections.Providers.Layouts
{
    [OrchardFeature("MainBit.General.Projections")]
    public class DataLayoutFormHanlder : FormHandler
    {
        public static readonly string CustomDataName = "CustomData";

        protected dynamic Shape { get; set; }
        private readonly Work<IResourceManager> _resourceManager;
        private readonly ITokenizer _tokenizer;
        public Localizer T { get; set; }

        public DataLayoutFormHanlder(
            IShapeFactory shapeFactory,
            Work<IResourceManager> resourceManager,
            ITokenizer tokenizer)
        {
            Shape = shapeFactory;
            _resourceManager = resourceManager;
            _tokenizer = tokenizer;
            T = NullLocalizer.Instance;
        }

        public override void Built(BuildingContext context)
        {
            // need to determine that the current form is suitable for client side
            // it can be defined by form name
            // but form name is not accessible from here and i can get form name only for my form
            string formId = context.Shape.Id;
            var avaliableFromIds = new string[] { "GridLayout", "ListLayout", "RawLayout", "ShapeLayout" };
            if (!avaliableFromIds.Contains(formId)) { return; }

            context.Shape._Data(Shape.FieldSet(
                Id: "custom-data-container",
                Title: T("Custom data"),
                _ClientSideSwitcher: Shape.TextArea(
                    Id: "data", Name: DataLayoutFormHanlder.CustomDataName,
                    Title: T("Data"),
                    Description: T("Enter custom data. You can access that property in view as \"var customData = Json.Decode((string)Model.Context.State.CustomData)\". Use single { for tokent or double {{ for json string.")
                    )
                )
            );

            // shoud include some plugin to edit json in convient way
            //_resourceManager.Value.Require("script", "jQuery");
            //_resourceManager.Value.Include("script",
            //    "~/Modules/MainBit.Projections.ClientSide/Scripts/mainbit-projection-clientside-editor.js",
            //    "~/Modules/MainBit.Projections.ClientSide/Scripts/mainbit-projection-clientside-editor.js");
        }

        public override void Validating(ValidatingContext context)
        {
            var customData = context.ValueProvider.GetValue(DataLayoutFormHanlder.CustomDataName);
            if(customData == null || string.IsNullOrEmpty(customData.AttemptedValue))
            {
                return;
            }

            try
            {
                //var customDataTokinized = _tokenizer.Replace(customData.AttemptedValue, new Dictionary<string, object>() { });
                //var customData = FormParametersHelper.ToDynamic(tokenizedState)
                //var jsonCustomData = Json.Decode(customData.AttemptedValue);

                // regular json - would be treated as a tokens
                // json with double {{ or }} whould be treated as regular json
                // here i need to check that
            }
            catch
            {
                context.ModelState.AddModelError(DataLayoutFormHanlder.CustomDataName, T("The field {0} must be empty or valid json string.", T("Custom data").Text).Text);
            }
        }
    }
}