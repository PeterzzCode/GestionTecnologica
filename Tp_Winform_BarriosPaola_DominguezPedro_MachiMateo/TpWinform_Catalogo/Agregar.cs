using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TpWinform_Catalogo
{
    public partial class Agregar : Form
    {
        private Articulo articulo = null;
        public Agregar()
        {
            InitializeComponent();
        }

        public Agregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {


                string marca;
                string categoria;
                if (tbCodigo.Text != "" && tbNombre.Text != "" && nudPrecio.Text != "") 
                { 
                    if (articulo == null)
                    {
                        articulo = new Articulo();
                    }
                    //articulo.Id = int.Parse(tbId.Text);
                    articulo.codigoArticulo = tbCodigo.Text;
                    articulo.nombre = tbNombre.Text;
                    articulo.descripcion = tbDescripcion.Text;


                    articulo.Categoria = new Categoria();
                    if (cboCategoria.Text == "Celulares")
                    {
                        articulo.Categoria.idCategoria = 1;
                    }
                    if (cboCategoria.Text == "Televisores")
                    {
                        articulo.Categoria.idCategoria = 2;
                    }
                    if (cboCategoria.Text == "Media")
                    {
                        articulo.Categoria.idCategoria = 3;
                    }
                    if (cboCategoria.Text == "Audio")
                    {
                        articulo.Categoria.idCategoria = 4;
                    }

                    articulo.Marca = new Marca();
                    if (cboMarca.Text == "Samsung")
                    {
                        articulo.Marca.idMarca = 1;
                    }
                    if (cboMarca.Text == "Apple")
                    {
                        articulo.Marca.idMarca = 2;
                    }
                    if (cboMarca.Text == "Sony")
                    {
                        articulo.Marca.idMarca = 3;
                    }
                    if (cboMarca.Text == "Huawei")
                    {
                        articulo.Marca.idMarca = 4;
                    }
                    if (cboMarca.Text == "Motorola")
                    {
                        articulo.Marca.idMarca = 5;
                    }
                    //articulo.Marca.marca = cboMarca.Text;


                    articulo.imagen = new Imagen();



                    articulo.imagen.idArticulo = negocio.TraerUltimoId();
                    articulo.imagen.url = tbURL.Text;
                    articulo.precio = decimal.Parse(nudPrecio.Text);

                    if (articulo.Id != 0)
                    {
                        negocio.ModificarArticulo(articulo);
                        MessageBox.Show("Modificado!");
                    }
                    else
                    {
                        articulo.Id = negocio.TraerUltimoId();
                        negocio.AgregarArticulo(articulo);
                        MessageBox.Show("Agregado!");
                    }

                    Close();
                }
                else
                {
                    nudPrecio.BackColor = Color.Red;
                    tbNombre.BackColor = Color.Red;
                    tbCodigo.BackColor = Color.Red;
                    MessageBox.Show("Por favor cargue Nombre, Codigo y Precio", "ATENCION ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            ArticuloNegocio art = new ArticuloNegocio();
            AccesoDatos datos = new AccesoDatos();
            Articulo aux = new Articulo();
            aux.Categoria = new Categoria();
            List<Articulo> lista = new List<Articulo>();

            datos.setearQuery("Select Descripcion as Categoria from CATEGORIAS");
            datos.ejecutarLectura();
            while (datos.Lector.Read())
            {
                try
                {
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.categoria = (string)datos.Lector["Categoria"];

                    cboCategoria.Items.Add(aux.Categoria.categoria);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

             ArticuloNegocio art1 = new ArticuloNegocio();
             AccesoDatos datos1 = new AccesoDatos();
             Articulo aux1 = new Articulo();
             aux1.Marca = new Marca();
             List<Articulo> lista1 = new List<Articulo>();
             
             datos1.setearQuery("Select Descripcion as Marca from MARCAS");
             datos1.ejecutarLectura();
             while (datos1.Lector.Read())
             {
                 try
                 {
                     if (!(datos1.Lector["Marca"] is DBNull))
                         aux1.Marca.marca = (string)datos1.Lector["Marca"];
             
                     cboMarca.Items.Add(aux1.Marca.marca);
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
             }
             
    if (articulo != null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                tbId.Text = articulo.Id.ToString();
                tbCodigo.Text = articulo.codigoArticulo;
                tbNombre.Text = articulo.nombre;
                tbDescripcion.Text = articulo.descripcion;
                articulo.Categoria = new Categoria();
                //cboCategoria.Text = articulo.Categoria.categoria;
                cboCategoria.SelectedIndex = articulo.Categoria.idCategoria;
                articulo.Marca = new Marca();
                //cboMarca.Text = articulo.Marca.marca;
                cboMarca.SelectedIndex = articulo.Marca.idMarca;

                if (articulo.Categoria.idCategoria == 1)
                {
                    cboCategoria.Text = "Samsung";
                    //cboCategoria.
                }
                if (articulo.Categoria.idCategoria == 2)
                {

                    cboCategoria.Text = "Televisores";
                }
                if (articulo.Categoria.idCategoria == 3)
                {
                    
                    cboCategoria.Text = "Media";
                }
                if (articulo.Categoria.idCategoria == 4)
                {
                    
                    cboCategoria.Text = "Audio";
                }

                if (articulo.Marca.idMarca == 1)
                {
                    
                    cboMarca.Text = "Samsung";
                }
                if (articulo.Marca.idMarca == 2)
                {
                    
                    cboMarca.Text = "Apple";
                }
                if (articulo.Marca.idMarca == 3)
                {
                    
                    cboMarca.Text = "Sony";
                }
                if (articulo.Marca.idMarca == 4)
                {
                    
                    cboMarca.Text = "Huawei";
                }
                if (articulo.Marca.idMarca == 5)
                {

                    cboMarca.Text = "Motorola";
                }

                articulo.imagen = new Imagen();
                articulo.imagen.idArticulo = negocio.TraerUltimoId();
                
                tbURL.Text = articulo.imagen.url;
                nudPrecio.Text = articulo.precio.ToString();
            }
        }

        private void tbURL_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbURL.Text);
        }


        private void cargarImagen(string imag)
        {
            try
            {
                pbxAddPokemon.Load(imag);
            }
            catch (Exception ex)
            {

                pbxAddPokemon.Load("https://www.webempresa.com/foro/wp-content/uploads/wpforo/attachments/3200/318277=80538-Sin_imagen_disponible.jpg");
            }
        }
    }
}
