using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SearchEngines.Classes;
using SearchEngines.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngines.UnitTests
{
    [TestClass]
    public class UtilsTests
    {
        private IHttpClientFactory _httpClientFactory;

        [TestMethod]
        public void parseImgs_HaveImgs_ReturnsListString()
        {

            // Arrange
            List<string> expectedUrls = new List<string> { 
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRB9Gjs5H0NdFUPuUpYeZ9WCRWVmgOOF90lL1txYpuhh88TNfXxBStBoqkDuc&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRunn-1Fp98_YALYNTI3XddtnmDMyqq-37dzue2TiY1k9C5fftTNNvjscUtQ&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRusnHEABrnX5FqUx1LLlATEoiNQ-VxBeM7o_kfMV6ZP2LjBmvt_XqS5v8z7g&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAYRHU-hLJYJzqcJ1BdiGqlqlr_YZI4RFo9DZhRoxghZ2CZpLh58fCyazoMA&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSmn200TaLyyPOr777zH_vssUGbPRJOd-50FZnrS04ZXbxqj_BTxJ1W8i2K6w&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRLzTkT93M5LR3GKZUWrCkRh_T3Oi6t-qK7bmvLSpxzKMd9ajbn8AtX-YJOCw&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcST4K7ou-phWCj1tcS3SXn4hSF_7Ayxykw4-EYrqOduOqP7jCHf395_0CPlaQ&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSstF8PwaO5K4_nLen4vM-LvGHFUm1zl6LNGPp5jmGXG4kqKB4JiVqcfjiBqIk&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTSt5Qbkvtdqqh-CnElNI5IAwfEz6DWP6qh3TX4XXW0VqA_CfXsr_6z407--Ig&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0JeqP1ZQjfHiLrfpfucrj5UfgKyxr1utu1CH0eDyKX3MqRI4ZCpAk6gOADjc&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUZHT30doXaJKMgGLh1i0XFZGli4vC2rnHowwBwdWrx99duj_aGatg_A_0OMs&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQM1-RI3kLME2tfFa_Hp8xFHfO0s9k98gBGZ9Jrx1V0wr2OENz0CNg5BWnFOg&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoo2K-MmzDDkr0I4-I7XyEVSay4H7F9JMpwIY5h-VSLhNV29nEcMxPqnKMVQ&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRwscKVk2Dq0DngYF0RCOZeYLQehrEm_hLcrm-KPnE3tvj6dwvRbsGf1SGVgo&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2P8yoLdVfgLdtoxEd_bkLZjxLmBLLcXeHS0pP_9ymbdVxVvx6HvDuxew0GA&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMpfK98zqad05NTpY4JIyyzILXA2dT1oVgMQet9qaja6-BJT4Vxc6vNXo8Yw&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAeeVYMa_9tX3H2UywcIV3ii0CEjPt_yh7AYkTbEj9hHoIqSGR5MkoRrap7es&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJhrp9zoJO18vzN1l8LYJdTsN8_jDOAk_CbJ3VVoEa6xRNeBikFpe_kfbrsQ&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtv5kkhTi9FsA_yn6DRNdzbXGVIs8tmqbrLX-GXYO-2TZO2B0LYGniUPOfIwc&amp;s",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJxs_yEaUtJaIXiq3IHZExKkXX_hJk2l846AcrWG61aARSEaaFDibq7UWM2w&amp;s"
            };
            string testString = "<!DOCTYPE html PUBLIC \" -//WAPFORUM//DTD XHTML Mobile 1.0//EN\" \"http://www.wapforum.org/DTD/xhtml-mobile10.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\" lang=\"nl\"><head><meta content=\"application/xhtml+xml; charset=UTF-8\" http-equiv=\"Content-Type\"/><meta content=\"no-cache\" name=\"Cache-Control\"/><title>doomr - Google zoeken</title><style>a{text-decoration:none;color:inherit}a:hover{text-decoration:underline}a img{border:0}body{font-family:Roboto,Helvetica,Arial,sans-serif;padding:8px;margin:0 auto;max-width:700px;min-width:240px}.FbhRzb{border-left:thin solid #e0e0e0;border-right:thin solid #e0e0e0;border-top:thin solid #e0e0e0;height:40px;overflow:hidden}.n692Zd{margin-bottom:10px}.cvifge{height:40px;border-spacing:0}.QvGUP{height:40px;padding:0 8px 0 8px;vertical-align:top}.O4cRJf{height:40px;width:100%;padding:0;padding-right:16px}.O1ePr{height:40px;padding:0;vertical-align:top}.kgJEQe{height:36px;width:98px;vertical-align:top;margin-top:4px}.lXLRf{vertical-align:top}.MhzMZd{border:0;vertical-align:middle;font-size:14px;height:40px;padding:0;width:100%;padding-left:16px}.xB0fq{height:40px;border:none;font-size:14px;background-color:#4285f4;color:#fff;padding:0 16px;margin:0;vertical-align:top;cursor:pointer}.xB0fq:focus{border:1px solid #000}.M7pB2{border:thin solid #e0e0e0;margin:0 0 3px 0;font-size:13px;font-weight:500;height:40px}.euZec{width:100%;height:40px;text-align:center;border-spacing:0}table.euZec td{padding:0;width:25%}.QIqI7{display:inline-block;padding-top:4px;font-weight:bold;color:#4285f4}.EY24We{border-bottom:2px solid #4285f4}.CsQyDc{display:inline-block;color:#757575}.TuS8Ad{font-size:14px}.dzp8ae{font-weight:bold;color:#333}.rEM8G{color:#686868}.bookcf{table-layout:fixed;width:100%;border-spacing:0}.InWNIe{text-align:center}.uZgmoc{border:thin solid #e0e0e0;color:#686868;font-size:14px;text-align:center;table-layout:fixed;width:100%}.frGj1b{display:block;padding:12px 0px 12px 0px;width:100%}.BnJWBc{text-align:center;padding:6px 0 13px 0;height:35px}.e3goi{vertical-align:top;padding:0;height:180px}.GpQGbf{margin:auto;border-collapse:collapse;border-spacing:0;width:100%}</style></head><body><style>.X6ZCif{color:rgba(0,0,0,.87);font-size:11px;line-height:16px;display:inline-block;padding-top:2px;overflow:hidden;padding-bottom:4px;width:100%}.TwVfHd{border-radius:16px;border:thin solid #e0e0e0;display:inline-block;padding:8px 8px;margin-right:8px;margin-bottom:4px}.yekiAe{background-color:#d3d3d3}.svla5d{width:100%}.ezO2md{border:thin solid #e0e0e0;padding:12px 16px 12px 16px;margin-bottom:10px;font-family:Roboto,Helvetica,Arial,sans-serif}.lIMUZd{font-family:Roboto,Helvetica,Arial,sans-serif}.TxbwNb{border-spacing:0}.K35ahc{width:100%}.owohpf{text-align:center}.RAyV4b{width:162px;height:140px;line-height:140px;overflow:'hidden';text-align:center}.t0fcAb{text-align:center;margin:auto;vertical-align:middle;max-width:162px;max-height:140px}.Tor4Ec{padding-top:2px;padding-bottom:8px}.fYyStc{word-break:break-word}.Fj3V3b{color:#1967D2;font-size:14px;line-height:20px}.FrIlee{color:rgba(0,0,0,.87);font-size:11px;line-height:16px}.F9iS2e{color:#70757A;font-size:11px;line-height:16px}.WMQ2Le{color:#70757A;font-size:12px;line-height:16px}.x3G5ab{color:rgba(0,0,0,.87);font-size:12px;line-height:16px}.fuLhoc{color:#1967D2;font-size:18px;line-height:24px}.epoveb{font-size:32px;line-height:40px;font-weight:400;color:rgba(0,0,0,.87)}.dXDvrc{color:#006621;font-size:14px;line-height:20px;word-wrap:break-word}.dloBPe{font-weight:bold}.YVIcad{color:#70757A}.JkVVdd{color:#dd4b39}.oXZRFd{color:#dd4b39}.MQHtg{color:#fbc02d}.pyMRrb{color:#3d9400}.EtTZid{color:#3d9400}.M3vVJe{color:#1967D2}.qXLe6d{display:block}.NHQNef{font-style:italic}.Cb8Z7c{white-space:pre}a.ZWRArf{text-decoration:none}a .CVA68e:hover{text-decoration:underline}</style><div class=\"n692Zd\"><div class=\"BnJWBc\"><a class=\"lXLRf\" href=\"/?output=images&amp;ie=UTF-8&amp;tbm=isch&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQPAgC\"><img class=\"kgJEQe\" src=\"/images/branding/searchlogo/1x/googlelogo_desk_heirloom_color_150x55dp.gif\" alt=\"Google logo\"/></a></div><div class=\"FbhRzb\"><form action=\"/search\"><input name=\"ie\" value=\"ISO-8859-1\" type=\"hidden\"/><input name=\"tbm\" value=\"isch\" type=\"hidden\"/><input name=\"oq\" type=\"hidden\"/><input name=\"aqs\" type=\"hidden\"/><table class=\"cvifge\"><tr><td class=\"O4cRJf\"><input class=\"MhzMZd\" value=\"doomr\" name=\"q\" type=\"text\"/></td><td class=\"O1ePr\"><input class=\"xB0fq\" value=\"Zoeken\" type=\"submit\"/></td></tr></table></form></div><div class=\"M7pB2\"><table class=\"euZec\"><tbody><tr><td><a class=\"CsQyDc\" href=\"/search?q=doomr&amp;ie=UTF-8&amp;source=lnms&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ_AUIBCgA\">ALLE</a></td><td class=\"EY24We\"><span class=\"QIqI7\">AFBEELDINGEN</span></td><td><a class=\"CsQyDc\" href=\"/search?q=doomr&amp;ie=UTF-8&amp;tbm=vid&amp;source=lnms&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ_AUIBigC\">VIDEO'S</a></td><td><a class=\"CsQyDc\" href=\"/search?q=doomr&amp;ie=UTF-8&amp;tbm=nws&amp;source=lnms&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ_AUIBygD\">NIEUWS</a></td></tr></tbody></table></div></div><div class=\"X6ZCif\"><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:islamic+art&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYICygA\">islamic art</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:twitch&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIDCgB\">twitch</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:range+rover&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIDSgC\">range rover</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:art+patterns&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIDigD\">art patterns</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:agraelus&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIDygE\">agraelus</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:avengethefallenmeme&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIECgF\">avengethefallenmeme</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:twitter&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIESgG\">twitter</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:patrixcraft&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIEigH\">patrixcraft</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:module&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIEygI\">module</a><a class=\"TwVfHd\" href=\"/search?ie=UTF-8&amp;tbm=isch&amp;q=doomr&amp;chips=q:doomr,online_chips:minecraft&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQ4lYIFCgJ\">minecraft</a></div><div><table class=\"GpQGbf\"><tr><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://twitter.com/Domestic_Denis&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAB6BAgCEAE&amp;usg=AOvVaw11Vla7nCnzJs6KUbNxjaOl\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRB9Gjs5H0NdFUPuUpYeZ9WCRWVmgOOF90lL1txYpuhh88TNfXxBStBoqkDuc&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://twitter.com/Domestic_Denis&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAB6BAgCEAI&amp;usg=AOvVaw3-5i-CJ2g8SGlOjLiclhii\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">DOOMR.A.T....</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">twitter.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DU1uYHGPB7No&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAF6BAgTEAE&amp;usg=AOvVaw0AJKfEowLaPTZd-jAoFlDn\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRunn-1Fp98_YALYNTI3XddtnmDMyqq-37dzue2TiY1k9C5fftTNNvjscUtQ&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DU1uYHGPB7No&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAF6BAgTEAI&amp;usg=AOvVaw1Bv-OjdKfky5qqIWWA5oD1\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Chce n&#283;kdo mas�&#382;? ( &#865;� &#860;&#662;...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DOvOphy-LR40&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAJ6BAgSEAE&amp;usg=AOvVaw2qk_I7X5lB0jA8MJDggfsY\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRusnHEABrnX5FqUx1LLlATEoiNQ-VxBeM7o_kfMV6ZP2LjBmvt_XqS5v8z7g&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DOvOphy-LR40&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAJ6BAgSEAI&amp;usg=AOvVaw0OwIO-oS9Cr8bBETVAw9E2\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Agraelus Oddshot - Friday...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAN6BAgREAE&amp;usg=AOvVaw2DaK6C58UH0JSQJkAcFGkb\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAYRHU-hLJYJzqcJ1BdiGqlqlr_YZI4RFo9DZhRoxghZ2CZpLh58fCyazoMA&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAN6BAgREAI&amp;usg=AOvVaw2od31bG1yJA0c799oxq9pM\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td></tr><tr><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://pholder.com/pavel&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAR6BAgQEAE&amp;usg=AOvVaw11cUZTbE7mJYrFfhzh3Bad\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSmn200TaLyyPOr777zH_vssUGbPRJOd-50FZnrS04ZXbxqj_BTxJ1W8i2K6w&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://pholder.com/pavel&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAR6BAgQEAI&amp;usg=AOvVaw0nzq0am2DUn1rqmJTxhBtR\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">573 best Pavel images on...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">pholder.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://twitter.com/doomr6&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAV6BAgPEAE&amp;usg=AOvVaw1KoSxCV-ZGPezDOV-2-Lrm\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRLzTkT93M5LR3GKZUWrCkRh_T3Oi6t-qK7bmvLSpxzKMd9ajbn8AtX-YJOCw&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://twitter.com/doomr6&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAV6BAgPEAI&amp;usg=AOvVaw2hg1Gwv6fvFSxR6Lr5LncH\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">DoomR (@DoomR6) | Twitter</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">twitter.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAZ6BAgOEAE&amp;usg=AOvVaw339Vc5YcQxn4tQ1-CIRaB6\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcST4K7ou-phWCj1tcS3SXn4hSF_7Ayxykw4-EYrqOduOqP7jCHf395_0CPlaQ&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAZ6BAgOEAI&amp;usg=AOvVaw1LYAyJ5kmyQjOkAKiEQBGO\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DeN5l-kbq_Oc&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAd6BAgNEAE&amp;usg=AOvVaw1gYzoKyvlz6i4Hk0M8s1o4\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSstF8PwaO5K4_nLen4vM-LvGHFUm1zl6LNGPp5jmGXG4kqKB4JiVqcfjiBqIk&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DeN5l-kbq_Oc&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAd6BAgNEAI&amp;usg=AOvVaw3GA6L9z7L8X7YqtAYDj5t-\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">PATTY DOOMR Gets Grounded...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td></tr><tr><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAh6BAgJEAE&amp;usg=AOvVaw06y-z5TbEpZ7hbMuVNfBna\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTSt5Qbkvtdqqh-CnElNI5IAwfEz6DWP6qh3TX4XXW0VqA_CfXsr_6z407--Ig&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAh6BAgJEAI&amp;usg=AOvVaw0gORJVCcsu-HGtaeCT31la\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DvpUUx-oUSGc&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAl6BAgLEAE&amp;usg=AOvVaw0feTo6Fb6ar9HckM2WaD4R\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0JeqP1ZQjfHiLrfpfucrj5UfgKyxr1utu1CH0eDyKX3MqRI4ZCpAk6gOADjc&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DvpUUx-oUSGc&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAl6BAgLEAI&amp;usg=AOvVaw1cyGyTUWf3kPI_U0bcNnnN\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Gears 5 | #1 | 8.9.2019 |...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.reddit.com/r/Doomers/comments/ewfklu/just_quit_cigarettes_can_i_still_qualify_as_a/&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAp6BAgMEAE&amp;usg=AOvVaw2BlDeYr8_omnAZjgpOgtR0\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUZHT30doXaJKMgGLh1i0XFZGli4vC2rnHowwBwdWrx99duj_aGatg_A_0OMs&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.reddit.com/r/Doomers/comments/ewfklu/just_quit_cigarettes_can_i_still_qualify_as_a/&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAp6BAgMEAI&amp;usg=AOvVaw0Aaz2HjU-gyig6a6NwWOSM\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Just quit cigarettes can I...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">reddit.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://twitter.com/Doomr910&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAt6BAgKEAE&amp;usg=AOvVaw0NbT6IX50B0nnLJr_S42q3\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQM1-RI3kLME2tfFa_Hp8xFHfO0s9k98gBGZ9Jrx1V0wr2OENz0CNg5BWnFOg&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://twitter.com/Doomr910&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAt6BAgKEAI&amp;usg=AOvVaw0OCW4JxZmVnEPsvQJON-Zd\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Doomr (@Doomr910) | Twitter...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">twitter.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td></tr><tr><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMAx6BAgIEAE&amp;usg=AOvVaw2nJszvLQYiJhu7hrvsZHhX\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoo2K-MmzDDkr0I4-I7XyEVSay4H7F9JMpwIY5h-VSLhNV29nEcMxPqnKMVQ&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMAx6BAgIEAI&amp;usg=AOvVaw2jwnD7XEDl27y9FbRp0aSH\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DPBlHxtau3aw&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMA16BAgHEAE&amp;usg=AOvVaw13HIg9dxN5-MIiyRH3o124\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRwscKVk2Dq0DngYF0RCOZeYLQehrEm_hLcrm-KPnE3tvj6dwvRbsGf1SGVgo&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3DPBlHxtau3aw&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMA16BAgHEAI&amp;usg=AOvVaw0dZjCDaptQXYb5wj7fuetZ\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Agraelus Oddshot - Dead...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMA56BAgFEAE&amp;usg=AOvVaw0ixRc6LE9EQiGwaBykDfrY\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2P8yoLdVfgLdtoxEd_bkLZjxLmBLLcXeHS0pP_9ymbdVxVvx6HvDuxew0GA&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMA56BAgFEAI&amp;usg=AOvVaw28op5_KRfTMvNGeHu5dgXa\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3Dw5sB8_x6ykY&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMA96BAgBEAE&amp;usg=AOvVaw36J41MwGcSyWRIPNchwxeX\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMpfK98zqad05NTpY4JIyyzILXA2dT1oVgMQet9qaja6-BJT4Vxc6vNXo8Yw&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.youtube.com/watch%3Fv%3Dw5sB8_x6ykY&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMA96BAgBEAI&amp;usg=AOvVaw1MbgRP3vA4I8Cpol6K6oAz\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Dead Rising 3 | #1 |...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">youtube.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td></tr><tr><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://github.com/Doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMBB6BAgEEAE&amp;usg=AOvVaw3j0oMHHF4U46cD-MH91B6G\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAeeVYMa_9tX3H2UywcIV3ii0CEjPt_yh7AYkTbEj9hHoIqSGR5MkoRrap7es&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://github.com/Doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMBB6BAgEEAI&amp;usg=AOvVaw1mrBaY8i-rcXNq1gFzPN-G\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">Doomr � GitHub</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">github.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://www.twitch.tv/spacemancz/clip/KitschyLittleNigiriFUNgineer%3Ffilter%3Dclips%26range%3D7d%26sort%3Dtime&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMBF6BAgAEAE&amp;usg=AOvVaw0Uo1o6lbru_0zzjjLU2p5C\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJhrp9zoJO18vzN1l8LYJdTsN8_jDOAk_CbJ3VVoEa6xRNeBikFpe_kfbrsQ&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://www.twitch.tv/spacemancz/clip/KitschyLittleNigiriFUNgineer%3Ffilter%3Dclips%26range%3D7d%26sort%3Dtime&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMBF6BAgAEAI&amp;usg=AOvVaw0bRRMGJcRYqBOuVzH8iH6_\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">SpacemanCZ - Dead Island w/...</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">twitch.tv</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMBJ6BAgDEAE&amp;usg=AOvVaw1T5dgHlkdTYeq-WWIAUF3S\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtv5kkhTi9FsA_yn6DRNdzbXGVIs8tmqbrLX-GXYO-2TZO2B0LYGniUPOfIwc&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=http://minecraft.novaskin.me/gallery/tag/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMBJ6BAgDEAI&amp;usg=AOvVaw2MOeaXoK9idqkwrUeBk-l_\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">doomr | Nova Skin</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">minecraft.novaskin.me</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td><td class=\"e3goi\" align=\"center\"><div class=\"svla5d\"> <div> <div class=\"lIMUZd\"><div><table class=\"TxbwNb\"><tr><td><a href=\"/url?q=https://twitter.com/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQqoUBMBN6BAgGEAE&amp;usg=AOvVaw0aJlcDjBhXNXIJsk_-c1-z\"><div class=\"RAyV4b\"><img class=\"t0fcAb\" alt=\"\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJxs_yEaUtJaIXiq3IHZExKkXX_hJk2l846AcrWG61aARSEaaFDibq7UWM2w&amp;s\"/></div></a></td></tr><tr><td><a href=\"/url?q=https://twitter.com/doomr&amp;sa=U&amp;ved=2ahUKEwi_krDRutLqAhWQzqQKHf40C4kQr4kDMBN6BAgGEAI&amp;usg=AOvVaw0MlQSdZ3IwbUdyG3ExHKrW\"><div class=\"Tor4Ec\">  <span class=\"qXLe6d x3G5ab\">  <span class=\"fYyStc\">henri (@DooMr) | Twitter</span>  </span>   <span class=\"qXLe6d F9iS2e\">  <span class=\"fYyStc\">twitter.com</span>  </span> </div></a></td></tr></table></div></div> </div> </div></td></tr></table></div><table class=\"uZgmoc\"><tbody><td><a class=\"frGj1b\" href=\"/search?q=doomr&amp;ie=UTF-8&amp;tbm=isch&amp;ei=-6MQX_-oHZCdkwX-6azICA&amp;start=20&amp;sa=N\">Volgende&nbsp;&gt;</a></td></tbody></table><br/><div class=\"TuS8Ad\" data-ved=\"0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQpyoIUg\"><div align=\"center\"><a class=\"rEM8G\" href=\"/url?q=https://accounts.google.com/ServiceLogin%3Fcontinue%3Dhttps://www.google.com/search%253Fq%253Ddoomr%2526tbm%253Disch%26hl%3Dnl&amp;sa=U&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQxs8CCFM&amp;usg=AOvVaw1UhcZffmQTU2OUjYk9udI4\">Inloggen</a></div><div><table class=\"bookcf\"><tbody class=\"InWNIe\"><tr><td><a class=\"rEM8G\" href=\"https://www.google.com/preferences?hl=nl&amp;sa=X&amp;ved=0ahUKEwi_krDRutLqAhWQzqQKHf40C4kQv5YECFQ\">Instellingen</a></td><td><a class=\"rEM8G\" href=\"https://www.google.com/intl/nl_nl/policies/privacy/\">Privacy</a></td><td><a class=\"rEM8G\" href=\"https://www.google.com/intl/nl_nl/policies/terms/\">Voorwaarden</a></td></tr></tbody></table></div></div><div>  </div></body></html>";

            // Act

            List<string> imgsUrls = Utils.parseImgs(testString);
            // Debug.WriteLine(imgsUrls);
            // Assert
            CollectionAssert.AreEqual(expectedUrls, imgsUrls);
        }

        [TestMethod]
        public void parseImgs_emptyStr_EmptyList()
        {
            // arrange
            List<string> expectedUrls = new List<string> { };

            // act
            List<string> imgsUrls = Utils.parseImgs("");

            // assert
            CollectionAssert.AreEqual(expectedUrls, imgsUrls);
        }

        [TestMethod]
        public async void getImageResults_Success()
        {
            List<string> model = new List<string> { };
            var httpClientMock = new Mock<HttpClientAdaptor>();
            httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(""));

            // _httpClientFactory = httpClientMock.Object;

            var client = new HttpClientAdaptor(_httpClientFactory);

            // Assuming doSomething uses the client to make
            // a request for a model of type SomeModelObject
            var results = await Utils.getImageResults("test", client);
            Debug.WriteLine(results);
            CollectionAssert.AreEqual(results, model);
        }
    }

   
}
