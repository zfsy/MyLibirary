using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CalculateAppTime
{
    /*如果要测试的多个代码段的函数参数不同，不适合*/
    public class MeasureCodeTime
    {
        private Action runcode = null;

        private TimeSpan codeTimeSpan;
        public double CodeMiliseconds 
        {
            get
            {
                return codeTimeSpan.Milliseconds;
            }
        }

        public MeasureCodeTime(Action codeFunc)
        {
            this.runcode = codeFunc;
        }

        public TimeSpan Run()
        {
            if (runcode != null)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                runcode();
                sw.Stop();

                this.codeTimeSpan = sw.Elapsed;
            }
            else
                throw new Exception("RunCode can't be null");

            return this.codeTimeSpan;
        }
    }
}
