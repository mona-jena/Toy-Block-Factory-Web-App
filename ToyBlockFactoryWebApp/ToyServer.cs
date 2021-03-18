using System;
using System.Net;
using System.Text;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryWebApp
{
    class ToyServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _uri;
        public static ToyBlockFactory _toyBlockFactory;
        public readonly HealthCheckController _healthCheckController;
        private readonly Router _router;

        public ToyServer(string[] prefixes, ToyBlockFactory toyBlockFactory)
        {
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }
            _uri = prefixes;
            _toyBlockFactory = toyBlockFactory;
            _httpListener = new HttpListener();
            Start();
            _healthCheckController = new HealthCheckController();
            _router = new Router(_healthCheckController);
        }

        private void Start()
        {
            foreach (string s in _uri)
            {
                _httpListener.Prefixes.Add(s);
            }
            _httpListener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext(); 
                try
                {
                    var request = context.Request;
                    _router.ReadRequests(request, context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    context.Response.StatusCode = 500;
                    context.Response.Close();
                }
            }

            _httpListener.Stop();
        }


        public static void SendResponse(HttpListenerResponse response, Object order)
            {
                //var httpResponse = new HttpResponse(response, order);
                response.Headers.Set("Server", "mona's-server");
                response.StatusCode = 201;

                string responseString = JsonSerializer.Serialize(order);
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            
        }
    
}