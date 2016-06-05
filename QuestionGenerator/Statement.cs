using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace QuestionGenerator
{
    class Statement : PositionComponent
    {
        private Label label;
        private TextBox textBox;

        private readonly static Size labelSize = new Size(50,25);
        private readonly static Size textBoxSize = new Size(675, 25);
        private int spacing;

        public Statement(string Label) : this(Label, "")
        {
        }

        public Statement(string Label, string Text)
        {
            spacing = 20;
            CreateLabel(Label);
            CreateTextBox(Label, Text);
        }

        private void CreateLabel(string Label)
        {
            label = new Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(0, 112);
            label.Name = Label;
            label.Size = labelSize;
            label.TabIndex = 0;
            label.Text = Label;
        }

        private void CreateTextBox(string Label,string Text)
        {
            textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(80, 112);
            textBox.Name = Label;
            textBox.Size = textBoxSize;
            textBox.TabIndex = 0;
        }

        public void SetSpacing(int Spacing )
        {
            spacing = Spacing;
        }


        public void SetX(int X)
        {
            SetLocation(X,label.Location.Y);
        }

        public void SetY(int Y)
        {
            SetLocation(label.Location.X,Y);
        }
        
        public void SetLocation(int X, int Y)
        {
            label.Location = new System.Drawing.Point(X, Y);
            int tempX = label.Location.X + label.Width + spacing;
            textBox.Location = new System.Drawing.Point(tempX, Y);
        }

        public void SetTabIndex(int Index)
        {
            textBox.TabIndex = Index;
        }

        public Label GetLabel()
        {
            return label;
        }

        public TextBox GetTextBox()
        {
            return textBox;
        }

        public string GetText()
        {
            return textBox.Text;
        }


        public Size GetSize()
        {
            return textBox.Size;
        }
    }
}
