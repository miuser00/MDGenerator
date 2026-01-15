using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MDLoader
{
    class SetWebbrowser
    {
        /// <summary>
        /// 修改Webbrowser控件模拟的IE版本
        /// </summary>
        /// <param name="ieMode">
        /// 7000: Pages containing standards-based <!DOCTYPE> directives aredisplayed in IE7 mode.
        /// 8000: Pages containing standards-based <!DOCTYPE> directives aredisplayed in IE8 mode
        /// 8888: Pages are always displayed in IE8mode, regardless of the <!DOCTYPE>directive. (This bypasses the exceptions listed earlier.)
        /// 9000: Use IE9 settings!
        /// 9999: Force IE9
        /// 10000: Use IE10 settings
        /// 11000: Use IE11 settings
        /// </param>
        public static bool ChangeWebbrowserMode(int ieMode)
        {
            string appName = AppDomain.CurrentDomain.FriendlyName;
            string regPath = "";

                regPath = @"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION";
                using (RegistryKey ieMainKey = Registry.CurrentUser.OpenSubKey(
                 regPath, true))
            {
                var orignalMode = ieMainKey.GetValue(appName);
                if (orignalMode == null || (int)orignalMode != ieMode)
                {
                    ieMainKey.SetValue(appName, ieMode, RegistryValueKind.DWord);
                    return true;
                }else
                {
                    return false;
                }
                //
            }
        }




    }


}
