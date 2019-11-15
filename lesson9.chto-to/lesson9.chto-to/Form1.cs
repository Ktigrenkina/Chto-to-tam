using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace lesson9.chto_to
{
    public partial class Form1 : Form
    {
        TextBox tb;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int IndexOfTb = 0, x = 16, y = 17;
            StreamReader sr = new StreamReader("koote.txt");
            List<string> AllNambers = new List<string>();
            while(!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(' ');
                for (int i = 0; i < data.Length; i++)
                    AllNambers.Add(data[i]);
            }
            for (int i = 0; i < 9; i++)
            {
                x = 16;
                for(int j = 0; j < 9; j++)
                {
                    if (AllNambers[IndexOfTb] == "*")
                    {
                        tb = new TextBox()
                        {
                            Name = "textBox" + IndexOfTb,
                            Size = new Size(27, 27),
                            Location = new Point(x, y),
                            TextAlign = HorizontalAlignment.Center,
                            Multiline = true,
                            BorderStyle = BorderStyle.None,
                            Font = new Font("Microsoft Sans Serif", 16),
                            Text = "0",
                            ForeColor = Color.Red
                        };
                    }
                    else
                    {
                        tb = new TextBox()
                        {
                            Name = "textBox" + IndexOfTb,
                            Size = new Size(27, 27),
                            Location = new Point(x, y),
                            TextAlign = HorizontalAlignment.Center,
                            Multiline = true,
                            BorderStyle = BorderStyle.None,
                            Font = new Font("Microsoft Sans Serif", 16),
                            Text = AllNambers[IndexOfTb],
                            Enabled = false
                        };
                    }
                    tb.TextChanged += tb_TextChanged;
                    tb.KeyPress += tb_KeyPress;
                    Controls.Add(tb);
                    IndexOfTb++;
                    x += 38;
                }
                y += 38;
            }
            pictureBox1.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> Vsechisla = new List<int>();
            try
            {
                for (int i = 0; i < 81; i++)
                {
                    Vsechisla.Add(Convert.ToInt32(Controls["textBox" + i.ToString()].Text));
                }
                int leftID = 0, rightID = 9, StopFor = 0;
                for (int i = 0; i < 9; i++) 
                {
                    if (StopFor != 0) break;
                    for (int j = leftID; j < rightID-1; j++)
                    {
                        if (StopFor != 0) break;
                        for (int n = j + 1; n < rightID; n++) 
                        {
                            if (Vsechisla[j] == Vsechisla[n])
                            {
                                StopFor++;
                                MessageBox.Show("В " + (i+1) + "строке присутствует ошибка","Ошибка заполнена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    leftID += 9; rightID += 9;
                }
                leftID = 0; rightID = 72; StopFor = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (StopFor != 0) break;
                    for (int j = leftID; j < rightID; j += 9)
                    {
                        if (StopFor != 0) break;
                        for (int n = j + 9; n <= rightID; n += 9) 
                        {
                            if (Vsechisla[j] == Vsechisla[n])
                            {
                                MessageBox.Show("В " + (i + 1) + "столбце присутствует ошибка", "Ошибка заполнена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                StopFor++;  break;
                            }
                        }
                    }
                    leftID++;rightID++;
                }
                leftID = 0; rightID = 21; StopFor = 0;
                int shag = 1;
                int[] kvadrati = new int[9]; 
                for(int i = 1; i < 10; i++)
                {
                    for (int j = leftID; j < rightID; j++)
                    {
                        int IndexOfkvadrati = 0;
                        for (int n = j + leftID; j < rightID; j++)
                        {
                            kvadrati[IndexOfkvadrati] = Vsechisla[j];
                            IndexOfkvadrati++;
                            if (shag % 3 == 0)
                            {
                                j += 6;
                            }
                            shag++;
                        }
                    }
                    for(int k = 0; k < 8; k++)
                    {
                        if (StopFor != 0) break;
                        for (int n = k + 1; n < 8; n++) 
                        {
                            if (Vsechisla[k] == Vsechisla[n])
                            {
                                StopFor++;
                                MessageBox.Show("В " + i.ToString() + "квадрате присутствует ошибка","Ошибка заполнена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                    
                        }
                    }
                    if (StopFor != 0) break;
                    if( i % 3 == 0)
                    {
                        leftID += 21; rightID += 21;
                    }
                    else
                    {
                        leftID += 3; rightID += 3;
                    }
                    leftID += 3; rightID += 3;
                    
                }

            }
            catch(Exception ix)
            {
                MessageBox.Show(ix.ToString());
                MessageBox.Show("Вероятно не все ячейки заполнены", "Произошла ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            tb = sender as TextBox;
            if(tb.Text.Length > 1)
            {
                tb.Text = tb.Text.Substring(0, 1);
            }
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 48 || e.KeyChar > 57 ) && e.KeyChar !=8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
