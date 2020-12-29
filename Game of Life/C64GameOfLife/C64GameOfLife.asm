!SOURCE "BasicStart.asm"

!SOURCE "Macros.asm"

!SOURCE "ProgramAddresses.asm"


; ZPA_BM = BUFFER_NEXT
;
          LDA #$00
          STA ZPA_ROW
          
ROW_LOOP: 
; For Each Row 0 to 199
;   
;
          +LOAD_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, CELLOFFSETS
          
;   Get sc0-sc8 from CELLOFFSETS[ row * 36 ] to ZPA
;     Offset by BUFFER_CURR
          LDY #$00            
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C0
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C1
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C2
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C3
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C4
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C5
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C6
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C7
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C8
          
;   Process 4 2 bit sections from ZPA => ZPA_BM, 0
          JSR CALC_GAME_CELL


          LDA #$01
          STA ZPA_COL
;   For Each Col 1 to 38          
COL_LOOP:
          
;     sc0 = sc1, sc1 = sc2, sc2 = sc2 + 8     
          +COPY_ZPA_WORD ZPA_C1, ZPA_C0
          +COPY_ZPA_WORD ZPA_C2, ZPA_C1
          +ADD_BYTE_TO_WORD_ZPA ZPA_C2, 8
;     sc3 = sc4, sc4 = sc5, sc5 = sc5 + 8
          +COPY_ZPA_WORD ZPA_C4, ZPA_C3
          +COPY_ZPA_WORD ZPA_C5, ZPA_C4
          +ADD_BYTE_TO_WORD_ZPA ZPA_C5, 8
;     sc6 = sc7, sc7 = sc8, sc8 = sc8 + 8               
          +COPY_ZPA_WORD ZPA_C7, ZPA_C6
          +COPY_ZPA_WORD ZPA_C8, ZPA_C7
          +ADD_BYTE_TO_WORD_ZPA ZPA_C8, 8
          
;     Process 4 2 bit sections from ZPA => ZPA_BM, COL
          JSR CALC_GAME_CELL

          ; End Col Loop
          INC ZPA_COL
          LDA ZPA_COL
          CMP #39
          BNE COL_LOOP

          
;   Get ec0-ec8 from CELLOFFSETS[ (row * 36) + 18 ] to ZPA
;     Offset by BUFFER_CUR
          LDY #$12
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C0
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C1
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C2
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C3
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C4
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C5
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C6
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C7
          +COPY_WORD_TO_ZPA ZPA_CELLOFFSET_LOOKUP, ZPA_C8
;
;   Process 4 2 bit sections from ZPA => ZPA_BM, 39
          JSR CALC_GAME_CELL

          ; End of the Row Loop
          
          ; Switch the buffers
;
;   ZPA_BM += 40
;
; Switch to BUFFER_NEXT
;

          +ADD_BYTE_TO_WORD_ZPA ZPA_CELLOFFSET_LOOKUP, 36
          INC ZPA_ROW
          LDA ZPA_ROW
          CMP #200
          ;BNE ROW_LOOP
          JMP ROW_LOOP



; Calculate the bits for the current cell          
CALC_GAME_CELL:          
; Prep for Lookup Activity
        LDY #$00
        TXA
        STA ZPA_NEXT_CELL

;c7 c6 hb = ((C0 AND 0x01) << 3) OR ((C1 AND 0xE0) >> 5)  
        LDA (ZPA_C0), Y
        AND #$01
        +ROL_3
        STA ZPA_LOGIC_LOOKUP + 1
        
        LDA (ZPA_C1), Y
        AND #$E0
        +ROR_5
        ORA ZPA_LOGIC_LOOKUP + 1
        STA ZPA_LOGIC_LOOKUP + 1

;      lb = ((C3 AND 0x01) << 7) OR ((C4 AND 0xE0) >> 1) OR ((C6 AND 0x01) << 3) OR ((C7 AND 0xE0) >> 5)
        LDA (ZPA_C3), Y
        AND #$01
        +ROL_7
        STA ZPA_LOGIC_LOOKUP
        
        LDA (ZPA_C4), Y
        AND #$E0
        ROR
        ORA ZPA_LOGIC_LOOKUP
        STA ZPA_LOGIC_LOOKUP
        
        LDA (ZPA_C6), Y
        AND #$01
        +ROL_3
        ORA ZPA_LOGIC_LOOKUP
        STA ZPA_LOGIC_LOOKUP
        
        LDA (ZPA_C7), Y
        AND #$E0
        +ROR_5
        ORA ZPA_LOGIC_LOOKUP
        STA ZPA_LOGIC_LOOKUP
        
        ADC CELLLOOKUP ; to ZPA_LOGIC_LOOKUP  
        LDA (ZPA_LOGIC_LOOKUP), Y
        AND #$C0
        STA ZPA_NEXT_CELL

;c5 c4 hb = (C1 AND 0x78) >> 3  
;      lb = ((C4 AND 0x78) << 1) OR ((C7 AND 0x78) >> 3)


;c3 c2 hb = (C1 AND 0x1E) >> 1  
;      lb = ((C4 AND 0x1E) << 3) OR ((C7 AND 0x1E) >> 1)


;c1 c0 hb = ((C1 AND 0x07) << 1) OR ((C2 AND 0x80) >> 7)  
;      lb = ((C4 AND 0x07) << 5) OR ((C5 AND 0x80) >> 3) OR ((C7 AND 0x07) << 1) OR ((C8 AND 0x80) >> 7)





; Store the assembled cell to the next buffer
        LDA ZPA_NEXT_CELL
        LDY ZPA_ROW ; ???
        STA (ZPA_BUFFER_NEXT), Y
        
        RTS
        
!SOURCE "CellLookupData.asm"
!SOURCE "CellOffsetData.asm"
