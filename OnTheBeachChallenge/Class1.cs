using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnTheBeachChallenge
{
    public static class StringExtensions
    {
        /// <summary>
        /// extension to ease split function
        /// </summary>
        /// <param name="input"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static string[] Split(this string input, string delimeter)
        {
            return input.Split(new[] { delimeter }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// convert string to dictionary
        /// split on line break then use regular expression to match job and its dependencies
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ConvertToDictionary(this string input)
        {
            var list = input.Split("\r\n");
            var jobs = list.Select(job => Regex.Match(job.Replace(" ", ""), @"(\w+)=>(\w?)"));
            var dictionary = jobs.ToDictionary(match => match.Groups[1].Value, match => match.Groups[2].Value);
            return dictionary;
        }

        /// <summary>
        /// extension to string that similar to for-each but with action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}
