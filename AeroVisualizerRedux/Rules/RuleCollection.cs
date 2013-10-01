using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace RuleManager
{
    public class RuleCollection : ICollection<Rule>
    {
        public int Count
        {
            get { return rules.Count; }
        }

        public bool IsReadOnly
        {
            get { return isRO; }
        }

        private List<Rule> rules = new List<Rule>();
        public List<Output> Outputs { get; private set; }
        public dynamic Util;

        private bool isRO = false;

        public RuleCollection( Rule[] rulesArray )
        {
            rules = rulesArray.ToList<Rule>();
            initResources();
        }
        public RuleCollection(List<Rule> rulesList)
        {
            rules = rulesList;
            initResources();
        }
        public RuleCollection()
        {
            initResources();
        }

        private void initResources()
        {
            Util = new ExpandoObject();
            Outputs = new List<Output>();
        }

        public Output AddOutput(Output output)
        {
            if (!Outputs.Contains(output))
            {
                Outputs.Add(output);
            }

            return output;
        }

        /// <summary>
        /// Request that a rule with the selected output is run, and return its value
        /// </summary>
        /// <param name="output">The specific output instance that'll be solved</param>
        /// <returns>The output value returned by the rule collection</returns>
        public float GetRulesOutput(string output)
        {
            float val=0;
            foreach (var rule in this.rules )
            {
                if (rule.OutputVal.Name != output) continue;

                float ruleOut = rule.ExecuteModifiers();
                val = CalculateAppendResult(rule.AppendMode, val, ruleOut);
            }

            return val;
        }

        /// <summary>
        /// Request that a rule with the selected output is run, and return its value
        /// </summary>
        /// <param name="output">The specific output instance that'll be solved</param>
        /// <returns>The output value returned by the rule collection</returns>
        public float GetRulesOutput(Output output)
        {
            float val = 0;
            foreach (var rule in this.rules)
            {
                if (rule.OutputVal != output) continue;

                float ruleOut = rule.ExecuteModifiers();
                val = CalculateAppendResult(rule.AppendMode, val, ruleOut);
            }

            return val;
        }

        public IEnumerator<Rule> GetEnumerator()
        {
            return rules.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(Rule item)
        {
            return rules.Contains(item);
        }

        public bool Contains(Rule item, EqualityComparer<Rule> comp)
        {
            return rules.Contains(item, comp);
        }

        public void Add(Rule r)
        {
            rules.Add(r);
            r.ParentCollection = this;
        }

        public void Clear()
        {
            rules.Clear();
        }

        public void CopyTo(Rule[] array, int arrayIndex)
        {
            rules.CopyTo(array, arrayIndex);
        }

        public bool Remove(Rule item)
        {
            return rules.Remove(item);
        }

        /// <summary>
        /// Depending on the append mode of the output, calculate the new value if multiple rules have the same output
        /// </summary>
        /// <param name="method">The append mode to apply to the current value and the new rule's value</param>
        /// <param name="curValue">The value calculated from previous rules</param>
        /// <param name="ruleValue">The new rule's raw value</param>
        /// <returns>The new value, calculated dependent on the append method</returns>
        private float CalculateAppendResult(AppendMethod method, float curValue, float ruleValue)
        {
            switch (method)
            {
                case AppendMethod.Set:
                    return ruleValue;

                case AppendMethod.Add:
                    return ruleValue + curValue;

                case AppendMethod.Multiply:
                    return ruleValue * curValue;
            }

            return curValue;
        }

        /// <summary>
        /// Set all of the rules in this collection to be a child of the specified RuleCollection
        /// </summary>
        /// <param name="parent">The parent of the rules</param>
        private void SetRulesParent(RuleCollection parent)
        {
            foreach (Rule r in this)
            {
                r.ParentCollection = this;
            }
        }
    }
}
