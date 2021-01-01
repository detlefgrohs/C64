using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeDataGenerator
{
    class Program
    {

        static int GetBitPopulation(int n) {
            int count = 0;
            while (n != 0) {
                count += ((n & 1) == 1) ? 1 : 0;
                n >>= 1;
            }
            return count;
        }
        static void GenerateCellLookupData()
        {
            var size = Math.Pow(2, 12);

            foreach (var pattern in Enumerable.Range(0, (int)size)) {
                //
                // 11 10 09 08
                // 07 06 05 04
                // 03 02 01 00
                //
                // 11
                // 1098 7654 3210
                //
                //             11  
                // 0123 4567 8901
                //
                // 1110 0000 0000
                // 0111 0000 0000
                // 0000 1110 0000
                // 0000 0111 0000
                // 0000 0000 1110
                // 0000 0000 0111

                // 1110 E
                // 0111 7

                // 1010 A
                // 0101 5

                // 1110 1010 1110 = 0x0EAE
                // 0000 0100 0000 = 0x0040

                // 0111 0101 0111 = 0x0757
                // 0000 0010 0000 = 0x0020

                //These rules, which compare the behavior of the automaton to real life, can be condensed into the following:

                // Any live cell with two or three live neighbors survives.
                // Any dead cell with three live neighbors becomes a live cell.
                // All other live cells die in the next generation.Similarly, all other dead cells stay dead.

                var msbBitCount = GetBitPopulation(pattern & 0x0EAE);
                var msbAlive = (pattern & 0x0040) > 0;

                if (msbBitCount == 2) ;
                else if (msbBitCount == 3) msbAlive = true;
                else msbAlive = false;


                var lsbBitCount = GetBitPopulation(pattern & 0x0757);
                var lsbAlive = (pattern & 0x0020) > 0;

                if (lsbBitCount == 2) ;
                else if (lsbBitCount == 3) lsbAlive = true;
                else lsbAlive = false;

                var value = (byte)(msbAlive ? (byte)0xAA : (byte)0x00) | (byte)(lsbAlive ? (byte)0x55 : (byte)0x00);

                var binary = ("000000000000" + Convert.ToString(pattern, 2));
                binary = binary.Substring(binary.Length - 12, 12);

                var leftPattern = binary.Substring(0, 3) + binary.Substring(4, 3) + binary.Substring(8, 3);
                var rightPattern = binary.Substring(1, 3) + binary.Substring(5, 3) + binary.Substring(9, 3);

                Console.WriteLine($"!BYTE ${value:x2} ; ${pattern:x4} {binary} {leftPattern} {rightPattern}");


            }
        }
        private static int GetC64CellOffset(int x, int y) {
            x = (x < 0) ? 39 : (x > 39) ? 0 : x;
            y = (y < 0) ? 199 : (y > 199) ? 0 : y;

            return (((y >> 3) * 320) + (y & 0x07)) + (x << 3);
        }
        private static void GetOffsetsForLine(int y) {
            var x = 0;
            var sc0 = GetC64CellOffset(x - 1, y - 1);
            var sc1 = GetC64CellOffset(x, y - 1);
            var sc2 = GetC64CellOffset(x + 1, y - 1);
            var sc3 = GetC64CellOffset(x - 1, y);
            var sc4 = GetC64CellOffset(x, y);
            var sc5 = GetC64CellOffset(x + 1, y);
            var sc6 = GetC64CellOffset(x - 1, y + 1);
            var sc7 = GetC64CellOffset(x, y + 1);
            var sc8 = GetC64CellOffset(x + 1, y + 1);

            x = 39;
            var ec0 = GetC64CellOffset(x - 1, y - 1);
            var ec1 = GetC64CellOffset(x, y - 1);
            var ec2 = GetC64CellOffset(x + 1, y - 1);
            var ec3 = GetC64CellOffset(x - 1, y);
            var ec4 = GetC64CellOffset(x, y);
            var ec5 = GetC64CellOffset(x + 1, y);
            var ec6 = GetC64CellOffset(x - 1, y + 1);
            var ec7 = GetC64CellOffset(x, y + 1);
            var ec8 = GetC64CellOffset(x + 1, y + 1);

            // 9 16 Bits x 2 = 18 x 2 = 36 Bytes
            // 36 Bytes x 200 = 7,200 bytes

            Console.WriteLine($"!WORD ${sc0:x4},${sc1:x4},${sc2:x4},${sc3:x4},${sc4:x4},${sc5:x4},${sc6:x4},${sc7:x4},${sc8:x4},${ec0:x4},${ec1:x4},${ec2:x4},${ec3:x4},${ec4:x4},${ec5:x4},${ec6:x4},${ec7:x4},${ec8:x4}");
        }
        static void GenerateCellOffsetsData()
        {
            Console.WriteLine($";       sc0,  sc1,  sc2,  sc3,  sc4,  sc5,  sc6,  sc7,  sc8,  ec0,  ec1,  ec2,  ec3,  ec4,  ec5,  ec6,  ec7,  ec8");

            foreach (var y in Enumerable.Range(0, 200))
                GetOffsetsForLine(y);

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            GenerateCellLookupData();

            Console.ReadLine();
        }
    }
}
