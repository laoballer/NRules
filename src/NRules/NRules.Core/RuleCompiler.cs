using System.Collections.Generic;
using System.Linq;
using NRules.Rule;

namespace NRules.Core
{
    internal interface IRuleCompiler
    {
        IEnumerable<ICompiledRule> Compile(IEnumerable<IRuleDefinition> ruleDefinitions);
    }

    internal class RuleCompiler : IRuleCompiler
    {
        public IEnumerable<ICompiledRule> Compile(IEnumerable<IRuleDefinition> ruleDefinitions)
        {
            return ruleDefinitions.Select(CompileRule).ToList();
        }

        private ICompiledRule CompileRule(IRuleDefinition ruleDefinition)
        {
            var actions = new List<IRuleAction>();
            foreach (var actionElement in ruleDefinition.RightHandSide)
            {
                var ruleAction = new RuleAction(actionElement.Expression);
                actions.Add(ruleAction);
            }
            var rule = new CompiledRule(ruleDefinition, actions);
            return rule;
        }
    }
}