using System;

namespace Job.Manager.Business.Model
{
    public class NumberArraySort : JobDetails
    {
        public string InputArray { get; set; }
        public string OutputArray { get; set; }

        public NumberArraySort(string inputdata)            
        {
            InputArray = inputdata;
            EnqueueDate = DateTime.Now;
        }
    }
}
