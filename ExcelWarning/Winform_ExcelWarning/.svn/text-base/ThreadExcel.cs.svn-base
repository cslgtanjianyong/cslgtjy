using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace Winform_ExcelWarning
{
    class ThreadExcel
    {
        public delegate void delAddFriend(string friInfo);
        FormMain _ff;
        public ThreadExcel(FormMain f)
        {
            _ff = f;
        }
        public void start()
        {
            while (true)
            {
                string describe = "";
                DateTime startTime = DateTime.Now;
                object[] pars = new object[1];
                syntec.BLL.ExcelRule rulebll = new syntec.BLL.ExcelRule();
                List<syntec.Model.ExcelRule> rulelist = new List<syntec.Model.ExcelRule>();
                DateTime d = DateTime.Now.AddMinutes(-3);
                rulelist = rulebll.GetModelList("nextDoTimeDate<'" + DateTime.Now + "'");
                //list表中有符合的数据 需要做检测了
                if (rulelist.Count != 0)
                {
                    foreach (syntec.Model.ExcelRule rule in rulelist)
                    {
                        //检测成功，要发邮件了亲
                        if (tools.ReadExcelToTable(rule))
                        {
                            //发送邮件
                         
                            string[] emails = rule.email.Split(new[] { "," }, StringSplitOptions.None);
                          
                            syntec.BLL.Log logbll = new syntec.BLL.Log();
                            syntec.Model.Log log = new syntec.Model.Log();
                            string article = "";
                            string[] ss1 = rule.article.Split(new[] { "$" }, StringSplitOptions.None);
                            for (int i = 0; i < ss1.Length; i++) 
                            {
                                string[] position = ss1[i].Split(new[] { "," }, StringSplitOptions.None);
                                if (position.Length == 1 && position[0].Trim() == "") 
                                {
                                   continue;
                                }
                                if (position.Length != 2) 
                                {
                                    article += position[0];
                                    continue;
                                }
                                try
                                {
                                    int hang = Convert.ToInt16(position[0]);
                                    int lei = tools.ToIndex(position[1]);
                                    article += tools.PostionToString(rule,ss1[i]);
                                }
                                catch (Exception ex) 
                                {
                                    article += position;
                                }
                            }

                                foreach (string email in emails)
                                {
                                    if (tools.MailReport(rule.attachmentSource, email, article))
                                    {
                                        log.RuleID = rule.ID;
                                        log.logStartTime = DateTime.Now;
                                        log.logDescribe = describe += "已经发送邮件给" + email + "发送成功"; 
                                        log.StateID = 1;
                                        logbll.Add(log);
                                    }
                                    else
                                    {
                                        log.RuleID = rule.ID;
                                        log.logStartTime = DateTime.Now;
                                        log.logDescribe = describe += "已经发送邮件给" + email + "但是发送失败";
                                        log.StateID = 2;
                                        logbll.Add(log);
                                    }
                                }
                            //填写日志
                            //如果发送邮件成功则state=1，如果发送邮件失败则state=2
                         
                         
                         
                        }
                        rule.nextDoTimeDate = Convert.ToDateTime(tools.getnextToDateTime(rule, false));

                        rulebll.Update(rule);

                    }
                }
                //pars[0] = rulelist.Count.ToString();
                //this._ff.BeginInvoke(new delAddFriend(this._ff.addFriend), pars);
                Thread.Sleep(10 * 1000);
                DateTime endTime = DateTime.Now;
                TimeSpan timespan = endTime.Subtract(startTime);

                if (timespan.Seconds <= 60)
                {
                    try
                    {
                        Thread.Sleep(30 * 1000 - timespan.Seconds * 1000);
                    }
                    catch (Exception ex) 
                    {

                    }
                }
            }
        }
    }
}
