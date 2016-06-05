using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringGenerator;

namespace QuestionGenerator
{
    class Question
    {
        private Panel panel;
        private Statement subject;
        private Statement verb;
        private Statement what;
        private Statement where;
        private Statement when;
        private Statement why;
        private Statement how;
        private Boolean fact;

        private List<PositionComponent> components;
        private static readonly Size spacing = new Size(10,10);

        StringGenerator.StringGenerator generatedQuestions;

        public Question() : this(0,0) { }
        public Question(int X, int Y)
        {
            panel = new System.Windows.Forms.Panel();
            panel.AutoSize = true;
            panel.Location = new System.Drawing.Point(X, Y);
            panel.Name = "Question";
            panel.TabIndex = 0;

            subject = new Statement("Subject");
            verb  = new Statement("Verb");
            what  = new Statement("What");
            where = new Statement("Where");
            when  = new Statement("When");
            why   = new Statement("Why");
            how   = new Statement("How");
            fact  = new Boolean("Fact");

            components = new List<PositionComponent>();
            components.Add(subject);
            components.Add(verb);
            components.Add(what);
            components.Add(where);
            components.Add(when);
            components.Add(why);
            components.Add(how);
            components.Add(fact);
            setLocation(0, 0);
            ResetPanel();
            
            generatedQuestions = new StringGenerator.StringGenerator("Questions.txt","\\(([^)]+)\\)");
        }


        private void setLocation(int X, int Y)
        {
            int startY = Y;
            int Spacing = 25;
            int tabIndex = 0;
            Size temp;
            foreach (PositionComponent component in components)
            {
                component.SetLocation(X, startY);
                component.SetTabIndex(tabIndex++);
                temp = component.GetSize();
                startY += temp.Height + Spacing;
            }
        }

        private void ResetPanel()
        {
            panel.Controls.Clear();
            AddToPanel(subject.GetLabel(), subject.GetTextBox());
            AddToPanel(verb.GetLabel(), verb.GetTextBox());
            AddToPanel(what.GetLabel(), what.GetTextBox());
            AddToPanel(where.GetLabel(), where.GetTextBox());
            AddToPanel(when.GetLabel() , when.GetTextBox());
            AddToPanel(why.GetLabel(), why.GetTextBox());
            AddToPanel(how.GetLabel(), how.GetTextBox());
            AddToPanel(fact.GetCheckBox());
        }

        private void AddToPanel(Control A)
        {
            panel.Controls.Add(A);
        }

        private void AddToPanel(Control A, Control B)
        {
            panel.Controls.Add(A);
            panel.Controls.Add(B);
        }

        public Panel GetPanel()
        {
            return panel;
        }

        public String Subject
        {
            get { return subject.GetText(); }
        }

        public String Verb
        {
            get { return verb.GetText(); }
        }

        public String What
        {
            get { return what.GetText(); }
        }

        public String Where
        {
            get { return where.GetText(); }
        }

        public String When
        {
            get { return when.GetText(); }
        }

        public String Why
        {
            get { return why.GetText(); }
        }

        public String How
        {
            get { return how.GetText(); }
        }

       
        public string toString()
        {
            generatedQuestions.ClearGeneratedStrings();
            // Generate Questions Types
            GenerateTrueFalseQuestions();
            if (fact.Value())
            {
                GenerateBasicQuestions();
                GenerateFillInTheBlanks();
            }
            string result = "";
            List<string> genQuestions = generatedQuestions.GetGeneratedStrings();
            foreach (string value in genQuestions)
            {
                result += value;
            }
            
            return result.Trim() ;
        }

        public void FlushToFile()
        {
            generatedQuestions.WriteToFile();
        }

        private void GenerateTrueFalseQuestions()
        {
            string answer = (fact.Value()) ? "True" : "False";
            ExpandandAdd(StringConstructor(Subject,Verb,What, "", answer));
            ExpandandAdd(StringConstructorExtraCheck(Subject, Verb, What, When, answer));
            ExpandandAdd(StringConstructorExtraCheck(Subject, Verb, What, How, answer));
            ExpandandAdd(StringConstructorExtraCheck(Subject, Verb, What, Why, answer));
            ExpandandAdd(StringConstructorExtraCheck(Subject, Verb, What, Where, answer));
        }

        private void GenerateBasicQuestions()
        {
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb, "", "what?", What));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb, What, "when?", When));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb, What, "how?", How));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb, What, "why?", Why));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb, What, "where?", Where));
        }

        private void GenerateFillInTheBlanks()
        {
            string Blank = "_____";
            ExpandandAdd(StringConstructorAnswerCheck(Blank  , Verb , What, "", Subject));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Blank, What, "", Verb));
            ExpandandAdd(StringConstructorAnswerCheck(Subject, Verb , Blank, "", What));
            
            GenerateFillInTheBlanksHelper(Subject, Verb, What, When);
            GenerateFillInTheBlanksHelper(Subject, Verb, What, How);
            GenerateFillInTheBlanksHelper(Subject, Verb, What, Why);
            GenerateFillInTheBlanksHelper(Subject, Verb, What, Where);
        }

        private void GenerateFillInTheBlanksHelper(string Sub, string Ver, string Wha, string Extra)
        {
            string Blank = "_____";
            if (Extra.Length == 0) return;
            ExpandandAdd(StringConstructorAnswerCheck(Blank, Ver, Wha, Extra, Sub));
            ExpandandAdd(StringConstructorAnswerCheck(Sub, Blank, Wha, Extra, Ver));
            ExpandandAdd(StringConstructorAnswerCheck(Sub, Ver, Blank, Extra, Wha));
            ExpandandAdd(StringConstructorAnswerCheck(Sub, Ver, Wha, Blank, Extra));
        }

        private void ExpandandAdd(string value)
        {
            generatedQuestions.ProcessString(value);
        }

        private string StringConstructorExtraCheck(string Sub, string Ver, string Wha, string Extra, string Answer)
        {
            if (Extra.Length == 0) return "";
            return StringConstructor(Sub, Ver, Wha, Extra, Answer);
        }

        private string StringConstructorAnswerCheck(string Sub, string Ver, string Wha, string Extra, string Answer)
        {
            if (Answer.Length == 0) return "";
            return StringConstructor(Sub, Ver, Wha, Extra, Answer);
        }

        private string StringConstructor(string Sub, string Ver, string Wha, string Extra, string Answer)
        {
            return string.Format("{0} {1} {2} {3}|{4}\r\n", Sub,Ver,Wha,Extra, Answer);
        }

    }
}
