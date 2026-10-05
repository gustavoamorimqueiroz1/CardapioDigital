using System.ComponentModel;

namespace CompreAqui.Domain.Enums
{
    public enum EStatus
    {
        [Description("Ok")]
        Ok = 200,
        [Description("Created")]
        Created = 201,
        [Description("Accepted")]
        Accepted = 202,
        [Description("Non-Authoritative Information")]
        NonAuthoritativeInformation = 203,
        [Description("No Content")]
        NoContent = 204,
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Payment Required")]
        PaymentRequired = 402,
        [Description("Forbidden")]
        Forbidden = 403,
        [Description("Not Found")]
        NotFound = 404,
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,
        [Description("Not Acceptable")]
        NotAcceptable = 406,
        [Description("Request Timeout")]
        RequestTimeout = 408,
        [Description("Invalid Token")]
        InvalidToken = 498,
        [Description("Internal Server Error")]
        InternalServerError = 500,
    }
}
