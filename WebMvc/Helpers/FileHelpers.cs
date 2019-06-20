using System.IO;
using System.Web.Hosting;

namespace WebMvc.Helpers
{
    public static class FileHelpers
    {
        public static bool CheckFileExist(string fileUrl)
        {
            try
            {
                if (File.Exists(HostingEnvironment.MapPath(fileUrl)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}