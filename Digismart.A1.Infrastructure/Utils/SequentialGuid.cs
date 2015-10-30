using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digismart.A1.Infrastructure.Utils
{
    /// <summary>
    /// 结合timestamp/guid, 生成有序的guid, 时间精确到0.1毫秒
    /// </summary>
    public static class SequentialGuid
    {
        private static readonly DateTime BaseDate = new DateTime(1900, 1, 1);
        /// <summary>
        /// 生成有序gud
        /// </summary>
        /// <param name="timeAtFirst">timestamp在前或在后</param>
        /// <returns></returns>
        public static Guid NewGuid(bool timeAtFirst = false)
        {
            //取当前timestamp, 减起始时间, 每个ticks=100纳秒, 除1000, 精确到0.1毫秒
            long time = (long)((DateTime.Now.Ticks - BaseDate.Ticks) / 1000);

            byte[] timeArray = BitConverter.GetBytes(time);
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            if (timeAtFirst)
            { //timestamp在前
                //guid右移6字节, 舍弃原guid中最后6字节
                for (int i = 15; i >= 6; i--)
                {
                    guidArray[i] = guidArray[i - 6];
                }
                //time加到guid前6字节
                Array.Copy(timeArray, 2, guidArray, 0, 2);
                Array.Copy(timeArray, 4, guidArray, 2, 2);
                Array.Copy(timeArray, 0, guidArray, 4, 2);

            }
            else
            { //timestamp在后
                // Reverse the bytes to match SQL Servers ordering
                Array.Reverse(timeArray);
                //time加到guid后6字节
                Array.Copy(timeArray, timeArray.Length - 6, guidArray, guidArray.Length - 6, 6);
            }

            return new Guid(guidArray);
        }
    }
}
