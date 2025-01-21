using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMievCan
{
    internal class dataModel
    {
        public DataTable dt;


        public dataModel()
        {
           dt = new DataTable();
            dt.Columns.Add("nr");
            dt.Columns.Add("title");
            dt.Columns.Add("pid");
            dt.Columns.Add("bit1");
            dt.Columns.Add("bit2");
            dt.Columns.Add("bit3");
            dt.Columns.Add("bit4");
            dt.Columns.Add("bit5");
            dt.Columns.Add("bit6");
            dt.Columns.Add("bit7");
            dt.Columns.Add("bit8");
            dt.Columns.Add("formula");
            dt.Columns.Add("val");
            dt.Columns.Add("lastupdate");
        }

    }
}
