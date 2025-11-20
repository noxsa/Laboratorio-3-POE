using System;
using System.Windows.Forms;

namespace laboratorio3.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            var hash = CryptoHelper.ComputeSha256Hash(txtContraseña.Text);

            using var conn = new System.Data.SqlClient.SqlConnection(Config.ConnectionString);
            conn.Open();

            var user = conn.Query<dynamic>(
                "SELECT * FROM Usuarios WHERE Usuario = @u AND PasswordHash = @p",
                new { u = txtUsuario.Text, p = hash }
            ).FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
                return;
            }

            var main = new frmRegistrarProducto();
            this.Hide();
            main.FormClosed += (s, a) => this.Close();
            main.Show();
        }
    }
}
