using System.Threading.Tasks;

namespace Web.Helpers
{
    // Global class accessible to whole project
    public static class Settings
    {
        public static string AccessToken;

        public static async Task<string> GetAccessTokenAsync()
        {
            await Task.Delay(1000);
            return AccessToken;            
        }
    }
}
