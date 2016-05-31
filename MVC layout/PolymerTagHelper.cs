using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6
{
    public class HeadTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string dep = "<script src=\"~/lib/webcomponentsjs/webcomponents.min.js\"></script><link rel=\"import\" href=\"~/lib/iron-flex-layout/iron-flex-layout.html\"><link rel=\"import\" href=\"~/lib/iron-icons/iron-icons.html\"><link rel=\"import\" href=\"~/lib/font-roboto/roboto.html\"><link rel=\"import\" href=\"~/lib/iron-pages/iron-pages.html\"><link rel=\"import\" href=\"~/lib/iron-menu-behavior/iron-menu-behavior.html\"><link rel=\"import\" href=\"~/lib/iron-selector/iron-selector.html\">";
            output.Content.AppendHtml(dep);
        }
    }
}
