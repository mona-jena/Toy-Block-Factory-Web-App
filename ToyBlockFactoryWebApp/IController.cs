using System.Collections.Specialized;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    public interface IController
    {
        string Post(string requestBody);
        void PostAddBlock(NameValueCollection queryString, string requestBody);
        bool Delete(NameValueCollection queryString);
        bool Put(NameValueCollection queryString);
        IReport Get(NameValueCollection queryString);
    }
}