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
            InitializeComponent();
            _fieldController = new FieldController();
            _fieldController.ShowingData += ShowData;
        }

        private void ShowData(string data) => resultTB.Text = data;


        private void button1_Click(object sender, EventArgs e)
        {
            resultTB.Text = String.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            if (!filename.Contains(".sql"))
                return;
            // логика обработки и добавления в текст-бокс
            string fileText = System.IO.File.ReadAllText(filename);
            fileTB.Text = fileText;
            _fieldController.SetSqlData(fileText);
            _fieldController.SetQuertCount(Convert.ToInt32(countTB.Text));
            MessageBox.Show("Файл открыт");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName + ".sql";
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, resultTB.Text);
            MessageBox.Show("Файл сохранен");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _fieldController.ShowingData -= ShowData;
            this.Close();
        }

        private void countTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
