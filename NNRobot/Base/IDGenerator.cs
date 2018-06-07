using System;
using System.Collections.Generic;
using System.Text;

namespace NCFrame {
public class IDGenerator {
    public const int InvalidID = int.MinValue;
    private HashSet<int> activeIDs_ = new HashSet<int>();
    private Stack<int> deactiveIDs_ = new Stack<int>();

    public int Require() {
        if (deactiveIDs_.Count > 0) {
            var id = deactiveIDs_.Peek();
            this.deactiveIDs_.Pop();
            this.activeIDs_.Add(id);
            return id;
        }
        long ticks = System.DateTime.Now.Ticks;
        int ret = (int)(ticks % int.MaxValue);
        int distance = 1;
        while (activeIDs_.Contains(ret) || ret == InvalidID) {
            ret += distance % 2 == 0 ? distance : -distance;
            ++distance;
        }
        activeIDs_.Add(ret);
        return ret;
    }

    public void Add(int id) {
        this.activeIDs_.Add(id);
    }

    public void Remove(int id) {
        if (this.activeIDs_.Contains(id)) {
            this.activeIDs_.Remove(id);
            this.deactiveIDs_.Push(id);
        }
    }
    public void Clear() {
        this.activeIDs_.Clear();
        this.deactiveIDs_.Clear();
    }
}
}
