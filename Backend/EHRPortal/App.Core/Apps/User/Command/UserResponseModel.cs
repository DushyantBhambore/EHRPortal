using Domain.ResponseModel;

namespace App.Core.Apps.User.Command
{
    internal class UserResponseModel : JSonModel
    {
        public UserResponseModel(int statusCode, string message, object data) : base(statusCode, message, data)
        {
        }
    }
}