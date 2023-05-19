using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using dominio;
using Negocio;


namespace TpWinform_Catalogo
{
    public partial class FrmPrincipal : Form
    {
        private List<Articulo> listaArticulo;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

            cargar();

        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvArticulo.CurrentRow != null)
            {

                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.imagen.url);

            }
            
            
        }

        private void cargar()
        {
            
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulo = negocio.listar();
                dgvArticulo.DataSource = listaArticulo;
                ocultarColumnas();
                pbArticulo.Load(listaArticulo[0].imagen.url);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulo.Columns["Id"].Visible = false;
            dgvArticulo.Columns["Imagen"].Visible = false;

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbArticulo.Load("https://www.webempresa.com/foro/wp-content/uploads/wpforo/attachments/3200/318277=80538-Sin_imagen_disponible.jpg");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            Agregar modificar = new Agregar(seleccionado);
            modificar.ShowDialog();
            cargar();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar alta = new Agregar();
            alta.ShowDialog();
            cargar();
        }

        private void dgvArticulo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void lblFiltro_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            

           
            
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {


             List<Articulo> listaFiltrada;

            string filtro = txtFiltro.Text; 

            if(filtro != "")
            {

                listaFiltrada = listaArticulo.FindAll(x => x.nombre.ToUpper().Contains(filtro.ToUpper()) || x.descripcion.ToUpper().Contains( filtro.ToUpper()) );//para que busque por coincidencia y se agrega para busqueda por descripcion 
            }
            else
            {
                listaFiltrada = listaArticulo;

            }


            dgvArticulo.DataSource = null; //limpia data
            dgvArticulo.DataSource = listaFiltrada;

            if(dgvArticulo.Rows.Count == 0)
            {
                btnAgregar.Enabled = false;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
            
                ocultarColumnas();










        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Quieres Realmente Eliminar Este Articulo?", "Eliminando..", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    articulo.EliminarArticulo(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
