using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMISoft.HSMS;


namespace Engion.A2HSMS
{
    #region 1. Module Definition
    /// <summary>
    /// A[n] Type의 경우 클래스 생성
    /// base(n)를 입력
    /// </summary>
    [Serializable]
    public class MODULEID : StringType
    {
        public static string moudleID = "INVALID";
        //20160613 모듈 아이디 수정..A3AMA01G
        //public MODULEID() : base(28) { Value = "A2AMA01E"; }
        public MODULEID() : base(28) { Value = moudleID; }
        public MODULEID(string value) :
            base(28)
        {
            Value = value == null ? "NULL" : value;
        }
    }

    /// <summary>
    /// Type U1, U2, U4, B 인경우 아래와 같이 입력
    /// base(type) 입력
    /// 필요한 변수들을 아래와 같이 public const int 타입으로 입력 -> byte
    /// </summary>
    [Serializable]
    public class MCMD : U1Type
    {
        public const byte offLine = 1;
        public const byte onlineLocal = 2;
        public const byte onLineRemote = 3;

        public MCMD()
            :base()
        {
            Value = offLine;
        }

        public MCMD(int value)
            : base(value)
        {
        }

        static public bool Validate(int mcmd)
        {
            if (mcmd < 1 || mcmd > 3)
                return false;
            return true;
        }

        static public string MCMDString(int mcmd)
        {
            string[] str = { "OFFLINE", "ONLOCAL", "ONREMOTE"};
            return str[mcmd];
        }

        public bool CanChange(int mcmd)
        {
            return  Value != mcmd;
        }

    }

    [Serializable]
    public class MODULE_STATE : U1Type
    {
        public const byte normal = 1;
        public const byte fault = 2;
        public const byte pm = 3;

        public MODULE_STATE()
        {
            Value = normal;
        }
        public MODULE_STATE(int value)
            : base(value)
        {
        }
    }

    [Serializable]
    public class PROC_STATE : U1Type
    {
        public const byte idle = 1;
        public const byte setup = 2;
        public const byte executing = 3;
        public const byte pause = 4;

        //20160629 PROC_STATE disable(5번상태) 추가됨.
        public const byte disable = 5;

        public PROC_STATE()
        {
            Value = idle;
        }
        public PROC_STATE(int value)
            : base(value)
        {
        }
    }

    [Serializable]
    public class BYWHO : U1Type
    {
        public const byte byHost = 1;
        public const byte byOperator = 2;
        public const byte byEquipment = 3;
        public BYWHO()
            : base()
        {
            Value = byOperator;
        }

        public BYWHO(int byWho)
            :base(byWho)
        {
        }
    }

    [Serializable]
    public class OPERID : StringType
    {
        public OPERID() : base(16) { Value = "Operator"; }
        public OPERID(string value) : base(16) { Value = value; }
    }
    #endregion

    #region 2.Port Definition
    [Serializable]
    public class PORTID : StringType
    {
        public PORTID() : base(4) { Value = "BP01"; }
        public PORTID(string value) : base(4) { Value = value; }
    }
    [Serializable]
    public class PORT_STATE : U1Type
    {
        public const byte empty = 0;
        public const byte idle = 1;
        public const byte wait = 2;
        public const byte reserve = 3;
        public const byte busy = 4;
        public const byte complete = 5;
        public const byte abort = 6;
        public const byte cancel = 7;
        public const byte pause = 8;
        public const byte disable = 9;
        public PORT_STATE()
            :base()
        {
            Value = wait;
        }

        public PORT_STATE(int value)
            : base(value)
        {
        }
    }

    [Serializable]
    public class PORT_TYPE : U1Type
    {
        public const byte both = 0;
        public const byte intput = 1;
        public const byte output = 2;
        public const byte shelf = 5;
        public PORT_TYPE()
        {
            Value = both;
        }
        public PORT_TYPE(int value)
            : base(value)
        { 
            
        }
    }

    [Serializable]
    public class PORT_MODE : StringType
    {
        public const string ok = "OK";
        public const string ng = "NG";

        public PORT_MODE() : base(2) { Value = "OK"; }
        public PORT_MODE(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class SORT_TYPE : U1Type
    {
        public const byte dumpFill = 0;
        public const byte dump = 1;
        public const byte lotID = 2;
        public const byte batchID = 3;
        public const byte jobID = 4;
        public const byte productKine = 5;
        public const byte prerunFlag = 6;
        public const byte workState = 7;
        public const byte flipState = 8;
        public const byte productIDproductType = 9;

        public SORT_TYPE()
        {
            Value = dumpFill;
        }
        public SORT_TYPE(int value)
            : base(value)
        {

        }

    }
    #endregion

    /// <summary>
    /// U1[] 과 같은 경우 아래와 같입 입력
    /// </summary>
    [Serializable]
    public class GLASS_SIZE : UArrayType
    {
        public GLASS_SIZE()
            : base(ItemFormat.UInt16, 2)
        {
            Value = new int[] { 1300, 1500 };
        }

        public GLASS_SIZE(int[] value)
            : base(ItemFormat.UInt16, 2)
        {
            Value = value;
        }
    }

    [Serializable]
    public class ALTX : StringType
    {
        public ALTX() : base(100) { }
        public ALTX(string value)
            : base(100)
        {
            Value = value;
        }
    }

    [Serializable]
    public class ALCD : U1Type
    {

        public ALCD() : base(0) { }
        public ALCD(int alcd)
            :base(alcd)
        {
        }
    }

    [Serializable]
    public class ALID : U4Type
    {
        public ALID() : base(0) { }
        public ALID(int alid)
            :base(alid)
        {
        }
    }

    [Serializable]
    public class ACKC2 : U1Type
    {
        public const byte OK = 0;
        public const byte ERROR_NOT_DONE = 1;
        public const byte TerminalNotAvailable = 2;

        public ACKC2(int value) : base(value) { }
        public ACKC2()
            : base(OK)
        {
        }
    }

    [Serializable]
    public class ACKC3 : U1Type
    {
        public const byte ACCEPTED = 0;
        public const byte BUSY_TRY_AGAIN = 1;
        public const byte THE_DATA_ALREADY_RECEIVED = 2;
        public const byte NO_CASSETTE = 3;
        public const byte CASSETTE_ID_IS_NOT_MATCH = 4;
        public const byte AT_LEAST_ONE_PARAMETER_INVALID = 5;
        public const byte PORT_STATE_IS_NOT_WAITING = 6;
        public const byte PPID_IS_NOT_MATCH = 7;
        public const byte PRODUCT_TYPE_IS_NOT_MATCH = 8;
        public const byte GLASS_MAPPING_MISMATCH = 9;

        public ACKC3(int value) : base(value) { }
        public ACKC3()
            : base(ACCEPTED)
        {
        }
    }

    [Serializable]
    public class ACKC5 : U1Type
    {
        public const byte ACCEPTED = 0;

        public ACKC5(int value) : base(value) { }
        public ACKC5()
            : base(ACCEPTED)
        {
        }
    }

    [Serializable]
    public class ACKC6 : U1Type
    {
        public const byte ACCEPTED = 0;

        public ACKC6(int value) : base(value) { }
        public ACKC6()
            : base(ACCEPTED)
        {
        }
    }

    [Serializable]
    public class ACKC7 : U1Type
    {
        public const byte ACCEPTED = 0;
        public const byte PERMISSION_NOT_GRANTED = 1;
        public const byte LENGTH_ERROR = 2;
        public const byte MODE_UNSUPPORTED = 3;
        public const byte TARGET_MODULE_IS_OFFLINE = 4;
        public const byte TARGET_MODULE_IS_DOWN = 5;
        public const byte TIME_OUT_HAPPENDED = 6;
        public const byte MODULEID_IS_NOT_EXIST = 7;
        public const byte PPID_TYPE_IS_NOT_MATCH = 8;
        public const byte CCODE_IS_NOT_MATCH = 9;
        public const byte PPID_IS_NOT_MATCH = 10;
        public const byte P_PARM_EXCEEDS_IN_DEFINED_RANGE = 11;

        public const byte S7F22_Accepted_for_Creation_Deletion = 0;
        public const byte S7F22_Denied_undefined_data = 1;
        public const byte S7F22_Busy_not_in_the_situation_to_accept_this_message = 2;
        public const byte S7F22_Mode_unsupported = 3;
        public const byte S7F22_Not_existing_PPID = 4;
        public const byte S7F22_Already_existing_PPID = 5;
        public const byte S7F22_Length_Error = 6;
        public const byte S7F22_The_list_of_P_PARM_is_not_enough = 7;
        public const byte S7F22_ModuleID_is_not_exist = 8;
        public const byte S7F22_PPID_TYPE_is_not_match = 9;
        public const byte S7F22_CCODE_is_not_match = 10;
        public const byte S7F22_At_least_one_of_P_PARM_exceeded_its_defined_range = 11;

        public ACKC7(int value) : base(value) { }
        public ACKC7()
            : base(ACCEPTED)
        {
        }
    }

    [Serializable]
    public class ACK7 : U1Type
    {
        public const byte EXIST = 0;
        public const byte MODULEID_IS_NOT_EXIST = 1;
        public const byte PPID_TYPE_IS_NOT_MATCH = 2;
        public const byte PPID_IS_NOT_MATCH = 3;


        public ACK7(int value) : base(value) { }
        public ACK7()
            : base(EXIST)
        {
        }
    }

    [Serializable]
    public class ACK : U1Type
    {
        public const byte OK = 0;
        public const byte MODULEID_MISMATCH = 1;
        public const byte PPID_TYPE_IS_NOT_MATCH = 2;

        public ACK(int value) : base(value) { }
        public ACK()
            : base(OK)
        {
        }
    }

    [Serializable]
    public class s8f2ACK : U1Type
    {
        public const byte OK = 0;
        public const byte MODULEID_MISMATCH = 1;
        public const byte DataInvalid = 2;

        public s8f2ACK(int value) : base(value) { }
        public s8f2ACK()
            : base(OK)
        {
        }
    }

    [Serializable]
    public class ACK7_PPID : U1Type
    {
        public const byte OK_UNDERSTOOD = 0;
        public const byte CONTENT_IS_NOT_UNDERSTOOD = 1;
        public const byte TRY_AGAIN = 2;
        public const byte ERROR_NOT_EXIST = 3;


        public ACK7_PPID(int value) : base(value) { }
        public ACK7_PPID()
            : base(OK_UNDERSTOOD)
        {
        }
    }

    [Serializable]
    public class PPID_TYPE : U1Type
    {
        public const byte HOST_USE_EQP_PPID = 0; // SAME //20160630 왜 0이지??
        public const byte EQP_PPID = 1; // NORMAL
        public const byte HOST_AND_EQP_PPID_MAPPING_TABLE = 2;

        public PPID_TYPE() : base(0) { }
        public PPID_TYPE(int value)
            : base(value)
        {
        }
    }

    [Serializable]
    public class MIACK : U1Type
    {
        public const byte NO_ERROR = 0;
        public const byte THERE_IS_NO_SUCH_A_MODULEID = 1;

        public MIACK() : base(0) { }
        public MIACK(int value) : base(value) { }  //base(NO_ERROR) or base(0) ?
    }

    [Serializable]
    public class PPGNT : U1Type
    {
        public const byte OK = 0;
        public const byte ALREAD_HAVE = 1;
        public const byte MODULEID_IS_NOT_EXIT = 2;
        public const byte NOSPACE = 3;
        public const byte PPID_TYPE_IS_NOT_MATCH = 4;
        public const byte INVALID_PPID = 5;
        public const byte BUSY_TRY_LATER = 6;
        public const byte WILL_NOT_ACCEPT = 7;

        //20160628
        public const byte NOT_MACHINE_RECIPE_TRANSFER = 8;
        // OtherError >8
        // Reserved 8~63

        public PPGNT() : base(0) { }
        public PPGNT(int value) : base(value) { }
    }

    [Serializable]
    public class SETCODE : U1Type
    {
        public const byte ALARM_RESET = 0; // ALRAM CLEARED
        public const byte ALARM_SET = 1; // ALARM_OCCURRED

        public SETCODE() : base(0) { }
        public SETCODE(int value) : base(value) { }
    }

    [Serializable]
    public class RCMD : U2Type
    {
        public const ushort JobProcessStart = 1;
        public const ushort JobProcessCancel = 2;
        public const ushort JobProcessAbort = 3;

        public const ushort JobProcessStartPermission = 30; //by ksyksy 20160620
        public const ushort ReloadCassette = 36;

        public const ushort Pause = 51;
        public const ushort Resume = 52;
        public const ushort PM = 53;
        public const ushort Normal = 54;

        public RCMD() : base(0) { }
        public RCMD(int value) : base(value) { }
    }

    [Serializable]
    public class TRID : U2Type
    {
        public TRID() : base(0) { }
        public TRID(int value) : base(value) { }
    }

    [Serializable]
    public class TOTSMP : U2Type
    {
        public TOTSMP() : base(0) { }
        public TOTSMP(int value) : base(value) { }
    }

    [Serializable]
    public class GRSIZE : U2Type
    {
        public GRSIZE() : base(0) { }
        public GRSIZE(int value) : base(value) { }
    }

    [Serializable]
    public class SVID : U2Type
    {
        public SVID() : base(0) { }
        public SVID(int value) : base(value) { }
    }

    [Serializable]
    public class CCODE : U2Type
    {
        public CCODE() : base(0) { }
        public CCODE(int value) : base(value) { }
    }

    [Serializable]
    public class EAC : U1Type
    {
        public const byte ACKNOWLEDGE = 0;
        public const byte DENIED_AT_LEAST_ONE_CONSTANT_OUT_OF_RANGE = 1;
        public const byte DENIED_BUSY = 2;
        public const byte RESERVED = 3; // 3~63

        public EAC(int value) : base(value) { }
        public EAC() : base(ACKNOWLEDGE) { }
    }

    [Serializable]
    public class s8f2EAC : U1Type
    {
        public const byte ACKNOWLEDGE = 0;
        public const byte DataTypeUndefined = 1;
        public const byte ItemNameUndefined = 2;
        public const byte ItemValueUndefined = 3;
        public const byte DeniedOtherReason = 4;

        public s8f2EAC(int value) : base(value) { }
        public s8f2EAC() : base(ACKNOWLEDGE) { }
    }

    [Serializable]
    public class TEAC : U1Type
    {
        public const byte ACKNOWLEDGE = 0;
        public const byte DENIED_AT_LEAST_ONE_CONSTANT_DOES_NOT_EXIST = 1;
        public const byte DENIED_BUSY = 2;
        public const byte DENIED_AT_LEAST_ONE_CONSTANT_OUT_OF_RANGE = 3;

        public TEAC() : base(0) { }
        public TEAC(int value) : base(value) { }
    }

    [Serializable]
    public class VERSION : StringType
    {
        public VERSION() : base(20) { Value = "2012_12_01_VER_1_0_0"; }
        public VERSION(string str) : base(str, 20) {}

    }

    [Serializable]
    public class TID : U1Type
    {
        public const byte ALL = 0;
        public const byte CIM_PC = 1;
        public const byte EQ_UI = 2;
        public const byte LOADER_UI = 3;

        public TID() : base(0) { }
        public TID(int value) : base(value) { }
    }

    [Serializable]
    public class MSG : StringType
    {
        public MSG() : base(100) { Value = string.Empty; }
        public MSG(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class DATA_TYPE : StringType
    {
        public DATA_TYPE() : base(10) { Value = string.Empty; }
        public DATA_TYPE(string value) : base(value, 10) { Value = value; }
    }

    [Serializable]
    public class ITEM_NAME : StringType
    {
        public ITEM_NAME() : base(40) { Value = string.Empty; }
        public ITEM_NAME(string value) : base(value, 40) { Value = value; }
    }

    [Serializable]
    public class ITEM_VALUE : StringType
    {
        public ITEM_VALUE() : base(100) { Value = string.Empty; }
        public ITEM_VALUE(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class REFERENCE : StringType
    {
        public REFERENCE() : base(20) { Value = string.Empty; }
        public REFERENCE(string value) : base(value, 20) { Value = value; }
    }

    [Serializable]
    public class OLDPPID : StringType
    {
        public OLDPPID() : base(20) { Value = string.Empty; }
        public OLDPPID(string value) : base(value, 20) { Value = value; }
    }

    [Serializable]
    public class NEWPPID : StringType
    {
        public NEWPPID() : base(20) { Value = string.Empty; }
        public NEWPPID(string value) : base(value, 20) { Value = value; }
    }

    [Serializable]
    public class TIME : StringType
    {
        public TIME() : base(14) { Value = "yyyyMMddHHmmss"; }
        public TIME(string value) :
            base(14)
        {
            Value = value == null ? "NULL" : value;
        }
    }

    [Serializable]
    public class MODE : U1Type
    {
        public const byte CREATION_OF_PPID = 1;
        public const byte DELETION_OF_PPID = 2;
        public const byte MODIFICATION_OF_TYPE = 3;
        public const byte EXPIRATION = 3;
        public const byte Override_Modify = 4;
        //Creation: 1, Deletion: 2, Expiration: 3, Override: 4

        public MODE() : base(0) { }
        public MODE(int value)
            : base(value)
        {
        }
    }

    [Serializable]
    public class CSTID : StringType
    {
        public CSTID() : base(12) { }
        public CSTID(string value) : base(12) { Value = value; }
    }

    [Serializable]
    public class HCACK : U1Type  // Host Command Parameter Acknowledge Code [ Unsigned 1 Byte ]
    {
        public const byte Acknowledge_is_OK = 0;
        public const byte Command_does_not_exist = 1;
        public const byte Cannot_perform_now = 2;  // Hardware Problem    
        public const byte InvalidParameter = 3;
        public const byte Ack_command_completion = 4;
        public const byte AlreadyDesiredCondition = 5;
        public const byte No_such_object_exists = 6;
        public const byte EqpRejectCommand = 7;
        public const byte Cannot_find_raw_data = 8;
        public const byte Cannot_read_raw_data = 9;
        public const byte PPID_is_not_exist = 10;
        public const byte RejectedForReserved = 12;
        public const byte RejectedForNotCassetteINFO = 13;
        public const byte SVPFull = 14; //Specific_Virtual_Port
        public const byte PPID_is_not_availlable = 15;
        public const byte RejectedForNotMaterialINFO = 16; // (Frame or Stick Mask) 
        public const byte Rejected_Eoid20_Is_Off = 17; // (Frame or Stick Mask) 

        public HCACK() : base(0) { }
        public HCACK(int value) : base(value) { }
    }

    [Serializable]
    public class CPACK : U1Type  // Host Command Parameter Acknowledge Code [ Unsigned 1 Byte ]
    {
        public const byte No_Error = 0;
        public const byte CPNameNotExist = 1;
        public const byte IllegalValue = 2;
        public const byte IllegalFormat = 3;
        public const byte No_cassette_Input = 4; //(input port) 
        public const byte No_cassette_Output = 5; //(output port)
        public const byte Cassette_ID_not_match_Input = 6; //(input port)   
        public const byte Cassette_ID_not_match_Output = 7; //(output port)  
        public const byte Port_state_error_Input = 8; //(input port) 
        public const byte Port_state_error_Output = 9;//(output port)
        public const byte Too_many_slots_are_selected = 10; // empty slots are short. (SLOT(QTY) > Empty)
        public const byte No_Glass_Error = 11;
        public const byte Glass_alreadyexist_cannot_replace = 12;
        public const byte Glass_State_error = 13;
        public const byte Denied_Module_State_Error = 14;
        public const byte Denied_Tool_State_Error = 15;
        public const byte Denied_Unit_State_Error = 16;
        public const byte Glass_Size_Mismatch = 21;// (input=source port) * for sorter 
        public const byte STEPID = 22; // (OPERATIONID) (Mismatch (output=target port)) * for sorter   
        public const byte PORTID_Mismatch = 23;// (input=Source port) * for sorter 
        public const byte Car_Size_Mismatch = 24; // (input≠output) * for sorter 
        public const byte Output_Slot_Occupied = 25; // (output=target port) * for sorter 
        public const byte Input_Slot_Empty = 26; // (input=Source port) * for sorter
        public const byte Reserved = 27;
        public const byte Output_is_NG_Cassette = 28;
        public const byte Cassette_Demand_is_not_matched = 29;
        public const byte ModuleName_Problem = 30;
        public const byte Glassid_Problem = 31;
        public const byte Lotid_Problem = 32;
        public const byte Product_Type_Problem = 33;
        public const byte PPID_Problem = 34;

        public CPACK() : base(0) { }
        public CPACK(int value) : base(value) { }
    }


    [Serializable]
    public class CST_TYPE : U1Type
    {
        public const byte fullCst = 1;
        public const byte quarterCst = 2;
        public const byte cellCst = 3;
        public const byte maskCst = 4;
        public const byte maskCase = 5;
        public const byte WireCst = 6;

        public CST_TYPE() : base(fullCst) { }
        public CST_TYPE(int value) : base(value) { }
    }

    [Serializable]
    public class MAP_STIF : StringType
    {
        static public readonly string emptySTIF = new string('0', 56);

        public MAP_STIF() : base(emptySTIF, 56) { }
        public MAP_STIF(string value) : base(56) { Value = value; }
        public MAP_STIF(StringBuilder value) : base(56) { Value = value.ToString(); }
    }

    [Serializable]
    public class CUR_STIF : StringType
    {
        static public readonly string emptySTIF = new string('0', 56);

        public CUR_STIF() : base(emptySTIF, 56) { }
        public CUR_STIF(string value) : base(56) { Value = value; }
        public CUR_STIF(StringBuilder value) : base(56) { Value = value.ToString(); }
    }

    [Serializable]
    public class BATCH_ORDER : U1Type
    {
        public const byte start = 1;
        public const byte normal = 2;
        public const byte end = 3;

        public BATCH_ORDER() : base(normal) { }
        public BATCH_ORDER(int value = normal) : base(value) { }
    }

    [Serializable]
    public class CEID : U2Type
    {
        #region Const CEID...
        public const int EqProcessStateChanged_51 = 51;
        public const int EqProcessStateChanged_PMtoNORMAL_53 = 53;
        public const int EqProcessStateChanged_PMtoFAULT_53 = 53;
        public const int EqProcessStateChanged_NORMALtoPM_53 = 53;
        public const int EqProcessStateChanged_FAULTtoPM_53 = 53;

        public const int ChangeToOffLineMode71 = 71;
        public const int ChangeToOnLineLocalMode72 = 72;
        public const int ChangeToRemoteMode73 = 73;

        public const int GlassDataCollection_82 = 82;
        public const int LotDataCollection_92 = 92;

        public const int ChangeToEOIDParameter101 = 101;
        public const int CURRENT_PPID_CHANGE = 131;

        public const int CassetteLoadRequest31 = 31;
        public const int CassettePreLoad32 = 32;
        public const int CassetteLoadComplete33 = 33;
        public const int CassetteUnLoadRequest34 = 34;
        public const int CassetteUnLoadComplete35 = 35;
        public const int CassetteReject36 = 36;

        //20160623
        public const int PortParameterChange_37 = 37;

        public const int JobProcessStartID_1 = 1;
        public const int JobProcessCancelID_2 = 2;
        public const int JobProcessAbort_3 = 3;
        public const int JobProcessEnd_4 = 4;
        public const int PanelProcessStartLoader_6 = 6;
        public const int PanelProcessEndLoader_7 = 7;

        public const int PanelProcessStartForModule_16 = 16;
        public const int PanelProcessEndForModule_17 = 17;

        public const int GLASS_SCRAP_18 = 18; //by ksyksy 
        public const int GLASS_UNSCRAP_19 = 19; //by ksyksy

        public const int loadPortTransferReadyToLoad_10 = 10;
        public const int loadPortTransferOutOfService_11 = 11;
        public const int loadPortTransferReadyToUnload_12 = 12;
        public const int loadPortTransferTransferBlocked_13 = 13;
        public const int loadPortNotReserved_14 = 14;
        public const int loadPortReserved_15 = 15;

        public const int carrierIDNotReady20 = 20;
        public const int carrierIDVerificationOK21 = 21;
        public const int carrierIDVerificationFailed22 = 22;
        public const int carrierIDWaitingForHost23 = 23;

        #region 20160623
        public const int UnitProcessStart_26 = 26;
        public const int UnitProcessEnd_27 = 27;
        #endregion

        //20160623 CEID=30 추가해야하는데.. 하기처럼 이미 선언된 부분이 있지만, 사양과 명칭이 다르고.. 참고하는데도 없어서 주석처리하고 CEID = 30번 넣음.
        //public const int carrierSlotMapNotRead30 = 30;
        public const int JobStartPermissionEvent_30 = 30;

        public const int carrierSlotMapVerificationOK31 = 31;
        public const int carrierSlotMapVerificationFailed32 = 32;
        public const int carrierSlotMapWaitingForHost33 = 33;

        public const int carrierAccessingNotAccessed40 = 40;
        public const int carrierAccessingAccess41 = 41;
        public const int carrierAccessingCarrierComplete42 = 42;
        public const int carrierAccessingInAccess43 = 43;

        public const int accessModeManual50 = 50;
        public const int accessModeAuto51 = 51;

        public const int loadPortOccupied70 = 70;

        public const int onlineRemote100 = 100;
        public const int onlineLocal101 = 101;
        public const int hostOffline102 = 102;
        public const int equipmentOffline103 = 103;

        public const int processIdle110 = 110;
        public const int processSetup111 = 111;
        public const int processReady112 = 112;
        public const int processExecuting113 = 113;
        public const int processPause114 = 114;

        public const int ftpserver_not_connection_171 = 171;

        public const int prJobQueued200 = 200;
        public const int prJobSettingUp201 = 201;
        public const int prJobWaitFoStart202 = 202;
        public const int prJobProcessing203 = 203;
        public const int prJobProcessComplete204 = 204;
        public const int prJobPausing205 = 205;
        public const int prJobPaused206 = 206;
        public const int prJobStopping207 = 207;
        public const int prJobAborting208 = 208;
        public const int prJobDeleted209 = 209;

        public const int cJQueued220 = 220;
        public const int cJSelected221 = 221;
        public const int cJWaitingForStart222 = 222;
        public const int cJExecuting223 = 223;
        public const int cJPaused224 = 224;
        public const int cJCompleted225 = 225;
        public const int cJDeleted226 = 226;
        public const int cJCreated227 = 227;
        public const int cJCreateFailed228 = 228;

        public const int carrierDoorOpened400 = 400;
        public const int carrierDoorClosed401 = 401;
        public const int waferMoved402 = 402;

        public const int pPSelected410 = 410;
        public const int pPStarted411 = 411;
        public const int pPCompleted412 = 412;
        public const int pPStepStarted413 = 413;
        public const int pPStepCompleted414 = 414;

        public const int eCChanged500 = 500;
        public const int terminalInfoRecognized502 = 502;
        public const int operatorAction502 = 502;

        #endregion
        public CEID() : base(0) { }
        public CEID(int value) : base(value) { }

    }

    [Serializable]
    public class GLASS_STATE : U2Type
    {
        public const byte IDLE = 1;
        public const byte SELECTED = 2;
        public const byte PROCESSING = 3;
        public const byte DONE = 4;
        public const byte ABORTED = 5;
        public const byte CANCELED = 6;
        public GLASS_STATE(int value) : base(value) { }
        public GLASS_STATE() : base(IDLE) { }
    }

    [Serializable]
    public class ONLACK : U1Type
    {
        public const byte accepted = 0;
        public const byte alreayState = 1;
        public const byte ModuleIDError = 2;
        public const byte McmdError = 3;

        public ONLACK(int value) : base(value) { }
        public ONLACK() : base(0) { }
    }

    [Serializable]
    public class SFCD : U1Type
    {
        public const byte EQ_ONLINE_PARM = 1;
        public const byte PORT_STATUS = 2;
        public const byte GLASS_TRACKING = 3;
        public const byte MODULE_STATES = 4;
        public const byte PROCESS_STEP_INQUIRY = 31; // by ksyksy 20160612

        public SFCD(int value) : base(value) { }
        public SFCD() : base(EQ_ONLINE_PARM) { }
    }

    [Serializable]
    public class EOID : U1Type
    {
        public EOID() : base(0) { }
        public EOID(int value) : base(value) { }
    }

    [Serializable]
    public class EOMD : U1Type
    {
        public EOMD() : base(0) { }
        public EOMD(int value) : base(value) { }
    }

    [Serializable]
    public class EOV : U1Type
    {
        public EOV() : base(0) { }
        public EOV(int value) : base(value) { }
    }

    [Serializable]
    public class HOT_DEVICE : U1Type
    {
        public HOT_DEVICE() : base(0) { }
        public HOT_DEVICE(int value) : base(value) { }
    }

    [Serializable]
    public class HOT_LEVEL : U1Type
    {
        public HOT_LEVEL() : base(0) { }
        public HOT_LEVEL(int value) : base(value) { }
    }

    [Serializable]
    public class TIACK : U1Type
    {
        public const byte OK = 0;
        public const byte ErrorNotDone = 1;
        public const byte NoSuchModuleID = 2;
        public const byte TRID_NotMatch = 3;

        public TIACK() : base(0) { }
        public TIACK(int value) : base(value) { }
    }

    [Serializable]
    public class RAWPATH : StringType
    {
        public RAWPATH() : base(16) { Value = string.Empty; }
        public RAWPATH(string value) : base(value, 6) { Value = value; }
    }

    [Serializable]
    public class GLASS_PATH : StringType
    {
        public GLASS_PATH() : base(100) { Value = string.Empty; }
        public GLASS_PATH(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class SUMPATH : StringType
    {
        public SUMPATH() : base(16) { Value = string.Empty; }
        public SUMPATH(string value) : base(value, 6) { Value = value; }
    }

    [Serializable]
    public class LOT_PATH : StringType
    {
        public LOT_PATH() : base(100) { Value = string.Empty; }
        public LOT_PATH(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class IMGPATH : StringType
    {
        public IMGPATH() : base(16) { Value = string.Empty; }
        public IMGPATH(string value) : base(value, 6) { Value = value; }
    }

    [Serializable]
    public class IMAGE_PATH : StringType
    {
        public IMAGE_PATH() : base(100) { Value = string.Empty; }
        public IMAGE_PATH(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class DISK : StringType
    {
        public DISK() : base(16) { Value = string.Empty; }
        public DISK(string value) : base(value, 6) { Value = value; }
    }

    [Serializable]
    public class FILEDISK : StringType
    {
        public FILEDISK() : base(100) { Value = string.Empty; }
        public FILEDISK(string value) : base(value, 100) { Value = value; }
    }

    [Serializable]
    public class SMPTIME : StringType
    {
        public SMPTIME() : base(6) { Value = string.Empty; }
        public SMPTIME(string value) : base(value, 6) { Value = value; }
    }

    [Serializable]
    public class P_PARM : StringType
    {
        public P_PARM() : base(40) { Value = string.Empty; }
        public P_PARM(string value) : base(value, 40) { Value = value; }
    }

    [Serializable]
    public class P_PARM_NAME : StringType
    {
        public P_PARM_NAME() : base(40) { Value = string.Empty; }
        public P_PARM_NAME(string value) : base(value, 40) { Value = value; }
    }


    [Serializable]
    public class H_GLASSID : StringType
    {
        public H_GLASSID() : base(16) { Value = string.Empty; }
        public H_GLASSID(string value) : base(value, 16) { }
    }

    [Serializable]
    public class E_GLASSID : StringType
    {
        public E_GLASSID() : base(16) { }
        public E_GLASSID(string value) : base(value, 16) { }
    }

    [Serializable]
    public class LOTID : StringType
    {
        public LOTID() : base(16) {  }
        public LOTID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class BATCHID : StringType
    {
        public BATCHID() : base(16) {  }
        public BATCHID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class JOBID : StringType
    {
        public JOBID() : base(16) {  }
        public JOBID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class IPID : StringType
    {
        public IPID() : base(4) {  }
        public IPID(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class ICID : StringType
    {
        public ICID() : base(12) {  }
        public ICID(string value) : base(12) { Value = value; }
    }

    [Serializable]
    public class OPID : StringType
    {
        public OPID() : base(4) {  }
        public OPID(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class OCID : StringType
    {
        public OCID() : base(12) {  }
        public OCID(string value) : base(12) { Value = value; }
    }

    [Serializable]
    public class SLOTINFO : StringType
    {
        public SLOTINFO() : base(56) {  }
        public SLOTINFO(string value) : base(56) { Value = value; }
    }

    [Serializable]
    public class SLOTNO : StringType
    {
        public SLOTNO() : base(2) {  }
        public SLOTNO(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class PRODUCT_TYPE : StringType
    {
        public PRODUCT_TYPE() : base(4) {  }
        public PRODUCT_TYPE(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class PRODUCT_KIND : StringType
    {
        public PRODUCT_KIND() : base(4) {  }
        public PRODUCT_KIND(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class PRODUCTID : StringType
    {
        public PRODUCTID() : base(16) {  }
        public PRODUCTID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class RUNSPECID : StringType
    {
        public RUNSPECID() : base(16) {  }
        public RUNSPECID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class LAYERID : StringType
    {
        public LAYERID() : base(8) {  }
        public LAYERID(string value) : base(8) { Value = value; }
    }

    [Serializable]
    public class STEPID : StringType
    {
        public STEPID() : base(8) {  }
        public STEPID(string value) : base(8) { Value = value; }
    }

    [Serializable]
    public class PPID : StringType
    {
        public PPID() : base(20) {  }
        public PPID(string value) : base(20) { Value = value; }
    }

    [Serializable]
    public class FLOWID : StringType
    {
        public FLOWID() : base(20) {  }
        public FLOWID(string value) : base(20) { Value = value; }
    }

    [Serializable]
    public class GLASS_THICKNESS : U2Type
    {
        public GLASS_THICKNESS() : base(500) { }
        public GLASS_THICKNESS(int value) : base(value) { }
    }

    [Serializable]
    public class GLASS_ORDER : StringType
    {
        public GLASS_ORDER() : base(4) {  }
        public GLASS_ORDER(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class COMMENT : StringType
    {
        public COMMENT() : base(16) {  }
        public COMMENT(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class USE_COUNT : StringType
    {
        public USE_COUNT() : base(4) {  }
        public USE_COUNT(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class JUDGEMENT : StringType
    {
        public JUDGEMENT() : base(4) {  }
        public JUDGEMENT(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class REASON_CODE : StringType
    {
        public REASON_CODE() : base(4) {  }
        public REASON_CODE(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class INS_FLAG : StringType
    {
        public INS_FLAG() : base(2) {  }
        public INS_FLAG(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class ENC_FLAG : StringType
    {
        public ENC_FLAG() : base(2) {  }
        public ENC_FLAG(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class PRERUN_FLAG : StringType
    {
        public PRERUN_FLAG() : base(2) {  }
        public PRERUN_FLAG(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class TURN_FLAG : StringType
    {
        public TURN_FLAG() : base(2) {  }
        public TURN_FLAG(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class FLIP_STATE : StringType
    {
        public FLIP_STATE() : base(2) {  }
        public FLIP_STATE(string value) : base(2) { Value = value; }
    }

    [Serializable]
    public class WORK_STATE : StringType
    {
        public WORK_STATE() : base(4) {  }
        public WORK_STATE(string value) : base(4) { Value = value; }
    }

    [Serializable]
    public class MULTI_USE : StringType
    {
        public MULTI_USE() : base(16) {  }
        public MULTI_USE(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class PAIR_GLASSID : StringType
    {
        public PAIR_GLASSID() : base(16) {  }
        public PAIR_GLASSID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class PAIR_PPID : StringType
    {
        public PAIR_PPID() : base(20) {  }
        public PAIR_PPID(string value) : base(20) { Value = value; }
    }

    [Serializable]
    public class OPTION_NAME1 : StringType
    {
        public OPTION_NAME1() : base(40) {  }
        public OPTION_NAME1(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_VALUE1 : StringType
    {
        public OPTION_VALUE1() : base(40) {  }
        public OPTION_VALUE1(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_NAME2 : StringType
    {
        public OPTION_NAME2() : base(40) {  }
        public OPTION_NAME2(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_VALUE2 : StringType
    {
        public OPTION_VALUE2() : base(40) {  }
        public OPTION_VALUE2(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_NAME3 : StringType
    {
        public OPTION_NAME3() : base(40) {  }
        public OPTION_NAME3(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_VALUE3 : StringType
    {
        public OPTION_VALUE3() : base(40) {  }
        public OPTION_VALUE3(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_NAME4 : StringType
    {
        public OPTION_NAME4() : base(40) {  }
        public OPTION_NAME4(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_VALUE4 : StringType
    {
        public OPTION_VALUE4() : base(40) {  }
        public OPTION_VALUE4(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_NAME5 : StringType
    {
        public OPTION_NAME5() : base(40) {  }
        public OPTION_NAME5(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class OPTION_VALUE5 : StringType
    {
        public OPTION_VALUE5() : base(40) {  }
        public OPTION_VALUE5(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECID : U2Type
    {
        public ECID() : base(0) { }
        public ECID(int value) : base(value) { }
    }

    [Serializable]
    public class ECNAME : StringType
    {
        public ECNAME() : base(40) {  }
        public ECNAME(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECDEF : StringType
    {
        public ECDEF() : base(40) {  }
        public ECDEF(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECSLL : StringType
    {
        public ECSLL() : base(40) {  }
        public ECSLL(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECSUL : StringType
    {
        public ECSUL() : base(40) {  }
        public ECSUL(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECWLL : StringType
    {
        public ECWLL() : base(40) {  }
        public ECWLL(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class ECWUL : StringType
    {
        public ECWUL() : base(40) {  }
        public ECWUL(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class SV : StringType
    {
        public SV() : base(40) { }
        public SV(int value) : base(value) { }
    }

    [Serializable]
    public class SVNAME : StringType
    {
        public SVNAME() : base(40) { }
        public SVNAME(int value) : base(value) { }
    }


    #region by ksyksy 20160612
    [Serializable]
    public class SPEC_CODE : StringType
    {
        public SPEC_CODE() : base(20) { Value = "SDC_OLED_V235_UNI"; }
        public SPEC_CODE(string value) :
            base(20)
        {
            Value = value == null ? "NULL" : value;
        }
    }

    [Serializable]
    public class STEP_DESC : StringType
    {
        public STEP_DESC() : base(40) { Value = "NULL"; }
        public STEP_DESC(string value) : base(40) { Value = value; }
    }

    [Serializable]
    public class MATERIAL_ID : StringType
    {
        public MATERIAL_ID() : base(16) { Value = "NULL"; }
        public MATERIAL_ID(string value) : base(16) { Value = value; }
    }

    [Serializable]
    public class CUR_STEPNO : U1Type
    {
        public CUR_STEPNO() : base() { Value = 0; }
        public CUR_STEPNO(int CurrentStepNo) : base(CurrentStepNo) { }
    }

    [Serializable]
    public class STEPNO : U1Type
    {
        public STEPNO() : base() { Value = 0; }
        public STEPNO(int StepNo) : base(StepNo) { }
    }

    [Serializable]
    public class APC_STATE : U1Type
    {
        public const byte Waiting = 1;
        public const byte Running = 2;
        public const byte Done = 3;

        public APC_STATE() : base() { Value = 0; }
        public APC_STATE(int mValue) : base(mValue) { }
    }

    [Serializable]
    public class TCACK : U1Type
    {
        public const byte S16F102_Acknowledged = 0;
        public const byte S16F102_Denied_MODULEID_mismatched = 1;
        public const byte S16F102_Denied_for_other_reason = 2;

        public const byte S16F104_Acknowledged = 0;
        public const byte S16F104_Denied_APC_is_OFF_now_EOID = 1;
        public const byte S16F104_Denied_data_are_full_and_no_more_data_are_acceptable = 2;
        public const byte S16F104_Denied_MODULEID_unmatched = 3;
        public const byte S16F104_Denied_at_least_one_of_PACKs_is_not_acceptable = 4;
        public const byte S16F104_Denied_the_target_is_already_in_processing_or_not_in_acceptable_status = 5;
        public const byte S16F104_Denied_the_name_of_RECIPE_doesnt_exist_in_this_equipment = 6;

        public const byte S16F106_Accepted = 0;
        public const byte S16F106_Denied_MODULEID_mismatched = 1;
        public const byte S16F106_Denied_tried_to_delete_non_existing_APC_data = 2;
        public const byte S16F106_Denied_at_least_one_of_the_H_GLASSID_is_already_running = 3;

        public const byte S16F112_F114_F116_Accepted = 0;
        public const byte S16F112_F114_F116_Error = 1;

        public TCACK() : base() { Value = 0; }
        public TCACK(int mValue) : base(mValue) { }
    }

    [Serializable]
    public class PACK : U1Type
    {
        public const byte Acknowledged = 0;
        public const byte Denied_P_PARM_NAME_does_not_exist = 1;
        public const byte Denied_at_least_one_of_P_PARM_VALUE_is_out_of_range = 2;

        public PACK() : base() { Value = 0; }
        public PACK(int mValue) : base(mValue) { }
    }

    //[Serializable]
    //public class MODE : U1Type
    //{
    //     public const byte Creation =  1;
    //     public const byte Deletion =  2;
    //     public const byte Expiration = 3;
    //     public const byte Override_or_Modify = 4;

    //    public MODE() : base() { Value = 0; }
    //    public MODE(int mValue) : base(mValue) { }
    //}

    [Serializable]
    public class RECIPE : StringType
    {
        public RECIPE() : base(20) { Value = "NULL"; }
        public RECIPE(string mValue) : base(20) { Value = mValue; }
    }

    [Serializable]
    public class SET_TIME : StringType
    {
        public SET_TIME() : base(16) { Value = "NULL"; }
        public SET_TIME(string mValue) : base(16) { Value = mValue; }
    }

    [Serializable]
    public class P_PARM_VALUE : StringType
    {
        public P_PARM_VALUE() : base(40) { Value = "NULL"; }
        public P_PARM_VALUE(string mValue) : base(40) { Value = mValue; }
    }


    [Serializable]
    public class RPC_PPID : StringType
    {
        public RPC_PPID() : base(20) { Value = "0"; }
        public RPC_PPID(string mValue) : base(20) { Value = mValue; }
    }

    [Serializable]
    public class RPC_STATE : U1Type
    {
        public RPC_STATE() : base() { Value = 0; }
        public RPC_STATE(int mValue) : base(mValue) { }
    }

    [Serializable]
    public class ACKC16 : U1Type
    {
        public const byte S16F122_Accepted = 0;
        public const byte S16F122_Denied_MODULEID_does_not_exist = 1;
        public const byte S16F122_Denied_for_other_reason = 2;

        public const byte S16F124_Accepted = 0;
        public const byte S16F124_Denied_RPC_is_off_now_EOID = 1;
        public const byte S16F124_Denied_data_are_full_and_no_more_data_are_acceptable = 2;
        public const byte S16F124_Denied_MODULEID_unmatched = 3;
        public const byte S16F124_Denied_given_RPC_PPID_does_not_exist = 4;
        public const byte S16F124_Denied_this_equipment_does_not_support_APC_and_RPC_at_the_same_time = 5;

        public const byte S16F126_Accepted = 0;
        public const byte S16F126_Denied_MODULEID_mismatched = 1;
        public const byte S16F126_Denied_RPC_data_for_at_least_one_of_H_GLASSID_do_not_exist = 2;
        public const byte S16F126_Denied_at_least_one_of_the_H_GLASSID_is_already_running = 3;

        public const byte S16F132_F134_F136_Accepted = 0;
        public const byte S16F132_F134_F136_Other_error = 1;

        public ACKC16() : base() { Value = 0; }
        public ACKC16(int mValue) : base(mValue) { }
    }
    

    #endregion
}
