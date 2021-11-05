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
    public partial class HastaKayitveTakibi : Form
    {
        public HastaKayitveTakibi()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=HastaKayitTakip.accdb;");

        private void Form1_Load(object sender, EventArgs e)
        {
            DoktorlarıYukle();
            RandevularıYukle();
        }

        private void DoktorlarıYukle()
        {

            comboBox1.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From Doktorlar", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    comboBox1.Items.Add(oku["AdSoyad"].ToString());
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

        private void RandevularıYukle()
        {
            listView1.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From Randevu", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    OleDbCommand komut1 = new OleDbCommand("Select * From HastaKayit Where Tc='"+oku["HTC"].ToString()+"'", baglanti);
                    OleDbDataReader oku1 = komut1.ExecuteReader();
                    oku1.Read();

                    OleDbCommand komut2 = new OleDbCommand("Select * From Doktorlar Where DID=" + Convert.ToInt32(oku["DID"].ToString()), baglanti);
                    OleDbDataReader oku2 = komut2.ExecuteReader();
                    oku2.Read();

                    OleDbCommand komut3 = new OleDbCommand("Select * From Bolumler Where BolumID=" + Convert.ToInt32(oku2["BolumID"].ToString()), baglanti);
                    OleDbDataReader oku3 = komut3.ExecuteReader();
                    oku3.Read();

                    ListViewItem hasta = new ListViewItem(new string[] 
                    {
                    oku["IslemNo"].ToString(),
                    oku["HTC"].ToString(),
                    oku1["AdSoyad"].ToString(),
                    oku2["AdSoyad"].ToString(),
                    oku3["BolumAdi"].ToString(),
                    oku["Tarih"].ToString()
                    }
                        );
                    listView1.Items.Add(hasta);
                    oku1.Close();
                    oku2.Close();
                    oku3.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Insert Into Randevu (HTC,DID,Tarih) Values('"+textBox1.Text+"',"+(comboBox1.SelectedIndex+1)+",'"+dateTimePicker1.Text+"')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Randevu Verildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RandevularıYukle();
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HastaEkle frm = new HastaEkle();
            frm.Guncelle = false;
            frm.Text = "Hasta Ekle";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (baglanti.State != ConnectionState.Open)
                        baglanti.Open();

                    OleDbCommand komut = new OleDbCommand("Insert Into HastaKayit (Tc,AdSoyad,DogumTarihi,DogumYeri,Cinsiyet,Adres,Tel,SosyalGuvenlik) Values('" + frm.Tc + "','" + frm.AdSoyad + "','" + frm.DogumTarihi + "','" + frm.DogumYeri + "','" + frm.Cinsiyet + "','" + frm.Adres + "','" + frm.Telefon + "','" + frm.SGuvenlik + "')", baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DoktorEkle frm = new DoktorEkle();
            frm.Text = "Doktor Ekle";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (baglanti.State != ConnectionState.Open)
                        baglanti.Open();

                    OleDbCommand komut = new OleDbCommand("Insert Into Doktorlar (Tc,AdSoyad,DogumTarihi,DogumYeri,Cinsiyet,Adres,Tel,BolumID) Values('" + frm.Tc + "','" + frm.AdSoyad + "','" + frm.DogumTarihi + "','" + frm.DogumYeri + "','" + frm.Cinsiyet + "','" + frm.Adres + "','" + frm.Telefon + "'," + (frm.Bolum+1)+ ")", baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Doktor Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DoktorlarıYukle();
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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            RandevularıYukle();
            DoktorlarıYukle();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            HastaListele frm = new HastaListele();
            frm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DoktorListele frm = new DoktorListele();
            frm.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Bolumler frm = new Bolumler();
            frm.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
