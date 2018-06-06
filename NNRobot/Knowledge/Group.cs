using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Knowledge {
public class Group: Point {
    private List<Point> points_ = new List<Point>();
    private bool canForget_ = false;

    public bool CanForget() {
        return this.canForget_;
    }

    public void AddPoint(Point point) {
        this.points_.Add(point);
    }
}
}
