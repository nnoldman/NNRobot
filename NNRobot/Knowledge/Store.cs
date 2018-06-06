using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Store: Point {
    private Knowledge.Group coreGroup_ = new Group();
    private List<Group> groups_ = new List<Group>();

    public void AddGroup(Group group) {
        this.groups_.Add(group);
    }

    public Store() {
        this.AddGroup(this.coreGroup_);
    }

    public Group GetCoreGroup() {
        return this.coreGroup_;
    }
}
}
