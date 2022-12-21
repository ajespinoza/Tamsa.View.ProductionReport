using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VerhoeffCheckDigitLibrary;

namespace VerhoeffCheckDigitApp
{
    public partial class VerhoeffCheckDigitForm : Form
    {
        public VerhoeffCheckDigitForm()
        {
            InitializeComponent();
        }

        private void btnCalculateCheckDigit_Click(object sender, EventArgs e)
        {
            try
            {
                txtInput2.Clear();
                txtResultWithCheckDigit.Clear();
                txtCheckDigit.Clear();
                txtCheckResult1.Clear();
                txtCheckResult2.Clear();

                txtInput.Text = txtInput.Text.Trim();

                if (txtInput.Text.Length > 0)
                {
                    if (rbString.Checked)
                    {
                        txtResultWithCheckDigit.Text = VerhoeffCheckDigit.AppendCheckDigit(txtInput.Text).ToString();
                        txtCheckDigit.Text = VerhoeffCheckDigit.CalculateCheckDigit(txtInput.Text).ToString();
                    }
                    else if (rbInt.Checked)
                    {
                        txtResultWithCheckDigit.Text = VerhoeffCheckDigit.AppendCheckDigit(Convert.ToInt32(txtInput.Text)).ToString();
                        txtCheckDigit.Text = VerhoeffCheckDigit.CalculateCheckDigit(Convert.ToInt32(txtInput.Text)).ToString();
                    }
                    else if (rbLong.Checked)
                    {
                        txtResultWithCheckDigit.Text = VerhoeffCheckDigit.AppendCheckDigit(Convert.ToInt64(txtInput.Text)).ToString();
                        txtCheckDigit.Text = VerhoeffCheckDigit.CalculateCheckDigit(Convert.ToInt64(txtInput.Text)).ToString();
                    }
                    else if (rbIntArray.Checked)
                    {
                        txtResultWithCheckDigit.Text = ConvertToLong(VerhoeffCheckDigit.AppendCheckDigit(ConvertToIntArray(txtInput.Text))).ToString();
                        txtCheckDigit.Text = VerhoeffCheckDigit.CalculateCheckDigit(ConvertToIntArray(txtInput.Text)).ToString();
                    }
                    txtInput2.Text = txtInput.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                txtCheckResult1.Clear();
                txtCheckResult2.Clear();

                txtResultWithCheckDigit.Text = txtResultWithCheckDigit.Text.Trim();
                txtInput2.Text = txtInput2.Text.Trim();
                txtCheckDigit.Text = txtCheckDigit.Text.Trim();

                if (txtResultWithCheckDigit.Text.Length > 0)
                {
                    if (rbString.Checked)
                    {
                        txtCheckResult1.Text = VerhoeffCheckDigit.Check(txtResultWithCheckDigit.Text).ToString();
                    }
                    else if (rbInt.Checked)
                    {
                        txtCheckResult1.Text = VerhoeffCheckDigit.Check(Convert.ToInt32(txtResultWithCheckDigit.Text)).ToString();
                    }
                    else if (rbLong.Checked)
                    {
                        txtCheckResult1.Text = VerhoeffCheckDigit.Check(Convert.ToInt64(txtResultWithCheckDigit.Text)).ToString();
                    }
                    else if (rbIntArray.Checked)
                    {
                        txtCheckResult1.Text = VerhoeffCheckDigit.Check(ConvertToIntArray(txtResultWithCheckDigit.Text)).ToString();
                    }
                }
 
                    if (txtInput2.Text.Length > 0 && txtCheckDigit.Text.Length > 0)
                    {
                        if (rbString.Checked)
                        {
                            txtCheckResult2.Text = VerhoeffCheckDigit.Check(txtInput2.Text, Convert.ToInt32(txtCheckDigit.Text)).ToString();
                        }
                        else if (rbInt.Checked)
                        {
                            txtCheckResult2.Text = VerhoeffCheckDigit.Check(Convert.ToInt32(txtInput2.Text), Convert.ToInt32(txtCheckDigit.Text)).ToString();
                        }
                        else if (rbLong.Checked)
                        {
                            txtCheckResult2.Text = VerhoeffCheckDigit.Check(Convert.ToInt64(txtInput2.Text), Convert.ToInt32(txtCheckDigit.Text)).ToString();
                        }
                        else if (rbIntArray.Checked)
                        {
                            txtCheckResult2.Text = VerhoeffCheckDigit.Check(ConvertToIntArray(txtInput2.Text), Convert.ToInt32(txtCheckDigit.Text)).ToString();
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception");
            }
        }

        private int[] ConvertToIntArray(string input)
        {
            int[] inputArray = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
                inputArray[i] = Convert.ToInt32(input.Substring(i, 1));

            return inputArray;
        }

        private long ConvertToLong(int[] input)
        {
            long result = 0;
            long power = 1;

            for (int i = 0; i < input.Length; i++)
            {
                result += input[input.Length - (i + 1)] * power;
                power *= 10;
            }

            return result;
        }
    }
}