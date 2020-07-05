using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_Keyboard___cSharp
{
    public partial class frmCalculator : Form
    {
        ResizeFormControls ReSizeFrm;

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void FrmCalculator_Load(object sender, EventArgs e)
        {
            ReSizeFrm = new ResizeFormControls(this);
            btnClear.PerformClick();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtDisplay.Text == "0")
            {
                txtDisplay.Text = btn.Text;
            }
            else
            {
                txtDisplay.Text += btn.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            txtMemory.Text = "";
            txtOperation.Text = "";
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text == "0")
            {
                txtDisplay.Text = "0.";
            }
            else
            {
                if (txtDisplay.Text.Contains(".") == false) txtDisplay.Text += ".";
            }
        }

        private void mathOperation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string mathOp = btn.Text;
            if (txtOperation.Text == "")
            {
                txtMemory.Text = txtDisplay.Text;
                txtOperation.Text = mathOp;
                txtDisplay.Text = "0";
            }
            else
            {
                switch (txtOperation.Text)
                {
                    case "+":
                        txtMemory.Text = Convert.ToString(double.Parse(txtMemory.Text) + double.Parse(txtDisplay.Text));
                        break;
                    case "-":
                        txtMemory.Text = Convert.ToString(double.Parse(txtMemory.Text) - double.Parse(txtDisplay.Text));
                        break;
                    case "X":
                        txtMemory.Text = Convert.ToString(double.Parse(txtMemory.Text) * double.Parse(txtDisplay.Text));
                        break;
                    case "/":
                        txtMemory.Text = Convert.ToString(double.Parse(txtMemory.Text) / double.Parse(txtDisplay.Text));
                        break;
                }
                txtOperation.Text = mathOp;// btn.Text;
                txtDisplay.Text = "0";
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            switch (txtOperation.Text)
            {
                case "+":
                    txtDisplay.Text = Convert.ToString(double.Parse(txtMemory.Text) + double.Parse(txtDisplay.Text));
                    break;
                case "-":
                    txtDisplay.Text = Convert.ToString(double.Parse(txtMemory.Text) - double.Parse(txtDisplay.Text));
                    break;
                case "X":
                    txtDisplay.Text = Convert.ToString(double.Parse(txtMemory.Text) * double.Parse(txtDisplay.Text));
                    break;
                case "/":
                    txtDisplay.Text = Convert.ToString(double.Parse(txtMemory.Text) / double.Parse(txtDisplay.Text));
                    break;
            }
            txtMemory.Text = "";
            txtOperation.Text = "";
        }

        private void frmCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            Button btn = new Button();
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                    btn0.PerformClick();
                    //btn_Click(btn0, null);
                    break;
                case Keys.NumPad1:
                    btn_Click(btn1, null);
                    break;
                case Keys.NumPad2:
                    btn_Click(btn2, null);
                    break;
                case Keys.NumPad3:
                    btn_Click(btn3, null);
                    break;
                case Keys.NumPad4:
                    btn_Click(btn4, null);
                    break;
                case Keys.NumPad5:
                    btn_Click(btn5, null);
                    break;
                case Keys.NumPad6:
                    btn_Click(btn6, null);
                    break;
                case Keys.NumPad7:
                    btn_Click(btn7, null);
                    break;
                case Keys.NumPad8:
                    btn_Click(btn8, null);
                    break;
                case Keys.NumPad9:
                    btn_Click(btn9, null);
                    break;
                case Keys.Add:
                    btn.Text = "+";
                    //mathOperation_Click(btnPlus, null);
                    btnPlus.PerformClick();
                    break;
                case Keys.Subtract:
                    btn.Text = "-";
                    mathOperation_Click(btnMinus, null);
                    break;
                case Keys.Multiply:
                    btn.Text = "*";
                    mathOperation_Click(btnMultiply, null);
                    break;
                case Keys.Divide:
                    btn.Text = "/";
                    mathOperation_Click(btnDivide, null);
                    break;
                case Keys.Decimal:
                    btnDecimal_Click(null, null);
                    break;
                case Keys.OemPeriod:
                    btnDecimal_Click(null, null);
                    break;
                case Keys.Enter:
                    btnEqual_Click(null, null);
                    break;
                case Keys.Back:
                    btnBackspace_Click(null, null);
                    break;
                case Keys.Delete:
                    btnClear_Click(null, null);
                    break;
            }
        }

        private void frmCalculator_Resize(object sender, EventArgs e)
        {
            ReSizeFrm.ResizeControls();
        }
    }

    public class ResizeFormControls
    {
        Rectangle frmResizeInit;
        Dictionary<string, Control> ctrls = new Dictionary<string, Control>();

        public Form FrmToResize;

        public ResizeFormControls(Form frm)
        {
            FrmToResize = frm;

            frmResizeInit = new Rectangle(frm.ClientRectangle.X, frm.ClientRectangle.Y,
                   frm.ClientRectangle.Width, frm.ClientRectangle.Height);
            frm.Tag = frmResizeInit;

            foreach (Control ctrl in frm.Controls)
            {
                ctrl.Tag = new Rectangle(ctrl.Left, ctrl.Top, ctrl.Width, ctrl.Height);
                ctrls.Add(ctrl.Name, ctrl);
            }
        }

        public void ResizeControls()
        {
            float xRatio = (float)FrmToResize.ClientRectangle.Width / (float)((Rectangle)FrmToResize.Tag).Width;
            float yRatio = (float)FrmToResize.ClientRectangle.Height / (float)((Rectangle)FrmToResize.Tag).Height;

            foreach (Control ctrl in FrmToResize.Controls)
            {
                ctrl.Left = Convert.ToInt32((((Rectangle)ctrls[ctrl.Name].Tag).Left) * xRatio);
                ctrl.Top = Convert.ToInt32((((Rectangle)ctrls[ctrl.Name].Tag).Top) * yRatio);
                ctrl.Width = Convert.ToInt32((((Rectangle)ctrls[ctrl.Name].Tag).Width) * xRatio);
                ctrl.Height = Convert.ToInt32((((Rectangle)ctrls[ctrl.Name].Tag).Height) * yRatio);
            }
        }

    }

}
