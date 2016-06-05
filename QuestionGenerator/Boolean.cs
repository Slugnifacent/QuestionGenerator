using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuestionGenerator
{
    class Boolean : PositionComponent
    {
        private CheckBox fact;

        private readonly static Size size = new System.Drawing.Size(113, 24);
        int spacing;

        public Boolean(string Label)
        {
            spacing = 10;
            CreateCheckBox(Label);
        }

        private void CreateCheckBox(string Label)
        {
            fact = new CheckBox();
            fact.AutoSize = true;
            fact.Location = new System.Drawing.Point(47, 64);
            fact.Name = Label;
            fact.Size = size;
            fact.TabIndex = 5;
            fact.Text = Label;
            fact.UseVisualStyleBackColor = true;
        }

        public void SetX(int X)
        {
            SetLocation(X, fact.Location.Y);
        }

        public void SetY(int Y)
        {
            SetLocation(fact.Location.X, Y);
        }

        public void SetLocation(int X, int Y)
        {
            fact.Location = new System.Drawing.Point(spacing + X, Y);
        }

        public void SetTabIndex(int Index)
        {
            fact.TabIndex = Index;
        }

        public CheckBox GetCheckBox()
        {
            return fact;
        }
        
        public bool Value()
        {
            return fact.Checked;
        }

        public Size GetSize()
        {
            return fact.Size;
        }
    }
}
