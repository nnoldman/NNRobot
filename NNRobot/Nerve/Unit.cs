using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class Unit {
    public List<Unit> inputs_ = new List<Unit>();
    public List<Unit> outputs_ = new List<Unit>();
    private Signal signal_;

    public void OnIn(Signal signal) {
        this.signal_ = signal;
    }


    private void DispatchDown_() {
        foreach(var it in this.outputs_) {
            it.OnIn(signal_);
        }
    }
}
}
