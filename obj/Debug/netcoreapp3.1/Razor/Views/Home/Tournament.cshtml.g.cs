#pragma checksum "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc40c6a4db0ab86c88bfab0602b7736c55279774"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Tournament), @"mvc.1.0.view", @"/Views/Home/Tournament.cshtml")]
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
#line 1 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\_ViewImports.cshtml"
using ChessStatistics;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\_ViewImports.cshtml"
using ChessStatistics.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc40c6a4db0ab86c88bfab0602b7736c55279774", @"/Views/Home/Tournament.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"197962091e9c6879f9dbe761c94ccf7c489f1fee", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Tournament : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChessStatistics.Models.Tournament>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Tournaments", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddTournamentParticipants", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "player.Id", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
    ViewData["CurrentPage"] = "Tournament";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<h5 class=""text-light""> Участники турнира </h5>
<table id=""playersParticipantsTable"" class=""table table-responsive-sm table-bordered table-hover table-dark regular-table"">
    <thead>
        <tr>
            <th scope=""col"">ФИО</th>
            <th scope=""col"">Рейтинг</th>
            <th scope=""col"">Звание</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 17 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
         foreach (var player in ViewBag.PlayersParticipaningInTournament)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <th scope=\"row\">\r\n                    ");
#nullable restore
#line 21 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
               Write(player.FIO);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th scope=\"row\">\r\n                    ");
#nullable restore
#line 24 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
               Write(player.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th scope=\"row\">\r\n                    ");
#nullable restore
#line 27 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
               Write(player.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n            </tr>\r\n");
#nullable restore
#line 30 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
#nullable restore
#line 34 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
 if (ViewBag.AutorizeUser != null && ViewBag.AutorizeUser.IsAdmin)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c552797747917", async() => {
                WriteLiteral("\r\n        <button");
                BeginWriteAttribute("tournamentId", " tournamentId=\"", 1105, "\"", 1129, 1);
#nullable restore
#line 37 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
WriteAttributeValue("", 1120, Model.Id, 1120, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"btn btn-primary\"> Добавление участников турнира </button>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 36 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                                                                                WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 39 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h5 class=\"text-light\"> Туры </h5>\r\n\r\n<script>\r\n    $(document).ready(function () {\r\n    new DataTable(\'#tournamentTable\', {\r\n        lengthMenu: [");
#nullable restore
#line 46 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                Write(Model.CountTours);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"]
    });
        });
</script>

<table id=""tournamentTable"" class=""table table-responsive-sm table-bordered table-hover table-dark"">
    <thead>
        <tr>
            <th scope=""col"">№</th>
            <th scope=""col"">Игрок за белых</th>
            <th scope=""col"">Результат</th>
            <th scope=""col"">Игрок за черных</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 61 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
         for (int i = 1; i <= ViewBag.CountPairs; i++)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <th scope=\"row\">\r\n                ");
#nullable restore
#line 65 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
           Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th scope=\"row\">\r\n                <select id=\"PlayerWhite\"\r\n                        name=\"PlayerWhite\"\r\n                        class=\"form-control\">\r\n");
#nullable restore
#line 71 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                     foreach (var player in ViewBag.PlayersParticipaningInTournament)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c5527977413342", async() => {
#nullable restore
#line 73 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                                             Write(player.FIO);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 74 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\r\n            </th>\r\n            <th scope=\"row\">\r\n                <select id=\"result\"\r\n                        name=\"GameResult\"\r\n                        class=\"form-control\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c5527977415151", async() => {
                WriteLiteral("1-0");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c5527977416328", async() => {
                WriteLiteral("1/2");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c5527977417505", async() => {
                WriteLiteral("0-1");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </select>\r\n            </th>\r\n            <th scope=\"row\">\r\n                <select id=\"PlayerBlack\"\r\n                        name=\"PlayerBlack\"\r\n                        class=\"form-control\">\r\n");
#nullable restore
#line 90 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                     foreach (var player in ViewBag.PlayersParticipaningInTournament)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cc40c6a4db0ab86c88bfab0602b7736c5527977419220", async() => {
#nullable restore
#line 92 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                                             Write(player.FIO);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 93 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\r\n            </th>\r\n        </tr>\r\n");
#nullable restore
#line 97 "C:\Users\Alex\Desktop\ChessStatistics\ChessStatistics\Views\Home\Tournament.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChessStatistics.Models.Tournament> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
