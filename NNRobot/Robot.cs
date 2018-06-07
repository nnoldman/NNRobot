using System;
using System.Collections.Generic;

namespace NNRobot {

public interface AnswerInterface {
    void Talk(Robot robot, string content);
}

public class Robot {
    private Nerve.Brain brain_ = new Nerve.Brain();

    public string GetName() {
        return this.brain_.GetCoreGroup().GetName();
    }

    public Robot(string name) {
        this.brain_.SetShell(this);
        this.brain_.GetCoreGroup().AddName(name);
    }

    public void SetAnswer(AnswerInterface answer) {
        this.brain_.SetAnswer(answer);
    }

    public void Dump() {
        this.brain_.Dump();
    }

    public void OnAsk(string words) {
        this.brain_.OnInput(words);
    }

    public void Learn(string something) {
    }

    public void Learn(string question, string answer) {
    }

    public void Close() {
        this.brain_.Close();
    }
}
}
