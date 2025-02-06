using System;
using System.Collections.Generic;
using System.Diagnostics;

// Base Question Class
abstract class Question
{
    public string Header { get; set; }
    public string Body { get; set; }
    public int Mark { get; set; }
    public List<Answer> AnswerList { get; set; }
    public Answer? UserAnswer { get; set; }
    public Answer? RightAnswer { get; set; }

    public Question(string header, string body, int mark)
    {
        Header = header;
        Body = body;
        Mark = mark;
        AnswerList = new List<Answer>();
    }
}

// Derived Question Types
class TrueFalseQuestion : Question
{
    public TrueFalseQuestion(string header, string body, int mark) : base(header, body, mark) { }
}

class MCQQuestion : Question
{
    public MCQQuestion(string header, string body, int mark) : base(header, body, mark) { }
}

// Answer Class
class Answer
{
    public int AnswerId { get; set; }
    public string? AnswerText { get; set; }

    public Answer()
    {

    }
    public Answer(int id)
    {
        AnswerId = id;
    }
    public Answer(int id, string text)
    {
        AnswerId = id;
        AnswerText = text;
    }
}

// Base Exam Class
abstract class Exam : ICloneable, IComparable<Exam>
{
    public int Time { get; set; }
    public int NumberOfQuestions { get; set; }
    public List<Question> Questions { get; set; }

    public Exam(int time, int numberOfQuestions)
    {
        Time = time;
        NumberOfQuestions = numberOfQuestions;
        Questions = new List<Question>();
    }

    public abstract void ShowExam();

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public int CompareTo(Exam other)
    {
        return this.Time.CompareTo(other.Time);
    }
}

// Derived Exam Types
class FinalExam : Exam
{
    public FinalExam(int time, int numberOfQuestions) : base(time, numberOfQuestions) { }

    public override void ShowExam()
    {
        int UserScore = 0;
        int FullMark = 0;
        Console.WriteLine("Final Exam:");
        foreach (var question in Questions)
        {
            Console.WriteLine($"Q: {question.Body} ");
            for (int i = 0; i < question.AnswerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{question.AnswerList[i].AnswerText}");
            }
            FullMark += question.Mark;
            int TempUserAnswer = 0;
            Console.WriteLine("Your Answer is choice number :");
            int.TryParse(Console.ReadLine(), out TempUserAnswer);
            question.UserAnswer = question.AnswerList[TempUserAnswer - 1];

            if (question.UserAnswer == question.RightAnswer)
            {
                UserScore += question.Mark;
            }
        }
        Console.Clear();
        Console.WriteLine($"Your Answers :\n");
        foreach (var question in Questions)
        {
            Console.WriteLine($"Q: {question.Body}");
            Console.WriteLine($"Your Answer: {question.UserAnswer?.AnswerText}");
            Console.WriteLine($"Correct Answer: {question.RightAnswer?.AnswerText}");
            Console.WriteLine("-----------------------");
        }
        Console.WriteLine($"End Of Exam .");

        Console.WriteLine($"Your Final Grade is : {UserScore} from {FullMark} .");
    }
}

class PracticalExam : Exam
{
    public PracticalExam(int time, int numberOfQuestions) : base(time, numberOfQuestions) { }

    public override void ShowExam()
    {
        int UserScore = 0;
        int FullGrade = 0;
        Console.WriteLine("Practical Exam:");
        foreach (var question in Questions)
        {

            Console.WriteLine($"Q: {question.Body} , marks({question.Mark})");
            for (int i = 0; i < question.AnswerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{question.AnswerList[i].AnswerText}");
            }
            Console.WriteLine("Your Answer is choice number :");
            int.TryParse(Console.ReadLine(), out int UserAnswer);
            FullGrade += question.Mark;
            if (question.AnswerList[UserAnswer - 1] == question.RightAnswer)
            {
                UserScore += question.Mark;
            }
        }
        Console.Clear();
        Console.WriteLine("Right Answers of this Practical Exam  :-");
        foreach (var question in Questions)
        {
            Console.WriteLine($"Q:{question.Body} ? \n: - Right Answer : {question.RightAnswer?.AnswerId}.{question.RightAnswer?.AnswerText}");
        }
        Console.WriteLine("End of  Exam ========================================================");
        Console.WriteLine($"Your grade is {UserScore} from {FullGrade} .");

    }
}

// Subject Class
class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public Exam? Exam { get; set; }

    public Subject(int id, string name)
    {
        SubjectId = id;
        SubjectName = name;

    }

    public void CreateExam()
    {

        while (true)
        {
            int ExamType;
            Console.WriteLine("Please Enter exam type you want to create ( 1 for practical , 2 for final ) : ");
            if (int.TryParse(Console.ReadLine(), out ExamType) && ExamType >= 1 && ExamType <= 2)
            {
                if (ExamType == 1)
                {
                    Console.Clear();
                    Console.WriteLine("================ Practical Exam .===================");
                    int ExamTime;
                    while (true)
                    {
                        Console.WriteLine("Please Enter the time of exam in minutes : ");
                        bool Flag = int.TryParse(Console.ReadLine(), out ExamTime);
                        if (Flag)
                            break;
                        else
                        {
                            Console.WriteLine("invalid number please try again with valid number ");
                        }

                    }
                    int ExamNumOfQuestions;
                    while (true)
                    {
                        Console.WriteLine("Please Enter the number of questions you need to create : ");
                        bool Flag = int.TryParse(Console.ReadLine(), out ExamNumOfQuestions);
                        if (Flag)
                            break;
                        else
                        {
                            Console.WriteLine("invalid number please try again with valid number ");
                        }

                    }
                    Console.Clear();
                    Exam = new PracticalExam(ExamTime, ExamNumOfQuestions);
                    Console.WriteLine(" Practical Exam is MCQ");
                    for (int i = 0; i < ExamNumOfQuestions; i++)
                    {
                        Console.WriteLine($"Fill Question Number {i + 1} :");
                        Console.WriteLine("Please enter question header :");
                        string? QHeader = Console.ReadLine();
                        Console.WriteLine("Please enter question body :");
                        string? QBody = Console.ReadLine();

                        int QMark;
                        while (true)
                        {
                            Console.WriteLine("Please enter question mark :");
                            bool Flag = int.TryParse(Console.ReadLine(), out QMark);
                            if (Flag)
                                break;
                            else
                            {
                                Console.WriteLine("invalid number please try again with valid  intger number ");
                            }

                        }
                        if (QHeader is not null && QBody is not null)
                        {
                            Question PracticalMCQ = new MCQQuestion(QHeader, QBody, QMark);
                            Console.WriteLine("Please enter question choices :");

                            for (int j = 0; j < 3; j++)
                            {
                                Console.WriteLine($"Please enter question choice number {j + 1} :");
                                string? choice = Console.ReadLine();
                                if (choice is not null)
                                    PracticalMCQ.AnswerList.Add(new Answer(j + 1, choice));
                            }
                            int TrueAnswer;

                            while (true)
                            {
                                Console.WriteLine("Please specify the number of right Answer from that choices . ");
                                bool Flag = int.TryParse(Console.ReadLine(), out TrueAnswer);
                                if (Flag && TrueAnswer > 0 && TrueAnswer <= 3)
                                {
                                    PracticalMCQ.RightAnswer = PracticalMCQ.AnswerList[TrueAnswer - 1];
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Choice Number invalid try again :");
                                }

                            }

                            Exam.Questions.Add(PracticalMCQ);
                        }

                    }




                }
                else if (ExamType == 2)
                {
                    Console.Clear();
                    Console.WriteLine("================ Final Exam .===================");
                    int ExamTime;
                    while (true)
                    {
                        Console.WriteLine("Please Enter the time of exam in minutes : ");
                        bool Flag = int.TryParse(Console.ReadLine(), out ExamTime);
                        if (Flag)
                            break;
                        else
                        {
                            Console.WriteLine("invalid number please try again with valid number ");
                        }

                    }

                    int ExamNumOfQuestions;
                    while (true)
                    {
                        Console.WriteLine("Please Enter the number of questions you need to create : ");
                        bool Flag = int.TryParse(Console.ReadLine(), out ExamNumOfQuestions);
                        if (Flag)
                            break;
                        else
                        {
                            Console.WriteLine("invalid number please try again with valid number ");
                        }

                    }

                    Exam = new FinalExam(ExamTime, ExamNumOfQuestions);

                    Console.Clear();
                    ////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < ExamNumOfQuestions; i++)
                    {
                        while (true)
                        {
                            Console.WriteLine($"Please Enter the type of Question {i + 1} : ( 1 for True | False  , 2 for MCQ) ");
                            bool flag = int.TryParse(Console.ReadLine(), out int QType);
                            if (flag && QType == 1)
                            {
                                Console.WriteLine($"Q{i + 1} is True | False Question ");

                                Console.WriteLine("Please enter question header :");
                                string? QHeader = Console.ReadLine();
                                Console.WriteLine("Please enter question body :");
                                string? QBody = Console.ReadLine();
                                int QMark;
                                while (true)
                                {
                                    Console.WriteLine("Please enter question mark :");
                                    bool Flag = int.TryParse(Console.ReadLine(), out QMark);
                                    if (Flag)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("invalid number please try again with valid  intger number ");
                                    }

                                }
                                if (QHeader is not null && QBody is not null)
                                {
                                    Question FinalTrueFalseQ = new TrueFalseQuestion(QHeader, QBody, QMark);

                                    FinalTrueFalseQ.AnswerList.Add(new Answer(1, "True"));
                                    FinalTrueFalseQ.AnswerList.Add(new Answer(2, "False"));
                                    int TrueAnswer;
                                    while (true)
                                    {
                                        Console.WriteLine("Please specify the number of right Answer from that choices ( 1 for True , 2 for False  ) ");
                                        bool Flag = int.TryParse(Console.ReadLine(), out TrueAnswer);
                                        if (Flag && TrueAnswer > 0 && TrueAnswer <= 2)
                                        {
                                            FinalTrueFalseQ.RightAnswer = FinalTrueFalseQ.AnswerList[TrueAnswer - 1];
                                            Console.Clear();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Choice Number invalid try again ( 1 for true , 2 for false ):");
                                        }

                                    }

                                    Exam.Questions.Add(FinalTrueFalseQ);
                                }


                                break;
                            }
                            else if (flag && QType == 2)
                            {

                                Console.Clear();
                                Console.WriteLine($"Q{i + 1} is MCQ Question ");

                                Console.WriteLine($"Fill Question Number {i + 1} :");
                                Console.WriteLine("Please enter question header :");
                                string? QHeader = Console.ReadLine();
                                Console.WriteLine("Please enter question body :");
                                string? QBody = Console.ReadLine();

                                int QMark;
                                while (true)
                                {
                                    Console.WriteLine("Please enter question mark :");
                                    bool Flag = int.TryParse(Console.ReadLine(), out QMark);
                                    if (Flag)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("invalid number please try again with valid  intger number ");
                                    }

                                }
                                if (QHeader is not null && QBody is not null)
                                {
                                    Question FinalMCQQ = new MCQQuestion(QHeader, QBody, QMark);

                                    Console.WriteLine("Please enter question choices :");
                                    for (int j = 0; j < 3; j++)
                                    {
                                        Console.WriteLine($"Please enter question choice number {j + 1} :");
                                        string? choice = Console.ReadLine();
                                        if (choice is not null)
                                            FinalMCQQ.AnswerList.Add(new Answer(j + 1, choice));
                                    }
                                    int TrueAnswer;

                                    while (true)
                                    {
                                        Console.WriteLine("Please specify the number of right Answer from that choices . ");
                                        bool Flag = int.TryParse(Console.ReadLine(), out TrueAnswer);
                                        if (Flag && TrueAnswer > 0 && TrueAnswer <= 3)
                                        {
                                            FinalMCQQ.RightAnswer = FinalMCQQ.AnswerList[TrueAnswer - 1];
                                            Console.Clear();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Choice Number invalid try again :");
                                        }

                                    }

                                    Exam.Questions.Add(FinalMCQQ);
                                }







                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid number please enter only  1 or 2 ");
                            }
                        }

                    }
                }

                break;
            }
            else
            {
                Console.WriteLine("the entry number not 1 nor 2 please try again");
            }

        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        Subject test = new Subject(1, "Mathematics");
        test.CreateExam();
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Do you want to start the exam ...? ( Y | N ) .");
            string? InputChoice = Console.ReadLine();
            if (InputChoice is not null && InputChoice.ToLower() == "y")
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Console.Clear();
                test.Exam?.ShowExam();
                Console.WriteLine($"The elapsed time = {sw.Elapsed}");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice to start exam please enter (Y,y) for yes or (N,n) for no ");
            }
        }
        /////////////////////////////////Done////////////////////////////////////////////////////


    }
}

