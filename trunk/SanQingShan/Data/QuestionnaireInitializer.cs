using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SanQingShan.Models;

namespace SanQingShan.Data
{
    public static class QuestionnaireInitializer
    {
        public static void Seed()
        {
            using (var db = new QuestionnaireContainer())
            {
                Question question;
                Option option;

                question = new Question()
                {
                    Literal = "根据您的使用体验，目前的临床路径系统存在哪些问题？（多选,在相应的项目前勾选）"
                };                

                option = new Option()
                {
                    Literal = "系统太慢"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                option = new Option()
                {
                    Literal = "临床路径对于临床工作没有帮助或好处"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                option = new Option()
                {
                    Literal = "不熟悉临床路径系统"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                option = new Option()
                {
                    Literal = "使用不方便，比如下医嘱或开申请不快捷"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                option = new Option()
                {
                    Literal = "临床路径模版制定不科学"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                option = new Option()
                {
                    Literal = "临床路径不适用于大部分病人，比如病情复杂，并发症或合并症等等"
                };
                db.ConceptSet.Add(option);
                question.Option.Add(option);

                db.ConceptSet.Add(question);

                var questionnaire = new Questionnaire()
                {
                    Name = "临床路径系统使用情况调查问卷",
                    Version = "1.0"
                };
                questionnaire.Question.Add(question);
                db.QuestionnaireSet.Add(questionnaire);
                db.SaveChanges();
            }
        }
    }
}