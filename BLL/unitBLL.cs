using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saivian.BLL
{
   public  class unitBLL
    {
       public static int MathRandBetween(int digit, int begin, int end)
       {
           string Num = "";
           Random random = null;
           while (Num == "")
           {
               for (var i = 0; i < digit; i++)
               {
                   random = new Random();
                   Num += random.Next(10).ToString();
               }
               if (int.Parse(Num) > begin && int.Parse(Num) < end)
                   Num = Num.Length < 2 ? '0'+Num : Num;
               else
                   Num = "";
           }
           return int.Parse(Num);
       }

       public static string GetRandByNum(int begin, int end)
       {
           int tempNum = new Random().Next(begin, end);
           return tempNum < 10 ? "0" + tempNum : tempNum.ToString();
       }
    }
}
