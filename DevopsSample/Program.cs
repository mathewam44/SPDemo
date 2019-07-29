using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevopsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string siteUrl = "https://edisonintlt.sharepoint.com/sites/FrameWorksample";

            //Get the realm for the URL
            string realm = TokenHelper.GetRealmFromTargetUrl(new Uri(siteUrl));
            //Get the access token for the URL.  
            string accessToken = TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, new Uri(siteUrl).Authority, realm).AccessToken;
            //Create a client context object based on the retrieved access token
            using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(siteUrl, accessToken))
            {
                string listName = ConfigurationManager.AppSettings["ListName"].ToString();
                List oList = clientContext.Web.Lists.GetByTitle(listName);
                try
                {
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.AddItem(itemCreateInfo);
                    oListItem["Title"] = DateTime.Now.ToString();                    
                    oListItem.Update();
                    clientContext.ExecuteQuery();
                    Console.WriteLine(" Item Created");
                }
                catch { }

            }
        }
    }
}
