using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTGame
{
    public class XCell
    {
        public int iRow { get; set; }
        public int iCol { get; set; }

        public XCell(int row,int col)
        {
            iRow = row;
            iCol = col;
        }

        public override string ToString()
        {
            return $"{iRow}-{iCol}";
        }

    }
}
