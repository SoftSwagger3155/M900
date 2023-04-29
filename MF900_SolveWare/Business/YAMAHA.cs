using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Business
{
    public class YAMAHA
    {
        private static YAMAHA mf901;
        private static object mutex = new object();

        private YAMAHA()
        {

        }
        public static YAMAHA MF901
        {
            get
            {
                if (mf901 == null)
                {
                    lock (mutex)
                    {
                        if (mf901 == null)
                        {
                            mf901 = new YAMAHA();
                        }
                    }
                }
                return mf901;
            }
        }


        //这边纪录 YAMAHA所有的资料



    }
}
