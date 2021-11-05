using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hasta_Kayit_Takip_Programi
{
    public partial class HastaEkle : Form
    {
        public HastaEkle()
        {
            InitializeComponent();
        }

        public bool Guncelle
        {
            set { textBox1.Enabled = !value; }
        }
        public string Tc
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string AdSoyad
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string DogumTarihi
        {
            get { return dateTimePicker1.Text; }
            set { dateTimePicker1.Text = value; }
        }
        public string DogumYeri
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string Cinsiyet
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }
        public string Adres
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public string Telefon
        {
            get { return textBox5.Text; }
            set { textBox5.Text = value; }
        }
        public string SGuvenlik
        {
            get { return comboBox2.Text; }
            set { comboBox2.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
