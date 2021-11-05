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
    public partial class DoktorListele : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=HastaKayitTakip.accdb;");

        public DoktorListele()
        {
            InitializeComponent();
        }

        private void doktorlistele_Load(object sender, EventArgs e)
        {
            DoktorlariListele();
        }

        private void DoktorlariListele()
        {
            listView1.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From Doktorlar", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    OleDbCommand komut1 = new OleDbCommand("Select * From Bolumler Where BolumID=" + Convert.ToInt32(oku["BolumID"].ToString()), baglanti);
                    OleDbDataReader oku1 = komut1.ExecuteReader();
                    oku1.Read();

                    ListViewItem doktor = new ListViewItem(new string[] 
                    {
                    oku["DID"].ToString(),
                    oku["Tc"].ToString(),
                    oku["AdSoyad"].ToString(),
                    oku["DogumTarihi"].ToString(),
                    oku["DogumYeri"].ToString(),
                    oku["Cinsiyet"].ToString(),
                    oku["Adres"].ToString(),
                    oku["Tel"].ToString(),
                    oku["BolumID"].ToString(),
                    oku1["BolumAdi"].ToString(),
                    }
                        );
                    listView1.Items.Add(doktor);
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
            DoktorEkle frm = new DoktorEkle();
            frm.Tc = item.SubItems[1].Text;
            frm.AdSoyad = item.SubItems[2].Text;
            frm.DogumTarihi = item.SubItems[3].Text;
            frm.DogumYeri = item.SubItems[4].Text;
            frm.Cinsiyet = item.SubItems[5].Text;
            frm.Adres = item.SubItems[6].Text;
            frm.Telefon = item.SubItems[7].Text;
            frm.Bolum = Convert.ToInt32(item.SubItems[8].Text)-1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (baglanti.State != ConnectionState.Open)
                        baglanti.Open();

                    OleDbCommand komut = new OleDbCommand("Update Doktorlar Set Tc='" + frm.Tc + "',AdSoyad='" + frm.AdSoyad + "',DogumTarihi='" + frm.DogumTarihi + "',DogumYeri='" + frm.DogumYeri + "',Cinsiyet='" + frm.Cinsiyet + "',Adres='" + frm.Adres + "',Tel='" + frm.Telefon + "',BolumID=" + (frm.Bolum+1) + " Where DID="+Convert.ToInt32(item.SubItems[0].Text), baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Doktor Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DoktorlariListele();
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

                OleDbCommand komut2 = new OleDbCommand("Delete From Doktorlar Where DID=" +Convert.ToInt32(item.SubItems[0].Text), baglanti);
                komut2.ExecuteNonQuery();
                MessageBox.Show("Doktor Kaydı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DoktorlariListele();
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
