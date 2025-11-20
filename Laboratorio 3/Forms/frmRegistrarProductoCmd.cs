// File: Forms/frmRegistrarProductoCmd.cs
using System;
using System.Windows.Forms;

namespace laboratorio3.Forms
{
    public partial class frmRegistrarProductoCmd : Form
    {
        public Producto Producto { get; private set; }

        public frmRegistrarProductoCmd(Producto p)
        {
            Producto = p;
            InitializeComponent();
            LoadProduct();
        }

        private void LoadProduct()
        {
            if (Producto == null) return;
            txtNombre.Text = Producto.Nombre;
            txtCodigo.Text = Producto.Codigo;
            txtCategoria.Text = Producto.Categoria;
            txtPrecioUnitario.Text = Producto.PrecioUnitario.ToString("0.00");
            txtCantidad.Text = Producto.Cantidad.ToString();
            txtStockMinimo.Text = Producto.StockMinimo.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // validaciones mínimas
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Nombre y Código son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out var precio))
            {
                MessageBox.Show("Precio inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out var cantidad))
            {
                MessageBox.Show("Cantidad inválida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtStockMinimo.Text, out var stock))
            {
                MessageBox.Show("Stock mínimo inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto.Nombre = txtNombre.Text.Trim();
            Producto.Codigo = txtCodigo.Text.Trim();
            Producto.Categoria = txtCategoria.Text.Trim();
            Producto.PrecioUnitario = precio;
            Producto.Cantidad = cantidad;
            Producto.StockMinimo = stock;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // KeyPress handlers reutilizables
        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void NumericDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && tb != null && tb.Text.Contains("."))
                e.Handled = true;
        }
    }
}
