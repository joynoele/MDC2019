using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebReference.Models;

namespace WebReference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForestController : ControllerBase
    {
        private ILogger<ForestController> _log;
        private IForest _forest;
        public ForestController(ILogger<ForestController> log, IForest forest)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _forest = forest ?? throw new ArgumentNullException(nameof(forest));
        }

        [HttpGet]
        public IEnumerable<Tree> GetTrees()
        {
            _log.LogInformation("Getting all the trees in the woods {@forest}", _forest);
            return _forest.Trees;
        }

        [HttpPost]
        public Tree CutTree(Guid id)
        {
            var victim = _forest.FellTree(id);
            _log.LogInformation("Cut down tree {@tree}", victim);
            return victim;
        }
    }
}
