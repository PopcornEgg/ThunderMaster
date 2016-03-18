using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQClient
{
    //这个文件主要管理于服务器数据互交
    public class Table
    {
        //更新账号数据
        public static void UpdateTable_accountex(string qq, object[,] values)
        {
            try
            {
                utitls.SQLUpdateData nvc = new utitls.SQLUpdateData("accountex");
                nvc.setInfo("1");
                utitls.SQLUpdateData.UpdateData data = new utitls.SQLUpdateData.UpdateData();
                data.setTags(new string[] { "qq" }, new Object[] { qq });
                List<String> ks = new List<string>();
                List<Object> vs = new List<Object>();
                string info = "";
                for (int i = 0; i < values.Length / 2; ++i)
                {
                    ks.Add(values[i, 0].ToString());
                    vs.Add(values[i, 1]);

                    info += values[i, 0].ToString() + " = " + values[i, 1].ToString() + "|";
                }
                data.setFeilds(ks.ToArray(), vs.ToArray());
                nvc.addData(data);

                if (nvc != null)
                {
                    string sttuas = utitls.Func.HttpPostData(utitls.SQLUpdateData.URL, 10000, nvc.getPostData());
                    if (sttuas.Contains("_succ_"))
                    {
                        Debug.LogLine(qq + " 上报数据成功(accountex) " + info);
                    }
                    else {
                        Debug.LogLine(qq + sttuas);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.OnException(e);
            }
        }

        //更新好友表
        public static void UpdateTable_friend(string qq, string qqfri,string[,] values)
        {
            try
            {
                utitls.SQLUpdateData nvc = new utitls.SQLUpdateData("friend");
                nvc.setInfo("1");
                
                utitls.SQLUpdateData.UpdateData data = new utitls.SQLUpdateData.UpdateData();
                data.setTags(new string[] { "qq","qqfri" }, new Object[] { qq,qqfri });
                List<String> ks = new List<string>();
                List<Object> vs = new List<Object>();
                string info = "";
                for (int i = 0; i < values.Length / 2; ++i)
                {
                    ks.Add(values[i, 0].ToString());
                    vs.Add(values[i, 1]);
                    info += values[i, 0].ToString() + " = " + values[i, 1].ToString() + "|";
                }
                data.setFeilds(ks.ToArray(), vs.ToArray());
                nvc.addData(data);

                if (nvc != null)
                {
                    string sttuas = utitls.Func.HttpPostData(utitls.SQLUpdateData.URL, 10000, nvc.getPostData());
                    if (sttuas.Contains("_succ_"))
                    {
                        Debug.LogLine(qq + " 上报数据成功(friend) " + info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.OnException(e);
            }
        }

        
        //更新用户服务器
        public static void UpdateTable_user(string qq, string[,] values)
        {
            try
            {
                utitls.SQLUpdateData nvc = new utitls.SQLUpdateData("user");
                nvc.setInfo("1");
               
                utitls.SQLUpdateData.UpdateData data = new utitls.SQLUpdateData.UpdateData();
                data.setTags(new string[] { "qq" }, new Object[] { qq });
                List<String> ks = new List<string>();
                List<Object> vs = new List<Object>();
                string info = "";
                for (int i = 0; i < values.Length / 2; ++i)
                {
                    ks.Add(values[i, 0].ToString());
                    vs.Add(values[i, 1]);
                    info += values[i, 0].ToString() + " = " + values[i, 1].ToString() + "|";
                }
                data.setFeilds(ks.ToArray(), vs.ToArray());
                nvc.addData(data);

                if (nvc != null)
                {
                    string sttuas = utitls.Func.HttpPostData(utitls.SQLUpdateData.URL, 10000, nvc.getPostData());
                    if (sttuas.Contains("_succ_"))
                    {
                        Debug.LogLine(qq + " 上报数据成功(user) " + info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.OnException(e);
            }
        }

        //更新用户服务器
        public static void UpdateTable_accountex_log(string qq, string[,] values)
        {
            try
            {
                utitls.SQLUpdateData nvc = new utitls.SQLUpdateData("accountex_log");
                nvc.setInfo("1");

                utitls.SQLUpdateData.UpdateData data = new utitls.SQLUpdateData.UpdateData();
                data.setTags(new string[] { "qq", "time" }, new Object[] { qq, DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00" });
                List<String> ks = new List<string>();
                List<Object> vs = new List<Object>();
                string info = "";
                for (int i = 0; i < values.Length / 2; ++i)
                {
                    ks.Add(values[i, 0].ToString());
                    vs.Add(values[i, 1]);
                    info += values[i, 0].ToString() + " = " + values[i, 1].ToString() + "|";
                }
                data.setFeilds(ks.ToArray(), vs.ToArray());
                nvc.addData(data);

                if (nvc != null)
                {
                    string sttuas = utitls.Func.HttpPostData(utitls.SQLUpdateData.URL, 10000, nvc.getPostData());
                    if (sttuas.Contains("_succ_"))
                    {
                        Debug.LogLine(qq + " 上报数据成功(accountex_log) " + info);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.OnException(e);
            }
        }
    }
}
