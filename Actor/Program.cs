using NNRobot;
using System;

namespace Actor {
public class Answer: NNRobot.AnswerInterface {
    public void Talk(Robot robot, string content) {
        Console.WriteLine(robot.GetName() + ":" + content);
    }
}
public class Program {
    public static void Main(string[] args) {
        NNRobot.Robot robot = new NNRobot.Robot("Minuowa");
        robot.SetAnswer(new Answer());
        do {
            string content = Console.ReadLine();
            if(content.Length > 0) {
                if (content.ToLower() == "quit")
                    break;
                if (content.ToLower() == "dump") {
                    robot.Dump();
                    continue;
                }
            }
            robot.OnAsk(content);
        } while (true);
        robot.Close();
    }
}
}
