using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTTGame
{
    public partial class frmGame : Form
    {
        private int iRow = 3, iCol = 3;
        private int iW = 0, iH = 0;
        private int[,] MapSet = null;
        private PictureBox[] lstBtn = null;
        private bool IsX = true;
        private bool IsGaveOver = false;

        public frmGame()
        {
            InitializeComponent();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {

            Init();
        }

        private void Init()
        {
            IsGaveOver = false;
            IsX = true;
            string sn = IsX ? "X" : "O";
            lblMessage.Text = $"Start with: {sn} ...";
            lblMessage.BackColor = Color.Transparent;
            lblMessage.ForeColor = Color.Black;
            picContainer.Controls.Clear();

            lstBtn = new PictureBox[iRow * iCol];
            MapSet = new int[iRow, iCol];
            iW = picContainer.Width / iCol;
            iH = picContainer.Height / iRow;

            for (int i = 0; i < iRow; i++)
            {
                for (int j = 0; j < iCol; j++)
                {
                    MapSet[i, j] = 0;
                    PictureBox picItem = new PictureBox();
                    picItem.Tag = new XCell(i, j);
                    picItem.Height = iH;
                    picItem.Width = iW;
                    picItem.BackColor = Color.CornflowerBlue;
                    picItem.BorderStyle = BorderStyle.FixedSingle;
                    picItem.Location = new Point(j * iW, i * iH);
                    picItem.Click += PicItem_Click;

                    picContainer.Controls.Add(picItem);
                    lstBtn[i * iCol + j] = picItem;
                    picContainer.Refresh();
                }
            }
        }

        private void PicItem_Click(object sender, EventArgs e)
        {
            if (!IsGaveOver)
            {
                PictureBox pic = sender as PictureBox;
                XCell xc = pic.Tag as XCell;
                lblMessage.Text = xc.ToString();

                if (MapSet[xc.iRow, xc.iCol] == 0)
                {
                    if (IsX)
                    {
                        lstBtn[xc.iRow * iCol + xc.iCol].BackColor = Color.White;
                        lstBtn[xc.iRow * iCol + xc.iCol].Image = imageList1.Images[1];
                        lstBtn[xc.iRow * iCol + xc.iCol].SizeMode = PictureBoxSizeMode.CenterImage;
                        MapSet[xc.iRow, xc.iCol] = 1;
                    }
                    else
                    {
                        lstBtn[xc.iRow * iCol + xc.iCol].BackColor = Color.White;
                        lstBtn[xc.iRow * iCol + xc.iCol].Image = imageList1.Images[0];
                        lstBtn[xc.iRow * iCol + xc.iCol].SizeMode = PictureBoxSizeMode.CenterImage;
                        MapSet[xc.iRow, xc.iCol] = -1;
                    }
                    if (CheckWiner(xc, IsX))
                    {
                        string wn = IsX ? "X" : "O";
                        lblMessage.Text = $"{wn} is Winner.";
                        lblMessage.BackColor = Color.Orange;
                        lblMessage.ForeColor = Color.White;
                        IsGaveOver = true;
                    }
                    else if (CheckDead())
                    {
                        lblMessage.Text = "GAME OVER!!!";
                        lblMessage.BackColor = Color.Red;
                        lblMessage.ForeColor = Color.White;
                        IsGaveOver = true;

                    }

                    IsX = !IsX;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckWiner(XCell chkItem, bool isXCell)
        {
            bool IsWin = false;
            int iTotal = 0;
            for (int i = 0; i < iCol; i++)
            {
                iTotal += MapSet[chkItem.iRow, i];
            }
            IsWin = Math.Abs(iTotal) == iCol;
            if (!IsWin)
            {
                iTotal = 0;
                for (int j = 0; j < iRow; j++)
                {
                    iTotal += MapSet[j, chkItem.iCol];
                }
                IsWin = Math.Abs(iTotal) == iRow;
            }
            if (!IsWin)
            {
                iTotal = 0;
                for (int i = 0; i < iRow && chkItem.iRow - i > -1 && chkItem.iCol + i < iCol; i++)
                {
                    iTotal += MapSet[chkItem.iRow - i, chkItem.iCol + i];
                }
                for (int i = 1; i < iRow && chkItem.iRow + i < iRow && chkItem.iCol - i > -1; i++)
                {
                    iTotal += MapSet[chkItem.iRow + i, chkItem.iCol - i];
                }
                IsWin = Math.Abs(iTotal) == iRow;
            }
            if (!IsWin)
            {
                iTotal = 0;
                for (int i = 0; i < iRow && chkItem.iRow - i > -1 && chkItem.iCol - i > -1; i++)
                {
                    iTotal += MapSet[chkItem.iRow - i, chkItem.iCol - i];
                }
                for (int i = 1; i < iRow && chkItem.iRow + i < iRow && chkItem.iCol + i < iCol; i++)
                {
                    iTotal += MapSet[chkItem.iRow + i, chkItem.iCol + i];
                }
                IsWin = Math.Abs(iTotal) == iRow;
            }
            return IsWin;
        }
        private bool CheckDead()
        {
            bool isDead = true;
            for (int i = 0; i < iRow; i++)
            {
                for (int j = 0; j < iCol; j++)
                {
                    if (MapSet[i, j] == 0)
                    {
                        isDead = false;
                        return isDead;
                    }
                }
            }
            return isDead;
        }
    }
}
