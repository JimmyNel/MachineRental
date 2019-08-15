using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MachineRental.Common
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString ActionImage(this HtmlHelper htmlHelper,
            string action,
            string controller,
            object routeValues,
            string imagePath,
            string alternateText = "",
            object htmlAttributes = null)
        {

            var anchorBuilder = new TagBuilder("a");
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));

            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alternateText);

            var attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            imgBuilder.MergeAttributes(attributes);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            anchorBuilder.InnerHtml = imgHtml;
            return MvcHtmlString.Create(anchorBuilder.ToString());
        }


        public static MvcHtmlString ShortNameFor<TModel, TValue>(this HtmlHelper<TModel> self,
                                                    Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var name = metadata.ShortDisplayName ?? metadata.DisplayName ?? metadata.PropertyName;

            return MvcHtmlString.Create(string.Format(@"<span>{0}</span>", name));
        }
    }
}