#pragma checksum "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "210ba063b0eb35941138b5baf59a779ec7b10acc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\_ViewImports.cshtml"
using FindAFriend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\_ViewImports.cshtml"
using FindAFriend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"210ba063b0eb35941138b5baf59a779ec7b10acc", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd56468e09b560e07f7e759a501cf256f43585be", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml"
   if (User.Identity.IsAuthenticated)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"text-center\">\r\n            <h4>Hej Inloggade kompis!</h4>\r\n        </div>\r\n");
#nullable restore
#line 10 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"text-center\">\r\n            <h4>Hej Utloggade kompis!</h4>\r\n            <h4>Skapa en profil idag!</h4>\r\n        </div>\r\n");
#nullable restore
#line 17 "C:\Users\danie\Desktop\FindAFriend-ASP.NET-MVC\FindAFriend\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <hr />
    <div>
        <div style=""display : table;"">
            <div style=""display : table-cell;"" id=""exempelBild0"">

            </div>
            <div style=""display : table-cell; padding : 0; float : left;"">
                <dl class=""row"" style=""margin-left : auto;"" id=""exempelProfil0"">
                </dl>
            </div>
        </div>
        <hr />
        <div style=""display : table;"">
            <div style=""display : table-cell;"" id=""exempelBild1"">
            </div>
            <div style=""display : table-cell; padding : 0; float : left;"">
                <dl class=""row"" style=""margin-left : auto;"" id=""exempelProfil1"">
                </dl>
            </div>
        </div>
        <hr />
        <div style=""display : table;"">
            <div style=""display : table-cell;"" id=""exempelBild2"">
            </div>
            <div style=""display : table-cell; padding : 0; float : left;"">
                <dl class=""row"" style=""margin-left : auto;"" id=""exempelProfi");
            WriteLiteral("l2\">\r\n                </dl>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <p hidden id=\"bildString\"></p>\r\n");
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <script>
        //Ett script som kollar om v�ra defaultanv�ndare finns i databasen
        //Finns inte en av defaultanv�ndarna postas den till databasen tillsammans med en tillh�rande bild
        $(() =>
            $(document).ready((e) => {
                $.ajax({
                    url: ""/api/ProfilesApi/loadprofiles"",
                    type: ""GET"",
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (data) {
                        var jsonString = JSON.stringify(data);
                        if (!jsonString.includes(""anna-andersson@hotmail.com"")) {
                            $.ajax({
                                url: ""/api/ProfilesApi/postprofile"",
                                type: ""POST"",
                                data: JSON.stringify({
                                    Name: ""Anna Andersson"", Description: ""Tredje snabbaste i klassen i mellanstadiet"", City: ""Karlst");
                WriteLiteral(@"ad"", UserID: ""anna-andersson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
                                dataType: ""json""
                            });
                            $.ajax({
                                url: ""/api/ProfilesApi/postimages"",
                                type: ""POST"",
                                data: JSON.stringify({
                                    ImageExtension: "".png"", UserEmail: ""anna-andersson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
                                dataType: ""json""
                            });
                        }
                        if (!jsonString.includes(""kenta-kentsson@hotmail.com"")) {
                            $.ajax({
                                url: ""/api/ProfilesApi/postprofile"",
                                type: ""POST"",
   ");
                WriteLiteral(@"                             data: JSON.stringify({
                                    Name: ""Kenta Kentsson"", Description: ""Studsar och stajlar"", City: ""Stockholm"", UserID: ""kenta-kentsson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
                                dataType: ""json""
                            });

                            $.ajax({
                                url: ""/api/ProfilesApi/postimages"",
                                type: ""POST"",
                                data: JSON.stringify({
                                    ImageExtension: "".jpg"", UserEmail: ""kenta-kentsson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
                                dataType: ""json""
                            });
                        }

                        if (!jsonString.includes(""harry-hermansson@hotmail");
                WriteLiteral(@".com"")) {
                            $.ajax({
                                url: ""/api/ProfilesApi/postprofile"",
                                type: ""POST"",
                                data: JSON.stringify({
                                    Name: ""Harry Hermansson"", Description: ""En vanlig karl"", City: ""Rundsparksdalen"", UserID: ""harry-hermansson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
                                dataType: ""json""
                            });
                            $.ajax({
                                url: ""/api/ProfilesApi/postimages"",
                                type: ""POST"",
                                data: JSON.stringify({
                                    ImageExtension: "".jpeg"", UserEmail: ""harry-hermansson@hotmail.com""
                                }),
                                contentType: ""application/json; charset=utf-8"",
           ");
                WriteLiteral(@"                     dataType: ""json""
                            });
                        }
                    }
                });
            })
        );
    </script>
    <script>
        //H�mtar ut alla bilder fr�n databasen
        $(() =>
            $(document).ready((e) => {
                $.ajax({
                    url: ""/api/ProfilesApi/loadimages"",
                    type: ""GET"",
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (data) {
                        var picString = JSON.stringify(data);
                        $(""#bildString"").html(picString);
                    }
                });
            })
        );
    </script>
    <script>
        //H�mtar de tre f�rsta profilerna i databasen och appendar de i htmlen
        //Scriptet kollar sen igenom alla bilder som h�mtats fr�n databasen och matchar r�tt bild med profilen som ska visas
        $(() =>
");
                WriteLiteral(@"            $(document).ready((e) => {
                $.ajax({
                    url: ""/api/ProfilesApi/loadprofiles"",
                    type: ""GET"",
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (data) {
                        for (var i = 0; i < 3; i++) {
                            var obj = data[i];
                            var birthday = obj[""birthday""].toString();
                            birthday = birthday.slice(0, 10) + "" - "" + birthday.slice(11, 16);
                            var profilDiv = $('#exempelProfil' + i);
                            var bildDiv = $('#exempelBild' + i);
                            var element = '<dt class=""col-sm-2"">Namn</dt><dd class=""col-sm-10"">' + obj[""name""] + '</dd><dt class=""col-sm-2"">F&oumldelsedag</dt><dd class=""col-sm-10"">' + birthday + '</dd><dt class=""col-sm-2"">Beskrivning</dt><dd class=""col-sm-10"">' + obj[""description""] + '</dd><dt class=");
                WriteLiteral(@"""col-sm-2"">Stad</dt><dd class=""col-sm-10"">' + obj[""city""] + '</dd><dt class=""col-sm-2"">Email</dt><dd class=""col-sm-10"">' + obj[""userID""] + '</dd>';
                            profilDiv.append(element);

                            var imageString = $('#bildString').html();
                            var jsonPics = JSON.parse(imageString);
                            for (var index = 0; index < jsonPics.length; index++) {
                                var picObj = jsonPics[index];
                                if (picObj[""userEmail""].includes(obj[""userID""])) {
                                    var picElement = '<img src=""../Images/' + picObj[""userEmail""] + '"" asp-append-version=""true"" width=""175px"" height=""175px"" style=""border-radius : 50%; object-fit: cover; float : left;"">';
                                    bildDiv.append(picElement);
                                }
                            }
                        }
                    }
                });
            })
  ");
                WriteLiteral("      );\r\n    </script>\r\n\r\n");
            }
            );
            WriteLiteral("\r\n\r\n");
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
