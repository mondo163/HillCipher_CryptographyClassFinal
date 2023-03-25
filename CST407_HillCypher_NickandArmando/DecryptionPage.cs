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
    public partial class DecryptionPage : Form
    {
        public string plainText { get; set; }
        public string reducablePlainText { get; set; }
        public string key { get; set; }
        public string reducableCipherText { get; set; }
        public string cipherText { get; set; }
        public string inverseKey { get; set; }
        public int sections { get; set; }
        public int[,] keyMatrixNumbers { get; set; }
        public int[,] inverseKeyMatrixNumbers { get; set; }
        public int[,] messageVectorNumbers { get; set; }
        public int[,] cipherMatrixNumbers { get; set; }
        int[,] shiftableMessageVector { get; set; }
        int[,] shiftableCipherVector { get; set; }

        HillCypherHelper helper = new HillCypherHelper();
        public DecryptionPage(string cipherText, string key)
        {
            InitializeComponent();
            this.cipherText = cipherText;
            this.key = key;
            inverseKey = helper.GetInverseKeyMatrixAsString(key);
            plainText = helper.Decryption(cipherText, key);
            reducableCipherText = cipherText;
            reducablePlainText = plainText;

            keyMatrixNumbers = new int[3, 3];
            sections = plainText.Length / 3;
            messageVectorNumbers = new int[3, sections];
            shiftableMessageVector = new int[3, sections];
            cipherMatrixNumbers = new int[plainText.Length, sections];
            shiftableCipherVector = new int[plainText.Length, sections];


            keyMatrixNumbers = helper.GetDisplayedKeyMatrix(key);
            inverseKeyMatrixNumbers = helper.GetDisplayedInverseKeyMatrix(key, keyMatrixNumbers);
            messageVectorNumbers = helper.GetDisplayedMessageVector(keyMatrixNumbers, plainText);
            cipherMatrixNumbers = helper.GetDisplayedCipherMatrix(keyMatrixNumbers, messageVectorNumbers, sections, plainText);

            shiftableMessageVector = messageVectorNumbers;
            shiftableCipherVector = cipherMatrixNumbers;
        }

        private void DecryptionPage_Load(object sender, EventArgs e)
        {
            label25.Visible = false;
            label5.Visible = false;

            //View The Key Matrix
            labelA.Text = key[0].ToString(); labelB.Text = key[1].ToString(); labelC.Text = key[2].ToString();
            labelD.Text = key[3].ToString(); labelE.Text = key[4].ToString(); labelF.Text = key[5].ToString();
            labelG.Text = key[6].ToString(); labelH.Text = key[7].ToString(); labelI.Text = key[8].ToString();

            //View The Inverse Key Matrix
            labelInversea2.Text = inverseKey[0].ToString(); labelInverseb2.Text = inverseKey[1].ToString(); labelInversec2.Text = inverseKey[2].ToString();
            labelInversed2.Text = inverseKey[3].ToString(); labelInversee2.Text = inverseKey[4].ToString(); labelInversef2.Text = inverseKey[5].ToString();
            labelInverseg2.Text = inverseKey[6].ToString(); labelInverseh2.Text = inverseKey[7].ToString(); labelInversei2.Text = inverseKey[8].ToString();

            //View The Inverse Key Matrix
            labelInverseA.Text = inverseKey[0].ToString(); labelInverseB.Text = inverseKey[1].ToString(); labelInverseC.Text = inverseKey[2].ToString();
            labelInverseD.Text = inverseKey[3].ToString(); labelInverseE.Text = inverseKey[4].ToString(); labelInverseF.Text = inverseKey[5].ToString();
            labelInverseG.Text = inverseKey[6].ToString(); labelInverseH.Text = inverseKey[7].ToString(); labelInverseI.Text = inverseKey[8].ToString();

            //Sets Plain Text That Will Be Encrypted
            labelX.Text = cipherText[0].ToString();
            labelY.Text = cipherText[1].ToString();
            labelZ.Text = cipherText[2].ToString();

            //Sets The Formulas Used To Calculate The Ecryption
            labelFormula1.Text = inverseKey[0].ToString() + cipherText[0].ToString() + "+"
                + inverseKey[1].ToString() + cipherText[1].ToString() + "+"
                + inverseKey[2].ToString() + cipherText[2].ToString();
            labelFormula2.Text = inverseKey[3].ToString() + cipherText[0].ToString() + "+"
                + inverseKey[4].ToString() + cipherText[1].ToString() + "+"
                + inverseKey[5].ToString() + cipherText[2].ToString();
            labelFormula3.Text = inverseKey[6].ToString() + cipherText[0].ToString() + "+"
                + inverseKey[7].ToString() + cipherText[1].ToString() + "+"
                + inverseKey[8].ToString() + cipherText[2].ToString();

            //Shows The Output of The Formula AKA The Encrypted Text
            labelxOut.Text = reducablePlainText[0].ToString();
            labelyOut.Text = reducablePlainText[1].ToString();
            labelzOut.Text = reducablePlainText[2].ToString();

            //Shows The Complete Plain Text
            labelPlain.Text = plainText.ToString();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (!ShowMathButton.Checked)
            {
                //Reduces The String To Show The Next Calculation
                reducablePlainText = reducablePlainText.Remove(0, 3);
                reducableCipherText = reducableCipherText.Remove(0, 3);

                if (reducableCipherText.Length != 0)
                {
                    //Sets Plain Text That Will Be Encrypted
                    labelX.Text = reducableCipherText[0].ToString();
                    labelY.Text = reducableCipherText[1].ToString();
                    labelZ.Text = reducableCipherText[2].ToString();

                    //Sets The Formulas Used To Calculate The Ecryption
                    labelFormula1.Text = inverseKey[0].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[1].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[2].ToString() + reducableCipherText[2].ToString();
                    labelFormula2.Text = inverseKey[3].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[4].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[5].ToString() + reducableCipherText[2].ToString();
                    labelFormula3.Text = inverseKey[6].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[7].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[8].ToString() + reducableCipherText[2].ToString();

                    //Shows The Output of The Formula AKA The Encrypted Text
                    labelxOut.Text = reducablePlainText[0].ToString();
                    labelyOut.Text = reducablePlainText[1].ToString();
                    labelzOut.Text = reducablePlainText[2].ToString();
                }
                else //If All The Calculations Have Been Shown Restart From Beginning
                {
                    reducablePlainText = plainText;
                    reducableCipherText = cipherText;

                    //Sets Plain Text That Will Be Encrypted
                    labelX.Text = reducableCipherText[0].ToString();
                    labelY.Text = reducableCipherText[1].ToString();
                    labelZ.Text = reducableCipherText[2].ToString();

                    //Sets The Formulas Used To Calculate The Ecryption
                    labelFormula1.Text = inverseKey[0].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[1].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[2].ToString() + reducableCipherText[2].ToString();
                    labelFormula2.Text = inverseKey[3].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[4].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[5].ToString() + reducableCipherText[2].ToString();
                    labelFormula3.Text = inverseKey[6].ToString() + reducableCipherText[0].ToString() + "+"
                        + inverseKey[7].ToString() + reducableCipherText[1].ToString() + "+"
                        + inverseKey[8].ToString() + reducableCipherText[2].ToString();

                    //Shows The Output of The Formula AKA The Encrypted Text
                    labelxOut.Text = reducablePlainText[0].ToString();
                    labelyOut.Text = reducablePlainText[1].ToString();
                    labelzOut.Text = reducablePlainText[2].ToString();
                }
            }
            else
            {
                int[,] tempVector = new int[3, sections];
                int count = 1;

                for (int i = 0; i < sections; i++)
                {
                    for (int j = 0; j < 3; j++)
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

                //Sets Plain Text That Will Be Encrypted
                labelX.Text = shiftableCipherVector[0, 0].ToString();
                labelY.Text = shiftableCipherVector[1, 0].ToString();
                labelZ.Text = shiftableCipherVector[2, 0].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = "(" + inverseKeyMatrixNumbers[0, 0].ToString() + ")" + "(" + shiftableCipherVector[0, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[0, 1].ToString() + ")" + "(" + shiftableCipherVector[1, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[0, 2].ToString() + ")" + "(" + shiftableCipherVector[2, 0].ToString() + ")";
                labelFormula2.Text = "(" + inverseKeyMatrixNumbers[1, 0].ToString() + ")" + "(" + shiftableCipherVector[0, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[1, 1].ToString() + ")" + "(" + shiftableCipherVector[1, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[1, 2].ToString() + ")" + "(" + shiftableCipherVector[2, 0].ToString() + ")";
                labelFormula3.Text = "(" + inverseKeyMatrixNumbers[2, 0].ToString() + ")" + "(" + shiftableCipherVector[0, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[2, 1].ToString() + ")" + "(" + shiftableCipherVector[1, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[2, 2].ToString() + ")" + "(" + shiftableCipherVector[2, 0].ToString() + ")";

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = shiftableCipherVector[0, 0].ToString();
                labelyOut.Text = shiftableCipherVector[1, 0].ToString();
                labelzOut.Text = shiftableCipherVector[2, 0].ToString();
            }
        }

        private void ShowMathButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowMathButton.Checked)
            {
                label25.Visible = true;
                label5.Visible = true;

                label22.Visible = false;
                label26.Visible = false;

                //View The Key Matrix As Numbers
                labelA.Text = keyMatrixNumbers[0,0].ToString(); labelB.Text = keyMatrixNumbers[0,1].ToString(); labelC.Text = keyMatrixNumbers[0,2].ToString();
                labelD.Text = keyMatrixNumbers[1,0].ToString(); labelE.Text = keyMatrixNumbers[1,1].ToString(); labelF.Text = keyMatrixNumbers[1,2].ToString();
                labelG.Text = keyMatrixNumbers[2,0].ToString(); labelH.Text = keyMatrixNumbers[2,1].ToString(); labelI.Text = keyMatrixNumbers[2,2].ToString();

                //View The Inverse Key Matrix As Numbers
                labelInversea2.Text = inverseKeyMatrixNumbers[0,0].ToString(); labelInverseb2.Text = inverseKeyMatrixNumbers[0,1].ToString(); labelInversec2.Text = inverseKeyMatrixNumbers[0,2].ToString();
                labelInversed2.Text = inverseKeyMatrixNumbers[1,0].ToString(); labelInversee2.Text = inverseKeyMatrixNumbers[1,1].ToString(); labelInversef2.Text = inverseKeyMatrixNumbers[1,2].ToString();
                labelInverseg2.Text = inverseKeyMatrixNumbers[2,0].ToString(); labelInverseh2.Text = inverseKeyMatrixNumbers[2,1].ToString(); labelInversei2.Text = inverseKeyMatrixNumbers[2,2].ToString();

                //View The Inverse Key Matrix As Numbers
                labelInverseA.Text = inverseKeyMatrixNumbers[0, 0].ToString(); labelInverseB.Text = inverseKeyMatrixNumbers[0, 1].ToString(); labelInverseC.Text = inverseKeyMatrixNumbers[0, 2].ToString();
                labelInverseD.Text = inverseKeyMatrixNumbers[1, 0].ToString(); labelInverseE.Text = inverseKeyMatrixNumbers[1, 1].ToString(); labelInverseF.Text = inverseKeyMatrixNumbers[1, 2].ToString();
                labelInverseG.Text = inverseKeyMatrixNumbers[2, 0].ToString(); labelInverseH.Text = inverseKeyMatrixNumbers[2, 1].ToString(); labelInverseI.Text = inverseKeyMatrixNumbers[2, 2].ToString();
               
                //Sets Plain Text That Will Be Encrypted
                labelX.Text = cipherMatrixNumbers[0,0].ToString();
                labelY.Text = cipherMatrixNumbers[1,0].ToString();
                labelZ.Text = cipherMatrixNumbers[2,0].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = "("+inverseKeyMatrixNumbers[0,0].ToString() + ")" + "(" + cipherMatrixNumbers[0,0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[0,1].ToString() + ")"+"("+ cipherMatrixNumbers[1,0].ToString() +")"+ "+"
                    + "("+inverseKeyMatrixNumbers[0,2].ToString()+")"+"(" + cipherMatrixNumbers[2,0].ToString()+")";
                labelFormula2.Text = "(" + inverseKeyMatrixNumbers[1, 0].ToString() + ")" + "(" + cipherMatrixNumbers[0, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[1, 1].ToString() + ")" + "(" + cipherMatrixNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[1, 2].ToString() + ")" + "(" + cipherMatrixNumbers[2, 0].ToString()+")";
                labelFormula3.Text = "(" + inverseKeyMatrixNumbers[2, 0].ToString() + ")" + "(" + cipherMatrixNumbers[0, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[2, 1].ToString() + ")" + "(" + cipherMatrixNumbers[1, 0].ToString() + ")" + "+"
                    + "(" + inverseKeyMatrixNumbers[2, 2].ToString() + ")" + "(" + cipherMatrixNumbers[2, 0].ToString()+")";

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = messageVectorNumbers[0,0].ToString();
                labelyOut.Text = messageVectorNumbers[1,0].ToString();
                labelzOut.Text = messageVectorNumbers[2,0].ToString();
            }
            else
            {
                label25.Visible = false;
                label5.Visible = false;

                label22.Visible = true;
                label26.Visible = true;

                //View The Key Matrix
                labelA.Text = key[0].ToString(); labelB.Text = key[1].ToString(); labelC.Text = key[2].ToString();
                labelD.Text = key[3].ToString(); labelE.Text = key[4].ToString(); labelF.Text = key[5].ToString();
                labelG.Text = key[6].ToString(); labelH.Text = key[7].ToString(); labelI.Text = key[8].ToString();

                //View The Inverse Key Matrix
                labelInversea2.Text = inverseKey[0].ToString(); labelInverseb2.Text = inverseKey[1].ToString(); labelInversec2.Text = inverseKey[2].ToString();
                labelInversed2.Text = inverseKey[3].ToString(); labelInversee2.Text = inverseKey[4].ToString(); labelInversef2.Text = inverseKey[5].ToString();
                labelInverseg2.Text = inverseKey[6].ToString(); labelInverseh2.Text = inverseKey[7].ToString(); labelInversei2.Text = inverseKey[8].ToString();

                //View The Inverse Key Matrix
                labelInverseA.Text = inverseKey[0].ToString(); labelInverseB.Text = inverseKey[1].ToString(); labelInverseC.Text = inverseKey[2].ToString();
                labelInverseD.Text = inverseKey[3].ToString(); labelInverseE.Text = inverseKey[4].ToString(); labelInverseF.Text = inverseKey[5].ToString();
                labelInverseG.Text = inverseKey[6].ToString(); labelInverseH.Text = inverseKey[7].ToString(); labelInverseI.Text = inverseKey[8].ToString();

                //Sets Plain Text That Will Be Encrypted
                labelX.Text = cipherText[0].ToString();
                labelY.Text = cipherText[1].ToString();
                labelZ.Text = cipherText[2].ToString();

                //Sets The Formulas Used To Calculate The Ecryption
                labelFormula1.Text = inverseKey[0].ToString() + cipherText[0].ToString() + "+"
                    + inverseKey[1].ToString() + cipherText[1].ToString() + "+"
                    + inverseKey[2].ToString() + cipherText[2].ToString();
                labelFormula2.Text = inverseKey[3].ToString() + cipherText[0].ToString() + "+"
                    + inverseKey[4].ToString() + cipherText[1].ToString() + "+"
                    + inverseKey[5].ToString() + cipherText[2].ToString();
                labelFormula3.Text = inverseKey[6].ToString() + cipherText[0].ToString() + "+"
                    + inverseKey[7].ToString() + cipherText[1].ToString() + "+"
                    + inverseKey[8].ToString() + cipherText[2].ToString();

                //Shows The Output of The Formula AKA The Encrypted Text
                labelxOut.Text = reducablePlainText[0].ToString();
                labelyOut.Text = reducablePlainText[1].ToString();
                labelzOut.Text = reducablePlainText[2].ToString();
            }
        }
    }
}
