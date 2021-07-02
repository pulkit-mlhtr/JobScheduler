using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.ProcessingService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Job.Manager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class JobsController : ControllerBase
    {
        private readonly IPublisher<JobDetails> _publisher;
        private readonly ILogger<JobsController> _logger;
        private INumberSortLogic _numberSortLogic;

        public JobsController(INumberSortLogic numberSortLogic,
                                IPublisher<JobDetails> publisher,
                                ILogger<JobsController> logger)
        {
            _numberSortLogic = numberSortLogic;
            _publisher = publisher;
            _logger = logger;
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> Enqueue([FromBody] NumberArraySort job)
        {
            try
            {
                var addedToJob = await _numberSortLogic.Add(job);

                _logger.LogInformation($"JOb id : {addedToJob.JobId} created.");

                _publisher.PublishData(addedToJob);

                _logger.LogInformation($"JOb id : {addedToJob.JobId} send for processing.");

                return await Get(addedToJob.JobId);
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                      ex is ApplicationException ||
                                      ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled queuing job", ex.Message, ex.StackTrace);

                return Problem("Something went wrong!!");
            }
        }

        [HttpPost]
        [Route("remove/{id}")]
        public async Task<IActionResult> Dequeue([FromRoute] Guid id)
        {
            try
            {
                var job = await _numberSortLogic.Get(id);

                if (job == null)
                {
                    _logger.LogWarning($"Get({id}) NOT FOUND");

                    return NotFound($"Job Id : {id}");
                }
                else if(job.Status==JobStatus.InProcess.ToString())
                {
                    return BadRequest($"Cannot delete running job");
                }
                else if (_numberSortLogic.Delete(job))
                {
                    return Ok($" Job:{id} deleted !!");
                }
                else
                {
                    return Ok($"Unable to delete job : {id}");
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                        ex is ApplicationException ||
                                        ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled deleting job", ex.Message, ex.StackTrace);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var job = await _numberSortLogic.Get(id);

            if (job == null)
            {
                return NotFound($"Job : {id} not found.");
            }

            return Ok(job);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _numberSortLogic.GetAll());
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                       ex is ApplicationException ||
                                       ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled deleting job", ex.Message, ex.StackTrace);

                return Problem("Something went wrong!!");
            }

        }

        [HttpGet]
        [Route("all/pending")]
        public async Task<IActionResult> GetAllPending()
        {
            try
            {
                return Ok(await _numberSortLogic.GetAllPending());
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                       ex is ApplicationException ||
                                       ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled deleting job", ex.Message, ex.StackTrace);

                return Problem("Something went wrong!!");
            }

        }

        [HttpGet]
        [Route("all/completed")]
        public async Task<IActionResult> GetAllCompleted()
        {
            try
            {
                return Ok(await _numberSortLogic.GetAllCompleted());
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                       ex is ApplicationException ||
                                       ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled deleting job", ex.Message, ex.StackTrace);

                return Problem("Something went wrong!!");
            }

        }

        [HttpGet]
        [Route("all/inprocess")]
        public async Task<IActionResult> GetAllInProcess()
        {
            try
            {
                return Ok(await _numberSortLogic.GetAllInProcesss());
            }
            catch (Exception ex) when (ex is InvalidOperationException ||
                                       ex is ApplicationException ||
                                       ex is ArgumentNullException)
            {
                _logger.LogError("error occured whiled deleting job", ex.Message, ex.StackTrace);

                return Problem("Something went wrong!!");
            }

        }
    }
}
