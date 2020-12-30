# C64 Debugger and 65XE Debugger Cheatsheet
##Global shortcuts:
|Key|Action|
|-|-|
Alt+Enter|Toggle fullscreen (MS Windows only)
Ctrl+F1|Show only C64 screen
Ctrl+F2|Show C64 disassembler, memory map and data dump
Ctrl+F3|Show C64 disassembler with hex codes, memory map, data dump and VIC state
Ctrl+F4|Show C64 and 1541 disk disassembler and memory maps
Ctrl+F5|Show states of chips
Ctrl+F6|Show C64 disassembler and a big memory map
Ctrl+F7|Show C64 and 1541 disk disassembler
Ctrl+F8|Show Monitor console and debugging tools
Ctrl+Shift+F1|Show zoomed C64 screen.
Ctrl+Shift+F2|Show cycle-exact debugging tools with C64 screen zoom and code labels
Ctrl+Shift+F4|Show VIC Display "lite" screen
Ctrl+Shift+F5|Show VIC Display screen
Ctrl+Shift+F6|Show VIC Editor screen
Ctrl+Shift+F7|Show All Graphics screen
Ctrl+Shift+F8|Show music notes tracker screen
Ctrl+Shift+F9|Show memory debugger screen
TAB|Change focus to next view
Shift+TAB|Change focus to previous view
F9|Show Main menu screen
Ctrl+B|Show Breakpoints screen
Ctrl+Shift+S|Show Snapshots screen
Ctrl+T|Mute sound On/Off
Ctrl+W|Replace memory dump view with watches view
Ctrl+[|Set slower emulation speed
Ctrl+]|Set faster emulation speed
Ctrl+8|Insert D64 file
Ctrl+Shift+8|Detach D64 file
Ctrl+Alt+8|Insert next D64 file from folder
Ctrl+O|Load PRG file
Ctrl+L|Reload PRG & Start
Ctrl+0|Attach cartridge
Ctrl+Shift+0|Detach cartridge
Ctrl+Shift+A|Toggle auto-load first PRG from inserted disk
Ctrl+F|Cartridge freeze button
Ctrl+R|Soft reset C64
Ctrl+Shift+R|Hard reset C64
Ctrl+Alt+R|Reset 1541 Disk drive
Ctrl+Shift+D|Detach everything
Ctrl+P|Limit emulation speed On/Off (warp mode)
Ctrl+Y|Use keyboard arrows as joystick On/Off, Right Alt to fire
F10|Pause code or run to next instruction (step)
Alt+F10|Back-Step one instruction
Ctrl+F10|Step to next line (step over JSR)
Shift+F10|Run one CPU cycle
F11|Run/continue emulation
Ctrl+Arrow Left|Rewind emulation back one frame
Ctrl+Arrow Right|Forward emulation one frame
Alt+Ctrl+Arrow Left|Rewind emulation back one second
Alt+Ctrl+Arrow Right|Forward emulation one second
Shift+Ctrl+Arrow Left|Rewind emulation back 10 seconds
Shift+Ctrl+Arrow Right|Forward emulation 10 seconds
Ctrl+M|Toggle data memory map/dump taken directly from RAM or as-is with I/O and ROMs selected by $0001
Ctrl+E|Toggle show current raster beam position
Ctrl+S|Store snapshot to a file
Ctrl+D|Restore snapshot from a file
Shift+Ctrl+1, 2, 3, ..., 6|Quick store snapshot to slot #1,#2,#3, ..., or #6
Ctrl+1, 2, 3, ..., 6|Quick restore snapshot from slot #1,#2,#3, ..., or #6
Ctrl+U|Dump C64's memory to file
Ctrl+Shift+U|Dump 1541 Drive's memory to file
Ctrl+Shift+E|Save current screen data to file
Ctrl+BACKSPACE|Clear memory markers
Ctrl+Shift+P|Save C64 screenshot and sprite bitmaps to PNG files
F7|Browse attached disk image
F3|Start first PRG from disk image
Ctrl+;|Select next code symbols segment
Ctrl+'|Select previous code symbols segment

## In Disassembly view:
|Key|Action|
|-|-|
Mouse Click on memory address|Add/remove breakpoint
` (~ tilde key)|Add/remove breakpoint
Arrow Up/Down|Scroll code one instruction up/down
Page Up/Page Down or Shift+Arrow Up/Shift+Arrow Down|Scroll code by $100 bytes up/down
Space|Toggle tracking of code display by current PC
Enter|Enter code editing mode (assemble)
[ or ]|Scroll code one byte up/down
Arrow Left/Right|If not editing code: follow code jumps and branches using Right-Arrow key, |and move back with Left-Arrow key. When argument is a memory address then |Memory Dump view will be scrolled to that address|If editing code and hex values visible: change edited hex value	
CTRL+G addr| Move cursor to specific address (f.e. CTRL+G EA31)
CTRL+J|JMP to current cursor's address (change CPU PC)
Mouse wheel|Scroll code (faster with Shift pressed)


## In Memory Dump view:
|Key|Action|
|-|-|
Mouse Click on hex value|Select hex value
Double Mouse Click on hex value|Scroll disassemble view to selected address
Arrow keys|Move editing cursor
Page Up/Page Down or Shift+Arrow Up/Shift+Arrow Down|Scroll code by $100 bytes up/down
Enter or 0-9 or A-F|Start editing value
Ctrl+Mouse Click|Scroll Disassembly to code address that stored that value
Ctrl+Shift+Mouse Click|Scroll Disassembly to code address that last read that value
Alt+Shift|Change CBM charset
Ctrl+K|Change colour mode on/off for sprites/characters
Ctrl+G addr|Move cursor to specific address (f.e. CTRL+G 0400)
Ctrl+V|Paste hex codes from clipboard into memory. Simple separators are|parsed, also the text can contain addresses as 4 hex digits
