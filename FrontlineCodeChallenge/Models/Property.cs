using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontlineCodeChallenge.Models
{
    public class Property
    {
        public string Value { get; set; }
        public int Depth { get; set; }
        public Property ParentProperty { get; set; }
        public List<Property> SubProperties { get; private set; } = new List<Property>();

        /// <summary>
        ///     Converts this property and its subproperties recursively to a human readable string.
        /// </summary>
        /// <param name="alphabetizeOutput">
        ///     Setting to true will display properties (and all subProperties) in alphabetical order.
        /// </param>
        /// <returns>
        ///     A string representation of this property group.
        /// </returns>
        public string ConvertToString(bool alphabetizeOutput = false)
        {
            var outstring = "";
            if (alphabetizeOutput)
            {
                //if performance is an issue this can be converted to a custom-written method
                SubProperties = SubProperties.OrderBy(p => p.Value).ToList();
            }
            for (var x = 0; x < Depth; x++)
            {
                outstring += "-";
            }
            outstring += Value;
            outstring += Environment.NewLine;

            foreach (var subProperty in SubProperties)
            {
                outstring += subProperty.ConvertToString(alphabetizeOutput);
            }

            return outstring;
        }

        /// <summary>
        ///     Overrides the default ToString(). Will not alphabetize output.
        /// </summary>
        /// <returns>
        ///     A string representation of this property group.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
