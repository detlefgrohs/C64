using System;
using System.Text;

namespace GameOfLifeSimulator
{
    class Program
    {
        /*
         Console.Write UI Update
            250 cycles in 14.57 seconds.
            17.15 cycles per second.    
        
        StringBuilder & Single Console.Write()
            10000 cycles in 10.11 seconds.
            989.22 cycles per second.

        No UI Update
            10000 cycles in 1.83 seconds.
            5473.46 cycles per second.
         */
        private const int Width = 40;
        private const int Height = 25;

        private static char[] Buffer0 = new char[Width * Height];
        private static char[] Buffer1 = new char[Width * Height];

        private static int UsingBuffer = 0;

        static void Main(string[] args) {
            ClearBuffer0(' ');
            LoadPattern();

            var done = false;

            var startDateTime = DateTime.Now;
            var cycles = 0;

            while (!done)
            {
                ShowBuffer();
                UpdateCells();

                UsingBuffer = UsingBuffer == 0 ? 1 : 0;

                if (Console.KeyAvailable) done = true;
                if (++cycles >= 10000) done = true;
            }

            var elapsed = DateTime.Now - startDateTime;
            Console.WriteLine($"{cycles} cycles in {elapsed.TotalSeconds:0.00} seconds.");
            Console.WriteLine($"{cycles / elapsed.TotalSeconds:0.00} cycles per second.");

            Console.ReadLine();
        }
        private static void UpdateCells()
        {
            var currCells = UsingBuffer == 0 ? Buffer0 : Buffer1;
            var nextCells = UsingBuffer == 0 ? Buffer1 : Buffer0;

            var position = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    var cellValue = currCells[position];
                    var cellNeighrborCount = GetCellNeighborCount(currCells, col, row);

                    if (cellNeighrborCount == 2) ;
                    else if (cellNeighrborCount == 3) cellValue = 'X';
                    else cellValue = ' ';

                    nextCells[position] = cellValue;

                    position++;
                }
            }
        }
        private static int IsCellAlive(char[] buffer, int col, int row)
        {
            col = (col < 0 ? Width - 1 : (col >= Width ? 0 : col));
            row = (row < 0 ? Height - 1 : (row >= Height ? 0 : row));

            return buffer[(row * Width) + col] == ' ' ? 0 : 1;
        }
        private static int GetCellNeighborCount(char[] buffer, int col, int row)
        {
            return IsCellAlive(buffer, col - 1, row - 1) + IsCellAlive(buffer, col, row - 1) + IsCellAlive(buffer, col + 1, row - 1) +
                   IsCellAlive(buffer, col - 1, row) + IsCellAlive(buffer, col + 1, row) +
                   IsCellAlive(buffer, col - 1, row + 1) + IsCellAlive(buffer, col, row + 1) + IsCellAlive(buffer, col + 1, row + 1);
        }
        private static void LoadPattern() {
            Buffer0[20 + (12 * 40)] = 'X';
            Buffer0[21 + (13 * 40)] = 'X';
            Buffer0[19 + (14 * 40)] = 'X';
            Buffer0[20 + (14 * 40)] = 'X';
            Buffer0[21 + (14 * 40)] = 'X';
        }
        private static void ClearBuffer0(char setChar) {
            for (int index = 0; index < Width * Height; index++) Buffer0[index] = setChar;
        }
        private static void ShowBuffer() {
            var bufferToShow = UsingBuffer == 0 ? Buffer0 : Buffer1;
            int position = 0;

            var outputBuffer = new StringBuilder();
            
            outputBuffer.Append("---+");
            for (int index = 0; index < Width; index++) outputBuffer.Append($"{(index/10) % 10}");
            outputBuffer.AppendLine("+");

            outputBuffer.Append("---+");
            for (int index = 0; index < Width; index++) outputBuffer.Append($"{index % 10}");
            outputBuffer.AppendLine("+");

            outputBuffer.Append("---+");
            for (int index = 0; index < Width; index++) outputBuffer.Append("-");
            outputBuffer.AppendLine("+");

            for (int row = 0; row < Height; row++)
            {
                outputBuffer.Append($"{row,3}|");
                for (int col = 0; col < Width; col++) {
                    outputBuffer.Append(bufferToShow[position]);
                    position++;
                }
                outputBuffer.AppendLine("|");
            }
            outputBuffer.Append("---+");
            for (int index = 0; index < Width; index++) outputBuffer.Append("-");
            outputBuffer.AppendLine("+");

            Console.SetCursorPosition(0, 0);
            Console.Write(outputBuffer);
        }
    }
}