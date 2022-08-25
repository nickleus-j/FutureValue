using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureValue.WebTests
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
    }
}
