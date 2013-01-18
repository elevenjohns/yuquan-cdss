using System;
using System.Collections.Generic;
using System.Linq;
using WuKeSong.Interfaces;
using YuQuan.Helpers;
using YuQuan.Models;

namespace WuKeSong.Services
{
    /// <summary>
    /// Provide report service.
    /// In real app, this class is a wrapper/proxy, which calls external services. 
    /// </summary>
    public class FactServiceMocker : IFactService
    {
        /// <summary>
        /// used for demo purpose only
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IEnumerable<Fact> GetFactsFromReportMock(int id,KBEntities db)
        {
            var facts = new List<Fact>();
            if (db == null)
                return facts;

            var report = db.Report.Find(id);
            if (report == null)
                return facts;

            if (report.Event.EventType == EnumEventType.体征数据.ToString()) 
                // && db.Fact.Where(x=>x.Report.Id==report.Id && x.LifeSpan == EnumLifeSpan.Temporary.ToString()).Count()<=0)
            {
                var contextItem = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肱动脉收缩压");
                if (contextItem != null)
                {
                    facts.Add(new Fact()
                    {
                        Report = report,
                        NumericValue = 160 + RandomNumberHelper.GetRandomNumber(-20, 20),
                        IsAbnormal = true,
                        Confidence = 100,
                        ContextItemDefinition = contextItem,
                        LifeSpan = EnumLifeSpan.Temporary.ToString()
                    });
                }

                contextItem = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肱动脉舒张压");
                if (contextItem != null)
                {
                    facts.Add(new Fact()
                    {
                        Report = report,
                        NumericValue = 120 + RandomNumberHelper.GetRandomNumber(-20, 20),
                        IsAbnormal = true,
                        Confidence = 100,
                        ContextItemDefinition = contextItem,
                        LifeSpan = EnumLifeSpan.Temporary.ToString()
                    });
                }
            }
            else
            {
                foreach(var x in db.Fact)
                {
                    if (x.Report.Id == id &&
                        x.LifeSpan == EnumLifeSpan.Temporary.ToString())
                        facts.Add(x);
                }
            }

            return facts;
        }

        #region IFactService Members

        public string GetFactsFromReport(string url)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}