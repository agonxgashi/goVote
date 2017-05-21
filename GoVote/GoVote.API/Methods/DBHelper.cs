using System.Configuration;

namespace GoVote.API.Methods
{
    public class DBHelper
    {
        public static string GetConStr()
        {
            return ConfigurationManager.AppSettings["conStr"].ToString();
        }
    }
}