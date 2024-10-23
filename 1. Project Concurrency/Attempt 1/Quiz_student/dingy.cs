// namespace Quiz
// {
// public class Answer
// {
// public Student Student;
// public string Text;
// public Answer(Student std,string txt = "")
// {
//     this.Student = std;
//     this.Text = txt;
// }
// public string ToString()
// {
//     string delimiter = " : ";
//     return "Answer "+delimiter+this.Student.ToString()+delimiter+this.Text;
// }
// }
// public class Question
// {
// public string Text { get; set; }
// public string TeacherCode;
// // Each question can collect answers by the students.
// public LinkedList<Answer> Answers;
// public Question(string txt, string tcode)
// {
//     this.Text = txt;
//     this.TeacherCode = tcode;
//     this.Answers = new LinkedList<Answer>();
// }
// public virtual void AddAnswer(Answer a)
// {
//         this.Answers.AddLast(a);
// }
// public String ToString()
// {
//     string delim = " : ";
//     return "Question Designed by: " + delim + this.TeacherCode;
// }
// }
// }