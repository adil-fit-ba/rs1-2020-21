#pragma checksum "C:\tmp dev1\gitprobalocal6\vs-code\WebApplication10\Views\Student\Poruka.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c86b4c8c303fdc4c3833def194c4cccd7b1ef9b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_Poruka), @"mvc.1.0.view", @"/Views/Student/Poruka.cshtml")]
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
#line 1 "C:\tmp dev1\gitprobalocal6\vs-code\WebApplication10\Views\_ViewImports.cshtml"
using WebApplication10;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\tmp dev1\gitprobalocal6\vs-code\WebApplication10\Views\_ViewImports.cshtml"
using WebApplication10.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\tmp dev1\gitprobalocal6\vs-code\WebApplication10\Views\Student\Poruka.cshtml"
using WebApplication10.EntityModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c86b4c8c303fdc4c3833def194c4cccd7b1ef9b7", @"/Views/Student/Poruka.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4ba2cecb357c8d27fd350c5ce2f2ce35eab7824", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_Poruka : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\tmp dev1\gitprobalocal6\vs-code\WebApplication10\Views\Student\Poruka.cshtml"
Write(TempData["PorukaInfo"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<a href=\"/Student/Prikaz\"> Go back </a>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
