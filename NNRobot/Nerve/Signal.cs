using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class Signal {
    public string content;
    public bool[] processedIndices;

    public Signal(string content) {
        this.processedIndices = new bool[content.Length];
    }

    public bool IsCompleted() {
        for(int i = 0; i < this.processedIndices.Length; ++i) {
            if(!this.processedIndices[i]) {
                return false;
            }
        }
        return true;
    }

    public void SetComplete(int index, int len = 1) {
        int count = len == -1 ? this.content.Length : len;
        for(int i = 0; i < count; ++i) {
            this.processedIndices[i] = true;
        }
    }
}
}
