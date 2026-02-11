using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMISoft.HSMS;
using System.Runtime.InteropServices;
using System.Reflection;

namespace LGHSMSBase
{
    public class Win32
    {
        public struct SYSTEMTIME                      //시간 구조체
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime([In] ref SYSTEMTIME st);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetLocalTime([In] ref SYSTEMTIME st);
    }

    public class LGGem : HSMSGem
    {
        // Stream 1 Event Definds
        public event EventHandler<GemEventArgs> OnS1F1Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F2Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F61Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F62Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F63Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F64Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F67Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F68Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F75Event = delegate { };
        public event EventHandler<GemEventArgs> OnS1F76Event = delegate { };

        public event EventHandler<GemEventArgs> OnS2F17Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F18Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F25Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F26Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F29Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F30Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F31Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F32Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F61Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F62Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F63Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F64Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F65Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F66Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F67Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F68Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F69Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F70Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F77Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F78Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F79Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F80Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F87Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F88Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F99Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F100Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F103Event = delegate { };
        public event EventHandler<GemEventArgs> OnS2F104Event = delegate { };

        public event EventHandler<GemEventArgs> OnS3F101Event = delegate { };
        public event EventHandler<GemEventArgs> OnS3F102Event = delegate { };        

        public event EventHandler<GemEventArgs> OnS5F1Event = delegate { };
        public event EventHandler<GemEventArgs> OnS5F2Event = delegate { };
        public event EventHandler<GemEventArgs> OnS5F5Event = delegate { };
        public event EventHandler<GemEventArgs> OnS5F101Event = delegate { };

        public event EventHandler<GemEventArgs> OnS6F1Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F11Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F12Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F13Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F14Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F61Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F62Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F63Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F64Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F65Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F66Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F67Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F68Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F69Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F70Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F71Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F72Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F73Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F74Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F83Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F84Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F85Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F86Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F89Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F90Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F91Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F92Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F93Event = delegate { };
        public event EventHandler<GemEventArgs> OnS6F94Event = delegate { };

        public event EventHandler<GemEventArgs> OnS7F1Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F2Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F19Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F20Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F21Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F61Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F62Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F63Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F64Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F67Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F68Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F69Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F70Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F77Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F78Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F79Event = delegate { };
        public event EventHandler<GemEventArgs> OnS7F80Event = delegate { };


        public event EventHandler<GemEventArgs> OnS8F1Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS8F2Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS8F3Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS8F4Event = delegate { }; //

        public event EventHandler<GemEventArgs> OnS9F1Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F3Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F5Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F7Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F9Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F11Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS9F13Event = delegate { }; //

        public event EventHandler<GemEventArgs> OnS10F3Event = delegate { };
        public event EventHandler<GemEventArgs> OnS10F4Event = delegate { };

        public event EventHandler<GemEventArgs> OnS15F65Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F66Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F71Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F72Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F73Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F74Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F75Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F76Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F77Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F78Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F79Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F80Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F81Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F82Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F83Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F84Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F95Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F96Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F97Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F98Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F99Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS15F100Event = delegate { }; //

        public event EventHandler<GemEventArgs> OnS16F101Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F102Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F103Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F104Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F105Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F106Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F111Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F113Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F115Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F121Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F122Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F123Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F124Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F125Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F126Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F131Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F133Event = delegate { }; //
        public event EventHandler<GemEventArgs> OnS16F135Event = delegate { }; //

        
        public LGGem()
        {
        }

        // Stream 1 Method Defines
        void OnS1F1Recv(GemEventArgs args) { OnS1F1Event(this, args); }
        void OnS1F2Recv(GemEventArgs args) { OnS1F2Event(this, args); }
        void OnS1F61Recv(GemEventArgs args) { OnS1F61Event(this, args); }
        void OnS1F62Recv(GemEventArgs args) { OnS1F62Event(this, args); }
        void OnS1F75Recv(GemEventArgs args) { OnS1F75Event(this, args); }
        void OnS1F76Recv(GemEventArgs args) { OnS1F76Event(this, args); }
        void OnS1F63Recv(GemEventArgs args) { OnS1F63Event(this, args); }
        void OnS1F64Recv(GemEventArgs args) { OnS1F64Event(this, args); }
        void OnS1F67Recv(GemEventArgs args) { OnS1F67Event(this, args); }
        void OnS1F68Recv(GemEventArgs args) { OnS1F68Event(this, args); }

        void OnS2F17Recv(GemEventArgs args) { OnS2F17Event(this, args); }
        void OnS2F18Recv(GemEventArgs args) { OnS2F18Event(this, args); }
        void OnS2F25Recv(GemEventArgs args) { OnS2F25Event(this, args); }
        void OnS2F26Recv(GemEventArgs args) { OnS2F26Event(this, args); }
        void OnS2F29Recv(GemEventArgs args) { OnS2F29Event(this, args); }
        void OnS2F30Recv(GemEventArgs args) { OnS2F30Event(this, args); }
        void OnS2F31Recv(GemEventArgs args) { OnS2F31Event(this, args); }
        void OnS2F32Recv(GemEventArgs args) { OnS2F32Event(this, args); }
        void OnS2F61Recv(GemEventArgs args) { OnS2F61Event(this, args); }
        void OnS2F62Recv(GemEventArgs args) { OnS2F62Event(this, args); }
        void OnS2F63Recv(GemEventArgs args) { OnS2F63Event(this, args); }
        void OnS2F64Recv(GemEventArgs args) { OnS2F64Event(this, args); }
        void OnS2F65Recv(GemEventArgs args) { OnS2F65Event(this, args); }
        void OnS2F66Recv(GemEventArgs args) { OnS2F66Event(this, args); }
        void OnS2F67Recv(GemEventArgs args) { OnS2F67Event(this, args); }
        void OnS2F68Recv(GemEventArgs args) { OnS2F68Event(this, args); }
        void OnS2F69Recv(GemEventArgs args) { OnS2F69Event(this, args); }
        void OnS2F70Recv(GemEventArgs args) { OnS2F70Event(this, args); }
        void OnS2F77Recv(GemEventArgs args) { OnS2F77Event(this, args); }
        void OnS2F78Recv(GemEventArgs args) { OnS2F78Event(this, args); }
        void OnS2F79Recv(GemEventArgs args) { OnS2F79Event(this, args); }
        void OnS2F80Recv(GemEventArgs args) { OnS2F80Event(this, args); }
        void OnS2F87Recv(GemEventArgs args) { OnS2F87Event(this, args); }
        void OnS2F88Recv(GemEventArgs args) { OnS2F88Event(this, args); }
        void OnS2F99Recv(GemEventArgs args) { OnS2F99Event(this, args); }
        void OnS2F100Recv(GemEventArgs args) { OnS2F100Event(this, args); }
        void OnS2F103Recv(GemEventArgs args) { OnS2F103Event(this, args); }
        void OnS2F104Recv(GemEventArgs args) { OnS2F104Event(this, args); }

        void OnS3F101Recv(GemEventArgs args) { OnS3F101Event(this, args); }
        void OnS3F102Recv(GemEventArgs args) { OnS3F102Event(this, args); }
        
        void OnS5F1Recv(GemEventArgs args) { OnS5F1Event(this, args); }
        void OnS5F2Recv(GemEventArgs args) { OnS5F2Event(this, args); }
        void OnS5F5Recv(GemEventArgs args) { OnS5F5Event(this, args); }
        void OnS5F101Recv(GemEventArgs args) { OnS5F101Event(this, args); }

        void OnS6F1Recv(GemEventArgs args) { OnS6F1Event(this, args); }
        void OnS6F11Recv(GemEventArgs args) { OnS6F11Event(this, args); }
        void OnS6F12Recv(GemEventArgs args) { OnS6F12Event(this, args); }
        void OnS6F61Recv(GemEventArgs args) { OnS6F61Event(this, args); }
        void OnS6F62Recv(GemEventArgs args) { OnS6F62Event(this, args); }
        void OnS6F63Recv(GemEventArgs args) { OnS6F63Event(this, args); }
        void OnS6F64Recv(GemEventArgs args) { OnS6F64Event(this, args); }
        void OnS6F65Recv(GemEventArgs args) { OnS6F65Event(this, args); }
        void OnS6F66Recv(GemEventArgs args) { OnS6F66Event(this, args); }
        void OnS6F67Recv(GemEventArgs args) { OnS6F67Event(this, args); }
        void OnS6F68Recv(GemEventArgs args) { OnS6F68Event(this, args); }
        void OnS6F69Recv(GemEventArgs args) { OnS6F69Event(this, args); }
        void OnS6F70Recv(GemEventArgs args) { OnS6F70Event(this, args); }
        void OnS6F71Recv(GemEventArgs args) { OnS6F71Event(this, args); }
        void OnS6F72Recv(GemEventArgs args) { OnS6F72Event(this, args); }
        void OnS6F73Recv(GemEventArgs args) { OnS6F73Event(this, args); }
        void OnS6F74Recv(GemEventArgs args) { OnS6F74Event(this, args); }
        void OnS6F83Recv(GemEventArgs args) { OnS6F83Event(this, args); }
        void OnS6F84Recv(GemEventArgs args) { OnS6F84Event(this, args); }
        void OnS6F85Recv(GemEventArgs args) { OnS6F85Event(this, args); }
        void OnS6F86Recv(GemEventArgs args) { OnS6F86Event(this, args); }
        void OnS6F89Recv(GemEventArgs args) { OnS6F89Event(this, args); }
        void OnS6F90Recv(GemEventArgs args) { OnS6F90Event(this, args); }
        void OnS6F91Recv(GemEventArgs args) { OnS6F91Event(this, args); }
        void OnS6F92Recv(GemEventArgs args) { OnS6F92Event(this, args); }
        void OnS6F93Recv(GemEventArgs args) { OnS6F93Event(this, args); }
        void OnS6F94Recv(GemEventArgs args) { OnS6F94Event(this, args); }


        void OnS7F1Recv(GemEventArgs args) { OnS7F1Event (this, args); }
        void OnS7F2Recv(GemEventArgs args) { OnS7F2Event (this, args); }
        void OnS7F19Recv(GemEventArgs args) { OnS7F19Event(this, args); }
        void OnS7F20Recv(GemEventArgs args) { OnS7F20Event(this, args); }
        void OnS7F21Recv(GemEventArgs args) { OnS7F21Event(this, args); }  //
        void OnS7F61Recv(GemEventArgs args) { OnS7F61Event(this, args); }
        void OnS7F62Recv(GemEventArgs args) { OnS7F62Event(this, args); }
        void OnS7F63Recv(GemEventArgs args) { OnS7F63Event(this, args); }
        void OnS7F64Recv(GemEventArgs args) { OnS7F64Event(this, args); }
        void OnS7F77Recv(GemEventArgs args) { OnS7F77Event(this, args); }
        void OnS7F78Recv(GemEventArgs args) { OnS7F78Event(this, args); }
        void OnS7F67Recv(GemEventArgs args) { OnS7F67Event(this, args); }
        void OnS7F68Recv(GemEventArgs args) { OnS7F68Event(this, args); }
        void OnS7F69Recv(GemEventArgs args) { OnS7F69Event(this, args); }
        void OnS7F70Recv(GemEventArgs args) { OnS7F70Event(this, args); }
        void OnS7F79Recv(GemEventArgs args) { OnS7F79Event(this, args); }
        void OnS7F80Recv(GemEventArgs args) { OnS7F80Event(this, args); }


        void OnS8F1Recv(GemEventArgs args) { OnS8F1Event(this, args); }  //
        void OnS8F3Recv(GemEventArgs args) { OnS8F3Event(this, args); }  //

        void OnS9F13Recv(GemEventArgs args) { OnS9F13Event(this, args); }  //


        void OnS10F3Recv(GemEventArgs args) { OnS10F3Event(this, args); }
        void OnS10F4Recv(GemEventArgs args) { OnS10F4Event(this, args); }


        void OnS15F65Recv(GemEventArgs args) { OnS15F65Event(this, args); }
        void OnS15F66Recv(GemEventArgs args) { OnS15F66Event(this, args); }
        void OnS15F71Recv(GemEventArgs args) { OnS15F71Event(this, args); }
        void OnS15F72Recv(GemEventArgs args) { OnS15F72Event(this, args); }
        void OnS15F73Recv(GemEventArgs args) { OnS15F73Event(this, args); }
        void OnS15F74Recv(GemEventArgs args) { OnS15F74Event(this, args); }
        void OnS15F75Recv(GemEventArgs args) { OnS15F75Event(this, args); }
        void OnS15F76Recv(GemEventArgs args) { OnS15F76Event(this, args); }
        void OnS15F77Recv(GemEventArgs args) { OnS15F77Event(this, args); }
        void OnS15F78Recv(GemEventArgs args) { OnS15F78Event(this, args); }
        void OnS15F79Recv(GemEventArgs args) { OnS15F79Event(this, args); }
        void OnS15F80Recv(GemEventArgs args) { OnS15F80Event(this, args); }
        void OnS15F81Recv(GemEventArgs args) { OnS15F81Event(this, args); }
        void OnS15F82Recv(GemEventArgs args) { OnS15F82Event(this, args); }
        void OnS15F83Recv(GemEventArgs args) { OnS15F83Event(this, args); }
        void OnS15F84Recv(GemEventArgs args) { OnS15F84Event(this, args); }
        void OnS15F95Recv(GemEventArgs args) { OnS15F95Event(this, args); }
        void OnS15F96Recv(GemEventArgs args) { OnS15F96Event(this, args); }
        void OnS15F97Recv(GemEventArgs args) { OnS15F97Event(this, args); }
        void OnS15F98Recv(GemEventArgs args) { OnS15F98Event(this, args); }
        void OnS15F99Recv(GemEventArgs args) { OnS15F99Event(this, args); }
        void OnS15F100Recv(GemEventArgs args) { OnS15F100Event(this, args); }


        void OnS16F101Recv(GemEventArgs args) { OnS16F101Event(this, args); }  //
        void OnS16F103Recv(GemEventArgs args) { OnS16F103Event(this, args); }  //
        void OnS16F105Recv(GemEventArgs args) { OnS16F105Event(this, args); }  //
        //void OnS16F112Recv(GemEventArgs args) { OnS16F112Event(this, args); }  //
        //void OnS16F114Recv(GemEventArgs args) { OnS16F114Event(this, args); }  //
        //void OnS16F116Recv(GemEventArgs args) { OnS16F116Event(this, args); }  //
        void OnS16F121Recv(GemEventArgs args) { OnS16F121Event(this, args); }  //
        void OnS16F123Recv(GemEventArgs args) { OnS16F123Event(this, args); }  //
        void OnS16F125Recv(GemEventArgs args) { OnS16F125Event(this, args); }  //
        //void OnS16F132Recv(GemEventArgs args) { OnS16F132Event(this, args); }  //
        //void OnS16F134Recv(GemEventArgs args) { OnS16F134Event(this, args); }  //
        //void OnS16F136Recv(GemEventArgs args) { OnS16F136Event(this, args); }  //
       
    }                                         
}
