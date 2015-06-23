using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wpf_client
{
    public partial class GameConfigForm : Form
    {
        public event EventHandler OnFieldsSet;

        private int rows;
        private int cols;

        public GameConfigForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GameConfigForm_Load(object sender, EventArgs e)
        {

        }

        public int Rows
        {
            get
            {
                return this.rows;
            }

            private set
            {
                this.rows = value;
            }
        }

        public int Cols
        {
            get
            {
                return this.cols;
            }
            set
            {
                this.cols = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var validRowAndCol = false;
            try
            {
                this.Rows = int.Parse(this.tb_rows.Text);
                this.Cols = int.Parse(this.tb_cols.Text);

                validRowAndCol = true;
            }
            catch (Exception ex)
            {
                validRowAndCol = false;
            }

            if (validRowAndCol)
            {
                if (this.OnFieldsSet != null)
                {
                    this.OnFieldsSet.Invoke(this, new EventArgs());
                }
                this.Close();
            }
        }
    }
}
