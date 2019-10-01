using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForestController : ControllerBase
    {
        private IForest _forest;
        public ForestController(IForest forest)
        {
            _forest = forest ?? throw new ArgumentNullException(nameof(forest));
        }

        [HttpGet]
        public IEnumerable<Tree> GetTrees()
        {
            return _forest.Trees;
        }

        [HttpPost]
        public Tree CutTree(Guid id)
        {
            var victim = _forest.FellTree(id);
            return victim;
        }
    }
}
