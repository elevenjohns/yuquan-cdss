using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WuKeSong.Models;
using System.Reflection;
using YuQuan.Helpers;
using System.Xml;
using System.Configuration;
using YuQuan.Models;

/*
 * Revert SeedEvent()
 * 
 delete from 
[KB].[dbo].[Fact]

delete from 
[KB].[dbo].[Report]

delete from 
[KB].[dbo].[Event]
 */

/*
 Revert SeedEBM
 * 
delete from
[KB].[dbo].[TriggerRule_EBM_Association]

delete from
[KB].[dbo].MedicalOrderDefinition_EBM_Association

delete from
[KB].[dbo].PhaseDefinition_EBM_Association

delete from
[KB].[dbo].TaskDefinition_EBM_Association

delete from
[KB].[dbo].PlanDefinition_EBM_Association

delete from
[KB].[dbo].EBM
 */

namespace WuKeSong.Data
{
    public class KBEntitiesInitializer
    {
        private KBEntities db = new KBEntities();

        public void Seed()
        {
            if (db.ClinicalProblemDefinition == null)
                throw new Exception("Make sure already having run CreateDB.sql and .edmx.sql.");


            // Commented items are imported directly from Rules.DB
            
            //
            // Run scripts in this sequence:
            // CreateDB.sql
            // KB.edmx.sql
            // SeedPathwayAndOrderSet.sql
            // SeedSeedProblemAndContextItemDefinition.sql
            // SeedTriggerRule.sql
            //
 

            // 
            //if (db.PlanDefinition.Count() <= 0)
            //{
            //    SeedClinicalPathway(); 
            //    SeedOrderSet();
            //    SeedOrder();
            //}
            if (db.TriggerRule.Count() <= 0)
            {
                SeedRuleSet();
            }
            //if (db.ContextItemDefinition.Count() <= 0)
            //{
            //    SeedLabTest("local");
            //    SeedLabTest("standard");
            //    SeedFinding();
            //    SeedPhysiologicalItems("local");
            //    SeedPhysiologicalItems("standard");
            //    SeedDemographicItems();
            //}
            //if (db.ClinicalProblemDefinition.Count() <= 0)
            //{
            //    SeedProblem("local");
            //    SeedProblem("standard");
            //    SeedContextItemFromProblem();
            //}
            if (db.Encounter.Count() <= 0)
            {
                SeedEncounter();
            }
            if (db.Event.Count() <= 0)
            {
                SeedEvent();
            }
            if (db.EBM.Count() <= 0)
            {
                SeedEBM();
            }
            UpdateContextItemType();
        }

        void UpdateContextItemType()
        {
            /*
update [KB].[dbo].[ContextItemDefinition]
set Type='LabTest'
  where CodingSystem ='LAB_TEST_RESULT_DICT'
  
  update [KB].[dbo].[ContextItemDefinition]
set Type='ClinicalProblem'
  where CodingSystem = 'ICD-9' or CodingSystem='ICD-10'
  
  update [KB].[dbo].[ContextItemDefinition]
set Type='Finding'
  where CodingSystem = 'SNOMED-CT'
  
update [KB].[dbo].[ContextItemDefinition]
set Type='Physiology'
  where CodingSystem = 'VITAL_SIGNS_DICT'
             */
        }

        void SeedEBM()
        {
            var ebm1 = new EBM()
            {
                Content = "Oral beta-blockers in all patients without contraindications",
                EvidenceLevel = EnumEvidenceLevel.A.ToString(),
                RecommendationClass = EnumRecommendationClass.I.ToString(),
                Source = "ACC/AHA Guidelines for the Management of Patients With ST-Elevation Myocardial Infarction (2004)",
                URL = "http://www.nepsy.szote.u-szeged.hu/~kincsesz/docs/acc_ami_2004.pdf"
            };
            var ebm2 = new EBM()
            {
                Content = @"beta-blockers are reasonable for patients unless
contraindicated, especially if a tachyarrhythmia or hypertension is present",
                EvidenceLevel = EnumEvidenceLevel.B.ToString(),
                RecommendationClass = EnumRecommendationClass.IIA.ToString(),
                Source = "ACC/AHA Guidelines for the Management of Patients With ST-Elevation Myocardial Infarction (2004)",
                URL = "http://www.nepsy.szote.u-szeged.hu/~kincsesz/docs/acc_ami_2004.pdf"
            };
            db.EBM.Add(ebm1);
            db.EBM.Add(ebm2);
            db.SaveChanges();

            db.MedicalOrderDefinition.Where(x => x.Name.Contains("洛尔")).ToList().ForEach(x =>
            {
                db.MedicalOrderDefinition_EBM_Association.Add(new MedicalOrderDefinition_EBM_Association() { EBM_Id = ebm1.Id, MedicalOrderDefinition_Id = x.Id });
                db.MedicalOrderDefinition_EBM_Association.Add(new MedicalOrderDefinition_EBM_Association() { EBM_Id = ebm2.Id, MedicalOrderDefinition_Id = x.Id });
                //x.EBM.Add(ebm1);
                //x.EBM.Add(ebm2);
                db.SaveChanges();
            });

            PlanDefinition plan;
            EBM ebm;

            plan = db.PlanDefinition.FirstOrDefault(x => x.Name == "冠脉造影");
            if (plan != null)
            {
                ebm = new EBM()
                {
                    Content = @"Primary PCI should be performed as quickly as
    possible, with the goal of a medical contact–toballoon or door-to-balloon time of within 90 minutes.",
                    EvidenceLevel = EnumEvidenceLevel.B.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "ACC/AHA Guidelines for the Management of Patients With ST-Elevation Myocardial Infarction (2004)",
                    URL = "http://www.nepsy.szote.u-szeged.hu/~kincsesz/docs/acc_ami_2004.pdf"
                };
                db.EBM.Add(ebm);
                db.SaveChanges(); // only after called SaveChanges, id is generated.
                db.PlanDefinition_EBM_Association.Add(new PlanDefinition_EBM_Association() { EBM_Id = ebm.Id, PlanDefinition_Id = plan.Id });                
                db.SaveChanges();

                ebm = new EBM()
                {
                    Content = @"Primary PCI should be performed as quickly as
    possible, with the goal of a medical contact–toballoon or door-to-balloon time of within 90 minutes.",
                    EvidenceLevel = EnumEvidenceLevel.A.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = @"Bittl, J. A., S. G. Ellis, et al. (2011). 2011 ACCF/AHA/SCAI Guideline for Percutaneous Coronary Intervention. Cardiol 58: e44-122.",
                    URL = "http://lib.kums.ac.ir/documents/10129/152774/e44.pdf"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.PlanDefinition_EBM_Association.Add(new PlanDefinition_EBM_Association() { EBM_Id = ebm.Id, PlanDefinition_Id = plan.Id });     
                db.SaveChanges();

                ebm = new EBM()
                {
                    Content = @"Primary PCI should be performed for patients less
    than 75 years old with ST elevation or LBBB who develop shock within 36 hours of MI and are suitable for revascularization that can be performed within 18 hours of shock, unless further support is futile because of the patient's wishes or contraindications/unsuitability for further invasive care.",
                    EvidenceLevel = EnumEvidenceLevel.B.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "ACC/AHA Guidelines for the Management of Patients With ST-Elevation Myocardial Infarction (2004)",
                    URL = "http://www.nepsy.szote.u-szeged.hu/~kincsesz/docs/acc_ami_2004.pdf"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.PlanDefinition_EBM_Association.Add(new PlanDefinition_EBM_Association() { EBM_Id = ebm.Id, PlanDefinition_Id = plan.Id });
                db.SaveChanges();
            }


            plan = db.PlanDefinition.FirstOrDefault(x => x.Name.Contains("冠心病"));
            if (plan != null)
            {
                ebm = new EBM()
                {
                    Content = @"Treatment for coronary artery disease usually involves lifestyle changes and, if necessary, drugs and certain medical procedures.",
                    EvidenceLevel = EnumEvidenceLevel.C.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "coronary artery disease Treatments and drugs",
                    URL = "http://www.mayoclinic.com/health/coronary-artery-disease/DS00064/DSECTION=treatments-and-drugs"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.PlanDefinition_EBM_Association.Add(new PlanDefinition_EBM_Association() { EBM_Id = ebm.Id, PlanDefinition_Id = plan.Id });
                db.SaveChanges();
            }

            plan = db.PlanDefinition.FirstOrDefault(x => x.Name.Contains("高血压"));
            if (plan != null)
            {
                ebm = new EBM()
                {
                    Content = @"Emergency Department Algorithm/Protocol
    for Patients With Symptoms and Signs of STEMI",
                    EvidenceLevel = EnumEvidenceLevel.A.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "ACC/AHA Guidelines for the Management of Patients With ST-Elevation Myocardial Infarction (2004)",
                    URL = "/KnowledgeBase/Problems/STEMI_ED_Algorithm.PNG"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.PlanDefinition_EBM_Association.Add(new PlanDefinition_EBM_Association() { EBM_Id = ebm.Id, PlanDefinition_Id = plan.Id });
                db.SaveChanges();
            }

            var rule = db.TriggerRule.FirstOrDefault(x => x.Name == "心肌梗死");
            if (rule != null)
            {
                ebm = new EBM()
                {
                    Content = @"the universal definition of myocardial infarction recommends the measurement of cardiac troponin either as cardiac troponin T (cTnT) or cardiac troponin I (cTnI).",
                    EvidenceLevel = EnumEvidenceLevel.A.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "Thygesen K, Alpert JS, White HD, et al. Universal definition of myocardial infarction. Circulation 2007;116:2634–53.",
                    URL = "http://heart.bmj.com/cgi/ijlink?linkType=FULL&journalCode=circulationaha&resid=116/22/2634"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.TriggerRule_EBM_Association.Add(new TriggerRule_EBM_Association() { EBM_Id = ebm.Id, TriggerRule_Id = rule.Id });
                db.SaveChanges();

                ebm = new EBM()
                {
                    Content = @"The measurement of the MB isoenzyme of creatine kinase by a mass measurement (CK-MB mass) to be significantly inferior to the measurement of cardiac troponin",
                    EvidenceLevel = EnumEvidenceLevel.A.ToString(),
                    RecommendationClass = EnumRecommendationClass.I.ToString(),
                    Source = "Collinson P, Goodacre S, Gaze D, et al. Very early diagnosis of chest pain by point-of-care testing: comparison of the diagnostic efficiency of a panel of cardiac biomarkers compared with troponin measurement alone in the RATPAC trial. Heart 2012;98:312–18.",
                    URL = "http://heart.bmj.com/cgi/ijlink?linkType=ABST&journalCode=heartjnl&resid=98/4/312"
                };
                db.EBM.Add(ebm);
                db.SaveChanges();
                db.TriggerRule_EBM_Association.Add(new TriggerRule_EBM_Association() { EBM_Id = ebm.Id, TriggerRule_Id = rule.Id });
                db.SaveChanges();
            }
        }

        /// <summary>
        /// NOTE:
        /// AssemblyPath and ActivityName should be manually specified by developers
        /// For TriggerRule table, PK is [Name & MajorVersion & MinorVersion]
        /// </summary>
        void SeedRuleSet()
        {
            // use assembly path to construct WorkflowAssemblyPath.
            var path = HttpContext.Current.ApplicationInstance.Server.MapPath("/");

            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string name in names)
            {
                if (name.StartsWith(@"WuKeSong.KnowledgeBase.RuleSet"))
                {
                    if (name.Contains(@"Trigger"))
                    {
                        String str = ResourceHelper.GetEmbededResourceFileAsString(name);
                        var doc = new XmlDocument();
                        doc.LoadXml(str);
                        var nodes = doc.GetElementsByTagName("RuleSet");
                        foreach (XmlNode node in nodes)
                        {
                            TriggerRule rule = new TriggerRule()
                            {
                                Name = node.Attributes["Name"].InnerText,
                                AssemblyPath = path + ConfigurationManager.AppSettings["WorkflowAssemblyPath"],
                                ActivityName = ConfigurationManager.AppSettings["WorkflowActivityName"],
                                TimeStamp = System.DateTime.Now,
                                RuleSet = node.OuterXml,
                                Status = EnumItemStatus.Effective.ToString(),
                                PPV = EnumPPVLevel.Probable.ToString()
                            };
                            db.TriggerRule.Add(rule);
                            rule.ClinicalProblemDefinition = db.ClinicalProblemDefinition.FirstOrDefault(x => x.Name == rule.Name);
                            db.SaveChanges();  
                        }
                    }
                    else if (name.Contains(@"Handler"))
                    {
                        String str = ResourceHelper.GetEmbededResourceFileAsString(name);
                    }
                }
            }
        }

        /// <summary>
        /// In this demo, FK_EMR_Encounter_Id equals CP_ID in the CP db.
        /// </summary>
        void SeedEncounter()
        {
            // Use case 6~25 (20 patients) from CP db as sample EMR data 
            for (int i = 6; i <= 25; i++)
            {
                var CP_ID = i.ToString().PadLeft(8, '0'); // i.ToString("0000");
                // var visit = db2.CP_VISIT.FirstOrDefault(x => x.CP_ID == CP_ID);
                var encounter = new Encounter() { FK_EMR_Encounter_Id = CP_ID};
                db.Encounter.Add(encounter);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Seed Events, meanwhile seed clinical problem instances
        /// </summary>
        void SeedEvent()
        {
            var encounter = db.Encounter.FirstOrDefault();
            if (encounter == null)
                return;

            Event evt;
            Report report;

            // make report & fact sample data using test suite dict   
            //
            // 1st event
            //
            evt = new Event()
            {
                Name = "心肌疾病的实验诊断",
                Encounter = encounter,                
                EventType = EnumEventType.化验单.ToString(),
                TimeStamp = DateTime.Now                
            };
            db.Event.Add(evt);

            report = new Report()
            {
                Event = evt,
                ReportType = EnumReportType.化验单.ToString(),
                TimeStamp = DateTime.Now,
                URL = "/KnowledgeBase/report/心肌酶.jpg"
            };
            db.Report.Add(report);

            var item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肌酸激酶CK-MB测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 6.0,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "α羟基丁酸脱氢酶测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 120,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肌钙蛋白T测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 1.1,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = true,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肌钙蛋白I测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 11,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = true,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肌红蛋白测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 80,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "缺血修饰白蛋白(IMA)测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 90,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "脑钠肽(BNP)测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 70,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "N端-前脑钠肽(NT-ProBNP)测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 66,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "妊娠相关蛋白测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    StringValue = "阴性(-)",
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }

            item = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "同型半胱氨酸测定");
            if (item != null)
            {
                var fact = new Fact()
                {
                    Report = report,
                    NumericValue = 9,
                    Confidence = RandomNumberHelper.GetRandomNumber(98, 100),
                    ContextItemDefinition = item,
                    IsAbnormal = false,
                    LifeSpan = EnumLifeSpan.Temporary.ToString()
                };
                db.UpdateFactCache(encounter.Id, fact);
            }
            db.SaveChanges();

            //
            // 2nd event
            //
            evt = new Event()
            {
                Name = "肾脏疾病的实验诊断",
                Encounter = db.Encounter.FirstOrDefault(),
                EventType = EnumEventType.化验单.ToString(),
                TimeStamp = DateTime.Now
            };
            db.Event.Add(evt);

            report = new Report()
            {
                Event = evt,
                ReportType = EnumReportType.化验单.ToString(),
                TimeStamp = DateTime.Now,
                URL = "/KnowledgeBase/report/肝肾.jpg"
            };
            db.Report.Add(report);

            //
            // 3rd events
            // Generate a series of continuous BP facts to demostrate the 
            //
            for (int i = 0; i < 5; i++)
            {
                evt = new Event()
                {
                    Name = "第" + (i + 1).ToString() + "次" + EnumEventType.体征数据.ToString(),
                    Encounter = db.Encounter.FirstOrDefault(),
                    Description = "",
                    EventType = EnumEventType.体征数据.ToString(),
                    TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(10 - i))
                };

                report = new Report()
                {
                    Event = evt,
                    ReportType = EnumReportType.住院病历.ToString(),
                    TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(10 - i)),
                    URL = "/KnowledgeBase/report/BP.jpg"
                };
                db.Report.Add(report);
            }

            //
            // 4th event
            //
            evt = new Event()
            {
                Name = EnumEventType.入院.ToString(),
                Encounter = db.Encounter.FirstOrDefault(),
                Description = "",
                EventType = EnumEventType.入院.ToString(),
                TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(20))
            };
            db.Event.Add(evt);

            report = new Report()
            {
                Event = evt,
                ReportType = EnumReportType.入院记录.ToString(),
                TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(20)),
                URL = "/KnowledgeBase/report/入院记录.htm"
            };
            db.Report.Add(report);

            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "急性冠脉综合征"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });

            db.SaveChanges();

            //
            // 5th event
            //
            evt = new Event()
            {
                Name = EnumEventType.诊断.ToString(),
                Encounter = db.Encounter.FirstOrDefault(),
                Description = "",
                EventType = EnumEventType.诊断.ToString(),
                TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(20))
            };

            report = new Report()
            {
                Event = evt,
                ReportType = EnumReportType.门诊诊断.ToString(),
                TimeStamp = DateTime.Now.Subtract(TimeSpan.FromDays(20)),
                URL = "/KnowledgeBase/report/RIM.png"
            };
            db.Report.Add(report);

            // in real application, facts are retrived by GetFactsFromreport(report obj/url) I/F.
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "心房扑动"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肺动脉高压"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "肥胖"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "鼻炎"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "远视"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "精神分裂症"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.UpdateFactCache(encounter.Id, new Fact()
            {
                Report = report,
                BooleanValue = true,
                IsAbnormal = true,
                Confidence = 100,
                ContextItemDefinition = db.ContextItemDefinition.FirstOrDefault(x => x.Name == "手术并发症"),
                LifeSpan = EnumLifeSpan.Temporary.ToString()
            });
            db.SaveChanges();
        }
    }
}