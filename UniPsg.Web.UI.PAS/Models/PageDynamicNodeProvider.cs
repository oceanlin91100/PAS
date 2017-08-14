using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using UniPsg.Web.UI.PAS;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Models
{
    public class PageDynamicNodeProvider : DynamicNodeProviderBase
    {
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodes)
        {
            var returnValue = new List<DynamicNode>();

            // 向web api層取得選單             
            OrganizationClient orgClient = new OrganizationClient();
            string account = HttpContext.Current.User.Identity.Name;
            //string account = @"PSCNET\AMY-LIN";
            account = account.Replace(@"labpsc\", "");
            account = account.Replace(@"LABPSC\", "");
            account = account.Replace(@"pscnet\", "");
            account = account.Replace(@"PSCNET\", "");
            //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
            DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(SAMAccountName=" + account.ToUpper() + "))";
            SearchResult results = search.FindOne();
            if (results != null)
            {
                ResultPropertyCollection resultPropColl;
                resultPropColl = results.Properties;
                var employeeNo = resultPropColl["description"][0];
                MenuClient client = new MenuClient();
                var menus = client.FindRoleMenu((string)employeeNo);
                if (menus == null || menus.Count() == 0)
                    menus = client.FindRoleMenu("ALL");

                foreach (var item in menus)
                {
                    DynamicNode node = new DynamicNode();
                    // 選單名稱 
                    node.Title = item.Name;
                    // 有無父類別，沒有的話則傳空字串 
                    node.ParentKey = item.ParentId == 0 ? "" : item.ParentId.ToString();

                    // 唯一值 
                    node.Key = item.Id.ToString();
                    // MVC的View 
                    node.Action = item.Action.Trim();
                    // MVC的Controller 
                    node.Controller = item.Controller.Trim();
                    // 選單所分配的腳色，逗號分隔
                    //List<string> roles = new List<string>();
                    //roles.Add(role);
                    //node.Roles = roles;
                    //  
                    node.RouteValues.Add("id", item.Id);
                    returnValue.Add(node);
                }
            }
            // Return 
            return returnValue;
        }
    }
}
