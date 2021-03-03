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

        private static void SimpleListenerExample(string[] prefixes)
        {
            // URI prefixes are required, exa:"http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }
                
            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            
            
            // Note: The GetContext method blocks while waiting for a request. -synchronous way
            // Waits for an incoming request and returns when one is received.
            HttpListenerContext context = listener.GetContext();
            
            SendRequest(context);
            
            SendResponse(context);
            
            // close HTTP listener
            listener.Stop();
        }
        
        private static void SendRequest(HttpListenerContext context)
        {
            // desc the request - HttpMethod string, UserAgent string, and request body data 
            HttpListenerRequest request = context.Request;
            PostCustomerDetails(request);
            
            /*// Gets the URL information (w/o the host and port) requested by the client.
            var url = request.RawUrl;
            // indicates whether the request has associated body data
            if (!request.HasEntityBody)
            {
                Console.WriteLine("No client data was sent with the request.");
                return;
            }

            // contains stream that contains body data sent by client
            System.IO.Stream body = request.InputStream;
            // indicates which type of compression was applied to the entity-body eg gzip, compress
            System.Text.Encoding encoding = request.ContentEncoding;
            // reads characters from a byte stream
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            // If a client incl body data in a request, it declares MIME type of the body data in the Content-Type header, otherwise null
            if (request.ContentType != null)
            {
                Console.WriteLine("Client data content type {0}", request.ContentType);
            }

            // length of the body data included in the request
            Console.WriteLine("Client data content length {0}", request.ContentLength64);

            Console.WriteLine("Start of client data:");
            // Convert the body data to a string and display it on the console.
            string bodyString = reader.ReadToEnd();
            Console.WriteLine(bodyString);
            Console.WriteLine("End of client data:");
            // If you are finished with the request, it should be closed also.
            body.Close();
            
            reader.Close();*/
        }

        private static void PostCustomerDetails(HttpListenerRequest request)
        {
            if (request.HttpMethod == "POST")
            {
                System.IO.Stream body = request.InputStream;
                System.Text.Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                if (request.ContentType != null)
                {
                    Console.WriteLine("Client data content type {0}", request.ContentType);
                }
                Console.WriteLine("Client data content length {0}", request.ContentLength64);
                Console.WriteLine("Start of client data:");
                body.Close();
                reader.Close();
            }
        }

        private static void CustomerDetailsResponse(HttpListenerResponse response, ToyBlockFactory toyBlockFactory)
        {
            var customerDetails = response.OutputStream.ToString()?.Split(",");
            // HOW TO GET OBJECT CONTENT AS STRING???
            
            var name = customerDetails[0]; 
            var address = customerDetails[1];
            var dueDate = DateTime.Parse(customerDetails[2]);
            
            
            Order order;
            if (dueDate >= DateTime.Today)
            {
                order = toyBlockFactory.CreateOrder(name, address, dueDate);
            }
            else
            {
                order = toyBlockFactory.CreateOrder(name, address);
            }
            
            var orderString = JsonSerializer.Serialize(order);
            string responseString = "<HTML><BODY>" + "<p>" + orderString[0] + "</p>" + "<p>" + orderString[1] + "</p>" + "<p>" + orderString[2] + "</p>"+ "</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            
            output.Close();
        }

        
        private static void SendResponse(HttpListenerContext context)
        {
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            // object that will be sent to the client in response to the client's request
            HttpListenerResponse response = context.Response;
            CustomerDetailsResponse(response, toyBlockFactory);
            
            /*// name of Server under Server tag
            response.Headers.Set("Server", "mona's-server");
            // 201 = request was successful and a resource was created - success of PUT or PUSH
            response.StatusCode = 201;
            
            
            var order = new Order("Mona", "30 mt eden rd", DateTime.Today);
            
            // converts order to JSON string
            var orderString = JsonSerializer.Serialize(order);
            // specify format of output 
            string responseString = "<HTML><BODY>" + orderString + "</BODY></HTML>";
            // Get a response stream and write the response to it.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // output response length
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            // prints out response 
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            
            output.Close();*/
        }

        
    }
}