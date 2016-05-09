using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Routing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Mvc.Polymer
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> AddProperty(this object obj, string name, object value)
        {
            var dictionary = obj.ToDictionary();
            dictionary.Add(name, value);
            return dictionary;
        }

        // helper
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                result.Add(property.Name, property.GetValue(obj));
            }
            return result;
        }
    }

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
            var builder = new PolymerTagBuilder("form", htmlAttributes);
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
            var builder = new PolymerTagBuilder("paper-checkbox", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (label != null)
                builder.InnerHtml.AppendHtml(label);

            if (check)
                builder.MergeAttribute("checked", "");

            return builder;
        }

        public static IHtmlContent Input(string label = null, string prefix = null, string suffix = null, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-input", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (htmlAttributes != null)
                builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

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
                htmlAttributes = htmlAttributes.AddProperty("type", "password");
            }
            return PolymerHtml.Input(label, null, null, htmlAttributes);
        }
    }
}
