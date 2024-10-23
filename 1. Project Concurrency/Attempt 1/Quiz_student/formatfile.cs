// using System;
// using Quiz;


// /// Concurrent version of the Quiz
// namespace ConcQuiz
// {
// public class ConcAnswer: Answer
// {
// public ConcAnswer(ConcStudent std, string txt = ""): base(std,txt){}
// }
// public class ConcQuestion : Question
// {
// private readonly object _lockObject = new object();

// public ConcQuestion(string txt, string tcode) : base(txt, tcode){}

// public override void AddAnswer(Answer a)
// {
// // done 
// lock (_lockObject)
// {
//     base.AddAnswer(a);
// }
// }
// }

// public class ConcStudent: Student
// {
// // todo: add required fields

// private readonly object _lockObject = new object();

// public ConcStudent(int num, string name): base(num,name){}

// public override void AssignExam(Exam e)
// {
// //todo: implement the body
// lock (_lockObject)
// {
//     base.AssignExam(e);
// }
// }

// public override void StartExam()
// {
// //todo: implement the body
// lock (_lockObject)
// {
//     base.StartExam();
// }
// }

// public override void Think()
// {
// //todo: implement the body
// lock (_lockObject)
// {
//     base.Think();
// }
// }

// public override void ProposeAnswer()
// {
// //todo: implement the body
// lock (_lockObject)
// {
//     base.ProposeAnswer();
// }
// }
// }
// }