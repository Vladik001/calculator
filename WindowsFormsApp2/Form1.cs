using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form

    {
        private string currentExpression = "";
        private const int bw = 50, bh = 50;
        private const int dx = 10, dy = 10;


        Label label1 = new Label();


        private Button[] btn = new Button[15];

        char[] btnText = {'7','8','9','+',
                          '4','5','6','-',
                          '1','2','3','=',
                          '0',',','c'};

        int[] btnTag = {7,8,9,-3,
                        4,5,6,-4,
                        1,2,3,-2,
                        0,-1,-5};

        private double ac = 0;
        private int op = 0;  

        private Boolean fd; 
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.BackColor = Color.Transparent;
            this.WindowState = FormWindowState.Minimized;
        }
        private void ConfigureButtons(Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.Blue;
                button.Font = new Font(button.Font.FontFamily, 16);

                if (button.Tag.Equals(-3) || button.Tag.Equals(-4) || button.Tag.Equals(-2) || button.Tag.Equals(-1) || button.Tag.Equals(-5))
                {
                    button.BackColor = Color.Gold;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 2;
                    button.FlatAppearance.BorderColor = Color.Blue;
                }
            }
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        public Form1()
        {
            InitializeComponent();
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;


            this.StartPosition = FormStartPosition.Manual;
            fd = true;


            int x = (screenWidth - formWidth) / 2;
            int y = (screenHeight - formHeight) / 2;
            this.Location = new Point(x, y);

            this.ClientSize = new Size(4 * bw + 8 * dx, 5 * bh + 7 * dy);


            this.Controls.Add(label1);
            label1.TextAlign = ContentAlignment.BottomRight;
            label1.Font = new Font(label1.Font.FontFamily, 16);
            label1.AutoSize = false;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.BackColor = SystemColors.ButtonFace;
            label1.SetBounds(dx, dy, 4 * bw + 2 * dx, bh);
            label1.Text = "0";
            y = label1.Bottom + dy;


            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                x = dx;
                for (int j = 0; j < 4; j++)
                {
                    if (!((i == 3) && (j == 0)))
                    {

                        btn[k] = new Button();
                        btn[k].SetBounds(x, y, bw, bh);
                        btn[k].Tag = btnTag[k];
                        btn[k].Text = btnText[k].ToString();
                        btn[k].Font = new Font(btn[k].Font.FontFamily, 16);



                        this.btn[k].Click += new
                            System.EventHandler(this.Button_Click);

                        if (btnTag[k] < 0)
                        {
                            btn[k].BackColor = SystemColors.ControlLight;
                        }

                        this.Controls.Add(this.btn[k]);

                        x = x + bw + dx;
                        k++;
                    }
                    else
                    {

                        btn[k] = new Button();
                        btn[k].SetBounds(x, y, bw * 2 + dx, bh);
                        btn[k].Tag = btnTag[k];
                        btn[k].Text = btnText[k].ToString();
                        btn[k].Font = new Font(btn[k].Font.FontFamily, 16);

                        this.btn[k].Click += new
                            System.EventHandler(this.Button_Click);

                        this.Controls.Add(this.btn[k]);

                        x = x + 2 * bw + 2 * dx;
                        k++;
                        j++;
                    }
                }
                y = y + bh + dy;
            }
            ConfigureButtons(btn);
        }
        private void Button_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;

            if (Convert.ToInt32(btn.Tag) > 0)
            {
                if (fd)
                {
                    label1.Text = btn.Text;
                    fd = false;
                }
                else
                    label1.Text += btn.Text;
                return;
            }

            if (Convert.ToInt32(btn.Tag) == 0)
            {
                if (fd) label1.Text = btn.Text;
                if (label1.Text != "0")
                    label1.Text += btn.Text;
                return;
            }

            if (Convert.ToInt32(btn.Tag) == -1)
            {
                if (fd)
                {
                    label1.Text = "0,";
                    fd = false;
                }
                else
                    if (label1.Text.IndexOf(",") == -1)
                    label1.Text += btn.Text;
                return;
            }

            if (Convert.ToInt32(btn.Tag) == -5)
            {
                ac = 0;
                op = 0;
                label1.Text = "0";

                fd = true;
                return;
            }


            if (Convert.ToInt32(btn.Tag) < -1)
            {
                double n;
                n = Convert.ToDouble(label1.Text);

                if (ac != 0)
                {
                    switch (op)
                    {
                        case -3: ac += n; break;
                        case -4: ac -= n; break;
                        case -2: ac = n; break;
                    }
                    label1.Text = ac.ToString("N");
                }
                else
                {
                    ac = n;
                }
                op = Convert.ToInt32(btn.Tag);
                fd = true;
            }
        }
    }
}
