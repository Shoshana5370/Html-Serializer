using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance =>_instance;

        public  string[] TagsHtml { get; set; }
        public string[] TagsHtmlUnclosing { get; set; }
        private HtmlHelper()
        {
            TagsHtml = JsonSerializer.Deserialize<string[]>(File.ReadAllText("JSON Files/HtmlTags.json"));
            TagsHtmlUnclosing = JsonSerializer.Deserialize<string[]>(File.ReadAllText("JSON Files/HtmlVoidTags.json"));
        }
    }
}
