using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button[,] butonlar = new Button[3, 3];
        private bool oyunBitti = false;
        private Random rastgele = new Random();

        public Form1()
        {
            BuSistemiKur();
        }

        private void BuSistemiKur()
        {
            this.Text = "Bilgisayara Karşı XOX Oyunu";
            this.Size = new Size(420, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 30); 

            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(110, 110);
                    btn.Location = new Point(30 + j * 115, 30 + i * 115);
                    btn.Font = new Font("Segoe UI", 36, FontStyle.Bold);
                    btn.BackColor = Color.FromArgb(50, 50, 50);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.Gray;

                    btn.Tag = new Point(i, j);
                    btn.Click += Buton_Click;

                    butonlar[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        private void Buton_Click(object sender, EventArgs e)
        {
            if (oyunBitti) return;

            Button tiklanan = (Button)sender;

            if (tiklanan.Text == "")
            {
                tiklanan.Text = "O";
                tiklanan.ForeColor = Color.LightGreen;

                if (KontrolEt("O"))
                {
                    MessageBox.Show("Tebrikler! Bilgisayarı yendin!");
                    oyunBitti = true;
                    OynuSifirla();
                    return;
                }

                if (BeraberlikKontrol())
                {
                    MessageBox.Show("Berabere bitti!");
                    oyunBitti = true;
                    OynuSifirla();
                    return;
                }

                BilgisayarHamlesi();
            }
        }

        private void BilgisayarHamlesi()
        {
            if (oyunBitti) return;

            int hamleSayisi = 0;
            while (hamleSayisi < 100)
            {
                int satir = rastgele.Next(0, 3);
                int sutun = rastgele.Next(0, 3);

                if (butonlar[satir, sutun].Text == "")
                {
                    butonlar[satir, sutun].Text = "X";
                    butonlar[satir, sutun].ForeColor = Color.Coral;

                    if (KontrolEt("X"))
                    {
                        MessageBox.Show("Bilgisayar kazandı! Tekrar dene.");
                        oyunBitti = true;
                        OynuSifirla();
                    }
                    return;
                }
                hamleSayisi++;
            }
        }

        private bool KontrolEt(string harf)
        {
            for (int i = 0; i < 3; i++)
            {
                if (butonlar[i, 0].Text == harf && butonlar[i, 1].Text == harf && butonlar[i, 2].Text == harf) return true;
                if (butonlar[0, i].Text == harf && butonlar[1, i].Text == harf && butonlar[2, i].Text == harf) return true;
            }

            if (butonlar[0, 0].Text == harf && butonlar[1, 1].Text == harf && butonlar[2, 2].Text == harf) return true;
            if (butonlar[0, 2].Text == harf && butonlar[1, 1].Text == harf && butonlar[2, 0].Text == harf) return true;

            return false;
        }

        private bool BeraberlikKontrol()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (butonlar[i, j].Text == "") return false;
            return true;
        }

        private void OynuSifirla()
        {
            foreach (Button btn in butonlar)
            {
                btn.Text = "";
                btn.BackColor = Color.FromArgb(50, 50, 50);
            }
            oyunBitti = false;
        }
    }
}