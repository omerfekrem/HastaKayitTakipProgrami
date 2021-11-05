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
    public partial class HastaListele : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=HastaKayitTakip.accdb;");

        public HastaListele()
        {
            InitializeComponent();
        }

        private void hastalistele_Load(object sender, EventArgs e)
        {
            ListeleHasta();
        }

        private void ListeleHasta()
        {
            listView1.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From HastaKayit", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem hasta = new ListViewItem(new string[] 
                    {
                    oku["Tc"].ToString(),
                    oku["AdSoyad"].ToString(),
                    oku["DogumTarihi"].ToString(),
                    oku["DogumYeri"].ToString(),
                    oku["Cinsiyet"].ToString(),
                    oku["Adres"].ToString(),
                    oku["Tel"].ToString(),
                    oku["SosyalGuvenlik"].ToString()
                    });
                    listView1.Items.Add(hasta);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];
            HastaEkle frm = new HastaEkle();
            frm.Guncelle = true;
            frm.Tc = item.SubItems[0].Text;
            frm.AdSoyad = item.SubItems[1].Text;
            frm.DogumTarihi = item.SubItems[2].Text;
            frm.DogumYeri = item.SubItems[3].Text;
            frm.Cinsiyet = item.SubItems[4].Text;
            frm.Adres = item.SubItems[5].Text;
            frm.Telefon = item.SubItems[6].Text;
            frm.SGuvenlik = item.SubItems[7].Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (baglanti.State != ConnectionState.Open)
                        baglanti.Open();

                    OleDbCommand komut = new OleDbCommand("Update HastaKayit Set AdSoyad='" + frm.AdSoyad + "',DogumTarihi='" + frm.DogumTarihi + "',DogumYeri='" + frm.DogumYeri + "',Cinsiyet='" + frm.Cinsiyet + "',Adres='" + frm.Adres + "',Tel='" + frm.Telefon + "',SosyalGuvenlik='" + frm.SGuvenlik + "' Where Tc='" + frm.Tc + "'", baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HastaListele();
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
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut2 = new OleDbCommand("Delete From HastaKayit Where Tc='" + item.SubItems[0].Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                MessageBox.Show("Hasta Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HastaListele();
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
