using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace EaaaWebDeveloper.Controllers
{
    public class HomeController : Umbraco.Web.Mvc.RenderMvcController
    {
        // GET: Home
        public override ActionResult Index(RenderModel model)
        {
            var subPages = model.Content.Children;
            return base.Index(model);
        }
    }
}