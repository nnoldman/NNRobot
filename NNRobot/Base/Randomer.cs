using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot {
public class Randomer {
    private System.Random random_ = new Random(System.DateTime.Now.Millisecond);
    public static Randomer Instance = new Randomer();
    public int Next(int min, int max) {
        return this.random_.Next(min, max);
    }
}
}
