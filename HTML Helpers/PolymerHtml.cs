using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Mvc.Polymer
{
    public class PolymerTagBuilder : TagBuilder
    {
        public PolymerTagBuilder(string tagName)
            : base(tagName)
        { }

        public PolymerTagBuilder(string tagName, object htmlAttributes)
            : base(tagName)
        {
            if (htmlAttributes != null)
                this.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        }
    }

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
            var builder = new PolymerTagBuilder("form");
            builder.TagRenderMode = TagRenderMode.StartTag;

            builder.MergeAttribute("is", ironFormName);

            if (action != null)
                builder.MergeAttribute("action", action);

            if (method != null)
                builder.MergeAttribute("method", method);

            return builder;
        }

        public static IHtmlContent CheckBox(string label = null, bool check = false, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-checkbox");
            builder.TagRenderMode = TagRenderMode.Normal;

            if (label != null)
                builder.InnerHtml.AppendHtml(label);

            if (check)
                builder.MergeAttribute("checked", "");

            return builder;
        }

        public static IHtmlContent Input(string label = null, string prefix = null, string suffix = null, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-checkbox");
            builder.TagRenderMode = TagRenderMode.Normal;

            if (prefix != null)
            {
                var pre = new TagBuilder("div");
                pre.MergeAttribute("prefix", "");
                pre.InnerHtml.AppendHtml(prefix);
                builder.InnerHtml.Append(pre);
            }

            if (suffix != null)
            {
                var suf = new TagBuilder("div");
                suf.MergeAttribute("suffix", "");
                suf.InnerHtml.AppendHtml(suffix);
                builder.InnerHtml.Append(suf);
            }

            if (label != null)
                builder.InnerHtml.AppendHtml(label);

            return builder;
        }

        public static IHtmlContent Password(string label = null, object htmlAttributes = null)
        {
            if (htmlAttributes == null)
                htmlAttributes = new { type = "password" };
            else
            {
                ((Dictionary<object, string>)htmlAttributes).Add("type", "password");
            }
            return PolymerHtml.Input(label, null, null, htmlAttributes);
        }
    }
}
