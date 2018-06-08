using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Resource {
internal class Resources {
    private List<ResourceDefine> resources_ = new List<ResourceDefine>();
    public Resources () {
        this.resources_.Add(new Energy());
        this.resources_.Add(new Memory());
    }
}
}
