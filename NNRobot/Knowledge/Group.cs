using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Group: Define {
    private List<Define> defines_ = new List<Define>();
    private bool canForget_ = false;

    public bool CanForget() {
        return this.canForget_;
    }

    public void AddDefine(Define define) {
        this.defines_.Add(define);
    }

    public bool IsInGroup(Define define) {
        return define.GetOwnerGroup() == this;
    }
}
}
