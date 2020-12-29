!MACRO ROL_3 {
        ROL
        ROL
        ROL
}
!MACRO ROL_7 {
        ROL
        ROL
        ROL
        ROL
        ROL
        ROL
        ROL
}
!MACRO ROR_5 {
        ROR
        ROR
        ROR
        ROR
        ROR
}
!MACRO LOAD_WORD_TO_ZPA ZPA, WORD {
        LDA #<WORD
        STA ZPA
        LDA #>WORD
        STA ZPA + 1
}  
!MACRO COPY_WORD_TO_ZPA ZPA_WORD, ZPA {
          LDA (ZPA_WORD), Y
          STA ZPA
          INY
          LDA (ZPA_WORD), Y
          STA ZPA + 1
          INY
}
!MACRO ADD_BYTE_TO_WORD_ZPA ZPA, BYTE {
        CLC
        LDA ZPA
        ADC #BYTE
        STA ZPA
        BCC +
        INC ZPA + 1
+  
}     
!MACRO COPY_ZPA_WORD ZPA_SRC, ZPA_DST {
          LDA ZPA_SRC
          STA ZPA_DST
          LDA ZPA_SRC + 1
          STA ZPA_DST + 1
}