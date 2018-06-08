using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Resource {
internal class ResourceDefine {
    public bool canRemove = true;
    public virtual float GetLeft() {
        return 1;
    }
    public float GetLeftTimeSeconds() {
        return int.MaxValue;
    }
}
}
