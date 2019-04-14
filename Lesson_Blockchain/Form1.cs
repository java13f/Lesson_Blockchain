using System;
using System.Windows.Forms;

namespace Lesson_Blockchain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Chain chain = new Chain();

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(chain.Blocks.ToArray());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            chain.Add(txtData.Text, txtUser.Text);

            listBox1.Items.AddRange(chain.Blocks.ToArray());
        }

        
    }
}
