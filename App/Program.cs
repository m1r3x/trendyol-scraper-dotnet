using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


{
    var options = new ChromeOptions();
    options.AddExcludedArgument("enable-logging");      //to remove extra log from terminal
    options.AddArguments("--disable-notifications");    //turn off notification popup
    
    Console.Write("Enter product to search: ");
    string toSearch = Console.ReadLine();
     
    IWebDriver driver = new ChromeDriver(options=options);
           
    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;       //js executer to scroll down

    driver.Navigate().GoToUrl("https://www.trendyol.com");

    driver.Manage().Cookies.AddCookie(new Cookie("countryCode","TR"));  //cookies needed to remove extra work
    driver.Manage().Cookies.AddCookie(new Cookie("language","tr"));
    driver.Manage().Cookies.AddCookie(new Cookie("storefrontId","1"));
    driver.Manage().Cookies.AddCookie(new Cookie("hvtb","1"));  

    driver.Navigate().GoToUrl("https://www.trendyol.com");
    Thread.Sleep(3000);

    IWebElement search_bar = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[2]/div/div/div[2]/div/div/div/div/input"));
    search_bar.SendKeys(toSearch);
    search_bar.SendKeys(Keys.Enter);
    Thread.Sleep(2000);

    js.ExecuteScript("window.scrollBy(0,250)");
    Thread.Sleep(1000);

    for (int i = 1; i<11; i++){
        IWebElement itemName;

        try{
            itemName = driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[3]/div/div[{i}]/div[1]/a/div[2]/div[1]/div/span[2]"));
        }
        catch{
            itemName = driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[4]/div/div[{i}]/div[1]/a/div[2]/div[1]/div/span[2]"));
        }

        IWebElement itemCost = driver.FindElements(By.ClassName("prc-box-sllng"))[i-1];

        Console.WriteLine($"{i}. {itemName.GetAttribute("title")}, Price: {itemCost.Text}");
    }

    driver.Quit();

    
    
}