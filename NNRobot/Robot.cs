using System;
using System.Collections.Generic;

namespace NNRobot {

public interface AnswerInterface {
    void Talk(string content);
}

public class Robot {
    private AnswerInterface answerInterface_;
    private Stack<string> lateWords_ = new Stack<string>();
    private Nerve.Brain brain_ = new Nerve.Brain();

    public Robot(string name) {
        var myName = new Knowledge.Point();
        myName.AddName(name);
        this.brain_.GetCoreGroup().AddPoint(myName);
    }

    public void SetAnswer(AnswerInterface answer) {
        this.answerInterface_ = answer;
    }

    public void OnAsk(string words) {
        this.lateWords_.Push(words);
    }

    public void Learn(string something) {
    }

    public void Learn(string question, string answer) {
    }

    public void Think() {
    }
}
}
