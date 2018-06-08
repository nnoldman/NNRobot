using System;
using System.Collections.Generic;
using System.Text;

namespace NNRobot.Nerve {
internal class BaseLogic {
    public class Operation {
    }

    public class Value {
        public virtual bool True() {
            return false;
        }
    }

    public class IsTrue: Operation {
        public bool Calc(Value a) {
            return a.True();
        }
    }

    public class OperationOr : Operation {
        public static bool Calc(Value a, Value b) {
            return a.True() || b.True();
        }
    }

    public class OperationAnd : Operation {
        public static bool Calc(Value a, Value b) {
            return a.True() && b.True();
        }
    }
}
}
