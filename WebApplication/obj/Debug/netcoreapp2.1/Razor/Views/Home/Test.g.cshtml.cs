#pragma checksum "X:\Projects\Reversi\Reversi\WebApplication\Views\Home\Test.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "72fab178f3fb20d1146d5ba2c2e95da797792aba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Test), @"mvc.1.0.view", @"/Views/Home/Test.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Test.cshtml", typeof(AspNetCore.Views_Home_Test))]
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
#line 1 "X:\Projects\Reversi\Reversi\WebApplication\Views\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#line 2 "X:\Projects\Reversi\Reversi\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72fab178f3fb20d1146d5ba2c2e95da797792aba", @"/Views/Home/Test.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa0ef8da47a84ffb33e8bc853509aa4fa5703a26", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Test : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "X:\Projects\Reversi\Reversi\WebApplication\Views\Home\Test.cshtml"
  
	ViewData["Title"] = "Test";

#line default
#line hidden
            BeginContext(37, 109, true);
            WriteLiteral("\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"css/game.css\">\r\n\r\n<input type=\"button\" value=\"Select Player 1\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 146, "\"", 204, 5);
            WriteAttributeValue("", 156, "location.href", 156, 13, true);
            WriteAttributeValue(" ", 169, "=", 170, 2, true);
            WriteAttributeValue(" ", 171, "\'", 172, 2, true);
#line 7 "X:\Projects\Reversi\Reversi\WebApplication\Views\Home\Test.cshtml"
WriteAttributeValue("", 173, Url.Action("Player1", "Home"), 173, 30, false);

#line default
#line hidden
            WriteAttributeValue("", 203, "\'", 203, 1, true);
            EndWriteAttribute();
            BeginContext(205, 48, true);
            WriteLiteral("/>\r\n<input type=\"button\" value=\"Select Player 2\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 253, "\"", 311, 5);
            WriteAttributeValue("", 263, "location.href", 263, 13, true);
            WriteAttributeValue(" ", 276, "=", 277, 2, true);
            WriteAttributeValue(" ", 278, "\'", 279, 2, true);
#line 8 "X:\Projects\Reversi\Reversi\WebApplication\Views\Home\Test.cshtml"
WriteAttributeValue("", 280, Url.Action("Player2", "Home"), 280, 30, false);

#line default
#line hidden
            WriteAttributeValue("", 310, "\'", 310, 1, true);
            EndWriteAttribute();
            BeginContext(312, 344, true);
            WriteLiteral(@"/>
<div id=""wrapper"">

</div>
<script src=""https://code.jquery.com/jquery-3.3.1.min.js""></script>
<script src=""js/spa.js""></script>
<script src=""js/data.js""></script>
<script src=""js/model.js""></script>
<script src=""js/reversi.js""></script>
<script>
	console.log(""SPA.Data -> "" + SPA.Data);
	SPA.initModule($(""#wrapper""));
</script>");
            EndContext();
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
