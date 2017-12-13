/* 
 Copyright <2017> <David L Ralph>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Collections.Generic;
using System.ComponentModel;

namespace Core.Targeting
{
    /// <summary>
    /// Data types and enums for acoustic targets
    /// </summary>
    public sealed class AcousticTargets
    {
        /// <summary>
        /// Original string constants as used in the PCD spreadsheet (expanded)
        /// </summary>
        #region Full Constants
        public const string no_Low_Pass_Target = "No Low Pass Target";
        public const string no_High_Pass_Target = "No High Pass Target";
        public const string first_Order_Butterworth = "First Order Butterworth";
        public const string second_Order_Butterworth = "Second Order Butterworth";
        public const string third_Order_Butterworth = "Third Order Butterworth";
        public const string fourth_Order_Butterworth = "Fourth Order Butterworth";
        public const string fifth_Order_Butterworth = "Fifth Order Butterworth";
        public const string sixth_Order_Butterworth = "Sixth Order Butterworth";
        public const string seventh_Order_Butterworth = "Seventh Order Butterworth";
        public const string eighth_Order_Butterworth = "Eighth Order Butterworth";
        public const string second_Order_LinkwitzRiley = "Second Order Linkwitz-Riley";
        public const string fourth_Order_LinkwitzRiley = "Fourth Order Linkwitz-Riley";
        public const string sixth_Order_LinkwitzRiley = "Sixth Order Linkwitz-Riley";
        public const string eighth_Order_LinkwitzRiley = "Eighth Order Linkwitz-Riley";
        public const string second_Order_Bessel = "Second Order Bessel";
        public const string third_Order_Bessel = "Third Order Bessel";
        public const string fourth_Order_Bessel = "Fourth Order Bessel";
        public const string fifth_Order_Bessel = "Fifth Order Bessel";
        public const string sixth_Order_Bessel = "Sixth Order Bessel";
        public const string seventh_Order_Bessel = "Seventh Order Bessel";
        public const string eighth_Order_Bessel = "Eighth Order Bessel";
        public const string second_Order_Bessel_PM = "Second Order Bessel Phase Match";
        public const string third_Order_Bessel_PM = "Third Order Bessel Phase Match";
        public const string fourth_Order_Bessel_PM = "Fourth Order Bessel Phase Match";
        public const string fifth_Order_Bessel_PM = "Fifth Order Bessel Phase Match";
        public const string sixth_Order_Bessel_PM = "Sixth Order Bessel Phase Match";
        public const string seventh_Order_Bessel_PM = "Seventh Order Bessel Phase Match";
        public const string eighth_Order_Bessel_PM = "Eighth Order Bessel Phase Match";
        public const string second_Order_Bessel_3db = "Second Order Bessel 3db";
        public const string third_Order_Bessel_3db = "Third Order Bessel 3db";
        public const string fourth_Order_Bessel_3db = "Fourth Order Bessel 3db";
        public const string fifth_Order_Bessel_3db = "Fifth Order Bessel 3db";
        public const string sixth_Order_Bessel_3db = "Sixth Order Bessel 3db";
        public const string seventh_Order_Bessel_3db = "Seventh Order Bessel 3db";
        public const string eighth_Order_Bessel_3db = "Eighth Order Bessel 3db";
        public const string second_Order_Bessel_Flattest = "Second Order Bessel Flattest";
        public const string third_Order_Bessel_Flattest = "Third Order Bessel Flattest";
        public const string fourth_Order_Bessel_Flattest = "Fourth Order Bessel Flattest";
        public const string fifth_Order_Bessel_Flattest = "Fifth Order Bessel Flattest";
        public const string sixth_Order_Bessel_Flattest = "Sixth Order Bessel Flattest";
        public const string seventh_Order_Bessel_Flattest = "Seventh Order Bessel Flattest";
        public const string eighth_Order_Bessel_Flattest = "Eighth Order Bessel Flattest";
        public const string imported_Target_Active = "Imported Target Active"; 
        #endregion

        public enum FilterName
        {
            [Description("No Target")]
            None,
            Butterworth,
            [Description("Linkwitz-Riley")] // Workaround to allow a dash in the enum value
            LinkwitzRiley,
            [Description("Bessel: Phase-Matched")]
            BesselPhaseMatch,
            [Description("Bessel: Flattest Sum")]
            BesselFlattest,
            [Description("Bessel: -3db at Fc")]
            Bessel3db,
            [Description("Bessel: Standard (Reference)")]
            Bessel
        }

        public enum Order
        {
            zero = 0, first = 1, second = 2, third = 3, fourth = 4, fifth = 5, sixth = 6, seventh = 7, eighth = 8
        }

        #region unused
        /// <summary>
        /// Acoustic Target String via enum description
        /// </summary>
        /*
         * private enum Selection
        {
            [Description(no_Low_Pass_Target)]
            No_Low_Pass_Target,
            [Description(no_High_Pass_Target)]
            No_High_Pass_Target,
            [Description(first_Order_Butterworth)]
            First_Order_Butterworth,
            [Description(second_Order_Butterworth)]
            Second_Order_Butterworth,
            [Description(third_Order_Butterworth)]
            Third_Order_Butterworth,
            [Description(fourth_Order_Butterworth)]
            Fourth_Order_Butterworth,
            [Description(fifth_Order_Butterworth)]
            Fifth_Order_Butterworth,
            [Description(sixth_Order_Butterworth)]
            Sixth_Order_Butterworth,
            [Description(seventh_Order_Butterworth)]
            Seventh_Order_Butterworth,
            [Description(eighth_Order_Butterworth)]
            Eighth_Order_Butterworth,
            [Description(second_Order_LinkwitzRiley)]
            Second_Order_LinkwitzRiley,
            [Description(fourth_Order_LinkwitzRiley)]
            Fourth_Order_LinkwitzRiley,
            [Description(sixth_Order_LinkwitzRiley)]
            Sixth_Order_LinkwitzRiley,
            [Description(eighth_Order_LinkwitzRiley)]
            Eighth_Order_LinkwitzRiley,
            [Description(second_Order_Bessel)]
            Second_Order_Bessel,
            [Description(third_Order_Bessel)]
            Third_Order_Bessel,
            [Description(fourth_Order_Bessel)]
            Fourth_Order_Bessel,
            [Description(fifth_Order_Bessel)]
            Fifth_Order_Bessel,
            [Description(sixth_Order_Bessel)]
            Sixth_Order_Bessel,
            [Description(second_Order_Bessel_PM)]
            Second_Order_BesselPhaseMatch,
            [Description(third_Order_Bessel_PM)]
            Third_Order_BesselPhaseMatch,
            [Description(fourth_Order_Bessel_PM)]
            Fourth_Order_BesselPhaseMatch,
            [Description(fifth_Order_Bessel_PM)]
            Fifth_Order_BesselPhaseMatch,
            [Description(sixth_Order_Bessel_PM)]
            Sixth_Order_BesselPhaseMatch,
            [Description(seventh_Order_Bessel_PM)]
            Seventh_Order_BesselPhaseMatch,
            [Description(eighth_Order_Bessel_PM)]
            Eighth_Order_BesselPhaseMatch,
            [Description(second_Order_Bessel_Flattest)]
            Second_Order_BesselFlattest,
            [Description(third_Order_Bessel_Flattest)]
            Third_Order_BesselFlattest,
            [Description(fourth_Order_Bessel_Flattest)]
            Fourth_Order_BesselFlattest,
            [Description(fifth_Order_Bessel_Flattest)]
            Fifth_Order_BesselFlattest,
            [Description(sixth_Order_Bessel_Flattest)]
            Sixth_Order_BesselFlattest,
            [Description(seventh_Order_Bessel_Flattest)]
            Seventh_Order_BesselFlattest,
            [Description(eighth_Order_Bessel_Flattest)]
            Eighth_Order_BesselFlattest,
            [Description(second_Order_Bessel_3db)]
            Second_Order_Bessel3db,
            [Description(third_Order_Bessel_3db)]
            Third_Order_Bessel3db,
            [Description(fourth_Order_Bessel_3db)]
            Fourth_Order_Bessel3db,
            [Description(fifth_Order_Bessel_3db)]
            Fifth_Order_Bessel3db,
            [Description(sixth_Order_Bessel_3db)]
            Sixth_Order_Bessel3db,
            [Description(seventh_Order_Bessel_3db)]
            Seventh_Order_Bessel3db,
            [Description(eighth_Order_Bessel_3db)]
            Eighth_Order_Bessel3db,
            [Description(imported_Target_Active)]
            Imported_Target_Active
        };
        */ 
        #endregion

        // Use FilterName + (orderHP or orderLP) as the key.
        // This dictionary will hold the original strings first  used for the user full filter ScrollBox selector.
        // Those strings are still used in the business code as of the creation of this dictionary.
        // It would be useful to refactor this out.
        private static Dictionary<string, string> fullFilter = new Dictionary<string, string>
        {
            { FilterName.Butterworth.ToString() + "1", first_Order_Butterworth  },
            { FilterName.Butterworth.ToString() + "2", second_Order_Butterworth },
            { FilterName.Butterworth.ToString() + "3", third_Order_Butterworth  },
            { FilterName.Butterworth.ToString() + "4", fourth_Order_Butterworth },
            { FilterName.Butterworth.ToString() + "5", fifth_Order_Butterworth  },
            { FilterName.Butterworth.ToString() + "6", sixth_Order_Butterworth  },
            { FilterName.Butterworth.ToString() + "7", seventh_Order_Butterworth },
            { FilterName.Butterworth.ToString() + "8", eighth_Order_Butterworth },

            { FilterName.LinkwitzRiley.ToString() + "2", second_Order_LinkwitzRiley },
            { FilterName.LinkwitzRiley.ToString() + "4", fourth_Order_LinkwitzRiley },
            { FilterName.LinkwitzRiley.ToString() + "6", sixth_Order_LinkwitzRiley  },
            { FilterName.LinkwitzRiley.ToString() + "8", eighth_Order_LinkwitzRiley },
            
//          { FilterName.Bessel.ToString() + "1", first_Order_Bessel },
            { FilterName.Bessel.ToString() + "2", second_Order_Bessel },
            { FilterName.Bessel.ToString() + "3", third_Order_Bessel  },
            { FilterName.Bessel.ToString() + "4", fourth_Order_Bessel },
//          { FilterName.Bessel.ToString() + "5", fifth_Order_Bessel  },
//          { FilterName.Bessel.ToString() + "6", sixth_Order_Bessel  },
//          { FilterName.Bessel.ToString() + "7", seventh_Order_Bessel  },
//          { FilterName.Bessel.ToString() + "8", eighth_Order_Bessel  },
            
//          { FilterName.BesselPhaseMatch.ToString() + "1", first_Order_Bessel_PM },
            { FilterName.BesselPhaseMatch.ToString() + "2", second_Order_Bessel_PM },
            { FilterName.BesselPhaseMatch.ToString() + "3", third_Order_Bessel_PM  },
            { FilterName.BesselPhaseMatch.ToString() + "4", fourth_Order_Bessel_PM },
//          { FilterName.BesselPhaseMatch.ToString() + "5", fifth_Order_Bessel_PM },
//          { FilterName.BesselPhaseMatch.ToString() + "6", sixth_Order_Bessel_PM },
//          { FilterName.BesselPhaseMatch.ToString() + "7", seventh_Order_Bessel_PM },
//          { FilterName.BesselPhaseMatch.ToString() + "8", eighth_Order_Bessel_PM },

            { FilterName.BesselFlattest.ToString() + "2", second_Order_Bessel_Flattest },
            { FilterName.BesselFlattest.ToString() + "3", third_Order_Bessel_Flattest  },
            { FilterName.BesselFlattest.ToString() + "4", fourth_Order_Bessel_Flattest },

            { FilterName.Bessel3db.ToString() + "2", second_Order_Bessel_3db },
            { FilterName.Bessel3db.ToString() + "3", third_Order_Bessel_3db  },
            { FilterName.Bessel3db.ToString() + "4", fourth_Order_Bessel_3db },
        };

        public Dictionary<string, string> FullFilter
        {
            get
            {
                return fullFilter;
            }

            set
            {
                fullFilter = value;
            }
        }

        /// <summary>
        /// Returns the string value of the enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static string FilterNameToString(FilterName value)
        //{
        //    var type = typeof(FilterName);
        //    var memInfo = type.GetMember(value.ToString());
        //    var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    var description = ((DescriptionAttribute)attributes[0]).Description;
        //    return description;
        //}

    }
}
