using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ej_U6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const string FILENAME = "peliculas.dat";
                 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un objeto de Pelicula a partir de la información ingresada por el usuario
                Pelicula pelicula = new Pelicula(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));

                // Abrir el archivo binario para escritura (creando uno nuevo si no existe)
                using (FileStream archivo = new FileStream(FILENAME, FileMode.Append, FileAccess.Write))
                {
                    // Serializar el objeto de Pelicula y escribirlo en el archivo
                    BinaryWriter escritor = new BinaryWriter(archivo);
                    pelicula.Serializar(escritor);
                }

                MessageBox.Show("La película ha sido guardada correctamente.");
            }
            catch (FormatException)
            {
                MessageBox.Show("El año ingresado no es un número válido.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar guardar la película: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir el archivo binario para lectura
                using (FileStream archivo = new FileStream(FILENAME, FileMode.Open, FileAccess.Read))
                {
                    // Leer el archivo y mostrar las películas guardadas
                    BinaryReader lector = new BinaryReader(archivo);
                    while (archivo.Position < archivo.Length)
                    {
                        Pelicula pelicula = new Pelicula();
                        pelicula.Deserializar(lector);
                        MessageBox.Show(pelicula.ToString());
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No se encontró el archivo de películas.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar leer el archivo de películas: " + ex.Message);
            }
        }
    }


}