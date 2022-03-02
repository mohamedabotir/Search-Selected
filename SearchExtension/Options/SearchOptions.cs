using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SearchExtension.Options
{
    [Guid("d722eaae-d23e-4774-a07a-f017b229e582")]
    public   class SearchOptions:DialogPage
    {
        private  string defaultSearch="google.com/search?q={0}";

        private static Dictionary<SearchEngines, string> allEngines = new Dictionary<SearchEngines,string>()
        {
            { SearchEngines.Google,"google.com/search?q={0}"},
            { SearchEngines.Bing,"bing.com/search?q={0}"},
            { SearchEngines.StackOverFlow,"stackoverflow.com/search?q=\"{0}\""}
            
        };


        [DisplayName("Use Visual Studio Browser")]
        [DefaultValue(true)]
        [Category("General")]
        [Description("A value indicating whether search should be displayed in Visual Studio browser or external browser")]
        public bool UseVSBrowser { get; set; }

        [DisplayName("Search Engine")]
        [DefaultValue("Google")]
        [Category("General")]
        [Description("The Search Engine to be used for searching")]
        [TypeConverter(typeof(EnumConverter))]
        public SearchEngines SearchEngine { get; set; } =
            SearchEngines.Google; 
        [DisplayName("Browser Path")]
        [DefaultValue("Edge")]
        [Category("General")]
        [Description("Put Browser you Prefere")]
        public string BrowserPath { get; set; } =
           @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";




        [DisplayName("Url")]
        [Category("General")]
        [Description("The Search Engine url to be used for searching")]
        [Browsable(false)]
        public string Url
        {
            get
            {
                var selectedEngineUrl = allEngines.
                    FirstOrDefault(j => j.Key == SearchEngine).Value;
                return string.IsNullOrWhiteSpace(selectedEngineUrl) ? defaultSearch : selectedEngineUrl;
            }
        }






    }
    public enum SearchEngines{
        Google,Bing,StackOverFlow
    }
}
