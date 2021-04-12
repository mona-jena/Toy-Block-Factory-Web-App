using System;
using System.Net;
using System.Threading.Tasks;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryWebApp
{
    public class ToyServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _uri;
        private readonly Router _router;

        public ToyServer(string[] prefixes, ToyBlockFactory toyBlockFactory)
        {
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }
            _uri = prefixes;
            _httpListener = new HttpListener();
            var orderController = new OrderController(toyBlockFactory);
            _router = new Router(orderController);
        }

        public async Task Start()
        {
            foreach (string s in _uri)
            {
                _httpListener.Prefixes.Add(s);
            }
            _httpListener.Start();
            Console.WriteLine("Listening...");

            await Task.Run(() =>
            {
                while (true)
                {
                    HttpListenerContext context = _httpListener.GetContext();
                    try
                    {
                        Console.WriteLine($"\nReceived request from: {context.Request.Url?.PathAndQuery}");
                        context.Response.Headers.Set("Server", "mona's-server");
                        _router.ReadRequests(context);
                        context.Response.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        context.Response.StatusCode = 500;
                        context.Response.StatusDescription = e.Message;
                        context.Response.Close();
                    }
                }
            });
            _httpListener.Stop();
       
        }
    }
    
}
