using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Threading;
using Plugins;

namespace ThunderMaster
{
    public partial class MainForm : Form
    {
        public static bool isRunning = false;
        public delegate void ProcessDelegate();

        public MainForm()
        {
            InitializeComponent();

            label1.Text = "账号加载中...";
            label2.Text = "";
            Input.Clipboard("test");
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                Thread thread = new Thread(new ThreadStart(LoginThread));
                thread.Start();
                isRunning = true;
            }
        }

        public void UpdateAccountShow()
        {
            label1.Text = "账号:" + thunderAccount.CurAccount.account;
            label2.Text = "密码:" + thunderAccount.CurAccount.password;
        }

        private void LoginThread()
        {
            FirstLogin();
            //首先检查迅雷开启没有
            while (isRunning)
            {
                this.Invoke(new ProcessDelegate(UpdateAccountShow));
                try
                {
                    List<Condition> conditions = new List<Condition>();
                    conditions.Clear();
                    conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC370));
                    conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "窗口"));
                    AutomationElementCollection items = AutomationElement.RootElement.FindAll(TreeScope.Children, 
                        new AndCondition(conditions.ToArray()));
                    if (items != null)
                    {
                        AutomationElement appElement = null;
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i].Current.Name.Contains("迅雷7") &&
                                items[i].Current.Name.Contains("迅雷7") &&
                                !items[i].Current.Name.Contains("登录"))
                            {
                                appElement = items[i];
                                break;
                            }
                            if (items[i].Current.Name == "迅雷7")
                            {
                                conditions.Clear();
                                conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC371));
                                conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "窗格"));
                                conditions.Add(new PropertyCondition(AutomationElement.NameProperty, "MessageBox"));
                                AutomationElement tag = items[i].FindFirst(TreeScope.Children,
                                    new AndCondition(conditions.ToArray()));
                                if(tag != null)
                                {
                                    Input.Click(tag, tag, 330, 170, false);
                                    Thread.Sleep(2000);
                                    FirstLogin();
                                    break;
                                }
                            }
                        }
                        if (appElement != null)
                        {
                            conditions.Clear();
                            conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC371));
                            conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "窗格"));
                            conditions.Add(new PropertyCondition(AutomationElement.NameProperty, "迅雷7登录"));

                            AutomationElement loginElement = appElement.FindFirst(TreeScope.Children, new AndCondition(conditions.ToArray()));
                            if (loginElement != null)
                            {
                                inputLoginInfo(loginElement);
                            }
                        }
                        else {
                            //webBrowser1.Document.Write("未找到迅雷7窗口!<br/>");
                        }
                    }
                }
                catch (Exception ecp)
                {
                    Plugins.Debug.OnException(ecp);
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        private void FirstLogin()
        {
            Time time = new Time();
            time.Start(0,0,10);
            while (true)
            {
                if (time.End())
                {
                    return;
                }
                try{
                    List<Condition> conditions = new List<Condition>();
                    conditions.Clear();
                    conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC370));
                    conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "窗口"));
                    AutomationElementCollection items = AutomationElement.RootElement.FindAll(TreeScope.Children, new AndCondition(conditions.ToArray()));
                    if (items != null)
                    {
                        AutomationElement appElement = null;
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i].Current.Name.Contains("迅雷7") && !items[i].Current.Name.Contains("登录"))
                            {
                                appElement = items[i];
                                break;
                            }
                        }
                        if (appElement != null)
                        {
                            Input.Click(appElement, appElement, 60, 60, false);
                            Thread.Sleep(2000);

                            conditions.Clear();
                            conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC371));
                            conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "窗格"));
                            conditions.Add(new PropertyCondition(AutomationElement.NameProperty, "迅雷7登录"));

                            AutomationElement loginElement = appElement.FindFirst(TreeScope.Children, new AndCondition(conditions.ToArray()));
                            if (loginElement != null)
                            {
                                inputLoginInfo(loginElement);
                                break;
                            }
                        }
                        else
                        {
                            //webBrowser1.Document.Write("未找到迅雷7窗口!<br/>");
                        }
                    }
                }
                catch (Exception ecp)
                {
                    Plugins.Debug.OnException(ecp);
                }
                System.Threading.Thread.Sleep(500);
            }
        }
        private void inputLoginInfo(AutomationElement loginElement)
        {
            thunderAccount _account = thunderAccount.NextAccount;
            //1. 设置账号
            Input.Click(loginElement, loginElement, 250, 210, true);
            for (int i = 0; i < 20; ++i)
                Input.KeybdClick(Input.VK_BACK);
            char[] cacc = _account.account.ToCharArray();
            for (int i = 0; i < cacc.Length; ++i)
            {
                if (Console.CapsLock)
                    Input.KeybdClick((byte)Keys.CapsLock);
                if (cacc[i] == ':')
                {
                    Input.KeybdClick((byte)Keys.CapsLock);
                    Input.KeybdClickTow((byte)Keys.LShiftKey, (byte)Input.VkKeyScan(cacc[i]));
                }
                else
                    Input.KeybdClick((byte)Input.VkKeyScan(cacc[i]));
            }
            
            //2. 设置密码
            Input.Click(loginElement, loginElement, 100, 100, false);
            System.Threading.Thread.Sleep(1000);
            Input.Click(loginElement, loginElement, 250, 235, false);
            for (int i = 0; i < 20; ++i)
                Input.KeybdClick(Input.VK_BACK);
            char[] cpws = _account.password.ToCharArray();
            for (int i = 0; i < cpws.Length; ++i)
            {
                if (Console.CapsLock)
                    Input.KeybdClick((byte)Keys.CapsLock);
                Input.KeybdClick((byte)Input.VkKeyScan(cpws[i]));
            }

            Input.Click(loginElement, loginElement, 350, 310, false);
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            isRunning = false;
        }

        //数据
        public class thunderAccount
        {
            public String account = "";
            public String password = "";

            public static int curState = 0;
            static thunderAccount curAccount = null;
            public static thunderAccount CurAccount
            {
                get
                {
                    if (curAccount == null)
                        curAccount = Get();
                    return curAccount;
                }
                set
                {
                    curAccount = value;
                }
            }
            public static thunderAccount NextAccount
            {
                get
                {
                    curAccount = Get();
                    return curAccount;
                }
            }
            static List<thunderAccount> datas = new List<thunderAccount>();
            public static void Add(String _a, String _p)
            {
                thunderAccount data = new thunderAccount();
                data.account = _a;
                data.password = _p;
                datas.Add(data);
            }
            public static thunderAccount Get()
            {
                Random random = new Random();
                int idx = random.Next(0, datas.Count);
                thunderAccount account = datas[idx];
                datas.RemoveAt(idx);
                return account;
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete &&
                webBrowser1.ReadyState != WebBrowserReadyState.Interactive)
                return;

            if (analyzeThunderAccounts() == 0)
                gotoThunderAccounts();
        }
        private void gotoThunderAccounts()
        {
            foreach (HtmlElement he in webBrowser1.Document.GetElementsByTagName("h2"))
            {
                DateTime time = DateTime.Now;
                String tag = String.Format("{0}月{1}日", time.Month, time.Day);
                //tag = "11月14日";
                if (he.InnerText == tag + " 迅雷会员vip账号分享 迅雷白金会员子账号")
                {
                    foreach (HtmlElement he2 in he.Document.GetElementsByTagName("a"))
                    {
                        if (he2.InnerText == tag + " 迅雷会员vip账号分享 迅雷白金会员子账号")
                        {
                            String href = he2.GetAttribute("href");
                            webBrowser1.Navigate(href);
                            return;
                        }
                    }
                }
            }
        }
        private int analyzeThunderAccounts()
        {
            if (thunderAccount.curState == 1)
                return 1;
            foreach (HtmlElement he in webBrowser1.Document.GetElementsByTagName("p"))
            {
                String tag = "VIP分享网独家迅雷vip账号";
                if (!String.IsNullOrEmpty(he.InnerText) && he.InnerText.Contains(tag))
                {

                    String[] ps = he.InnerText.Split(new char[] { '\r', '\n' });
                    if (ps != null)
                    {
                        for (int i = 0; i < ps.Length; i++)
                        {
                            string oneacc = ps[i];
                            if (String.IsNullOrEmpty(oneacc))
                                continue;
                            int idx1 = oneacc.IndexOf("账号") + 2;
                            int idx2 = oneacc.IndexOf("密码");
                            String acc = oneacc.Substring(idx1, idx2 - idx1);
                            String psw = oneacc.Substring(idx2 + 2);
                            thunderAccount.Add(acc, psw);
                        }
                        thunderAccount.curState = 1;
                        label1.Text = "加载完成";
                        webBrowser1.Document.Write("加载完成<br/>");
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}
