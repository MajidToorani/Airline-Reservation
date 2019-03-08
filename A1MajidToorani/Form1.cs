using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//--------------------------
//Majid Tooranisama
//#7725070
//May 30 2018
//Assn1
//--------------------------

namespace A1MajidToorani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Button[,] seatArray = new Button[5, 3];            // Declare arrays and other variables
        bool[,] seatBool = new bool[5, 3];
        string[,] nameArray = new string[5, 3];
        string[] row = new string[] { "A", "B", "C", "D", "E" };
        string[] column = new string[] { "0", "1", "2" };
        string[] waitingList = new string[10];
        int i = 0;
        int j = 0;
        int k = 0;
        private void Form1_Load(object sender, EventArgs e) // Initialize button array and boolean array
        {
            seatArray[0, 0] = buttonA0;
            seatArray[0, 1] = buttonA1;
            seatArray[0, 2] = buttonA2;
            seatArray[1, 0] = buttonB0;
            seatArray[1, 1] = buttonB1;
            seatArray[1, 2] = buttonB2;
            seatArray[2, 0] = buttonC0;
            seatArray[2, 1] = buttonC1;
            seatArray[2, 2] = buttonC2;
            seatArray[3, 0] = buttonD0;
            seatArray[3, 1] = buttonD1;
            seatArray[3, 2] = buttonD2;
            seatArray[4, 0] = buttonE0;
            seatArray[4, 1] = buttonE1;
            seatArray[4, 2] = buttonE2;
            for (int i = 0; i <= seatArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= seatArray.GetUpperBound(1); j++)
                {
                    if (seatArray[i, j].BackColor == Color.Red)
                    {
                        seatBool[i, j] = true;
                    }
                    else
                    {
                        seatBool[i, j] = false;
                    }
                }
            }
        }
        private void buttonBook_Click(object sender, EventArgs e)              //Click "Book" button and its process 
        {
            int countEmpty = 0;
            int countFull = 0;
            if (textBoxName.Text == "" || textBoxName.Text == "Please Enter your name:")
            {
                textBoxName.Text = "Please Enter your name:";
                MessageBox.Show(" Hi, please enter your name:");
            }
            else if (listBoxRow.Text == "" || listBoxColumn.Text == "")
            {
                MessageBox.Show(textBoxName.Text+", please choose a seat for booking!");
            }
            else
            {
                for (int i = 0; i < row.Length; i++)
                {
                    for (int j = 0; j < column.Length; j++)
                    {
                        if (listBoxRow.Text == row[i] && listBoxColumn.Text == column[j] && !seatBool[i, j])
                        {
                            nameArray[i, j] = textBoxName.Text;
                            seatArray[i, j].BackColor = Color.Red;
                            seatBool[i, j] = true;
                            MessageBox.Show(textBoxName.Text+", your seat has been reserved successfully!");
                        }
                        else if (listBoxRow.Text == row[i] && listBoxColumn.Text == column[j] && seatBool[i, j])
                        {
                            for (i = 0; i < row.Length; i++)
                            {
                                for (j = 0; j < column.Length; j++)
                                {
                                    if (!seatBool[i, j])
                                    {
                                        countEmpty++;
                                    }
                                    else
                                    {
                                        countFull++;
                                    }
                                }
                            }
                            if (countEmpty != 0)
                            {
                                MessageBox.Show(textBoxName.Text + ", this seat is reserved, there are " + countEmpty + " seats available!");
                            }
                            else
                            {
                                MessageBox.Show(textBoxName.Text+" ,all seats are reserved, you can add your name to waiting list.");
                            }
                        }
                    }
                }
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)      //Click "Cancel" button and its process
        {
            if (listBoxRow.Text == "" || listBoxColumn.Text == "")
            {
                MessageBox.Show("Please choose a seat to cancel!");
            }
            else
            {
                for (int i = 0; i < row.Length; i++)
                {
                    for (int j = 0; j < column.Length; j++)
                    {
                        if (seatBool[i, j] && listBoxRow.Text == row[i] && listBoxColumn.Text == column[j])
                        {
                            nameArray[i, j] = null;
                            seatArray[i, j].BackColor = Color.Gainsboro;
                            seatBool[i, j] = false;
                            MessageBox.Show("This seat is canceled, now it is available!");
                            if (waitingList[0] != null)
                            {
                                nameArray[i, j] = waitingList[0];
                                MessageBox.Show(" Hi " + nameArray[i, j] + ", Your seat has been reserved !");
                                for (int k = 0; k < 9; k++)
                                {
                                    waitingList[k] = waitingList[k + 1];
                                }
                                waitingList[9] = null;
                                k--;
                                seatArray[i, j].BackColor = Color.Red;
                                seatBool[i, j] = true;
                            }
                        }
                        else if (!seatBool[i, j] && listBoxRow.Text == row[i] && listBoxColumn.Text == column[j])
                        {
                            MessageBox.Show("This seat is not reserved !");
                        }
                    }
                }
            }
        }
        private void buttonAddWaitingList_Click(object sender, EventArgs e)   //Click "Add to Waiting List" button and its process
        {
            int countEmpty = 0;
            int countFull = 0;
            if (textBoxName.Text == "" || textBoxName.Text == "Please Enter your name:")
            {
                MessageBox.Show(" Hi, please enter your name:");
            }
            else
            {
                for (int i = 0; i < row.Length; i++)
                {
                    for (int j = 0; j < column.Length; j++)
                    {
                        if (!seatBool[i, j])
                        {
                            countEmpty++;
                        }
                        else
                        {
                            countFull++;
                        }
                    }
                }
                if (k < 10 && countEmpty == 0)
                {
                    waitingList[k] = textBoxName.Text;
                    MessageBox.Show(" Hi " + textBoxName.Text + ", Your name has been added to waiting list successfully!");
                    ++k;
                }
                else if (k == 10)
                {
                    MessageBox.Show("Waiting list is full");
                }
                else
                {
                    MessageBox.Show(" Hi " + textBoxName.Text + ", there are " + countEmpty + " seats available!");
                }
            }
        }
        private void buttonShowWaitingList_Click(object sender, EventArgs e)    //Click "Show Waiting List" button and its process 
        {
            if (richTextBoxShowWaiting.Text == "")
            {
                for (int k = 0; k < waitingList.Length; k++)
                {
                    richTextBoxShowWaiting.Text += waitingList[k]+"\n";
                }
            }
            else if (richTextBoxShowWaiting.Text != "")
            {
                richTextBoxShowWaiting.Text = "";
            }
        }
        private void textBoxName_MouseClick(object sender, MouseEventArgs e)    //Click "Text box name" by mouse and clear the content
        {
            textBoxName.Text = "";
        }
        private void buttonShowAll_Click(object sender, EventArgs e)           //Click "Show All" button and its process 
        {
            if (richTextBoxShowAll.Text == "")
            {
                for (i = 0; i <= seatArray.GetUpperBound(0); i++)
                {
                    for (j = 0; j <= seatArray.GetUpperBound(1); j++)
                    {
                        richTextBoxShowAll.Text += "Seat[" + i + "," + j + "]= " + nameArray[i, j]+"\n";
                    }
                }
            }
            else
            {
                richTextBoxShowAll.Text = "";
            }
        }
        private void buttonFillAll_Click(object sender, EventArgs e)         //Click "Fill All" button and its process 
        {
            for (int i = 0; i < row.Length; i++)
            {
                for (int j = 0; j < column.Length; j++)
                {
                    nameArray[i, j] = "Reserved";
                    seatArray[i, j].BackColor = Color.Red;
                    seatBool[i, j] = true;
                }
            }
        }
        private void buttonStatus_Click(object sender, EventArgs e)        //Click "Status" button and its process 
        {
            for (int i = 0; i < row.Length; i++)
            {
                for (int j = 0; j < column.Length; j++)
                {
                    if (seatBool[i, j] && listBoxRow.Text == row[i] && listBoxColumn.Text == column[j])
                    {
                        textBoxStatus.Text = "Not available";
                    }
                    else if (!seatBool[i, j] && listBoxRow.Text == row[i] && listBoxColumn.Text == column[j])
                    {
                        textBoxStatus.Text = "Available";
                    }
                }
            }
        }
        private void buttonA0_Click(object sender, EventArgs e)    //Click "Button A0 to E2" and conjuct 
        {                                                          //to "List Box Row and Column" and "Textbox Status"
            textBoxStatus.Text = ""; 
            listBoxRow.SelectedItem = "A";
            listBoxColumn.SelectedItem = "0";
        }
        private void buttonA1_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "A";
            listBoxColumn.SelectedItem = "1";
        }
        private void buttonA2_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "A";
            listBoxColumn.SelectedItem = "2";
        }
        private void buttonB0_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "B";
            listBoxColumn.SelectedItem = "0";
        }
        private void buttonB1_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "B";
            listBoxColumn.SelectedItem = "1";
        }
        private void buttonB2_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "B";
            listBoxColumn.SelectedItem = "2";
        }
        private void buttonC0_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "C";
            listBoxColumn.SelectedItem = "0";
        }
        private void buttonC1_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "C";
            listBoxColumn.SelectedItem = "1";
        }
        private void buttonC2_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "C";
            listBoxColumn.SelectedItem = "2";
        }
        private void buttonD0_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "D";
            listBoxColumn.SelectedItem = "0";
        }
        private void buttonD1_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "D";
            listBoxColumn.SelectedItem = "1";
        }
        private void buttonD2_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "D";
            listBoxColumn.SelectedItem = "2";
        }
        private void buttonE0_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "E";
            listBoxColumn.SelectedItem = "0";
        }
        private void buttonE1_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "E";
            listBoxColumn.SelectedItem = "1";
        }
        private void buttonE2_Click(object sender, EventArgs e)
        {
            textBoxStatus.Text = "";
            listBoxRow.SelectedItem = "E";
            listBoxColumn.SelectedItem = "2";
        }
    }
}

        
