using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculadora
{
    public class CalculadoraForm : Form
    {
        private TextBox pantalla;
        private double primerNumero = 0;
        private string operacion = "";
        private bool nuevaOperacion = true;

        // Definimos la paleta de colores pastel// Fondo general de la calculadora
Color colorFondo = Color.FromArgb(40, 44, 52);        // Gris oscuro principal

// Pantalla de la calculadora
Color colorPantalla = Color.FromArgb(55, 60, 68);     // Gris oscuro más claro

// Botones numéricos
Color colorNumeros = Color.FromArgb(60, 65, 75);      // Gris oscuro botones

// Texto de los números
Color colorTextoNumeros = Color.FromArgb(230, 230, 230); // Blanco suave

// Botones operadores neutros (/ * -)
Color colorOperadores = Color.FromArgb(72, 78, 88);   // Gris operador

// Botón Clear
Color colorClear = Color.FromArgb(220, 70, 70);       // Rojo intenso

// Botón suma (+)
Color colorSuma = Color.FromArgb(60, 180, 90);        // Verde

// Botón igual (=)
Color colorIgual = Color.FromArgb(70, 160, 230);      // Azul
Color colorEspecial = Color.FromArgb(100, 149, 237); // Azul claro para botones especiales

        public CalculadoraForm()
        {
            // Configuración de la ventana
            this.Text = "Calculadora";
            this.Size = new Size(320, 450);
            this.BackColor = colorFondo; // Fondo rosa
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Pantalla de texto (Display)
            pantalla = new TextBox();
            pantalla.Size = new Size(280, 50);
            pantalla.Location = new Point(12, 20);
            pantalla.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            pantalla.TextAlign = HorizontalAlignment.Right;
            pantalla.ReadOnly = true;
            pantalla.BackColor = colorPantalla;
            pantalla.BorderStyle = BorderStyle.None; // Sin bordes feos
            this.Controls.Add(pantalla);

            // Etiquetas de los botones
            string[] etiquetas = {
                "7", "8", "9", "/",
                "4", "5", "6", "*",
                "1", "2", "3", "-",
                "C", "0", "=", "+"
            };

            int x = 12, y = 90;
            for (int i = 0; i < etiquetas.Length; i++)
            {
                Button btn = new Button();
                btn.Text = etiquetas[i];
                btn.Size = new Size(65, 65);
                btn.Location = new Point(x, y);
                btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                btn.FlatStyle = FlatStyle.Flat; // Estilo plano para que se vea moderno
                btn.FlatAppearance.BorderSize = 0; // Quitar borde negro
                btn.ForeColor = Color.White; // Texto blanco para que resalte

                // Asignar colores según el tipo de botón
                if (int.TryParse(etiquetas[i], out _)) btn.BackColor = colorNumeros;
                else if (etiquetas[i] == "=" || etiquetas[i] == "C") btn.BackColor = colorEspecial;
                else btn.BackColor = colorOperadores;

                btn.Click += Boton_Click;
                this.Controls.Add(btn);

                x += 72;
                if ((i + 1) % 4 == 0) { x = 12; y += 72; }
            }
        }

        private void Boton_Click(object? sender, EventArgs e)
        {
            string texto = ((Button)sender!).Text;

            if (int.TryParse(texto, out _))
            {
                if (nuevaOperacion) { pantalla.Clear(); nuevaOperacion = false; }
                pantalla.Text += texto;
            }
            else if (texto == "C")
            {
                pantalla.Clear();
                primerNumero = 0;
            }
            else if (texto == "=")
            {
                if (double.TryParse(pantalla.Text, out double segundoNumero))
                {
                    double resultado = 0;
                    switch (operacion)
                    {
                        case "+": resultado = primerNumero + segundoNumero; break;
                        case "-": resultado = primerNumero - segundoNumero; break;
                        case "*": resultado = primerNumero * segundoNumero; break;
                        case "/": resultado = segundoNumero != 0 ? primerNumero / segundoNumero : 0; break;
                    }
                    pantalla.Text = resultado.ToString();
                    nuevaOperacion = true;
                }
            }
            else
            {
                if (double.TryParse(pantalla.Text, out double num))
                {
                    primerNumero = num;
                    operacion = texto;
                    nuevaOperacion = true;
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculadoraForm());
        }
    }
}