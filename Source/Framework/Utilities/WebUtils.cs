using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;

namespace OppenIT.Core.Framework.Utilities
{
    public class WebUtils
    {
        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }
        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 写cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 读cookie值

        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        public static void SetSessionObj(string key, object obj)
        {
            HttpContext.Current.Session[key] = obj;
        }
        public static object GetSessionObj(string key)
        {
            return HttpContext.Current.Session[key];
        }
        public static Control FindContrl(Control parentCtrl,string ctrlID)
        {
            Control ctrl = parentCtrl.FindControl(ctrlID);
            if (ctrl == null)
            {
                foreach (Control subCtrl in parentCtrl.Controls)
                {
                    ctrl = FindContrl(subCtrl,ctrlID);
                    if (ctrl != null) break;
                }
            }
            return ctrl;
        }
        public static Control FindContrl(Page page,string fullCtrlID)
        {
            string[] ctrlIDs = fullCtrlID.Split('.');
            Control ctrl = null, parentCtrl = page;
            for (short i = 0; i < ctrlIDs.Length; i++)
            {
                ctrl = FindContrl(parentCtrl, ctrlIDs[i]);
                if (ctrl != null && i < ctrlIDs.Length)
                    parentCtrl = ctrl;
            }
            return ctrl;
        }
        public static void WriteEscapedJavaScriptString(TextWriter writer, string value, char delimiter, bool appendDelimiters)
        {
            // leading delimiter
            if (appendDelimiters)
                writer.Write(delimiter);

            if (value != null)
            {
                int lastWritePosition = 0;
                int skipped = 0;
                char[] chars = null;

                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];
                    string escapedValue;

                    switch (c)
                    {
                        case '\t':
                            escapedValue = @"\t";
                            break;
                        case '\n':
                            escapedValue = @"\n";
                            break;
                        case '\r':
                            escapedValue = @"\r";
                            break;
                        case '\f':
                            escapedValue = @"\f";
                            break;
                        case '\b':
                            escapedValue = @"\b";
                            break;
                        case '\\':
                            escapedValue = @"\\";
                            break;
                        case '\u0085': // Next Line
                            escapedValue = @"\u0085";
                            break;
                        case '\u2028': // Line Separator
                            escapedValue = @"\u2028";
                            break;
                        case '\u2029': // Paragraph Separator
                            escapedValue = @"\u2029";
                            break;
                        case '\'':
                            // only escape if this charater is being used as the delimiter
                            escapedValue = (delimiter == '\'') ? @"\'" : null;
                            break;
                        case '"':
                            // only escape if this charater is being used as the delimiter
                            escapedValue = (delimiter == '"') ? "\\\"" : null;
                            break;
                        default:
                            escapedValue = (c <= '\u001f') ? StringUtils.ToCharAsUnicode(c) : null;
                            break;
                    }

                    if (escapedValue != null)
                    {
                        if (chars == null)
                            chars = value.ToCharArray();

                        // write skipped text
                        if (skipped > 0)
                        {
                            writer.Write(chars, lastWritePosition, skipped);
                            skipped = 0;
                        }

                        // write escaped value and note position
                        writer.Write(escapedValue);
                        lastWritePosition = i + 1;
                    }
                    else
                    {
                        skipped++;
                    }
                }

                // write any remaining skipped text
                if (skipped > 0)
                {
                    if (lastWritePosition == 0)
                        writer.Write(value);
                    else
                        writer.Write(chars, lastWritePosition, skipped);
                }
            }

            // trailing delimiter
            if (appendDelimiters)
                writer.Write(delimiter);
        }

        public static string ToEscapedJavaScriptString(string value)
        {
            return ToEscapedJavaScriptString(value, '"', true);
        }

        public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
        {
            using (StringWriter w = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
            {
                WriteEscapedJavaScriptString(w, value, delimiter, appendDelimiters);
                return w.ToString();
            }
        }
        public static string ClearHTMLTag(string html, string tagWord)
        {
            string findPattern = @"<\s*" + tagWord + @"\b.*?>(?<innerText>.*?)</\s*" + tagWord + @"\s*>";
            string replacePattern = "${innerText}";
            Regex rgx = new Regex(findPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return rgx.Replace(html, replacePattern);
        }
        public static string DeleteHTMLElement(string html, string tagWord)
        {
            Regex rgx = new Regex(@"<\s*" + tagWord + @"\b.*?(/>|>.*</\s*" + tagWord + @"\s*>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return rgx.Replace(html, "");
        }
        public static string ConvertHTMLInputFieldToText(string html)
        {
            string[] findPatterns = new string[] { @"<select\b.*?<option.*?\bselected\b.*?>(?<SelValue>.*?)</option>.*?</select>", 
@"<\s*input\s[^>]*type=(['\x22]?)text\1[^>]*\bvalue=(['\x22]?)(?<TxtValue>.*?)\2.*?(/>|>\s*</\s*input\s*>)", 
@"<\s*input\b.*?(/>|>.*</\s*input\s*>)" };
            string[] replacePatterns = new string[] { "${SelValue}", "${TxtValue}", "" };
            for (int i = 0; i < findPatterns.Length; i++)
            {
                html = Regex.Replace(html, findPatterns[i], replacePatterns[i], RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            return html;
        }
    }
}
