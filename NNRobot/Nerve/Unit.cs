using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class Unit {
    private static NCFrame.IDGenerator sIDGenerator_ = new NCFrame.IDGenerator();

    private int id_;
    private readonly List<Unit> inputs_ = new List<Unit>();
    private readonly List<Unit> outputs_ = new List<Unit>();
    private string content_;
    private int hitTimes_ = 0;
    private Signal signal_;

    public int GetID() {
        return this.id_;
    }
    public int GetHitTiemes() {
        return this.hitTimes_;
    }

    public void OnIn(Signal signal) {
        this.signal_ = signal;
        var position = this.signal_.content.IndexOf(this.content_);
        if(position != -1) {
            this.hitTimes_++;
            this.signal_.SetComplete(position, this.content_.Length);
        } else {
            if(!this.signal_.IsCompleted() && this.outputs_.Count > 0) {
                this.DispatchDown_();
            }
        }
    }

    private void DispatchDown_() {
        foreach(var it in this.outputs_) {
            it.OnIn(signal_);
        }
    }

    public static Unit MakeUnit(string content, int id = -1) {
        var ret = NCFrame.PoolMgr.Instance.Require<Unit>();
        ret.id_ = id == -1 ? sIDGenerator_.Require() : id;
        ret.content_ = content;
        return ret;
    }

    public static Unit MakeUnit(UnitParam param) {
        var ret = MakeUnit(param.content, param.id);
        ret.hitTimes_ = param.hitTimes;
        return ret;
    }

    public override string ToString() {
        return $"[{this.id_}]{this.content_}";
    }
}
}
