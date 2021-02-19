using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace mldlib
{
	public class mldFile
	{
		public class mldHeader
		{
			public int objTotal { get; set; }       //Total Objects in File
			[Browsable(false)]
			public mldObject objTable { get; set; } //mldObject Pointer to array of objects in file.
			[Browsable(false)]
			public int objTable_eof { get; set; }   //Points to the end of the mldOject array. Might be a size value instead of pointer.
			[Browsable(false)]
			public int grndInfo { get; set; }       //Points to just prior to the main data start. Points to GRND array in Level files.
			[Browsable(false)]
			public int texArr { get; set; }         //Pointer to the total for the mldTexname structure which is just before the mldTexname array.
			public string NMLD { get; set; }        //NMLD string, only exists in MLD files with more than one object.
		}

		public class mldObject
		{
			public int index { get; set; }  
			public int yRot { get; set; }
			public int unkInt1 { get; set; }
			public int unkInt2 { get; set; }
			public mldObjectMaster objectMaster { get; set; }
			public mldGround Ground { get; set; }
			public int unkInt3 { get; set; }
			public int unkInt4 { get; set; }
			public int Texlist { get; set; }
			public string Name { get; set; }	//String for the name of the object. Length = 32.
			public float unkFloat1 { get; set; }
			public float unkFloat2 { get; set; }
			public float unkFloat3 { get; set; }
			public int unkInt5 { get; set; }
			public int unkInt6 { get; set; }
			public int unkInt7 { get; set; }
			public float unkFloat4 { get; set; }
			public float unkFloat5 { get; set; }
			public float unkFloat6 { get; set; }
		}

		public class mldObjectMaster//Object master, holds each individual anim or ObjectEntry assigned to this object
		{
			public int objEntryTotal { get; set; }
			public mldObjectEntry objEntry { get; set; }
		}

		public class mldObjectEntry //Each individual animation and the object that goes with it
		{
			public int Object { get; set; }	//Pointer to NJS_OBJECT
			public int Motion { get; set; }	//Pointer to NJS_MOTION
			public int TexList { get; set; }	//Pointer to NJS_TEXLIST
		}

		public class mldGround
		{

		}

		public class mldTexname
		{
			public string Texname { get; set; }	//Name for the texture. Length = 28
			public int texData { get; set; }	//Pointer to related PVR/GVR texture data.
		}
	}
}
