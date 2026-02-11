using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace MMISoft.HSMS
{
    internal static class Auth
    {
        [DllImport("EngLock.dll", CharSet = CharSet.Ansi)]
        static extern int CheckLock(int key);

        static internal unsafe bool AuthCheck()
        {
            Console.WriteLine("Auth.AuthCheck: Called.");
            return true;
//            if (auth == true)
//                return true;
//            if (firstCheck == true)
//            {
//                startTime = DateTime.Now;
//                int size = DllCheck();
////                MessageBox.Show(size.ToString());
//                if (sizeof(IntPtr) == 4)
//                {
//                    if (size != Engion.EngCommon.lockDllSizeWin32)
//                        return false;
//                }
//                else
//                {
//                    if (size != Engion.EngCommon.lockDllSizeX64)
//                        return false;
//                }
//                firstCheck = true;
//            }
//            if (CheckLock(0) == 0)
//            {
//                auth = true;
//                return true;
//            }

//            if (DateTime.Now - startTime > new TimeSpan(4, 0, 0))
//            {
//                MessageBox.Show("만료되었습니다.");
//                return false;
//            }
//            return false;
        }

        static  int DllCheck()
        {
            if (File.Exists("englock.dll"))
            {
                int sum = 0;
                byte[] array = new byte[10000];
                using (FileStream sr = new FileStream("englock.dll", FileMode.Open))
                {
                    sr.Read(array, 0, 10000);
                    for (int i = 0; i < 10000; i++)
                        sum += array[i];
                }
                return sum;
            }
            else
                return -1;
        }


    }
}
