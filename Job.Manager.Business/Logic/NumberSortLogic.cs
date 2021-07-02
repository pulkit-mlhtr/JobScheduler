using AutoMapper;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job.Manager.Business.Logic
{
    public class NumberSortLogic : INumberSortLogic
    {

        private IRepository<NumberSortJobs> _jobRepository;
        private readonly IMapper _mapper;

        public NumberSortLogic(IRepository<NumberSortJobs> jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<IList<NumberArraySort>> GetAll()
        {
            return _jobRepository.GetAll().ToListAsync().Result
                .ConvertAll(c => _mapper.Map<NumberArraySort>(c));
        }

        public async Task<IList<NumberArraySort>> GetAllPending()
        {
            return _jobRepository.GetAll()
                .Where(w => w.InPending).ToListAsync().Result
                .ConvertAll(c => _mapper.Map<NumberArraySort>(c));
        }

        public async Task<IList<NumberArraySort>> GetAllCompleted()
        {
            return _jobRepository.GetAll()
                .Where(w => w.IsComplete).ToListAsync().Result
                .ConvertAll(c => _mapper.Map<NumberArraySort>(c));
        }

        public async Task<NumberArraySort> Get(Guid Id)
        {
            var job = _jobRepository.GetAll()
                .Where(w => w.Id == Id).FirstOrDefaultAsync().Result;

            return job == null ? null : _mapper.Map<NumberArraySort>(job);
        }

        public bool Delete(NumberArraySort job)
        {
            try
            {
                _jobRepository.Delete(_mapper.Map<NumberSortJobs>(job));
                _jobRepository.SaveChanges();

                return true;
            }

            catch (Exception ex) when (ex is DbUpdateException ||
                                        ex is AutoMapperMappingException)
            {
                return false;
            }
        }

        public async Task<IList<NumberArraySort>> GetAllInProcesss()
        {
            return _jobRepository.GetAll()
                .Where(w => w.InProcess).ToList()
                .ConvertAll(c => _mapper.Map<NumberArraySort>(c));
        }

        public async Task<JobDetails> Add(JobDetails job)
        {
            var jobToDb = _mapper.Map<NumberSortJobs>(job);
            _jobRepository.Add(jobToDb);
            _jobRepository.SaveChanges();

            return _mapper.Map<NumberArraySort>(jobToDb);
        }

        public async Task Update(JobDetails job)
        {
            _jobRepository.Update(_mapper.Map<NumberSortJobs>(job));
            _jobRepository.SaveChanges();
        }
    }
}
