using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API
{
    public static class ErrorLogText
    {
        public static string ErrorText(string controllerName, string actionMethodName, string exception, string statusCode, string sourceName)
        {
            string msgBody = null;
            msgBody += Environment.NewLine;
            msgBody += "Controller Name : " + controllerName + Environment.NewLine;
            msgBody += "ActionMethodName : " + actionMethodName + Environment.NewLine;
            msgBody += "Exception : " + exception + Environment.NewLine;
            msgBody += "statusCode : " + statusCode + Environment.NewLine;
            msgBody += "Source : " + sourceName + Environment.NewLine;
            msgBody += Environment.NewLine + "-----------------------------------------" + Environment.NewLine;
            return msgBody;
        }


    }
}
