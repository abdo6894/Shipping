using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WebApi.Models;

namespace WebApi.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
     private  readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        // هذه هي الدالة التي يتم استدعاؤها عندما يحدث استثناء غير معالج
        public void OnException(ExceptionContext context)
        {
            // 1. تسجيل الاستثناء (الأمان)
            _logger.LogError(context.Exception, "An unhandled exception occurred during API execution.");

            // 2. تحديد رمز حالة HTTP ونوع الاستجابة
            HttpStatusCode status = HttpStatusCode.InternalServerError; // الافتراضي هو 500
            string message = "An unexpected error occurred. Please contact support.";

            // يمكنك إضافة معرّف فريد للخطأ هنا (مثلاً: Guid.NewGuid().ToString())

            // 3. التعامل مع أنواع استثناءات محددة (مثل خطأ الوصول للبيانات)
            if (context.Exception is DataAccessException)
            {
                status = HttpStatusCode.InternalServerError; // أو 503 Service Unavailable إذا كانت قاعدة البيانات معطلة
                message = "A system error occurred while accessing the necessary data.";
            }
            // يمكنك إضافة أنواع استثناءات أخرى (مثل NotFoundException لـ 404 أو ValidationException لـ 400)

            // 4. تنسيق الاستجابة (ApiResponse)
            var errorResponse = ApiResponse<object>.FailResponse(message);

            // 5. إعداد استجابة الخطأ وإيقاف المعالجة
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)status
            };

            // إعلام الـ Pipeline أن الاستثناء قد تم التعامل معه
            context.ExceptionHandled = true;
        }
    }
}

