using System.Net;

namespace ToyBlockFactoryWebApp
{
    public class HealthCheckController
    {
        public void HealthCheck(HttpListenerRequest request, HttpListenerResponse response)
        {
            response.StatusCode = 200;
                
            string responseString = "{\"status\": \"ok\"}";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}