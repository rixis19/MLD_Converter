using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mldlib.mldFile;

namespace mldlib
{
    public class mldFunctions
    {
        //Gets the Endianness of the file
        public bool checkEndianness(string file)
        {
            bool bigEndian = false;
            string trueName = Path.GetFileName(file);

            if (trueName.Contains("_d"))
                bigEndian = true;

            return bigEndian;
        }
        
        //File is converted to binary code via a byte array
        public byte[] convertToBinary(string file)
        {
            byte[] fileBytes = File.ReadAllBytes(file);

            return fileBytes;
        }

        //Gets the amount of objs in the file
        public int getObjTotal(byte[] arr, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i];
            }

            if(endian)
                Array.Reverse(b);
            int total = BitConverter.ToInt32(b, 0);

            return total;
        }

        //Gets the pointer to the obj array
        public int getObjTablePtr(byte[] arr, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i+4];
            }

            if (endian)
                Array.Reverse(b);
            int tablePtr = BitConverter.ToInt32(b, 0);

            return tablePtr;
        }

        //Gets the pointer to the end of the obj array
        public int getObjTableEOF(byte[] arr, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + 8];
            }

            if (endian)
                Array.Reverse(b);
            int tablePtr = BitConverter.ToInt32(b, 0);

            return tablePtr;
        }

        //Checks if the file is a level file
        public bool isNMLD(byte[] arr, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i+20];
            }

            if (endian)
                Array.Reverse(b);
            if (1313688644 == BitConverter.ToInt32(b, 0))
                return true;
            else
                return false;
        }

        //Gets the Texture Array Amount pointer
        public int getTexArr(byte[] arr, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + 16];
            }

            if (endian)
                Array.Reverse(b);

            int texArrPtr = BitConverter.ToInt32(b, 0);

            return texArrPtr;
        }

        public mldHeader makeHeader(byte[] arr, bool endian)
        {
            mldHeader header = new mldHeader();
            header.objTotal = getObjTotal(arr, endian);
            header.objTable_eof = getObjTableEOF(arr, endian);
            header.objTable = makeObjTable(arr, header.objTotal, getObjTablePtr(arr, endian), header.objTable_eof, endian);
            //header.grndInfo = getgrndInfo(); - TODO: Find the grndInfo
            header.texArr = getTexArr(arr, endian);
            if (isNMLD(arr, endian))
                header.NMLD = "NMLD";

            return header;
        }

        //Makes the Object Table
        public mldObject[] makeObjTable(byte[] arr, int objTotal, int objTablePtr, int objTablePtr_EOF, bool endian)
        {
            mldObject[] obj = new mldObject[0];
            int[] mldObjArray = new int[0];
            int cursor = objTablePtr;
            int objEntryMasterPtr = 0;

            //Gets all the obj pointers
            for (int i = 0; i < objTotal; i++)
            {
                cursor += 20;
                byte[] b = new byte[4];
                for (int j = 0; j <= 3; j++)
                {
                    b[j] = arr[j + cursor];
                }

                if (endian)
                    Array.Reverse(b);

                objEntryMasterPtr = BitConverter.ToInt32(b, 0);

                if (objEntryMasterPtr != 0)
                {
                    byte[] t = new byte[4];
                    for (int j = 0; j <= 3; j++)
                    {
                        t[j] = arr[j + objEntryMasterPtr + 4];
                    }

                    if (endian)
                        Array.Reverse(t);

                    if (BitConverter.ToInt32(t, 0) != 0)
                        obj.Append(makeMLDObject(arr, cursor - 20, endian));
                }
                cursor += 84;
            }

            return obj;
        }

        public mldObject makeMLDObject(byte[] arr, int cursor, bool endian)
        {
            //Anything commented out is something I cannot find/don't know how to/don't know what it is, for the moment this will provide what I could find
            mldObject mldObj = new mldObject();

            mldObj.index = getIndex(arr, cursor, endian);
            //mldObj.yRot = getYRot(arr, cursor, endian);
            //mldObj.unkInt1 = getUnkInt1(arr, cursor, endian);
            //mldObj.unkInt2 = getUnkInt2(arr, cursor, endian);
            mldObj.objectMaster = makeObjMaster(arr, cursor + 20, endian);
            //mldObj.Ground = getGround(arr, cursor, endian);
            //mldObj.unkInt3 = getUnkInt3(arr, cursor, endian);
            //mldObj.unkInt4 = getUnkInt4(arr, cursor, endian);
            //mldObj.Texlist = getTexList(arr, cursor, endian);
            mldObj.Name = getObjName(arr, cursor + 36, endian);
            //mldObj.unkFloat1 = getUnkFloat1(arr, cursor, endian);
            //mldObj.unkFloat2 = getUnkFloat2(arr, cursor, endian);
            //mldObj.unkFloat3 = getUnkFloat3(arr, cursor, endian);
            //mldObj.unkInt5 = getUnkInt5(arr, cursor, endian);
            //mldObj.unkInt6 = getUnkInt6(arr, cursor, endian);
            //mldObj.unkInt7 = getUnkInt7(arr, cursor, endian);
            //mldObj.unkFloat4 = getUnkFloat4(arr, cursor, endian);
            //mldObj.unkFloat5 = getUnkFloat5(arr, cursor, endian);
            //mldObj.unkFloat6 = getUnkFloat6(arr, cursor, endian);

            return mldObj;
        }

        public mldObjectMaster makeObjMaster(byte[] arr, int cursor, bool endian)
        {
            //Not entirely sure where the entry total is located/how to get it, so this may not be accurate
            mldObjectMaster objMaster = new mldObjectMaster();

            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor];
            }

            if (endian)
                Array.Reverse(b);
            objMaster.objEntryTotal = BitConverter.ToInt32(b, 0);

            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor + 4];
            }

            if (endian)
                Array.Reverse(b);
            cursor = BitConverter.ToInt32(b, 0);

            mldObjectEntry[] entryArr = new mldObjectEntry[objMaster.objEntryTotal];
            for(int i = 0; i < objMaster.objEntryTotal; i++)
            {
                entryArr[i] = makeObjEntry(arr, cursor, endian);
            }
            objMaster.objEntry = new mldObjectEntry[objMaster.objEntryTotal];

            return objMaster;
        }

        public mldObjectEntry makeObjEntry(byte[] arr, int cursor, bool endian)
        {
            mldObjectEntry objEntry = new mldObjectEntry();

            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor];
            }

            if (endian)
                Array.Reverse(b);
            objEntry.Object = BitConverter.ToInt32(b, 0) + cursor;

            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor + 4];
            }

            if (endian)
                Array.Reverse(b);
            objEntry.Motion = BitConverter.ToInt32(b, 0) + cursor;

            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor + 8];
            }

            if (endian)
                Array.Reverse(b);
            objEntry.TexList = BitConverter.ToInt32(b, 0) + cursor;
            
            return objEntry;
        }

        public string getObjName(byte[] arr, int cursor, bool endian)
        {
            byte[] b = new byte[12];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor];
            }

            if (!endian)
                Array.Reverse(b);

            return Encoding.ASCII.GetString(b);
        }

        public int getIndex(byte[] arr, int cursor, bool endian)
        {
            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + cursor];
            }

            if (endian)
                Array.Reverse(b);
            int index = BitConverter.ToInt32(b, 0);

            return index;
        }
        public void convertFile(string file)
        {
            bool endianness = checkEndianness(file);

            byte[] fileBytes = convertToBinary(file);

            mldHeader header = makeHeader(fileBytes, endianness);

            Console.WriteLine("Object Total: {0}",  header.objTotal);
            Console.WriteLine("Object Table Pointer EOF: {0}", header.objTable_eof);
            Console.WriteLine("Object Texture Array Pointer: {0}", header.texArr);
            Console.WriteLine("NMLD: {0}", isNMLD(fileBytes, endianness));
            //Console.WriteLine("Object Master Pointer: {0}", getObjMaster(fileBytes, header.objTotal, getObjTablePtr(fileBytes, endianness), endianness));
            //makeMLDObject(fileBytes, header.objTotal, getObjTablePtr(fileBytes, endianness), header.objTable_eof, endianness);




        }
    }
}
