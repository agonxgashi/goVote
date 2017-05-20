using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoVote.API.Models.AppModels;

namespace GoVote.API.Controllers
{
    public class ATestController : ApiController
    {

        public IEnumerable<Person> Get()
        {
            return Person.GetAll();
        }

    }
}
