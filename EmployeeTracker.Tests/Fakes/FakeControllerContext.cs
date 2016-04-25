using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace EmployeeTracker.Tests.Fakes
{
    class FakeControllerContext : ControllerContext
    {
        HttpContextBase _context = new FakeHttpContext();

        public override System.Web.HttpContextBase HttpContext
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }
    }

    class FakeHttpContext : HttpContextBase
    {
        HttpRequestBase _request = new FakeHttpRequest();

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }          
        }
    }

    class FakeHttpRequest : HttpRequestBase
    {       
        public override string this[string key]
        {
            get
            {
                return null;        
            }
        }

        public override NameValueCollection Headers
        {
            get
            {
                return new NameValueCollection();
            }
        }
    }
}
