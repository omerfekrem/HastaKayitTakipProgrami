using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Hasta_Kayit_Takip_Programi
{
    public partial class DoktorEkle : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=HastaKayitTakip.accdb;");

        public DoktorEkle()
        {
            InitializeComponent();
            BolumleriYukle();
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
        public int Bolum
        {
            get { return comboBox2.SelectedIndex; }
            set { comboBox2.SelectedIndex = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void doktorekle_Load(object sender, EventArgs e)
        {
            BolumleriYukle();
        }
        private void BolumleriYukle()
        {

            comboBox2.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From Bolumler", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    comboBox2.Items.Add(oku["BolumAdi"].ToString());
                }
                oku.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }
    }
}
