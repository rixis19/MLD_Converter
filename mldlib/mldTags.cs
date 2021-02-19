using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mldlib
{
    class mldTags
    {
        readonly string GRND = "01000111010100100100111001000100";//Only in level file

        readonly string NMLD = "01001110010011010100110001000100";//Only exist in files with more than one object

        readonly string AKLZ = "01000001010010110100110001011010";//File is still encrypted if this is present

        readonly string NJTL = "01001110010010100101010001001100";//Ninja Texture List

        readonly string NJCM = "01001110010010100100001101001101";

        readonly string NMDM = "01000111010000110100100101011000"; 

        readonly string POF0 = "01010000010011110100011000110000";

        readonly string NJPX = "01001110010010100101000001011000"; 

        //Gamecube exculsive tags
        readonly string GVRT = "01000111010101100101001001010100";

        readonly string GCIX = "01000111010000110100100101011000";

        //Dreamcast exculsive tags
        readonly string GBIX = "01000111010000100100100101011000";

        readonly string PVRT = "01010000010101100101001001010100";
    }
}
