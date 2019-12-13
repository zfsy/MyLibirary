using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibirary
{
    /*
     任意DataType包装成VisualDataItem送入Combox, 显示可见的字符串。
     selectItem转成VisualDataItem取得Tag，即可获得真实值
         */
    public class VisualDataItem<DataType>
    {
        public DataType Tag
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            set;
        }

        public VisualDataItem(DataType tag)
        {
            this.Tag = tag;
            this.Text = tag.ToString();
        }

        public VisualDataItem(DataType tag, string text)
        {
            this.Tag = tag;
            this.Text = text;
        }
        public override string ToString()
        {
            return this.Text;
        }
    }
}
