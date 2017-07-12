using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FrontlineCodeChallenge.Models
{
    public class PropertyGroup
    {
        public List<Property> Properties { get; private set; } = new List<Property>();
        public int MaxDepth { get; set; }
        public int PropertyCount { get; set; }


        /// <summary>
        ///     Initializes this class by parsing the input string;
        /// </summary>
        /// <param name="input">
        ///     The input string to be parsed.
        /// </param>
        public void LoadFromString(string input)
        {
            //reset class to default.
            Properties.Clear();
            MaxDepth = 0;
            PropertyCount = 0;

            //initialize counters
            var currentDepth = 0;
            Property currentProperty = null;
            var currentPropertyList = Properties;
            var currentPropertyValue = "";
            Property newProperty;

            //parse the string
            for (var x = 0; x < input.Length; x++)
            {
                var c = input[x];
                switch (c)
                {
                    //a comma means a property value is done being inpit
                    case ',':
                        //make sure we are not adding blank properties
                        if (currentPropertyValue == "")
                        {
                            break;
                        }

                        //add the property
                        newProperty = new Property
                        {
                            Value = currentPropertyValue,
                            Depth = currentDepth,
                            ParentProperty = currentProperty
                        };
                        currentPropertyValue = "";
                        currentPropertyList.Add(newProperty);

                        //increment any counters
                        PropertyCount++;

                        Debug.WriteLine($"Added prop (by comma): {newProperty.Value} with depth {newProperty.Depth}");
                        break;

                    //an open parenthesis means we are now adding subproperties to the current property and depth increases
                    case '(':
                        //input strings may begin with an open parens, ignore these
                        if (currentDepth == 0 &&
                            Properties.Count == 0)
                        {
                            break;
                        }

                        //add the current property
                        newProperty = new Property
                        {
                            Value = currentPropertyValue,
                            Depth = currentDepth,
                            ParentProperty = currentProperty
                        };
                        currentPropertyList.Add(newProperty);

                        //increment and reset any counters
                        PropertyCount++;
                        currentDepth++;
                        if (currentDepth > MaxDepth)
                        {
                            MaxDepth = currentDepth;
                        }
                        currentPropertyValue = "";
                        currentProperty = newProperty; //we are now adding to this property instead of the previous list
                        currentPropertyList = newProperty.SubProperties;


                        Debug.WriteLine($"Added prop: {newProperty.Value} with depth {newProperty.Depth}");
                        break;

                    //close parens means we are back to adding to the parent
                    case ')':
                        //add the last property to the subproperties
                        if (currentPropertyValue != "")
                        {
                            newProperty = new Property
                            {
                                Value = currentPropertyValue,
                                Depth = currentDepth,
                                ParentProperty = currentProperty
                            };
                            currentPropertyList.Add(newProperty);

                            //reset counters
                            PropertyCount++;
                            currentPropertyValue = "";

                            Debug.WriteLine($"Added prop: {newProperty.Value} with depth {newProperty.Depth}");
                        }

                        //depth goes down, check for negative dept)
                        currentDepth--;
                        if (currentDepth < 0)
                        {
                            currentDepth = 0;
                        }

                        //change the current list we are adding to
                        if (currentDepth == 0 || 
                            currentProperty?.ParentProperty == null)
                        {
                            currentProperty = null;
                            currentPropertyList = Properties;
                        }
                        else
                        {
                            currentProperty = currentProperty.ParentProperty;
                            currentPropertyList = currentProperty.SubProperties;
                        }
                        break;

                    //add any characters to the value
                    default:
                        currentPropertyValue += c;
                        break;

                    //ignore these characters
                    case '"':
                    case ' ':
                        break;

                    
                }
            }



            //check if the user forgot to close parens
            if (currentPropertyValue != "")
            {
                newProperty = new Property
                {
                    Value = currentPropertyValue,
                    Depth = currentDepth
                };
                currentPropertyList.Add(newProperty);
                PropertyCount++;

                Debug.WriteLine($"Added prop: {newProperty.Value} with depth {newProperty.Depth}");
            }
            Debug.WriteLine("Parse complete");
        }

        /// <summary>
        ///     Converts the properties contained within this group to a human readable string.
        /// </summary>
        /// <param name="alphabetizeOutput">
        ///     Setting to true will display properties (and all subProperties) in alphabetical order.
        /// </param>
        /// <returns>
        ///     A string representation of this property group.
        /// </returns>
        public string ConvertToString(bool alphabetizeOutput = false)
        {
            if (alphabetizeOutput)
            {
                //if performance is an issue this can be converted to a custom-written method, but for this data set it seemed OK
                Properties = Properties.OrderBy(p => p.Value).ToList();
            }
            var outstring = "";
            foreach (var property in Properties)
            {
                outstring += property.ConvertToString(alphabetizeOutput);
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
            return ConvertToString();
        }
    }
}
