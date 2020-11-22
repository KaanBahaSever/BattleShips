using System;
using System.Drawing;
using System.Windows.Forms;

namespace Amiral_Battı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] ShipCountANDLength = new int[] { 4, 4, 3, 3, 2 };

        AI Computer;
        MyField MyField = new MyField(10, 10);
        EnemyField EnemyField = new EnemyField(10, 10);
        Button[] MyShip;
        Byte Stage = 0;
        bool Vertical = false;
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool Trouble = false;
            Vertical = false;

            if (Stage < ShipCountANDLength.Length)
            {
                if (!MyField.IsItSuitable(MyShip))
                {
                    ButtonSupport.ResetShip(MyShip, this);
                    Trouble = true;
                    Refresh();
                    MessageBox.Show("Bir sorun var");
                }
            }

            if (!Trouble)
            {
                Stage++;

                if (Stage < ShipCountANDLength.Length)
                {
                    MyField.PositionTOsea(MyShip);
                    MyShip = ButtonSupport.CreateShip(ShipCountANDLength[Stage], this);
                }
                else if (Stage == ShipCountANDLength.Length)
                {
                    button1.Text = "OYUNU BAŞLAT";
                    button3.Enabled = false;
                }
                else if (Stage == (ShipCountANDLength.Length + 1))
                {
                    button1.Enabled = false;
                    EnemyField.PickShips();
                    ButtonSupport.RemoveAllShips(this);
                    ButtonSupport.CreateANewButton(10, 10, 10, 20, this);
                    Refresh();
                    
                }
            }
            Computer = new AI(MyField);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int x = MyShip[0].Location.X;
            int y = MyShip[0].Location.Y;

            if (Vertical)
            {
                for (int i = 1; i < MyShip.Length; i++)
                {
                    MyShip[i].Location = new Point(x + 50 * i, y);
                }
                Vertical = false;
            }
            else
            {
                for (int i = 1; i < MyShip.Length; i++)
                {
                    MyShip[i].Location = new Point(x, y + 50 * i);
                }
                Vertical = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MyShip = ButtonSupport.CreateShip(ShipCountANDLength[Stage], this);
            int y = 20;
            int x = 640;
            Button[,] Enemy = new Button[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Enemy[i, j] = new Button();
                    Enemy[i, j].Size = new Size(50, 50);
                    Enemy[i, j].Location = new Point(x, y);
                    Enemy[i, j].Name = i.ToString();
                    Enemy[i, j].Click += new System.EventHandler(this.ClickEnemyShips);
                    Enemy[i, j].TabStop = false;
                    x += 50;
                    this.Controls.Add(Enemy[i, j]);
                }

                x = 640;
                y += 50;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Stage < (ShipCountANDLength.Length + 1))
            {
                GridLine(e.Graphics);
            }
        }
        public void GridLine(Graphics G)
        {
            int x = 10;
            int y = 20;
            Pen Pen = new Pen(Color.Black, 1);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    G.DrawRectangle(Pen, new Rectangle(x, y, 50, 50));
                    x += 50;
                }
                x = 10;
                y += 50;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Stage < ShipCountANDLength.Length)
            {
                if (keyData == Keys.Down)
                {
                    for (int i = 0; i < MyShip.Length; i++)
                    {
                        MyShip[i].Top += 50;
                    }
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    for (int i = 0; i < MyShip.Length; i++)
                    {
                        MyShip[i].Top -= 50;
                    }
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    for (int i = 0; i < MyShip.Length; i++)
                    {
                        MyShip[i].Left += 50;
                    }
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    for (int i = 0; i < MyShip.Length; i++)
                    {
                        MyShip[i].Left -= 50;
                    }
                    return true;
                }
            }
            return true;
        }
        
        public void ClickEnemyShips(object sender, EventArgs e)
        {
            Button Clicked = (Button)sender;

            Clicked.Enabled = false;
            (int i, int j) = EnemyField.FindIndex(Clicked.Location.X, Clicked.Location.Y);
            if (EnemyField.IsItTrue(i, j))
            {
                MessageBox.Show("Vurdun");
            }





        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
