using Xunit;
using FutureValue.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using FutureValue.WebTests;
using Microsoft.CodeAnalysis.Scripting;

namespace FutureValue.Web.Controllers.Tests
{
    public class ProjectionFormControllerTests
    {
        private string homeUrl = "https://localhost:7228";
        private string createUrl = "https://localhost:7228/ProjectionForm/Create";
        

        [Fact()]
        [Trait("Category", "Smoke")]
        public void IndexTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                Assert.True(driver.FindElement(By.Id("TBL")).Enabled);
                Assert.True(driver.FindElement(By.TagName("tbody")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".btn-primary")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".edit-link")).Enabled);
            }
        }
        [Fact()]
        public void DetailsTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                string script = "document.querySelector(\"a.details-link\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                var now = DateTime.Now;
                Waiter.wait(driver);
                Assert.NotEqual(driver.Url, homeUrl);
            }
        }
        [Fact()]
        public void DetailsTestAjaxApiCall()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                string script = "document.querySelector(\"a.details-link\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                Waiter.wait(driver);
                Assert.True(driver.FindElement(By.CssSelector(".projection-tbl")).Enabled);
            }
        }

        [Fact()]
        public void CreateTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(createUrl);
                Assert.True(driver.FindElement(By.Id("PresetValue")).Enabled);
                Assert.True(driver.FindElement(By.TagName("form")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".Name")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".btn-create")).Enabled);

                driver.FindElement(By.Name("PresetValue")).SendKeys("5000");
                driver.FindElement(By.CssSelector(".Name")).SendKeys(" " + DateTime.Now.ToString());
                string createClick = "document.querySelector(\"a.btn-create\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(createClick);
                Waiter.wait(driver,1500,2000);
                Assert.True(driver.FindElement(By.Id("TBL")).Enabled);
            }
        }
        [Fact()]
        public void Create_TestPreview()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(createUrl);
                driver.FindElement(By.Name("PresetValue")).SendKeys("500");
                driver.FindElement(By.CssSelector(".Name")).SendKeys(" " + DateTime.Now.ToString());
                string previewScript = "document.querySelector(\"a.btn-preview\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(previewScript);
                Waiter.wait(driver);
                Assert.True(driver.FindElement(By.CssSelector(".projection-tbl")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".projection-tbl tbody tr")).Enabled);
            }
        }
        [Fact()]
        public void EditTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                string script = "document.querySelector(\"a.edit-link\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);
                Waiter.wait(driver);
                Assert.NotEqual(driver.Url, homeUrl);
            }
        }
        [Fact()]
        public void Edit_TestPreview()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                string script = "document.querySelector(\"a.edit-link\").click()";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);
                Waiter.wait(driver);
                string previewScript = "document.querySelector(\"a.btn-preview\").click()";
                js.ExecuteScript(previewScript);
                Waiter.wait(driver);
                Assert.True(driver.FindElement(By.CssSelector(".projection-tbl")).Enabled);
                Assert.True(driver.FindElement(By.CssSelector(".projection-tbl tbody tr")).Enabled);
            }
        }
        [Fact()]
        public void DeleteTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}