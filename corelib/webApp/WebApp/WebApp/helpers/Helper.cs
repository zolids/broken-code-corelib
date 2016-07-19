using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.helpers
{
    public class Helper
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}