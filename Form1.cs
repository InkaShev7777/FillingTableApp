using FillingTable.Controller;
using System;
using System.Windows.Forms;

namespace FillingTable
{
    public partial class Form1 : Form
    {
        private FieldController _fieldController;
        public Form1()
        {
            _fieldController = new FieldController();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            if (!filename.Contains(".sql"))
                return;
            // логика обработки и добавления в текст-бокс
            string fileText = System.IO.File.ReadAllText(filename);
            _fieldController.SetSqlData(fileText);
            MessageBox.Show("Файл открыт");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, textBox1.Text);
            MessageBox.Show("Файл сохранен");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
