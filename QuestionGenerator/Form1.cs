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

namespace QuestionGenerator
{
    public partial class QuestionGenerator : Form
    {
        Question question;
        public QuestionGenerator()
        {
            question = new Question(10,10);
            this.Controls.Add(this.question.GetPanel());
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = question.toString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            question.FlushToFile();
        }
    }
}
