namespace ImagineApps.Infrastructure.Utilities
{
    internal static class SQLBank
    {
        public static string GetBankById 
            = $@"SELECT Bank_ID WITH NOLOCK
                  ,Bank_Name
                  ,FontID
                  ,Def_Account_Mask
                  ,Def_User_Micr_Value
                  ,MICR_Font
                  ,filler1
                  ,Address_1
                  ,Address_2
                  ,Address_3
                  ,Address_4
                  ,Branch_Office_Code
                  ,Bank_Code
                  ,Transact_Fraction_1
                  ,Transact_Fraction_2
                  ,Transact_Fraction_3
                  ,Def_Transit_MICR
                  ,Last_Check_No
                  ,Stamp_Duty
                  ,Duty_Stamp_FontID
                  ,ProjectKey
                  ,filler2
                  ,Addenda_YesNo
                  ,Addenda_No_Lines
                  ,Col1
                  ,Col2
                  ,Col3
                  ,Col4
                  ,Col5
              FROM Banks
              WHERE Bank_ID = @Bank_ID";

        public static string GetBanks
            = $@"SELECT Bank_ID WITH NOLOCK
                  ,Bank_Name
                  ,FontID
                  ,Def_Account_Mask
                  ,Def_User_Micr_Value
                  ,MICR_Font
                  ,filler1
                  ,Address_1
                  ,Address_2
                  ,Address_3
                  ,Address_4
                  ,Branch_Office_Code
                  ,Bank_Code
                  ,Transact_Fraction_1
                  ,Transact_Fraction_2
                  ,Transact_Fraction_3
                  ,Def_Transit_MICR
                  ,Last_Check_No
                  ,Stamp_Duty
                  ,Duty_Stamp_FontID
                  ,ProjectKey
                  ,filler2
                  ,Addenda_YesNo
                  ,Addenda_No_Lines
                  ,Col1
                  ,Col2
                  ,Col3
                  ,Col4
                  ,Col5
              FROM Banks";
    }
}
