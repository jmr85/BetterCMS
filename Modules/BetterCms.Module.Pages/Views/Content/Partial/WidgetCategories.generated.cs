﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
    using BetterCms.Module.Root.Content.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Content/Partial/WidgetCategories.cshtml")]
    public partial class _Views_Content_Partial_WidgetCategories_cshtml : System.Web.Mvc.WebViewPage<IList<BetterCms.Module.Pages.ViewModels.Widgets.WidgetCategoryViewModel>>
    {
        public _Views_Content_Partial_WidgetCategories_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
 if (Model != null && Model.Count > 0)
{
    foreach (var category in Model)
    {
        
            
            #line default
            #line hidden
            
            #line 8 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
   Write(Html.Partial("~/Areas/bcms-pages/Views/Content/Partial/WidgetCategory.cshtml", category));

            
            #line default
            #line hidden
            
            #line 8 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
                                                                                                 
    }
}
else
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"bcms-category\"");

WriteLiteral(">           \n        <div");

WriteLiteral(" class=\"bcms-category-titles\"");

WriteLiteral(">");

            
            #line 14 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
                                     Write(RootGlobalization.Grid_NoDataAvailable_Message);

            
            #line default
            #line hidden
WriteLiteral("</div>\n    </div>\n");

            
            #line 16 "..\..\Views\Content\Partial\WidgetCategories.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
