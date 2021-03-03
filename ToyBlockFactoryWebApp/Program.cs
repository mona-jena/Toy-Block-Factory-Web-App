using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = {"http://localhost:3000/"};
            SimpleListenerExample(prefixes);
        }

        public static void SimpleListenerExample(string[] prefixes)
        {
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            var url = request.RawUrl;
            // Obtain a response object.
            
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return;
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the data to a string and display it on the console.
            string bodyString = reader.ReadToEnd();
            Console.WriteLine(bodyString);
            Console.WriteLine("End of client data:");
            body.Close();
            reader.Close();
            // If you are finished with the request, it should be closed also.
            
            HttpListenerResponse response = context.Response;
            response.Headers.Set("Server", "mona's-server");
            response.StatusCode = 201;
            // Construct a response.
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            Order order = new Order("Mona", "30 mt eden rd", DateTime.Today);
            var orderString = JsonSerializer.Serialize(order);
            string responseString = "<HTML><BODY>" + orderString + "</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer,0,buffer.Length);
            // You must close the output stream.
            
            output.Close();
            listener.Stop();
        }
        
    }
}