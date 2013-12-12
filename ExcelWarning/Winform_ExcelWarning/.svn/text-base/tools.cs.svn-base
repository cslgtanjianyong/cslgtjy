using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;
using System.Threading;
using System.Net.Mail;
using Syntec.Notifier;

namespace Winform_ExcelWarning
{
    class tools
    {
        //数据库连接字符串
        
        public static bool IsStart = false;
        public static bool IsClick = false;
        public static bool Isupdate = false;
        public static Thread thread;
        public static DataTable GetExcelTableName(string p_ExcelFile)
        {
            try
            {
                if (System.IO.File.Exists(p_ExcelFile))
                {
                    OleDbConnection _ExcelConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + p_ExcelFile + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
                    _ExcelConn.Open();
                    DataTable _Table = _ExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    _ExcelConn.Close();
                    return _Table;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool MailReport(string strAttachment, string strAddress,string content)
        {
            string strContent = content;
            string strMailTo = strAddress;

            try
            {
                int iTitleStart=0;
                int iTitleEnd = 0;
                if (strAttachment.Trim() == "")
                {
                     iTitleStart = strAttachment.LastIndexOf(@"\") + 1;
                     iTitleEnd = strAttachment.LastIndexOf(".");
                }
                
                INotifier mailNotifier = new MailNotifier();
                mailNotifier.SetParam("Server", "msa.syntecclub.com.tw");
                mailNotifier.SetParam("Account", "IT@syntecclub.com.tw");
                mailNotifier.SetParam("Port", "25");
                mailNotifier.SetParam("Password", "syntecclubitit");
                mailNotifier.Content = strContent;
                if (strAttachment.Trim() != "")
                {
                    mailNotifier.Title = strAttachment.Substring(iTitleStart, iTitleEnd - iTitleStart);
                    mailNotifier.SetParam("Attachment", strAttachment);
                }
                mailNotifier.UserName = strMailTo;

                if (mailNotifier.Notify() == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool HasEmail(string source)
        {

            return

            Regex.IsMatch(source,

            @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})",

            RegexOptions.IgnoreCase);

        }
        public void getNumber()
        {
            Console.WriteLine("请输入要提取的字符串：");
            string str = "10分钟";
            int number = 0; string num = null;
            foreach (char item in str)
            {
                if (item >= 48 && item <= 58)
                {
                    num += item;
                }
            }
            number = int.Parse(num);
            Console.WriteLine(number);
        }
        public static bool validateNum(string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }
        public static bool validateString(string str)
        {
            return Regex.IsMatch(str, "^[A-Za-z]*$");
        }
        //第一个参数d1的年月日和d2的时分秒结合
        public static DateTime AddTwoDate(DateTime d1, DateTime d2)
        {
            DateTime t = new DateTime();
            t = t.AddYears(d1.Year - 1);
            t = t.AddMonths(d1.Month - 1);
            t = t.AddDays(d1.Day - 1);
            t = t.AddHours(d2.Hour);
            t = t.AddMinutes(d2.Minute);
            t = t.AddSeconds(d2.Second);
            return t;
        }
        //第一个参数d1的年月日和t1的时分秒结合
        public static DateTime AddTwoDate(DateTime d1, TimeSpan d2)
        {
            DateTime t = new DateTime();
            t = t.AddYears(d1.Year - 1);
            t = t.AddMonths(d1.Month - 1);
            t = t.AddDays(d1.Day - 1);
            t = t.AddHours(d2.Hours);
            t = t.AddMinutes(d2.Minutes);
            t = t.AddSeconds(d2.Seconds);
            return t;
        }

        //最重要的方法，把model传入，传出下一次执行的时间，如果返回值为NULL则规则已经失效
        public static Object getnextToDateTime(syntec.Model.ExcelRule rule, bool IsFirst)
        {
            DateTime nextToDateTime = new DateTime();
            DateTime nullTime = new DateTime();
            //现在的系统时间大于规则制定开始时间，从现在的系统时间开始计时
            if (IsFirst)
            {
                //如果规则的每日频率不为空，则第一次执行为他的开始执行时间
                if (rule.timeType == "D")
                {
                    //指的是一天只执行一次！！！
                    if (rule.everydayFreSpace == null)
                    {
                        DateTime d = AddTwoDate(rule.startDate, rule.startTime);
                        if (rule.endDate == nullTime && d >= rule.startDate)
                        {
                            return d;
                        }
                        if (d >= rule.startDate && d <= rule.endDate)
                        {
                            return d;
                        }
                        else
                        {
                            d = d.AddDays(1);
                            if (rule.endDate == nullTime)
                            {
                                return d;
                            }
                            if (d >= rule.startDate && d <= rule.endDate)
                            {
                                return d;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return rule.startDate;
                    }
                }
                if (rule.timeType == "W")
                {

                    string s = rule.timeSpace;
                    string[] ss1 = s.Split(new[] { "," }, StringSplitOptions.None);
                    DateTime d = AddTwoDate(rule.startDate, rule.startTime);
                    if (rule.everydayFreSpace == null)
                    {
                        if (rule.endDate == nullTime)
                        {
                            for (int i = 0; i <= 7; i++)
                            {
                                if (DayofWeekIsSuitable(ss1, d.AddDays(i).DayOfWeek) && d.AddDays(i) >= rule.startDate)
                                {
                                    return d.AddDays(i);
                                }
                            }
                            return null;
                        }
                        if (d >= rule.startDate && d <= rule.endDate)
                        {
                            return d;
                        }
                        else
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                d.AddDays(i);
                                if (DayofWeekIsSuitable(ss1, d.AddDays(i).DayOfWeek) && d.AddDays(i) <= rule.endDate)
                                {
                                    return d.AddDays(i);
                                }
                            }
                            return null;
                        }

                    }
                    else{
                        for (int i = 0; i <= 7; i++)
                          {
                            DateTime testTime = new DateTime();
                            testTime = rule.startDate.AddDays(i);
                            if (testTime > rule.endDate) 
                            {
                                return null;
                            }
                            if (DayofWeekIsSuitable(ss1, testTime.DayOfWeek) && rule.everydayFreSpace != null)
                            {
                                if (testTime.Date >= rule.startDate)
                                {
                                    return ClearHMS(testTime);
                                }
                                else 
                                {
                                    return testTime;
                                }
                            }
                            if (DayofWeekIsSuitable(ss1, testTime.DayOfWeek) && AddTwoDate(testTime, rule.startTime) >= rule.startDate)
                            {
                                nextToDateTime = AddTwoDate(testTime, rule.startDate);
                                if (rule.endDate == nullTime)
                                {
                                    if (rule.everydayFreSpace == null)
                                    {
                                        return ClearHMS(nextToDateTime);
                                    }
                                    else
                                    {
                                        return nextToDateTime;
                                    }
                                }

                                if (nextToDateTime > rule.endDate)
                                {
                                    return null;
                                }
                                if (nextToDateTime >= rule.startDate)
                                {
                                    if (rule.everydayFreSpace == null)
                                    {
                                        return ClearHMS(nextToDateTime);
                                    }
                                    else
                                    {
                                        return nextToDateTime;
                                    }
                                }
                            }
                        }
                    }
                }

                if (rule.timeType == "M")
                {
                    string[] ss1 = rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                    int day = Convert.ToInt16(ss1[0]);
                    int month = Convert.ToInt16(ss1[1]);
                    DateTime testTime = new DateTime();
                    testTime = AddTwoDate(rule.startDate, rule.startTime);
                    testTime = testTime.AddDays(testTime.Day * -1 + day);
                    if (rule.everydayFreSpace == null)
                    {
                        DateTime d = new DateTime();
                        d = AddTwoDate(rule.startDate, rule.startTime);
                        if (d.Day == day && d >= rule.startDate && d <= rule.endDate)
                        {
                            return d;
                        }
                        else
                        {
                            DateTime dd = d.AddMonths(1);
                            dd = dd.AddDays(dd.Day * -1 + day);
                            if (rule.endDate == nullTime)
                            {
                                return dd;
                            }
                            if (dd > rule.endDate)
                            {
                                return null;
                            }
                            return dd;

                        }
                    }
                    if (testTime.Day == day && testTime <= rule.startDate)
                    {
                        return rule.startDate;
                    }


                    if (testTime <= rule.startDate)
                    {
                        testTime = testTime.AddMonths(1);
                    }
                    else
                    {
                        return testTime;
                    }
                    if (rule.everydayFreSpace == null)
                    {
                        testTime = ClearHMS(testTime);
                    }

                    if (rule.endDate == nullTime)
                    {
                        return testTime;
                    }

                    if (testTime > rule.endDate)
                    {
                        return null;
                    }
                    else
                    {
                        return AddTwoDate(testTime, rule.startDate);
                    }

                }

            }
            //修改后才用到的
            else
            {
                DateTime nextTime = Convert.ToDateTime(rule.nextDoTimeDate);
                if (rule.planType.Trim() == "once")
                {
                    return null;
                }
                else
                {
                    //以天为单位
                    if (rule.timeType.Trim() == "D")
                    {
                        if (rule.everydayFreSpace.Trim().Length == 0)
                        {
                            int dayspace = Convert.ToInt32(rule.timeSpace);

                            nextTime = nextTime.AddDays(dayspace);
                            if (rule.endDate == nullTime)
                            {
                                return nextTime;
                            }
                            if (nextTime > rule.endDate && rule.endDate != nullTime)
                            {
                                return null;
                            }
                            else
                            {
                                return nextTime;
                            }
                        }
                        else
                        {
                            //是以小时为单位
                            if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                            {
                                nextTime = nextTime.AddHours(GetNumberInt(rule.everydayFreSpace));
                            }
                            else
                            {
                                nextTime = nextTime.AddMinutes(GetNumberInt(rule.everydayFreSpace));
                            }
                            if (rule.endDate == nullTime)
                            {
                                return nextTime;
                            }
                            if (nextTime > rule.endDate)
                            {
                                return null;
                            }
                            else
                            {
                                return nextTime;
                            }
                        }
                    }
                    //以周为单位
                    if (rule.timeType.Trim() == "W")
                    {
                        string[] ss1 = rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                        if (rule.everydayFreSpace == "")
                        {
                            for (int i = 1; i <= 7; i++)
                            {

                                if (DayofWeekIsSuitable(ss1, nextTime.AddDays(i).DayOfWeek))
                                {
                                    TimeSpan time = new TimeSpan();
                                    time = DateTime.Now.Date - rule.startDate.Date;
                                   
                                        if (rule.endDate == nullTime)
                                        {
                                            return nextTime.AddDays(i+(Convert.ToInt16(ss1[0])-1)*7);
                                        }
                                        if (nextTime.AddDays(i + (Convert.ToInt16(ss1[0]) - 1) * 7) > rule.endDate)
                                        {
                                            return null;
                                        }
                                        else
                                        {
                                            return nextTime.AddDays(i + (Convert.ToInt16(ss1[0]) - 1) * 7);
                                        }
                                    
                                }
                            }
                        }
                        else
                        {

                            if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                            {
                                nextTime = nextTime.AddHours(GetNumberInt(rule.everydayFreSpace));

                            }
                            else
                            {
                                nextTime = nextTime.AddMinutes(GetNumberInt(rule.everydayFreSpace));
                            }
                            if (DayofWeekIsSuitable(ss1, nextTime.DayOfWeek))
                            {
                                return nextTime;
                            }
                            else
                            {
                                for (int i = 1; i <= 7; i++)
                                {

                                    if ((nextTime.AddDays(i).Date - rule.startDate.Date).Days <= 6)
                                    {
                                        DateTime test = ClearHMS(nextTime.AddDays(i));
                                        if (rule.endDate == nullTime && DayofWeekIsSuitable(ss1, test.DayOfWeek))
                                        {
                                            return test;
                                        }
                                        if (DayofWeekIsSuitable(ss1, test.DayOfWeek) && test < rule.endDate)
                                        {
                                            return test;
                                        }
                                    }
                                    else 
                                    {
                                        DateTime test = ClearHMS(nextTime.AddDays(i + (Convert.ToInt16(ss1[0]) - 1) * 7));
                                        if (rule.endDate == nullTime && DayofWeekIsSuitable(ss1, test.DayOfWeek))
                                        {
                                            return test;
                                        }
                                        if (DayofWeekIsSuitable(ss1, test.DayOfWeek) && test < rule.endDate)
                                        {
                                            return test;
                                        }
                                    }
                                }
                                return null;
                            }
                        }
                    }
                    //以月为单位
                    if (rule.timeType.Trim() == "M")
                    {
                        string[] ss1 = rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                        int day = Convert.ToInt16(ss1[0]);
                        int month = Convert.ToInt16(ss1[1]);
                        if (rule.everydayFreSpace.Length == 0)
                        {
                            nextTime = nextTime.AddMonths(month);
                            nextTime = nextTime.AddDays(nextTime.Day * -1 + day);
                            if (nextTime > rule.endDate)
                            {
                                return null;
                            }
                            else
                            {
                                return nextTime;
                            }
                        }
                        else
                        {
                            if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                            {
                                nextTime = nextTime.AddHours(GetNumberInt(rule.everydayFreSpace));

                            }
                            else
                            {
                                nextTime = nextTime.AddMinutes(GetNumberInt(rule.everydayFreSpace));
                            }
                            if (nextTime.Day == day)
                            {
                                return nextTime;
                            }
                            else
                            {
                                nextTime = ClearHMS(nextTime);
                                nextTime = nextTime.AddMonths(month);
                                nextTime = nextTime.AddDays(nextTime.Day * -1 + day);
                                if (rule.endDate == nullTime)
                                {
                                    return nextTime;
                                }
                                if (nextTime > rule.endDate)
                                {
                                    return null;
                                }
                                else
                                {
                                    return nextTime;
                                }
                            }
                        }
                    }
                }
            }
            return nextToDateTime;
        }











        public static int GetNumberInt(string str)
        {
            int result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）

                str = Regex.Replace(str, @"[^\d.\d]", "");

                // 如果是数字，则转换为decimal类型

                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {

                    result = int.Parse(str);

                }

            }

            return result;

        }
        //传入字符串数组以及日期，判断是否符合
        public static bool DayofWeekIsSuitable(string[] a, DayOfWeek d)
        {
            bool flag = false;
            foreach (string b in a)
            {
                if (d.ToString() == b)
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }
        //




        public static void KillProcess(string ProcessName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(ProcessName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
               
            }
        }
        public static int ToIndex(string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+")) { throw new Exception("invalid parameter"); }
            int index = 0;
            char[] chars = columnName.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index - 1;
        }
        public static DateTime ClearHMS(DateTime nextToDateTime)
        {
            nextToDateTime = nextToDateTime.AddHours(nextToDateTime.Hour * -1);
            nextToDateTime = nextToDateTime.AddMinutes(nextToDateTime.Minute * -1);
            nextToDateTime = nextToDateTime.AddSeconds(nextToDateTime.Second * -1);
            return nextToDateTime;
        }
        public static string PostionToString(syntec.Model.ExcelRule rule, string position) 
        {
            string excelfilePath = rule.reportSource;
            Excel.Application myExcel = new Excel.ApplicationClass();
            Excel.Workbooks myBooks = myExcel.Application.Workbooks;
            object oMissing = System.Reflection.Missing.Value;

            Excel.Workbook myBook = myBooks.Open(excelfilePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            //Excel.Workbook myBook = myExcel.Workbooks[1];
            int sheetint = myBook.Worksheets.Count;//能得到sheet的数量


            Excel.Worksheet mySheet = null;
            for (int i = 1; i <= sheetint; i++)
            {
                Excel.Worksheet sheet = (Excel.Worksheet)myBook.Worksheets[i];
                sheet.Name += "$";
                string sheet2 = "'" + sheet.Name + "'";
                if (sheet.Name == rule.sheetName || sheet2 == rule.sheetName)
                {
                    mySheet = sheet;
                }
            }


            string[] ss1 = position.Split(new[] { "," }, StringSplitOptions.None);
            if (ss1.Length == 1) 
            {
                return null;
            }
            int hang = Convert.ToInt32(ss1[0]);
            int lei = Convert.ToInt32(ToIndex(ss1[1])) + 1;
            Excel.Range skoitem = (Excel.Range)mySheet.Cells[hang, lei];
            if (skoitem == null)
            {
                return null;
            }
            string a = skoitem.Text.ToString();
            return a;
        }
        //传入rule对象传出是否符合该规则，符合传出True，不符合传出False
        public static bool ReadExcelToTable(syntec.Model.ExcelRule rule)
        {
            string excelfilePath = rule.reportSource;
            Excel.Application myExcel = new Excel.ApplicationClass();
            Excel.Workbooks myBooks = myExcel.Application.Workbooks;
            object oMissing = System.Reflection.Missing.Value;
            
            Excel.Workbook myBook = myBooks.Open(excelfilePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            //Excel.Workbook myBook = myExcel.Workbooks[1];
            int sheetint = myBook.Worksheets.Count;//能得到sheet的数量

            
            Excel.Worksheet mySheet = null;
            for (int i = 1; i <= sheetint; i++)
            {
                Excel.Worksheet sheet = (Excel.Worksheet)myBook.Worksheets[i];
                sheet.Name += "$";
                string sheet2 = "'" + sheet.Name + "'";
                if (sheet.Name == rule.sheetName || sheet2 == rule.sheetName)
                {
                    mySheet = sheet;
                }
            }


            string[] ss1 = rule.position.Split(new[] { "," }, StringSplitOptions.None);
            int hang = Convert.ToInt32(ss1[0]);
            int lei = Convert.ToInt32(ToIndex(ss1[1])) + 1;
            Excel.Range skoitem = (Excel.Range)mySheet.Cells[hang, lei];
            if (skoitem == null) 
            {
                return false;
            }
            string a = skoitem.Text.ToString();

            myExcel.DisplayAlerts = false;
            myExcel.AlertBeforeOverwriting = false;
            if (myBook != null) myBook.Close(oMissing, oMissing, oMissing);
            if (myExcel.Workbooks != null) myExcel.Workbooks.Close();
            if (myExcel != null) myExcel.Quit();
        
            myExcel = null;
           
            GC.Collect();
            GC.WaitForPendingFinalizers();
            KillProcess("Excel");


            //保存到指定目录 

            

            try
            {
                if (rule.symbol.ToString() == ">")
                {
                    if (Convert.ToDouble(a) > Convert.ToDouble(rule.aim))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == ">=")
                {
                    if (Convert.ToDouble(a) >= Convert.ToDouble(rule.aim))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == "<=")
                {
                    if (Convert.ToDouble(a) <= Convert.ToDouble(rule.aim))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == "<")
                {
                    if (Convert.ToDouble(a) < Convert.ToDouble(rule.aim))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == "=")
                {
                    if (Convert.ToDouble(a) == Convert.ToDouble(rule.aim.ToString().Trim()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == "!=")
                {
                    if (Convert.ToDouble(a) != Convert.ToDouble(rule.aim))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (rule.symbol.ToString() == "in")
                {
                    int n = a.IndexOf(rule.aim);
                    if (n == -1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                if (rule.symbol.ToString() == "not in")
                {
                    int n = a.IndexOf(rule.aim);
                    if (n == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }


        public static string returndate(string[] date)
        {
            string a = "";
            foreach (string b in date)
            {
                if (b == "Monday")
                {
                    a += ",星期一";
                }
                if (b == "Tuesday")
                {
                    a += ",星期二";
                }
                if (b == "Wednesday")
                {
                    a += ",星期三";
                }
                if (b == "Thursday")
                {
                    a += ",星期四";
                }
                if (b == "Friday")
                {
                    a += ",星期五";
                }
                if (b == "Saturday")
                {
                    a += ",星期六";
                }
                if (b == "Sunday")
                {
                    a += ",星期天";
                }
            }
            return a;
        }
        public static string describe(syntec.Model.ExcelRule rule)
        {
            string a = "";
            if (rule.planType.Trim() == "once")
            {
                a = "只执行一次，执行时间为" + AddTwoDate(rule.startDate, rule.startTime);
            }
            else
            {
                if (rule.timeType == "D")
                {
                    if (rule.everydayFreSpace.Length == 0)
                    {
                        a = "每隔" + rule.timeSpace.ToString() + "天,执行，每天只执行一次，时间为当天的" + rule.startTime.ToString();
                    }
                    else
                    {
                        GetNumberInt(rule.everydayFreSpace);
                        string type = "";
                        if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                        {
                            type = "小时";
                        }
                        else
                        {
                            type = "分钟";
                        }
                        a = "每隔" + rule.timeSpace.ToString() + "天,执行，每天执行多次，每隔" + GetNumberInt(rule.everydayFreSpace).ToString() + type + "就执行一次";
                    }
                }
                if (rule.timeType == "W")
                {

                    string[] timespace = rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                    if (rule.everydayFreSpace == "")
                    {
                        a = "每隔" + timespace[0] + "周,执行" + returndate(timespace) + "才会执行，每天执行一次，执行时间为当天的" + rule.startTime.ToString();
                    }
                    else
                    {
                        string type = "";
                        if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                        {
                            type = "小时";
                        }
                        else
                        {
                            type = "分钟";
                        }
                        a = "每隔" + timespace[0] + "周执行，每天执行多次" + returndate(timespace) + "才会执行，每隔" + GetNumberInt(rule.everydayFreSpace).ToString() + type + "就执行一次";
                    }

                }
                if (rule.timeType == "M")
                {

                    string[] timespace = rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                    if (rule.everydayFreSpace.Length == 0)
                    {
                        a = "每隔" + timespace[0] + "月的第" + timespace[1] + "天才会执行，每天执行一次，执行时间为当天的" + rule.startTime.ToString();
                    }
                    else
                    {
                        GetNumberInt(rule.everydayFreSpace);
                        string type = "";
                        if (rule.everydayFreSpace.Substring(rule.everydayFreSpace.Length - 1) == "u")
                        {
                            type = "小时";
                        }
                        else
                        {
                            type = "分钟";
                        }
                        a = "每隔" + timespace[1] + "月的第" + timespace[0] + "天才会执行，每天执行多次，每隔" + GetNumberInt(rule.everydayFreSpace).ToString() + type + "就执行一次";
                    }

                }
            }
            return a;
        }

    }
}
