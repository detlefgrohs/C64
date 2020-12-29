
Conway's Game of Life

Rules
The universe of the Game of Life is an infinite, two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, 
live or dead, (or populated and unpopulated, respectively). Every cell interacts with its eight neighbors, which are the cells that are horizontally, 
vertically, or diagonally adjacent. At each step in time, the following transitions occur:

Any live cell with fewer than two live neighbors dies, as if by underpopulation.
Any live cell with two or three live neighbors lives on to the next generation.
Any live cell with more than three live neighbors dies, as if by overpopulation.
Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.


These rules, which compare the behavior of the automaton to real life, can be condensed into the following:
    Any live cell with two or three live neighbors survives.
    Any dead cell with three live neighbors becomes a live cell.
    All other live cells die in the next generation. Similarly, all other dead cells stay dead.

The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules simultaneously to every cell in the seed; 
births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick. Each generation is a pure function of the 
preceding one. The rules continue to be applied repeatedly to create further generations.


012
345
678

000
000
000 => 0

000
000
001 => 

256 x 2 = 512 array of bits for the next value of the cell
512 / 8 => 64 bytes... So get the 9 bit value and index into a 64 bit array for the value of the next cell...

0000000000
0xxxxxxxx0
0000000000

000000
0xxxx0
000000 => 5 + 5 + 5 = 2^15 => 32768 nibbles or 16,384 bytes
    Fuck got this wrong 6+6+6 


0000
0xx0
0000 => 2^12 =>


So proposed Memory Layout

16 banks of 4k
    0x0000
    0x1000
    0x2000
    0x3000
    0x4000
    0x5000
    0x6000
    0x7000
    0x8000     Lookup Table
    0x9000
    0xa000
    0xb000
    0xc000      Graphics
    0xd000
    0xe000
    0xf000

2 8k Pages for Graphics
Current/next


So 16k for Graphics Pages
     16k for Lookup Table (Generated?)

So what does that leave for code?

set pagePosition = 0
So for each nibble in 8k (4,000)
    Get lookup position from current page
    Get lookup value from lookup
    put in next page






10 + 10 + 10 = 30 bits yields 1,073,741,824 bytes Wow!!!

NC = neighbor count

NC = 2 Copy Cell Value
NC = 3 Cell = Live
Else Cell = dead


     PB    CB      NB
PR 0 76543210 7
CR 0 76543210 7
NR 0 76543210 7

(Cx, Cy) = BitPosition

if BitPosition = 7
if BitPosition = 0
else


---
Conway's Game of Life C=64 version
Conway's Game of Life is a simulation where cells are born or die depending on the state of other cells.  Most versions that run on the 
commodore 64 use a 40x25 cell grid.  This version uses an 80x50 cell grid.

40x25 = 1000 cells
80x50 = 4000 cells

C64 HiRes graphics mode
Standard Bitmap Mode = 320 x 200 => 64,000 bits (8,000 bytes)
    so 16 times the size of the other one...


Glider
    _X_
    __X
    XXX

Oscillator
    XXX

Block
    XX
    XX


Timing of Assembly Instructions

Simple Instructions
    Increment Register



    Load Register Immediate



    Load Register from Memory





C64 Memory Map
$F000
$E000   Kernel ROM

$D000   Char ROM

$C000

$B000
$A000   Basic ROM

$9000
$8000   CellLookup (Length=$0FFF)

$7000
$6000   CellOffsets (Length=$1C20)

$5000
$4000   Bank 1 Bitmap Memory (Length=$1F40)

$3000
$2000   Bank 0 Bitmap Memory (Length=$1F40)

$1000       Program
    $0800   Basic
    $0400   Color Memory
$0000


1.023MHz

 1,023,000 		 0.00000098 	
		 0.00097752 	* 1000
		 0.97751711 	* 1000


