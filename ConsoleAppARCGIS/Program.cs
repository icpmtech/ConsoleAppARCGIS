using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;

namespace ConsoleAppARCGIS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CSOM
            //LIST PINS NO SHAREPOIN ONLINE
            //CREDECIAIS DE AUTENTICAÇÂO
            string siteUrlOnline = "https://ascendiigi.sharepoint.com/sites/GOdigital";
            string userNameOnline = "godigitaladm@ascendiigi.onmicrosoft.com";
            string passwordStringOnline = "";
           
            //AUTENTICAÇÂO VIA CODIGO
            OfficeDevPnP.Core.AuthenticationManager authManager = new OfficeDevPnP.Core.AuthenticationManager();
            var passWord = new SecureString();
            foreach (char c in passwordStringOnline.ToCharArray()) passWord.AppendChar(c);

            //NEGOCIO
            ClientContext clientContextSiteDigital = authManager.GetSharePointOnlineAuthenticatedContextTenant(siteUrlOnline, userNameOnline, passWord);

            //OBTER LISTA DOS PIQS
            SP.List oList = clientContextSiteDigital.Web.Lists.GetByTitle("PIQS");

            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View><Query><Where><Geq><FieldRef Name='ID'/>" +
                "<Value Type='Number'>10</Value></Geq></Where></Query><RowLimit>1</RowLimit></View>";
            ListItemCollection collListItem = oList.GetItems(camlQuery);

            clientContextSiteDigital.Load(collListItem);

            clientContextSiteDigital.ExecuteQuery();

            foreach (ListItem oListItem in collListItem)
            {
                Console.WriteLine("ID: {0} \nTitle: {1} \nNumero PIQ: {2}", oListItem.Id, oListItem["Title"], oListItem["numeropiq"]);
            }
            Console.ReadLine();
        }
    }
}
