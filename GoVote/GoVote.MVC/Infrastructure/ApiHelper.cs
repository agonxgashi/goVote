using System.Configuration;

namespace GoVote.MVC.Infrastructure
{
    public class ApiHelper
    {
        public static string GetApiUrl()
        {
            return ConfigurationManager.AppSettings["apiUrl"].ToString();
        }
    }
}