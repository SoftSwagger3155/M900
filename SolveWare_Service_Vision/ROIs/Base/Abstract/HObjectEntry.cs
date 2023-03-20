using HalconDotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Base.Abstract
{
    public class HObjectEntry
    {
        /// <summary>Hashlist defining the graphical context for HObj</summary>
        public Hashtable gContext;

        /// <summary>HALCON object</summary>
        public HObject HObj;



        /// <summary>Constructor</summary>
        /// <param name="obj">
        /// HALCON object that is linked to the graphical context gc. 
        /// </param>
        /// <param name="gc"> 
        /// Hashlist of graphical states that are applied before the object
        /// is displayed. 
        /// </param>
        public HObjectEntry(HObject obj, Hashtable gc)
        {
            gContext = gc;
            HObj = obj;
        }

        /// <summary>
        /// Clears the entries of the class members Hobj and gContext
        /// </summary>
        public void clear()
        {
            gContext.Clear();
            HObj.Dispose();
        }

    }//end of class
}
