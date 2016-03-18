using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

#region QQ客户端一些通用的接口
namespace Plugins
{
    public class Tools
    {
        //关闭界面
        public static void closeNormalUI(AutomationElement appElement)
        {
            if (appElement != null)
            {
                //关闭
                List<Condition> conditions = new List<Condition>();
                conditions.Clear();
                conditions.Add(new PropertyCondition(AutomationElement.ControlTypeProperty, 0xC350));
                conditions.Add(new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "按钮"));
                AutomationElementCollection items = appElement.FindAll(TreeScope.Descendants, new AndCondition(conditions.ToArray()));
                for (int i = 0; i < items.Count; ++i)
                {
                    LegacyIAccessiblePattern pattern =
                             (LegacyIAccessiblePattern)items[i].GetCurrentPattern(LegacyIAccessiblePattern.Pattern);
                    if (pattern != null && (pattern.Current.Description == "关闭" || pattern.Current.Description == "取消"))
                    {
                        Input.Click(appElement,items[i], false);
                    }
                }
            }
        }

    }
}
#endregion
