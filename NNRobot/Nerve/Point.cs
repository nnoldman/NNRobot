using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class Point {
    internal int id;
    internal string content;
    internal int hitTimes = 0;
    private readonly List<Connection> connections_ = new List<Connection>();
    private Signal signal_;

    public string GetContent() {
        return content;
    }

    public int GetID() {
        return this.id;
    }
    public int GetHitTimes() {
        return this.hitTimes;
    }

    public void OnIn(Signal signal) {
        this.signal_ = signal;
        var position = this.signal_.content.IndexOf(this.content);
        if(position != -1) {
            this.hitTimes++;
            this.signal_.SetComplete(position, this.content.Length);
        } else {
            if(!this.signal_.IsCompleted() && this.connections_.Count > 0) {
                this.DispatchDown_();
            }
        }
    }

    private void DispatchDown_() {
        foreach(var it in this.connections_) {
            it.OnIn(this.signal_);
        }
    }

    public override string ToString() {
        return $"[{this.id.ToString().PadRight(10)}" +
               $"{this.hitTimes.ToString().PadLeft(8)}" +
               $" {this.content}";
    }
}
}
