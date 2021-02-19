using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mldlib
{
    public class mldFunctions
    {
        public bool checkEndianness(string file)
        {
            bool bigEndian = false;
            string trueName = Path.GetFileName(file);

            if (trueName.Contains("_d"))
                bigEndian = true;

            return bigEndian;
        }

        public byte[] convertToBinary(string file)
        {
            byte[] fileBytes = File.ReadAllBytes(file);

            return fileBytes;
        }

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

        public int getObjMaster(byte[] arr, int objTotal, int ptr_objMaster, bool endian)
        {
            int origin = ptr_objMaster;

            byte[] b = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                b[i] = arr[i + ptr_objMaster+20];
            }

            if (endian)
                Array.Reverse(b);

            int texArrPtr = BitConverter.ToInt32(b, 0);

            return texArrPtr;
        }

        public void convertFile(string file)
        {
            bool endianness = checkEndianness(file);

            byte[] fileBytes = convertToBinary(file);


            mldFile.mldHeader header = new mldFile.mldHeader();

            Console.WriteLine("Object Total: {0}",  getObjTotal(fileBytes, endianness));
            Console.WriteLine("Object Table Pointer: {0}", getObjTablePtr(fileBytes, endianness));
            Console.WriteLine("Object Table Pointer EOF: {0}", getObjTableEOF(fileBytes, endianness));
            Console.WriteLine("Object Texture Array Pointer: {0}", getTexArr(fileBytes, endianness));
            Console.WriteLine("Object Master Pointer: {0}", getObjMaster(fileBytes, getObjTotal(fileBytes, endianness), getObjTablePtr(fileBytes, endianness), endianness));
        }
    }
}
