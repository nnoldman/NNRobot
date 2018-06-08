using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Define {
    private List<string> names_ = new List<string>();//名字
    private List<Group> groups_ = new List<Group>();//子类
    private Group ownerGroup_;// 类别
    private Define proof_;// 归类凭据

    public void AddName(string name) {
        this.names_.Add(name);
    }

    public string GetName() {
        return this.names_[Randomer.Instance.Next(0, this.names_.Count)];
    }

    public Group GetOwnerGroup() {
        return this.ownerGroup_;
    }
}
}
