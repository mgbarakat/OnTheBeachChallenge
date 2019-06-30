using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnTheBeachChallenge
{
    public class OrderesSequence
    {
        public string JobsList { get; private set; }
        private string jobsInCurrentRecursion;

        /// <summary>
        /// Constructor that get string input and create dictionary then loop on dictionary and build dependency for each results 
        /// </summary>
        /// <param name="input"></param>
        public OrderesSequence(string input)
        {
            JobsList = String.Empty;

            // if input is empty return empty string ROLE#1
            if (string.IsNullOrEmpty(input)) return;

            //create dictionary of the input
            var dictionary = input.ConvertToDictionary();

            // loop on each key on the dictionary and call get distinct list function
            dictionary.Each(job => GetDistinctList(BildingDependencies(job.Key, dictionary)));
        }

        /// <summary>
        /// add job to output list if not exists
        /// </summary>
        /// <param name="jobs"></param>
        private void GetDistinctList(string jobs)
        {
            jobs.Where(job => !JobsList.Contains(job.ToString())).Each(job => JobsList += job);
        }

        /// <summary>
        /// recursive call on dependencies to check self referencing and circular dependencies 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private string BildingDependencies(string job, IDictionary<string, string> map)
        {
            //recursion break rule 
            // if there are no dependencies left
            if (string.IsNullOrEmpty(map[job]))
            {
                jobsInCurrentRecursion = String.Empty;
                return job;
            }

            // if current job (key) in current job dependencies so it is a self referencing
            if (job == map[job]) throw new SelfReferencingException($"Job: {job} cannot depend on itself");

            CheckCircularDependencies(job);

            return BildingDependencies(map[job], map) + job;
        }

        /// <summary>
        /// if current recursion string length is greater than its distinct value so the added job is casing a circular reference
        /// </summary>
        /// <param name="job"></param>
        private void CheckCircularDependencies(string job)
        {
            jobsInCurrentRecursion += job;

            if (jobsInCurrentRecursion.Length != jobsInCurrentRecursion.Distinct().Count())
            {
                var invalidJob = jobsInCurrentRecursion
                                    .GroupBy(c => c)
                                    .Select(g => new { Letter = g.Key, Count = g.Count() })
                                    .Where(a => a.Count > 0).FirstOrDefault();


                throw new CircularDependencyException($"Circular dependency detected, invalid job is {invalidJob}");
            }

        }
    }
}
