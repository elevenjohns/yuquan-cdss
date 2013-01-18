using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuQuan.Models;
using System.Workflow.Activities.Rules;
using System.CodeDom;
using YuQuan.Helpers;

namespace YuQuan.Helpers
{
    public class RuleSetSerive
    {
        public static string AssembleRuleSet(List<LaymanRuleCondition> list, string name)
        {
            if (list == null || list.Count <= 0)
                return string.Empty;

            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var ruleSet = new RuleSet(name);
            var rule = new Rule("Inclusion");
            ruleSet.Rules.Add(rule);

            // Define property and activity reference expressions through CodeDom functionality
            var typeRef = new CodeTypeReferenceExpression("Microsoft.Samples.Rules.RuleSetToolkitUsageSample.TriggerRuleWorkflow"); // for accesing static members
            var thisRef = new CodeThisReferenceExpression(); // for access instance members. i.e. the "this" ref
            var problemsRef = new CodeFieldReferenceExpression(typeRef, "Problems");
            var contextRef = new CodePropertyReferenceExpression(thisRef, "Context");
            var NumericParametersRef = new CodeFieldReferenceExpression(contextRef, "NumericParameters");
            var BooleanParametersRef = new CodeFieldReferenceExpression(contextRef, "BooleanParameters");
            var StringParametersRef = new CodeFieldReferenceExpression(contextRef, "StringParameters");

            CodeFieldReferenceExpression parametersRef = NumericParametersRef;
            CodeBinaryOperatorType innerOperator = CodeBinaryOperatorType.GreaterThanOrEqual;
            var exprPrevious = new CodeBinaryOperatorExpression();
            var outerOperator = CodeBinaryOperatorType.BooleanOr;
            int i = 0;

            foreach (var item in list)
            {
                switch (item.DataType)
                {
                    case EnumDataType.数值型:
                        parametersRef = NumericParametersRef;
                        break;
                    case EnumDataType.布尔型:
                        parametersRef = BooleanParametersRef;
                        break;
                    case EnumDataType.字符串型:
                        parametersRef = StringParametersRef;
                        break;
                }

                switch (item.InnerOperator)
                {
                    case EnumComparisonOperator.GreaterThanOrEqualTo:
                        innerOperator = CodeBinaryOperatorType.GreaterThanOrEqual;
                        break;
                    case EnumComparisonOperator.LessThanOrEqualTo:
                        innerOperator = CodeBinaryOperatorType.LessThanOrEqual;
                        break;
                    case EnumComparisonOperator.GreaterThan:
                        innerOperator = CodeBinaryOperatorType.GreaterThan;
                        break;
                    case EnumComparisonOperator.LessThan:
                        innerOperator = CodeBinaryOperatorType.LessThan;
                        break;
                }

                var expr = new CodeBinaryOperatorExpression();
                expr.Left = new CodeMethodInvokeExpression(parametersRef, "get_Item", new CodePrimitiveExpression[] { new CodePrimitiveExpression(item.Name) });
                expr.Operator = innerOperator;
                expr.Right = new CodePrimitiveExpression(item.NumericValue);


                if (i == 0)
                {
                    exprPrevious = expr;
                }

                if (i > 0)
                {
                    // join two predicates into a single condition
                    var ruleCondition = new CodeBinaryOperatorExpression();
                    ruleCondition.Left = exprPrevious;
                    ruleCondition.Operator = outerOperator;
                    ruleCondition.Right = expr;

                    exprPrevious = ruleCondition;
                }

                // used in next loop
                switch (item.OuterOperator)
                {
                    case EnumLogicalOperator.And:
                        outerOperator = CodeBinaryOperatorType.BooleanAnd;
                        break;
                    case EnumLogicalOperator.Or:
                        outerOperator = CodeBinaryOperatorType.BooleanOr;
                        break;
                }

                i++;
            }
#if false
            // set values
            var BpSysExp = new CodeMethodInvokeExpression(NumericParametersRef, "get_Item", new CodePrimitiveExpression[] { new CodePrimitiveExpression("肱动脉收缩压") });
            var BpDiaExp = new CodeMethodInvokeExpression(NumericParametersRef, "get_Item", new CodePrimitiveExpression[] { new CodePrimitiveExpression("肱动脉舒张压") });

            Rule rule = new Rule("Inclusion");
            ruleSet.Rules.Add(rule);

            // define first predicate: BpSys >= 140 mmHg
            var expr1 = new CodeBinaryOperatorExpression();
            expr1.Left = BpSysExp;
            expr1.Operator = CodeBinaryOperatorType.GreaterThanOrEqual;
            expr1.Right = new CodePrimitiveExpression(140);

            // define second predicate: BpDia >= 90 mmHg
            var expr2 = new CodeBinaryOperatorExpression();
            expr2.Left = BpDiaExp;
            expr2.Operator = CodeBinaryOperatorType.GreaterThanOrEqual;
            expr2.Right = new CodePrimitiveExpression(90);

            // join the first two predicates into a single condition
            var ruleCondition = new CodeBinaryOperatorExpression();
            ruleCondition.Left = expr1;
            ruleCondition.Operator = CodeBinaryOperatorType.BooleanOr;
            ruleCondition.Right = expr2;

            rule.Condition = new RuleExpressionCondition(ruleCondition);
#endif
            rule.Condition = new RuleExpressionCondition(exprPrevious);
            var action = new CodeMethodInvokeExpression(problemsRef, "Add", new CodePrimitiveExpression[] { new CodePrimitiveExpression(name) });
            rule.ThenActions.Add(new RuleStatementAction(action));

            return RuleSetHelper.Serialize(ruleSet);
        }

        //[TODO] unfinished. () not considered. Only numeric items are considered
        public static List<LaymanRuleCondition> BreakDownRuleCondition(Rule rule)
        {
            using (var db = new KBEntities())
            {
                var list = new List<LaymanRuleCondition>();

                var condition = rule.Condition.ToString()
                        .Replace("this.Context.NumericParameters.get_Item(\"", "")
                        .Replace("this.Context.BooleanParameters.get_Item(\"", "")
                        .Replace("this.Context.StringParameters.get_Item(\"", "")
                        .Replace("\")", ""); ;

                var delimiters = new List<string>();
                foreach (EnumLogicalOperator value in Enum.GetValues(typeof(EnumLogicalOperator)))
                {
                    delimiters.Add(EnumHelper.GetEnumDescription<EnumLogicalOperator>(value));
                }
                string[] parts = condition.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                string temp = condition;
                string delimiter = "&&";
                foreach (var part in parts)
                {
                    var item = new LaymanRuleCondition();

                    temp = temp.Substring(part.Length).Trim();
                    delimiter = delimiters.FirstOrDefault(x => temp.StartsWith(x) == true);
                    item.OuterOperator = EnumHelper.GetEnumFromDescription<EnumLogicalOperator>(delimiter);

                    var innerDelimiters = new List<string>();
                    foreach (EnumComparisonOperator value in Enum.GetValues(typeof(EnumComparisonOperator)))
                    {
                        innerDelimiters.Add(EnumHelper.GetEnumDescription<EnumComparisonOperator>(value));
                    }
                    string[] innerParts = part.Split(innerDelimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                    if (innerParts.Length == 2) //lhs & rhs
                    {
                        var lhs = innerParts[0].Trim();
                        var rhs = innerParts[1].Trim();

                        var contextItem = db.ContextItemDefinition.FirstOrDefault(x => x.Name == lhs);
                        item.Name = contextItem.Name;
                        item.Unit = contextItem.Unit;
                        item.DataType = (EnumDataType)Enum.Parse(typeof(EnumDataType), contextItem.DataType);
                        item.NumericValue = double.Parse(rhs);
                        item.InnerOperator = EnumHelper.GetEnumFromDescription<EnumComparisonOperator>(part.Replace(lhs, "").Replace(rhs, "").Trim());

                        list.Add(item);
                    }
                }
                return list;
            }
        }
    }
}