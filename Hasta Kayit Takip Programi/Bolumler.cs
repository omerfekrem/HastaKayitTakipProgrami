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
    public partial class Bolumler : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=HastaKayitTakip.accdb;");

        public Bolumler()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut2 = new OleDbCommand("Delete From Bolumler Where BolumID=" + Convert.ToInt32(item.SubItems[0].Text), baglanti);
                komut2.ExecuteNonQuery();
                MessageBox.Show("Bölüm Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BolumleriYukle();
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

        private void bolumler_Load(object sender, EventArgs e)
        {
            BolumleriYukle();
        }

        private void BolumleriYukle()
        {
            listView1.Items.Clear();

            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Select * From Bolumler", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem Bolum = new ListViewItem(new string[] 
                    {
                    oku["BolumID"].ToString(),
                    oku["BolumAdi"].ToString()
                    }
                        );
                    listView1.Items.Add(Bolum);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];
            try
            {
                if (baglanti.State != ConnectionState.Open)
                    baglanti.Open();

                OleDbCommand komut = new OleDbCommand("Update Bolumler Set BolumAdi='" + textBox1.Text + "' Where BolumID=" + Convert.ToInt32(item.SubItems[0].Text), baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Bölüm Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BolumleriYukle();
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

                OleDbCommand komut = new OleDbCommand("Insert Into Bolumler (BolumAdi) Values('" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Bölüm Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BolumleriYukle();
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            ListViewItem item = listView1.SelectedItems[0];
            textBox1.Text=item.SubItems[1].Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
