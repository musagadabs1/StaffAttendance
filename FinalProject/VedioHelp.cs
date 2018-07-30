using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class VedioHelp : Form
    {
        static VedioHelp box;
        public VedioHelp()
        {
            InitializeComponent();
        }
        public static void MsgShow(string tittel)
        {
            box = new VedioHelp();
            box.Text = tittel;
            box.ShowDialog();
        }
        #region watch video
        //private void Watch()
        //{ 
        //axWindowsMediaPlayer1.URL=
        //}
        #endregion
    }
}
