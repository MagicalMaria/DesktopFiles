#pragma checksum "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e31905cdbe363ea88efd964f2a75bd4b2ddb60a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__EmployeeList), @"mvc.1.0.view", @"/Views/Shared/_EmployeeList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e31905cdbe363ea88efd964f2a75bd4b2ddb60a3", @"/Views/Shared/_EmployeeList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0aea50753624f0bf8730e6f49671c7a320076a93", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__EmployeeList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SimpleCoreWebApp.Models.Employee>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h2>Partial View</h2>\r\n\r\n");
#nullable restore
#line 8 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
 foreach (Employee employee in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row col-md-4\">\r\n\r\n");
#nullable restore
#line 12 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
         if (employee.IsPermanent == true)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <b>\r\n                <span>");
#nullable restore
#line 15 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
                 Write(employee.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                <span>");
#nullable restore
#line 16 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
                 Write(employee.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                <span>");
#nullable restore
#line 17 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
                 Write(employee.Salary);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                <span>");
#nullable restore
#line 18 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
                 Write(employee.IsPermanent);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </b>\r\n");
#nullable restore
#line 20 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span>");
#nullable restore
#line 23 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
             Write(employee.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <span>");
#nullable restore
#line 24 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
             Write(employee.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <span>");
#nullable restore
#line 25 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
             Write(employee.Salary);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <span>");
#nullable restore
#line 26 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
             Write(employee.IsPermanent);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 27 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 29 "C:\Users\892639\Training\Stage_3\1_Dot_Net_Advanced_Features\Day_5\SimpleCoreWebAppSolution\SimpleCoreWebApp\Views\Shared\_EmployeeList.cshtml"
}

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