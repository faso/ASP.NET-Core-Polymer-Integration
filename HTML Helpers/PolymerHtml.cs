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

        public static IHtmlContent Button(string label = null, bool raised = false, bool noink = false, bool toggles = false, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-button", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (label != null)
                builder.InnerHtml.AppendHtml(label);

            if (raised)
                builder.MergeAttribute("raised", "");

            if (noink)
                builder.MergeAttribute("noink", "");

            if (toggles)
                builder.MergeAttribute("toggles", "");

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

        public static IHtmlContent ToggleButton(bool active = false, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-toggle-button", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (active)
                builder.MergeAttribute("checked", "true");

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

        public static IHtmlContent RadioButton(string label = null, bool active = false, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-radio-button", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (label != null)
                builder.InnerHtml.AppendHtml(label);

            if (active)
                builder.MergeAttribute("active", "");

            return builder;
        }

        public static IHtmlContent DropdownMenu(List<string> options, string label = null, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-dropdown-menu", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (label != null)
                builder.MergeAttribute("label", label);

            var listbox = new PolymerTagBuilder("paper-listbox");
            builder.TagRenderMode = TagRenderMode.Normal;
            listbox.AddCssClass("dropdown-content");

            foreach(var option in options)
            {
                var current = new PolymerTagBuilder("paper-item");
                current.TagRenderMode = TagRenderMode.Normal;
                current.InnerHtml.AppendHtml(option);
                listbox.InnerHtml.Append(current);
            }

            builder.InnerHtml.Append(listbox);

            return builder;
        }

        public static IHtmlContent Menu(List<string> options, int selected = -1, bool multi = false, object htmlAttributes = null)
        {
            var builder = new PolymerTagBuilder("paper-menu", htmlAttributes);
            builder.TagRenderMode = TagRenderMode.Normal;

            if (selected > 0)
                builder.MergeAttribute("selected", selected.ToString());

            if (multi)
                builder.MergeAttribute("multi", "");

            foreach (var option in options)
            {
                var current = new PolymerTagBuilder("paper-item");
                current.TagRenderMode = TagRenderMode.Normal;
                current.InnerHtml.AppendHtml(option);
                builder.InnerHtml.Append(current);
            }

            return builder;
        }
    }
}
