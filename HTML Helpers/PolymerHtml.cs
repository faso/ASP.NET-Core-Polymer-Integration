using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Web.Mvc.Polymer
{
    public static class PolymerHtml
    {
        private const string ironFormName = "iron-form";

        public static IHtmlContent EndForm()
        {
            var builder = new TagBuilder("form");
            builder.TagRenderMode = TagRenderMode.EndTag;

            return builder;
        }

        public static IHtmlContent BeginForm(string action = null, string method = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("form");
            builder.TagRenderMode = TagRenderMode.StartTag;

            builder.MergeAttribute("is", ironFormName);

            if (action != null)
                builder.MergeAttribute("action", action);

            if (method != null)
                builder.MergeAttribute("method", method);

            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return builder;
        }
    }
}
