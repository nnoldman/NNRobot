using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Point {
    private List<string> names_ = new List<string>();
    public void AddName(string name) {
        this.names_.Add(name);
    }
}
}
