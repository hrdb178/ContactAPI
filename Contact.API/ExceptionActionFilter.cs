using Contact.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        private ILogger _logger;
        private int noOfLoop = 0;
        public ExceptionActionFilter(ILogger<ExceptionActionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            if (noOfLoop == 0)
            {
                StatusModel statusModel = new StatusModel();
                var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var controllType = actionDescriptor.ControllerTypeInfo;

                var controllerBase = typeof(ControllerBase);
                var controller = typeof(Controller);

                if (controllType.IsSubclassOf(controllerBase) && !controllType.IsSubclassOf(controller))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.HttpContext.Response.ContentType = "application/json";
                    statusModel.StatusCode = context.HttpContext.Response.StatusCode;
                    statusModel.StatusMessage = context.Exception.Message;

                    context.Result = new JsonResult(new StatusModel()
                    {
                        StatusCode = context.HttpContext.Response.StatusCode,
                        StatusMessage = context.Exception.Message
                    });
                }
                #region LogTheErrorInErrorLogFile
                string actionMethodName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                string controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
                string sourcePath = context.Exception.StackTrace;
                string exception = context.Exception.Message;
                string statusCode = Convert.ToString(context.HttpContext.Response.StatusCode);
                _logger.LogError(ErrorLogText.ErrorText(controllerName, actionMethodName, exception, statusCode, sourcePath));
                #endregion
                noOfLoop++;
            }
            else
            {
                noOfLoop = 0;
            }
        }
    }
}
