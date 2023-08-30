using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.AngularTests
{
    public class Waiter
    {
        public static void wait(IWebDriver driver,double milisecodsSpan=1000,double pollingInterval=1500)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milisecodsSpan));
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollingInterval);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromMilliseconds(milisecodsSpan) > TimeSpan.Zero);
        }
        public static void WaitJs(IWebDriver driver, string jsExecution= "return document.readyState", double milisecodsSpan = 1000)
        {
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milisecodsSpan));
            wait.PollingInterval = TimeSpan.FromMilliseconds(milisecodsSpan);
            var jsLoad = ((IJavaScriptExecutor)driver).ExecuteScript(jsExecution).ToString().Equals("complete");
            bool jsReady = jsLoad.Equals("complete");

            wait.Until(wd=> jsLoad);
        }
    }
}
