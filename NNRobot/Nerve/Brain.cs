using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NNRobot.Knowledge;

namespace NNRobot.Nerve {
internal class Brain {
    private Store store_ = new Knowledge.Store();
    private Queue<Signal> singals_ = new Queue<Signal>();
    private List<Unit> units_ = new List<Unit>();
    private object signalLocker_ = new object();
    private Thread thinkTread_;
    private bool running_;

    public Brain() {
        this.thinkTread_ = new Thread(this.Think);
        this.thinkTread_.Start();
        this.running_ = true;
    }

    public Group GetCoreGroup() {
        return this.store_.GetCoreGroup();
    }

    public void OnInput(string content) {
        if (string.IsNullOrEmpty(content))
            return;
        var signal = new Signal();
        signal.content = content;
        lock(signalLocker_) {
            this.singals_.Enqueue(signal);
        }
    }

    private void ProcessWithKnowledge_(Signal signal) {
        foreach (var unit in this.units_) {
            if (signal.completed)
                break;
            unit.OnIn(signal);
        }
    }

    private void ExtendKnowledge_(Signal signal) {
    }

    private void CreateAnswer(Signal signal) {
    }

    private void Process_(Signal signal) {
        this.ProcessWithKnowledge_(signal);
        if (!signal.completed) {
            this.ExtendKnowledge_(signal);
        }
    }

    private void Think() {
        while(this.running_) {
            if (this.singals_.Count > 0) {
                lock (this.signalLocker_) {
                    var signal = this.singals_.Dequeue();
                    this.Process_(signal);
                }
            }
            Thread.Sleep(25);
        }
    }
}
}
