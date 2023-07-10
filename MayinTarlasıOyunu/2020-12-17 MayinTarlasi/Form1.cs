using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_12_17_MayinTarlasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int seviye;
        private void Seviye_Click(object sender, EventArgs e)
        {
            //önceki kontrolleri temizle
            grpTarla.Controls.Clear();
            //foreach (Control item in grpTarla.Controls)
            //{
            //    grpTarla.Controls.Remove(item);
            //}

            //var asdf = sender.GetType().ToString();
            //bir nesnenin çalışma anında tipini öğrenmek için kullanılır.

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            switch (tsmi.Text)
            {
                case "Kolay":
                    seviye = 1;
                    break;
                case "Orta":
                    seviye = 2;
                    break;
                case "Zor":
                    seviye = 3;
                    break;
            }

            Random rnd = new Random();
            int[,] mayinKoordinatlari = new int[10 * seviye, 2];
            //çok boyutlu diziler
            //aşağıdaki örnekte gördüğünüz int[3,2] için 3 toplam küme sayısı, 2 ise kümelerin içinde barındıracağı eleman sayısı
            //int[,] asdf2 = new int[3, 2]
            //int[,] x = new int[10, 2]
            //int[,] b = { { 3, 5 }, { 1, 1 }, { 3, 4 } };
            //b[0, 0] = 3
            //b[0, 1] = 5
            //b[1, 0] = 1
            //b[1, 1] = 1
            //b[2, 0] = 3
            //b[2, 1] = 4

            for (int i = 0; i < (10 * seviye); i++)
            {
                mayinKoordinatlari[i, 0] = rnd.Next(0, seviye * 10);
                mayinKoordinatlari[i, 1] = rnd.Next(0, seviye * 10);
            }

            int olcu = 20;
            for (int y = 0; y < seviye * 10; y++)
            {
                for (int x = 0; x < seviye * 10; x++)
                {
                    Button kutu = new Button();
                    kutu.Height = olcu;
                    kutu.Width = olcu;
                    kutu.Top = y * olcu + 20;
                    kutu.Left = x * olcu;
                    kutu.BackColor = Color.White;
                    kutu.Click += Kutu_Click;

                    kutu.Tag = false;
                    for (int i = 0; i < 10 * seviye; i++)
                    {
                        if (mayinKoordinatlari[i, 0] == x && mayinKoordinatlari[i, 1] == y)
                        {
                            kutu.Tag = true;
                            break;
                        }
                    }

                    grpTarla.Controls.Add(kutu);
                }
            }

            //groupbox'ın boyutlarını içindeki butonlara göre ayarlayalım
            grpTarla.Width = seviye * 10 * olcu;
            grpTarla.Height = seviye * 10 * olcu + 20;

            //formun boyutlarını içindeki groupboxın konumuna göre ayarlayalım
            this.Width = grpTarla.Width;
            this.Height = grpTarla.Height + grpTarla.Top;

            grpTarla.Visible = true;

            lblPuan.Text = "0";
        }

        private void Kutu_Click(object sender, EventArgs e)
        {
            Button tiklananKutu = sender as Button;
            if (Convert.ToBoolean(tiklananKutu.Tag) == true) //mayın var
            {
                foreach (Control item in grpTarla.Controls) //diğer mayınları da ortaya çıkaralım
                {
                    if (Convert.ToBoolean(item.Tag) == true)
                    {
                        item.BackColor = Color.Red;
                    }
                }

                MessageBox.Show("Yandınız! Tekrar dene....");

                grpTarla.Visible = false;
                lblPuan.Text = "0";
            }
            else // mayın yok
            {
                tiklananKutu.Enabled = false;
                tiklananKutu.BackColor = Color.Blue;
                lblPuan.Text = (Convert.ToInt32(lblPuan.Text) + 1).ToString();
            }
        }

        private void Cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            grpTarla.Visible = false;
        }

        private void grpTarla_Enter(object sender, EventArgs e)
        {

        }
    }
}
