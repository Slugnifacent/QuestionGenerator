using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionGenerator
{
    interface PositionComponent
    {
        void SetX(int X);
        void SetY(int Y);
        void SetLocation(int X, int Y);
        void SetTabIndex(int Index);
        Size GetSize();
    }
}
