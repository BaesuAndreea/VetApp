using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;

namespace TestVetApp
{
    public class TestEndToEnd
    {
        private IWebDriver _driver;
        [SetUp]
        public void SetupDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;
            _driver = new ChromeDriver("C:\\drivers", options);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }


        [Test]
        public void PetsTest()
        {
            _driver.Url = "https://localhost:44398/";
            bool foundButton = false;

            Thread.Sleep(3000);
            foreach (var button in _driver.FindElements(By.TagName("a")))
            {
                if (button.Text == "Pets")
                {
                    foundButton = true;
                    button.Click();
                    break;
                }
            }
            Assert.IsTrue(foundButton);
            Thread.Sleep(3000);

            foundButton = false;
            foreach (var button in _driver.FindElements(By.TagName("button")))
            {
                if (button.Text == "Add Pet")
                {
                    foundButton = true;
                    button.Click();
                    break;
                }
            }
            Assert.IsTrue(foundButton);
            Thread.Sleep(3000);

            bool foundTitle = false;
            foreach (var text in _driver.FindElements(By.TagName("h1")))
            {
                if (text.Text == "Add Pet")
                {
                    foundTitle = true;
                    break;
                }
            }
            Assert.IsTrue(foundTitle);
            Thread.Sleep(3000);
        }
    }
}
