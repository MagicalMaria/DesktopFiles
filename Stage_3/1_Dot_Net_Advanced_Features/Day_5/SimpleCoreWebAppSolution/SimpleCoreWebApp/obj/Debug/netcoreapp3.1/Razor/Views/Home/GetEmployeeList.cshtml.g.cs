#pragma checksum "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Home\GetEmployeeList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "594165f1fa09ad571b581251b68b9d98dc2b82c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetEmployeeList), @"mvc.1.0.view", @"/Views/Home/GetEmployeeList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\_ViewImports.cshtml"
using SimpleCoreWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\_ViewImports.cshtml"
using SimpleCoreWebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"594165f1fa09ad571b581251b68b9d98dc2b82c4", @"/Views/Home/GetEmployeeList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0aea50753624f0bf8730e6f49671c7a320076a93", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetEmployeeList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SimpleCoreWebApp.Models.Employee>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Home\GetEmployeeList.cshtml"
  
    ViewData["Title"] = "GetEmployeeList";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>GetEmployeeList</h1>\r\n\r\n\r\n");
#nullable restore
#line 10 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Home\GetEmployeeList.cshtml"
Write(Html.Partial("_EmployeeList",Model));

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SimpleCoreWebApp.Models.Employee>> Html { get; private set; }
    }
}
#pragma warning restore 1591
