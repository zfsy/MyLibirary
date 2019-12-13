using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibirary
{
    /*
        把阿拉伯数字转化成简体中文数字
        思路：一、将阿拉伯数字转换成字符串，按照从右至左的顺序，根据当位的数字和位数做转换
            问题: 10 需要考虑后面零的个数
                    11  一十一 = 十一

            连续零的情况: 10      十
                            100     一百
                            1001    一千零一
                            1010    一千零一十
            常规用法： 一十 说成 十


        中文数字的零: 
            规则1：以1 0000 为小节，小节的结尾即使是0，也不使用“零”                  
            规则2：小节内两个非0数字之间要使用"零"
            规则3: 当小节的千为0时，若本小节的前一小节无其他数字，则不用“零”，否则用“零”
     */
    public class NumberToChinese
    {
        public readonly static string[] nums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

        public readonly static string[] sections = { "", "万", "亿", "万亿" };

        public readonly static string[] unit = { "", "十", "百", "千" };

        public static string MakeNumberToChinese(int number)
        {
            string result = "";

            int secPos = 0;    // 小节的位置
            bool needZero = false;  //初始默认规则3不需要0

            if (number == 10)      //特殊情况
            {
                result = "十";
                return result;
            }

            if (number == 0)
            {
                result = nums[0];
                return result;
            }

            while (number > 0)
            {
                string secToChn = "";
                int section = number % 10000;
                if (needZero)  // ??
                {
                    result = result.Insert(0, nums[0]);
                }

                secToChn = SectionToChinese(section);
                secToChn += (section != 0) ? sections[secPos] : sections[0];
                result = result.Insert(0, secToChn);

                needZero = (section < 1000) && (section > 0);
                number /= 10000;
                secPos++;
            }

            if (result.StartsWith("一十"))
            {
                result = result.Remove(0, 1);
            }
            return result;
        }

        private static string SectionToChinese(int sectionNum)
        {
            string res = "";
            string strIns = "";
            // 当前小节内的当前个数的独立计数的权位
            int unitPos = 0;
            // 先设置zero为true, 为了测试规则二，两个相连的0只留一个
            bool zero = true;

            while (sectionNum > 0)
            {
                int v = sectionNum % 10;

                if (v == 0 && !zero)   // ??
                {
                    zero = true;
                    res = res.Insert(0, nums[v]);
                }
                else
                {
                    zero = false;
                    strIns = nums[v];
                    strIns += unit[unitPos];
                    res = res.Insert(0, strIns);
                }

                unitPos++;
                sectionNum /= 10;
            }

            return res;
        }
    }
}
