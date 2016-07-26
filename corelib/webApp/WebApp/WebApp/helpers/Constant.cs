using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.helpers
{
    public class Constant
    {
        public const string HASHKEY          = "ams-tmp-web";
        
        // Page Titles
        public const string DASHBOARD        = "Dashboard";
        public const string DEFAULT_PAGE     = "Access FASTrax";
        public const string SYSTEM_USERS     = "System Users";
        public const string GENERAL_SETTINGS = "General System Settings";
        public const string ACQUISITION      = "Acquisition";
        public const string PICKLIST         = "Picklist";
        public const string DEALER_PAGE      = "Dealers";

        // Message Notif
        public const string ERROR_404        = "Server not found!";
        public const string ERROR_500        = "Internal server error!";
        public const string UNABLE_TO_SAVE   = "Server Error : Unable to proceed with the request";
        public const string UNABLE_TO_UPDATE = "Server Error : Unable to proceed updating changes";

        public const string SUCCESS_SAVE     = "Request has successfully been processed";


    }

}