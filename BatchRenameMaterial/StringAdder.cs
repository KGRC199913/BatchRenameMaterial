using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRenameMaterial
{
    [Serializable]
    class StringAdder : IStringProcessor
    {
        private StringAdderArg addrule;
        public string Description
            => $"Add token \"{addrule.Token }\" to position \"{addrule.Position}\"";

        public StringAdderArg Arg { get => addrule; set => addrule = value; }
        public bool ApplyToExtension { get; set; }

        public string Process(string haystack)
        {
            var position = addrule.Position;
            if (position > haystack.Length)
                position = haystack.Length;
            return haystack.Insert(position, addrule.Token);
        }
    }
}
