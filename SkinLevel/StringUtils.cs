using dc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkinLevel
{
    public static class StringUtils
    {

        public static dc.String ToDCString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            dc.String result = null;
            for (int i = 0; i < input.Length; i++)
            {
                int codePoint;

                // 处理UTF-16代理对（Surrogate Pairs）
                if (char.IsHighSurrogate(input[i]))
                {
                    if (i + 1 >= input.Length || !char.IsLowSurrogate(input[i + 1]))
                    {
                        throw new ArgumentException("Invalid surrogate pair at position " + i);
                    }

                    codePoint = char.ConvertToUtf32(input, i);
                    i++; // 跳过第二个代理字符
                }
                // 处理独立字符
                else
                {
                    codePoint = input[i];
                }

                // 转换为dc.String片段
                dc.String fragment = dc.String.Class.fromCharCode(codePoint);
                if (result == null)
                {
                    result = fragment;
                }
                else
                {
                    result = dc.String.Class.__add__(result, fragment);
                }
            }

            return result ?? dc.String.Class.__alloc__(IntPtr.Zero, 0); // 确保返回有效对象
        }
    }




}
