using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        // My own private Variables
        private String _fname = null;
        private SqlConnection _conn;

        public Form1()
        {
            InitializeComponent();

            // Informações do BD aqui
            _conn = new SqlConnection("data source=XEON;" +
                "initial catalog=Northwind;" +
                "user id=sa;password=manager;");
        }

        //Lê a imagem em um array de bytes
        public static byte[] GetPhoto(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] photo = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return photo;
        }


        private void button1_Click(
            string photoFilePath)
        {
            // Le a foto em array de bytes usando a funcao declarada acima
            byte[] photo = GetPhoto(photoFilePath);

            //INSERT no banco de dados
            SqlCommand addFoto = new SqlCommand(
                "INSERT INTO fotos (" +
                "Foto) " +
                "VALUES(@Foto)", _conn);

            addFoto.Parameters.Add("@Foto", SqlDbType.Image, photo.Length).Value = photo;

            // Open the Connection and INSERT the BLOB into the Database
            _conn.Open();
            addFoto.ExecuteNonQuery();
            _conn.Close();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
