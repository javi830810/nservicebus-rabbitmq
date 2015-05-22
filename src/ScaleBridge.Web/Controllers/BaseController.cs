using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ScaleBridge.Web
{
	public class BaseController : ApiController{

		//Preflight Optionsrequest

		public string Options()
		{
			return null; // HTTP 200 response with empty body
		}
	}
}

