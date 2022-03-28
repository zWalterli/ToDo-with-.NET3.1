using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Todo.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public string Email {
            get
            {
                return this.User.Claims.FirstOrDefault(i => i.Type == "Email").Value;
            }
        }
    }
}
