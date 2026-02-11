using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMISoft.HSMS
{
    public class HSMSException : Exception
    {
        public HSMSException()
        {

        }

        public HSMSException(string Message)
            : base(Message)
        {
        }
    }

    /// <summary>
    /// <U1, 1223>
    /// <KK, hh>
    /// 처럼 해석 할 수 없는 명령이 들어 왔을 때 발생하는 에러
    /// </summary>
    public class HSMSMInvalidData : HSMSException
    {
        public HSMSMInvalidData()
            : base(string.Format("해석할 수 없는 명령입니다."))
        {

        }

        public HSMSMInvalidData(string msg)
            : base(string.Format("해석할 수 없는 명령입니다.", msg))
        {

        }

    }
    /// <summary>
    /// 리스트 항목의 개수와 실제 라인수가 맞지 않아서 발생함.
    /// 일반적으로 중간에 항목이 빠지거나 더있어서 발생하는 문제임
    /// </summary>
    public class HSMSMessageCountMissMatchException : HSMSException
    {
        public HSMSMessageCountMissMatchException()
            : base(string.Format("리스트의 개수가 맞지 않습니다. Class: {0}", ""))
        {

        }
        public HSMSMessageCountMissMatchException(string className)
            : base (string.Format("리스트의 개수가 맞지 않습니다. Class: {0}", className))
        {
        }

    }


    /// <summary>
    /// Binary에서 HSMS데이터를 해석할 때 잘못된 포맷의 데이터가 있으면 발생함
    /// </summary>
    public class HSMSInvalidFormatExcepton : HSMSException
    {
        public HSMSInvalidFormatExcepton()
            : base("허용하지 않는 데이터 포맷을 해석하려 했습니다")
        {

        }

        public HSMSInvalidFormatExcepton(int line, byte format)
            : base(MakeMessage<byte>(line, format))
        {

        }

        public HSMSInvalidFormatExcepton(int line, string format)
            : base(MakeMessage<string>(line, format))
        {

        }

        private static string MakeMessage<T>(int line, T format)
        {
            return string.Format("잘못된 데이터 포맷입니다: Line: {0}, Foramt; {1}",
                line, format);
        }

    }

    public class HSMSIndexBoundException : HSMSException
    {
        public HSMSIndexBoundException(int messageLength, int requestLength)
            : base(MakeMessage(messageLength, requestLength))
        {
            MessageLength = messageLength;
            RequestLenght = requestLength;
        }

        private static string MakeMessage(int messageLength, int requestLength)
        {
            return string.Format("요청한 Line 데이터 없음, 메시지 라인: {0}, 요청 라인; {1}",
                messageLength, requestLength);
        }

        public int MessageLength;
        public int RequestLenght;
    }

    public class HSMSParsingFormatException : HSMSException
    {
        public HSMSParsingFormatException(int lineNo, ItemFormat targetFormat, ItemFormat readFormat, string className = "", string propertyName = "")
            : base(MakeMessage(lineNo, targetFormat, readFormat, className, propertyName))
        {
            ClassName = className;
            ReadFormat = readFormat;
            TargetFormat = targetFormat;
            LineNo = lineNo;
        }

        private static string MakeMessage(int lineNo, ItemFormat targetFormat, ItemFormat readFormat, string className, string propertyName = "")
        {
            return string.Format("Message Format이 틀림, lineNo;{0}, TargetFormat: {1}, ReadFormat: {2}, ClassName: {3}, PropertyName: {4}",
                lineNo + 1, targetFormat, readFormat, className, propertyName);
        }
        public string ClassName;
        public int LineNo;
        public ItemFormat TargetFormat;
        public ItemFormat ReadFormat;
    }

    public class HSMSParsingLengthException : HSMSException
    {
        public HSMSParsingLengthException(int lineNo, int targetLength, int readLength, string className = "")
        {
            ClassName = className;
            ReadLength = readLength;
            TargetLength = targetLength;
            LineNo = lineNo;
        }
        public string ClassName;
        public int LineNo;
        public int TargetLength;
        public int ReadLength;
    }
    

    /// <summary>
    /// 클래슬르 메시지로 만드는데 에러 발생
    /// </summary>
    public class HSMSMakeDataException : HSMSException
    {
        public HSMSMakeDataException(string className, string propertyName)
            : base(MakeMessage(className, propertyName))
        {
            ClassName = className;
            PropertyName = propertyName;
        }

        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        private static string MakeMessage(string className, string propertyName)
        {
            return string.Format("속성이 초기화 되지 않음 class: {0}, Property: {1}", className, propertyName);

        }
    }
}
