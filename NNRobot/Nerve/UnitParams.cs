using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
public class UnitParam {
    public int id;
    public string content;
    public int hitTimes;
}
public class UnitParams {
    public List<UnitParam> units = new List<UnitParam>();
}
}
