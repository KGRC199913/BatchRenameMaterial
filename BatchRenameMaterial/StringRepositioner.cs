using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    [Serializable]
    class StringRepositioner : IStringProcessor
    {
        private StringRepositionArg repositionRule;
        public string Description
            => $"Reposition needle match pattern \"{repositionRule.Pattern}\" to position \"{repositionRule.Position}\"";

        internal StringRepositionArg Arg { get => repositionRule; set => repositionRule = value; }

        public bool ApplyToExtension { get; set; }

        public string Process(string haystack)
        {
        

            var opt = repositionRule.IgnoreCase;
            MatchCollection matches;
            if (opt)
            {
                matches = Regex.Matches(haystack, repositionRule.Pattern, RegexOptions.IgnoreCase);
                haystack = Regex.Replace(haystack, repositionRule.Pattern, @"|", RegexOptions.IgnoreCase);
            }
            else
            {
                matches = Regex.Matches(haystack, repositionRule.Pattern);
                haystack = Regex.Replace(haystack, repositionRule.Pattern, @"|");
            }
            StringBuilder strMatchedBuilder = new StringBuilder();
            foreach (Match match in matches)
            {
                strMatchedBuilder.Append(match.Value);
            }

            var position = repositionRule.Position;
            if (position > haystack.Length)
                position = haystack.Length;

            haystack = haystack.Insert(position, strMatchedBuilder.ToString());
            haystack = Regex.Replace(haystack, @"\|+", String.Empty);

            return haystack;
        }
    }
}
