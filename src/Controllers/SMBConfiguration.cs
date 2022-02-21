using System;

namespace NetFrameworkApp.Controllers
{
    public class SMBConfiguration
    {
        public String GetSharePath()
        {
            return Environment.GetEnvironmentVariable("SMB_PATH");
        }

        public String GetUserName()
        {
            return Environment.GetEnvironmentVariable("SMB_USERNAME");
        }

        public String GetPassword()
        {
            return Environment.GetEnvironmentVariable("SMB_PASSWORD");
        }
    }
}