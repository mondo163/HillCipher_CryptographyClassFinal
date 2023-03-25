using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST407_HillCypher_NickandArmando
{
    public partial class EncryptionPage : Form
    {
        public string plainText { get; set; }
        public string reducablePlainText { get; set; }
        public string key { get; set; }
        public string reducableCipherText { get; set; }
        public string cipherText { get; set; }
        public int sections { get; set; }
        public int[,] keyMatrixNumbers { get; set; }
        public int[,] messageVectorNumbers { get; set; }
        public int[,] cipherMatrixNumbers { get; set; }
        int[,] shiftableMessageVector { get; set; }
        int[,] shiftableCipherVector { get; set; }

        HillCypherHelper helper = new HillCypherHelper();
        public EncryptionPage(string plainText, string key)
        {
            InitializeComponent();
            this.plainText = plainText;
            this.key = key;
            cipherText = helper.Encryption(plainText, key);
            reducableCipherText = cipherText;
            reducablePlainText = plainText;

            keyMatrixNumbers = new int[3, 3];
            sections = plainText.Length / 3;
            messageVectorNumbers = new int[3, sections];
            shiftableMessageVector = new int[3, sections];
            cipherMatrixNumbers = new int[plainText.Length, sections];
            shiftableCipherVector = new int[plainText.Length, sections];


            keyMatrixNumbers = helper.GetDisplayedKeyMatrix(key);
            messageVectorNumbers = helper.GetDisplayedMessageVector(keyMatrixNumbers, plainText);
            cipherMatrixNumbers = helper.GetDisplayedCipherMatrix(keyMatrixNumbers, messageVectorNumbers, sections, plainText);

            shiftableMessageVector = messageVectorNumbers;
            shiftableCipherVector = cipherMatrixNumbers;
        }

        private void EncryptionPage_Load(object sender, EventArgs e)
        {
            label22.Visible = false;
            label6.Visible = false;
            //Set The Key Matrix
            labelA.Text = key[0].ToString(); labelB.Text = key[1].ToString(); labelC.Text = key[2].ToString();
            labelD.Text = key[3].ToString(); labelE.Text = key[4].ToString(); labelF.Text = key[5].ToString();
            labelG.Text = key[6].ToString(); labelH.Text = key[7].ToString(); labelI.Text = key[8].ToString();

            //Sets Plain Text That Will Be Encrypted
            labelX.Text = plainText[0].ToString();
            labelY.Text = plainText[1].ToString();
            labelZ.Text = plainText[2].ToString();

            //Sets The Formulas Used To Calculate The Ecryption
            labelFormula1.Text = key[0].ToString() + plainText[0].ToString() + "+"
                + key[1].ToString() + plainText[1].ToString() + "+"
                + key[2].ToString() + plainText[2].ToString();
            labelFormula2.Text = key[3].ToString() + plainText[0].ToString() + "+"
                + key[4].ToString() + plainText[1].ToString() + "+"
                + key[5].ToString() + plainText[2].ToString();
            labelFormula3.Text = key[6].ToString() + plainText[0].ToString() + "+"
                + key[7].ToString() + plainText[1].ToString() + "+"
                + key[8].ToString() + plainText[2].ToString();

            //Shows The Output of The Formula AKA The Encrypted Text
            labelxOut.Text = cipherText[0].ToString();
            labelyOut.Text = cipherText[1].ToString();
            labelzOut.Text = cipherText[2].ToString();

            ////Shows The Complete Cipher Text
            labelCipher.Text = cipherText.ToString();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (!ShowMathButton.Checked)
            {
                //Reduces The String To Show The Next Calculation
                reducablePlainText = reducablePlainText.Remove(0, 3);
                reducableCipherText = reducableCipherText.Remove(0, 3);
                
                if (reducablePlainText.Length != 0)
                {
                    //Sets Plain Text That Will Be Encrypted
                    labelX.Text = reducablePlainText[0].ToString();
                    labelY.Text = reducablePlainText[1].ToString();
                    labelZ.Text = reducablePlainText[2].ToString();

                    //Sets The Formulas Used To Calculate The Ecryption
                    labelFormula1.Text = key[0].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[1].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[2].ToString() + reducablePlainText[2].ToString();
                    labelFormula2.Text = key[3].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[4].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[5].ToString() + reducablePlainText[2].ToString();
                    labelFormula3.Text = key[6].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[7].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[8].ToString() + reducablePlainText[2].ToString();

                    //Shows The Output of The Formula AKA The Encrypted Text
                    labelxOut.Text = reducableCipherText[0].ToString();
                    labelyOut.Text = reducableCipherText[1].ToString();
                    labelzOut.Text = reducableCipherText[2].ToString();
                }
                else //If All The Calculations Have Been Shown Restart From Beginning
                {
                    reducablePlainText = plainText;
                    reducableCipherText = cipherText;

                    //Sets Plain Text That Will Be Encrypted
                    labelX.Text = reducablePlainText[0].ToString();
                    labelY.Text = reducablePlainText[1].ToString();
                    labelZ.Text = reducablePlainText[2].ToString();

                    //Sets The Formulas Used To Calculate The Ecryption
                    labelFormula1.Text = key[0].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[1].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[2].ToString() + reducablePlainText[2].ToString();
                    labelFormula2.Text = key[3].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[4].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[5].ToString() + reducablePlainText[2].ToString();
                    labelFormula3.Text = key[6].ToString() + reducablePlainText[0].ToString() + "+"
                        + key[7].ToString() + reducablePlainText[1].ToString() + "+"
                        + key[8].ToString() + reducablePlainText[2].ToString();

                    //Shows The Output of The Formula AKA The Encrypted Text
                    labelxOut.Text = reducableCipherText[0].ToString();
                    labelyOut.Text = reducableCipherText[1].ToString();
                    labelzOut.Text = reducableCipherText[2].ToString();
                }
            }
            else
            {

                int[,] tempVector = new int[3, sections];
                int count = 1;

                for(int i = 0; i < sections; i++)
                {
                    for(int j = 0; j <3; j++)
                    {
                        if (count != sections)
                        {
                            tempVector[j, i] = shiftableMessageVector[j, i + 1];                            
                        }
                        else
                        {
                            tempVector[j, i] = shiftableMessageVector[j, 0];
                        }
                    }
                    count++;
                }

                shiftableMessageVector = tempVector;
                tempVector = null;
                tempVector = new int[plainText.Length, sections];
                count = 1;

                for (int i = 0; i < sections; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (count != sections)
                        {
                            tempVector[j, i] = shiftableCipherVector[j, i + 1];
                        }
                        else
                        {
                            tempVector[j, i] = shiftableCipherVector[j, 0];
                        }
                    }
                    count++;
                }

                shiftableCipherVector = tempVector;
                tempVector = null;

                //Shows Plain Text As Numbers That Will Be Encrypted
                labelX.Text = shiftableMessageVector[0, 0].ToString();
                labelY.Text = shiftableMessageVector[1, 0].ToString();
                labelZ.Text = shiftableMessageVector[2, 0].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = "(" + keyMatrixNumbers[0, 0].ToString() + ")" + "(" + shiftableMessageVector[0, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[0, 1].ToString() + ")" + "(" + shiftableMessageVector[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[0, 2].ToString() + ")" + "(" + shiftableMessageVector[2, 0].ToString() + ")";
                labelFormula2.Text = "(" + keyMatrixNumbers[1, 0].ToString() + ")" + "(" + shiftableMessageVector[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[1, 1].ToString() + ")" + "(" + shiftableMessageVector[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[1, 2].ToString() + ")" + "(" + shiftableMessageVector[2, 0].ToString() + ")";
                labelFormula3.Text = "(" + keyMatrixNumbers[2, 0].ToString() + ")" + "(" + shiftableMessageVector[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[2, 1].ToString() + ")" + "(" + shiftableMessageVector[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[2, 2].ToString() + ")" + "(" + shiftableMessageVector[2, 1].ToString() + ")";

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = shiftableCipherVector[0, 0].ToString();
                labelyOut.Text = shiftableCipherVector[1, 0].ToString();
                labelzOut.Text = shiftableCipherVector[2, 0].ToString();
            }
        }

        private void ShowMathButton_CheckedChanged(object sender, EventArgs e)
        {
            if(ShowMathButton.Checked)
            {
                label22.Visible = true;
                label6.Visible = true;

                label4.Visible = false;
                label5.Visible = false;

                //Shows Key As Number Values
                labelA.Text = keyMatrixNumbers[0,0].ToString(); labelB.Text = keyMatrixNumbers[0,1].ToString(); labelC.Text = keyMatrixNumbers[0,2].ToString();
                labelD.Text = keyMatrixNumbers[1,0].ToString(); labelE.Text = keyMatrixNumbers[1,1].ToString(); labelF.Text = keyMatrixNumbers[1,2].ToString();
                labelG.Text = keyMatrixNumbers[2,0].ToString(); labelH.Text = keyMatrixNumbers[2,1].ToString(); labelI.Text = keyMatrixNumbers[2,2].ToString();

                //Shows Plain Text As Numbers That Will Be Encrypted
                labelX.Text = messageVectorNumbers[0,0].ToString();
                labelY.Text = messageVectorNumbers[1,0].ToString();
                labelZ.Text = messageVectorNumbers[2,0].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = "("+keyMatrixNumbers[0,0].ToString() + ")" + "(" + messageVectorNumbers[0,0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[0,1].ToString() + ")" + "(" + messageVectorNumbers[1,0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[0,2].ToString() + ")" + "(" + messageVectorNumbers[2,0].ToString() + ")";
                labelFormula2.Text = "(" + keyMatrixNumbers[1, 0].ToString() + ")" + "(" + messageVectorNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[1, 1].ToString() + ")" + "(" + messageVectorNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[1, 2].ToString() + ")" + "(" + messageVectorNumbers[2, 0].ToString() + ")";
                labelFormula3.Text = "(" + keyMatrixNumbers[2, 0].ToString() + ")" + "(" + messageVectorNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[2, 1].ToString() + ")" + "(" + messageVectorNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + keyMatrixNumbers[2, 2].ToString() + ")" + "(" + messageVectorNumbers[2, 1].ToString() + ")";

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = cipherMatrixNumbers[0,0].ToString();
                labelyOut.Text = cipherMatrixNumbers[1,0].ToString();
                labelzOut.Text = cipherMatrixNumbers[2,0].ToString();
            }
            else
            {
                label22.Visible = false;
                label6.Visible = false;

                label4.Visible = true;
                label5.Visible = true;

                //Set The Key Matrix
                labelA.Text = key[0].ToString(); labelB.Text = key[1].ToString(); labelC.Text = key[2].ToString();
                labelD.Text = key[3].ToString(); labelE.Text = key[4].ToString(); labelF.Text = key[5].ToString();
                labelG.Text = key[6].ToString(); labelH.Text = key[7].ToString(); labelI.Text = key[8].ToString();

                //Sets Plain Text That Will Be Encrypted
                labelX.Text = plainText[0].ToString();
                labelY.Text = plainText[1].ToString();
                labelZ.Text = plainText[2].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = key[0].ToString() + plainText[0].ToString() + "+"
                    + key[1].ToString() + plainText[1].ToString() + "+"
                    + key[2].ToString() + plainText[2].ToString();
                labelFormula2.Text = key[3].ToString() + plainText[0].ToString() + "+"
                    + key[4].ToString() + plainText[1].ToString() + "+"
                    + key[5].ToString() + plainText[2].ToString();
                labelFormula3.Text = key[6].ToString() + plainText[0].ToString() + "+"
                    + key[7].ToString() + plainText[1].ToString() + "+"
                    + key[8].ToString() + plainText[2].ToString();

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = cipherText[0].ToString();
                labelyOut.Text = cipherText[1].ToString();
                labelzOut.Text = cipherText[2].ToString();
            }
        }
    }
}
