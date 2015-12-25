using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.UI.PageClass;
using System;
using System.Linq;

namespace MainBit.General.Shapes
{
    public class LayoutShape : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;

        public LayoutShape(
            IWorkContextAccessor wca
            )
        {
            _wca = wca;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Layout")
                .Configure(descriptor => descriptor.Wrappers.Insert(0, "DocumentCodeBefore"))
                .OnDisplaying(displaying =>
                {
                    var workContext = _wca.GetContext();
                    var httpContext = workContext.HttpContext;
                    var pageClassBuilder = workContext.Resolve<IPageClassBuilder>();

                    // add layout name alternate
                    if (!string.IsNullOrWhiteSpace(httpContext.Request["layout-name"]))
                    {
                        var layoutName = EncodeAlternateElement(httpContext.Request["layout-name"]);
                        displaying.ShapeMetadata.Alternates.Add(string.Format("Layout__Name__{0}", layoutName));
                        //it should be added from .cshtml because i don't know that alternate will be rendered
                        //pageClassBuilder.AddClassNames(string.Format("layout-{0}", layoutName));
                    }

                    // add not found alternate
                    if (httpContext.Response.StatusCode == 404)
                    {
                        displaying.ShapeMetadata.Alternates.Add("Layout__NotFound");
                        //it should be added from .cshtml because i don't know that alternate will be rendered
                        //pageClassBuilder.AddClassNames(string.Format("layout-", "not-found"));
                    }

                    // add class name from query string
                    if (!string.IsNullOrWhiteSpace(httpContext.Request["class-name"]))
                    {
                        pageClassBuilder.AddClassNames(httpContext.Request["class-name"]);
                    }

                    // add homepage class
                    var isHomepage = string.Equals(
                        httpContext.Request.Url.AbsolutePath.TrimEnd('/'),
                        httpContext.Request.ApplicationPath.TrimEnd('/'),
                        StringComparison.InvariantCultureIgnoreCase);
                    pageClassBuilder.AddClassNames(isHomepage ? "homepage" : "not-homepage");

                    // add routing alternates
                    var routeValues = httpContext.Request.RequestContext.RouteData.Values;
                    displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area"));
                    displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area", "controller"));
                    displaying.ShapeMetadata.Alternates.Add(BuildShapeName(routeValues, "area", "controller", "action"));
                });
        }

        /// <summary>
        /// Encodes dashed and dots so that they don't conflict in filenames 
        /// </summary>
        /// <param name="alternateElement"></param>
        /// <returns></returns>
        private string EncodeAlternateElement(string alternateElement)
        {
            return alternateElement.Replace("-", "__").Replace(".", "_");
        }

        // add routing alternates
        private static string BuildShapeName(System.Web.Routing.RouteValueDictionary values, params string[] names)
        {
            return "Layout__" +
                string.Join("__",
                    names.Select(s =>
                        ((string)values[s] ?? "").Replace(".", "_")));
        }
    }
}