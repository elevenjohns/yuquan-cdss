using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WuKeSong.Interfaces;

namespace WuKeSong.Services
{
    public class FactService:IFactService
    {
        /// <summary>
        /// Get Facts From report URL.
        /// This function is supposed to be provided by EMR/CPOE.
        /// </summary>
        /// <param name="url">report URL. In real app, URL should be unique, which can be used as id</param>
        /// <returns>A list of structured Fact objects</returns>       
        /// <remarks>[TODO]
        /// public IEnumerable&lt;YuQuan.Models.Fact&gt; GetFactsFromReport(string url) 
        /// transform unstructured info to structured info
        /// translate terms in local coding systems to standard coding system.
        ///</remarks>
        public string GetFactsFromReport(string url)
        {
            throw new NotImplementedException();
        }
    }
}