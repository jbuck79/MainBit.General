using System.Collections.Generic;
using System.Linq;
using Orchard.Commands;
using Orchard.Localization.Services;
using Orchard;

namespace MainBit.General.Commands {
    public class CultureCommands : DefaultOrchardCommandHandler {
        private readonly ICultureManager _cultureManager;
        private readonly IOrchardServices _orchardServices;

        public CultureCommands(ICultureManager cultureManager, IOrchardServices orchardServices) {
            _cultureManager = cultureManager;
            _orchardServices = orchardServices;
        }

        [CommandHelp("cultures remove <culture-name-1> ... <culture-name-n>\r\n\t" + "Remove one or more cultures from the site")]
        [CommandName("cultures remove")]
        public void RemoveCultures(params string[] cultureNames) {

            Context.Output.WriteLine(T("Removing site cultures {0}", string.Join(",", cultureNames)));

            IEnumerable<string> siteCultures = _cultureManager.ListCultures();

            if(siteCultures.All(cultureName => cultureNames.Contains(cultureName))) {
                Context.Output.WriteLine(T("You try to remove all site cultures."));
                return;
            }

            foreach (var cultureName in cultureNames)
            {
                if(_cultureManager.IsValidCulture(cultureName))
                {
                    if (siteCultures.Contains(cultureName))
                    {
                        _cultureManager.DeleteCulture(cultureName);
                    }
                    else {
                        Context.Output.WriteLine(T("Supplied culture name {0} doesn't exist on the site.", cultureName));
                    }
                }
                else {
                    Context.Output.WriteLine(T("Supplied culture name {0} is not valid.", cultureName));
                }
            }
        }
    }
}

