using System.Text;
using InnerCircle.Authentication.Service.Services.Models;
using Newtonsoft.Json;

namespace InnerCircle.Authentication.Service.Services.Middlewares
{
    public class GeneratePasswordMiddleware
    {
        private RequestDelegate _next;
        public GeneratePasswordMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var stream = request.Body;
            var originalContent = await new StreamReader(stream).ReadToEndAsync();
            var notModified = true;
            try
            {
                var dataSource = JsonConvert.DeserializeObject<RegistrationModel>(originalContent);
                if (dataSource != null)
                {
                    dataSource.Password = GeneratePassword(15);
                    var json = JsonConvert.SerializeObject(dataSource);
                    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                    stream = await requestContent.ReadAsStreamAsync();
                    notModified = false;
                }
            }
            catch
            {
            }
            if (notModified)
            {
                var requestData = Encoding.UTF8.GetBytes(originalContent);
                stream = new MemoryStream(requestData);
            }

            request.Body = stream;
            await _next.Invoke(context);
        }
        private static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijqklnoprstuvwxyz0123456789!#@%&*-_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
