using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class Connection {
    public Unit point;
    public int threshould;
    public int hitTimes;

    public bool IsQualityChange() {
        return this.threshould <= this.hitTimes;
    }
    public void OnIn(Signal signal) {
        if (signal.content == this.point.GetContent())
            this.hitTimes++;
        if (this.IsQualityChange())
            this.point.OnIn(signal);
    }
}
}
