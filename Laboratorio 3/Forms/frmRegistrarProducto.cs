using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace laboratorio3.Forms
{
    public partial class frmRegistrarProducto : Form
    {
        public frmRegistrarProducto()
        {
            InitializeComponent();
        }

        private void frmRegistrarProducto_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var list = DbRepo.GetAll<Producto>().ToList();
            dgvRegistro.DataSource = list;

            foreach (DataGridViewRow row in dgvRegistro.Rows)
            {
                if (row.DataBoundItem is Producto p)
                {
                    if (p.Cantidad < p.StockMinimo)
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var p = new Producto
            {
                Nombre = txtNombre.Text,
                Codigo = txtCodigo.Text,
                Categoria = txtCategoria.Text,
                PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text),
                Cantidad = int.Parse(txtCantidad.Text),
                StockMinimo = int.Parse(txtStockMinimo.Text)
            };

            DbRepo.Insert(p);
            LoadData();
        }

        private void dgvRegistro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var prod = dgvRegistro.Rows[e.RowIndex].DataBoundItem as Producto;

            var edit = new frmRegistrarProductoCmd(new Producto
            {
                Id = prod.Id,
                Nombre = prod.Nombre,
                Codigo = prod.Codigo,
                Categoria = prod.Categoria,
                PrecioUnitario = prod.PrecioUnitario,
                Cantidad = prod.Cantidad,
                StockMinimo = prod.StockMinimo
            });

            if (edit.ShowDialog() == DialogResult.OK)
            {
                DbRepo.Update(edit.Producto);
                LoadData();
            }
        }
    }
}
