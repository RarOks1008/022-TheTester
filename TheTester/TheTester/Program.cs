using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TheTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("--ignore-gpu-blocklist");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddExcludedArgument("enable-logging");

            ChromeDriver browser = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            browser.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(1));
            List<TestingObject> testingObjects = new List<TestingObject>();
            List<ActionsObject> actions1 = new List<ActionsObject>();
            actions1.Add(new ActionsObject("/html/body/div[2]/div[2]/input", "B", "input"));
            actions1.Add(new ActionsObject("/html/body/div[2]/div[3]/input", "random", "input"));
            actions1.Add(new ActionsObject("/html/body/div[2]/div[4]/button", "click", "click"));

            testingObjects.Add(new TestingObject("http://nikolanedeljkovic.com", 2000, 1, 10, actions1));
            RandomGenerator randomGenerator = new RandomGenerator();
            foreach (TestingObject testObj in testingObjects)
            {
                for (int i = 0; i < testObj.NumberOfActions; i++)
                {
                    browser.Url = testObj.Url;
                    browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    browser.Navigate().Refresh();
                    foreach (ActionsObject actionObj in testObj.ActionsObject)
                    {
                        if (actionObj.ActionAction == "input")
                        {
                            IWebElement webElem = browser.FindElement(By.XPath(actionObj.ActionXPath));
                            if (actionObj.ActionValue == "random")
                            {
                                webElem.SendKeys(randomGenerator.RandomString(1));
                            }
                            else
                            {
                                webElem.SendKeys(actionObj.ActionValue);
                            }
                        }
                        if (actionObj.ActionAction == "click")
                        {
                            IWebElement webElem = browser.FindElement(By.XPath(actionObj.ActionXPath));
                            Actions act = new Actions(browser);
                            act.MoveToElement(webElem).Click().Build().Perform();
                        }
                        if (actionObj.ActionAction == "select")
                        {
                            IWebElement webElem = browser.FindElement(By.XPath(actionObj.ActionXPath));
                            SelectElement selectElement = new SelectElement(webElem);
                            selectElement.SelectByValue(actionObj.ActionValue);
                        }
                        System.Threading.Thread.Sleep(testObj.DelayBetweenActions * 1000);
                    }
                }
                System.Threading.Thread.Sleep(testObj.DelayToNextObject * 1000);
            }
        }
    }

    public class TestingObject
    {
        public string Url { get; set; }
        public int NumberOfActions { get; set; }
        public int DelayBetweenActions { get; set; }
        public int DelayToNextObject { get; set; }
        public List<ActionsObject> ActionsObject { get; set; }
        public TestingObject(string url, int numberOfActions, int delayBetweenActions, int delayToNextObject, List<ActionsObject> actionsObject)
        {
            Url = url;
            NumberOfActions = numberOfActions;
            DelayBetweenActions = delayBetweenActions;
            DelayToNextObject = delayToNextObject;
            ActionsObject = actionsObject;
        }
        public TestingObject()
        {
            ActionsObject = new List<ActionsObject>();
        }
    }

    public class RandomGenerator
    {
        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class ActionsObject
    {
        public string ActionXPath { get; set; }
        public string ActionValue { get; set; }
        public string ActionAction { get; set; }
        public ActionsObject(string actionXPath, string actionValue, string actionAction)
        {
            ActionXPath = actionXPath;
            ActionValue = actionValue;
            ActionAction = actionAction;
        }
        public ActionsObject(ActionsObject actions)
        {
            ActionXPath = actions.ActionXPath;
            ActionValue = actions.ActionValue;
            ActionAction = actions.ActionAction;
        }
        public ActionsObject()
        {
            ActionXPath = "";
            ActionValue = "";
            ActionAction = "";
        }
    }
}
