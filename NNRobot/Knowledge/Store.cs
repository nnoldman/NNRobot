using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Store: Define {
    private Group core_ = new Group();
    private List<Group> groups_ = new List<Group>();

    public void AddGroup(Group group) {
        this.groups_.Add(group);
    }

    public Store() {
        this.AddGroup(this.core_);
    }

    public Group GetCoreGroup() {
        return this.core_;
    }
}
}
