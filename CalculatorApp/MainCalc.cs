using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class calcForm : Form
    {
        private Operations operations = new Operations();
        private double previousAnswer = 0;
        public calcForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += MainCalc_FormClosing;
        }

        private void calcForm_Load(object sender, EventArgs e)
        {
        }

        private void MainCalc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void CheckAndAdjustFontSize()
        {
            const int maxFontSize = 60, minFontSize = 45, maxTextLength = 9;

            if (tbDisplay.Text.Length > maxTextLength)
            {
                // Calculate the ratio of current length to the maximum length
                float ratio = (float)tbDisplay.Text.Length / maxTextLength;

                // Calculate the new font size within the specified range
                int newFontSize = (int)(maxFontSize - ratio * (maxFontSize - minFontSize));

                // Set the font size for the entire text in the TextBox
                tbDisplay.Font = new Font(tbDisplay.Font.FontFamily, newFontSize);
            }
        }

        // Operation buttons
        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnAdd = (Button)sender;
            tbDisplay.Text += btnAdd.Text;

            CheckAndAdjustFontSize();
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnSubtract = (Button)sender;
            tbDisplay.Text += btnSubtract.Text;

            CheckAndAdjustFontSize();

        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnMultiply = (Button)sender;
            tbDisplay.Text += btnMultiply.Text;

            CheckAndAdjustFontSize();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {

            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnDivide = (Button)sender;
            tbDisplay.Text += btnDivide.Text;

            CheckAndAdjustFontSize();
        }

        // Number buttons
        private void btnZero_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnZero = (Button)sender;
            tbDisplay.Text += btnZero.Text;

            CheckAndAdjustFontSize();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnOne = (Button)sender;
            tbDisplay.Text += btnOne.Text;

            CheckAndAdjustFontSize();
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnTwo = (Button)sender;
            tbDisplay.Text += btnTwo.Text;

            CheckAndAdjustFontSize();
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnThree = (Button)sender;
            tbDisplay.Text += btnThree.Text;

            CheckAndAdjustFontSize();
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnFour = (Button)sender;
            tbDisplay.Text += btnFour.Text;

            CheckAndAdjustFontSize();
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnFive = (Button)sender;
            tbDisplay.Text += btnFive.Text;

            CheckAndAdjustFontSize();
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnSix = (Button)sender;
            tbDisplay.Text += btnSix.Text;

            CheckAndAdjustFontSize();
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnSeven = (Button)sender;
            tbDisplay.Text += btnSeven.Text;

            CheckAndAdjustFontSize();
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnEight = (Button)sender;
            tbDisplay.Text += btnEight.Text;

            CheckAndAdjustFontSize();
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnNine = (Button)sender;
            tbDisplay.Text += btnNine.Text;

            CheckAndAdjustFontSize();
        }
        private void btnPoint_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnPoint = (Button)sender;
            tbDisplay.Text += btnPoint.Text;

            CheckAndAdjustFontSize();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = string.Empty;
        }

        private void btnOPar_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnOpar = (Button)sender;
            tbDisplay.Text += btnOpar.Text;

            CheckAndAdjustFontSize();
        }

        private void btnCPar_Click(object sender, EventArgs e)
        {
            tbDisplay.SelectionAlignment = HorizontalAlignment.Right;

            Button btnCPar = (Button)sender;
            tbDisplay.Text += btnCPar.Text;

            CheckAndAdjustFontSize();
        }

        private void btnPrevAnswer_Click(object sender, EventArgs e)
        {
            // check if there is a previous answer
            if (previousAnswer != 0)
            {
                tbDisplay.Text += previousAnswer.ToString();
            }
            else
            {
                MessageBox.Show("There is no previous answer.");
            }
        }
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                double result = operations.Equals(tbDisplay.Text);
                previousAnswer = result;
                tbDisplay.Text = result.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
