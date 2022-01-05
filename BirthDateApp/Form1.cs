using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BirthDateApp
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        void MusteriGetir()
        {
            baglanti = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database= DateOfBirth;Trusted_Connection=true;");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM [Table]", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MusteriGetir();
        }

        private void musteriKaydet_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO [Table](ad,soyad,telefon,dogumtarihi,meslek,adres) VALUES (@ad,@soyad,@telefon,@dogumtarihi,@meslek,@adres)";
            komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@ad", musteriAdEkle.Text);
            komut.Parameters.AddWithValue("@soyad", musteriSoyadEkle.Text);
            komut.Parameters.AddWithValue("@telefon", musteriTelEkle.Text);
            komut.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@meslek", musteriMeslekEkle.Text);
            komut.Parameters.AddWithValue("@adres", musteriAdresEkle.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            idEkle.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            musteriAdEkle.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            musteriSoyadEkle.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            musteriTelEkle.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            musteriMeslekEkle.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            musteriAdresEkle.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM [Table] WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(idEkle.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();
        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE [Table] SET ad=@ad,soyad=@soyad,telefon=@telefon,dogumtarihi=@dogumtarihi,meslek=@meslek,adres=@adres WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", Convert.ToInt32(idEkle.Text));
            komut.Parameters.AddWithValue("@ad", musteriAdEkle.Text);
            komut.Parameters.AddWithValue("@soyad", musteriSoyadEkle.Text);
            komut.Parameters.AddWithValue("@telefon", musteriTelEkle.Text);
            komut.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@meslek", musteriMeslekEkle.Text);
            komut.Parameters.AddWithValue("@adres", musteriAdresEkle.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MusteriGetir();

        }

        private void listeleButton_Click(object sender, EventArgs e)
        {
            Listele_LBOX();
        }




        void Listele_LBOX()
        {
            string sorgu = "SELECT ad,soyad,dogumtarihi FROM[Table] WHERE DAY(dogumtarihi) = DAY(GETDATE()) AND MONTH(dogumtarihi) = MONTH(GETDATE())";
            komut = new SqlCommand(sorgu, baglanti);
            baglanti.Open();
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                listBox1.Items.Add(dr["ad"] + "\t" + dr["soyad"]);

            }
            baglanti.Close();


            //listBox1.Items.Add(dataGridView1.Rows[i].Cells["ad"].Value.ToString());
            //listBox1.Items.Add(dataGridView1.Rows[i].Cells["dogumtarihi"].Value.ToString());
        }
    }
}
