using System;
using System.Collections.Generic;
using System.Text;

namespace NCFrame {
public class PoolCore {
    public const int DefaultGrowStep = 16;
    protected Stack<object> deactiveObjects_ = new Stack<object>();
    protected HashSet<object> activeObjects_ = new HashSet<object>();
    private int step_ = DefaultGrowStep;
    private int hitTimes_ = 0;
    private int accessTimes_ = 0;

    public PoolCore() {
    }
    public PoolCore(int step) {
        this.step_ = step;
    }
    public float GetHitPointPerStep() {
        if (this.accessTimes_ > 0) {
            //Console.WriteLine("Before Call GetHitPoint Must Call Require Once least!");
            return (float)this.hitTimes_ / this.accessTimes_
                   * this.activeObjects_.Count / (this.activeObjects_.Count + this.deactiveObjects_.Count);
        }
        return 0;
    }

    public int Count {
        get {
            return this.deactiveObjects_.Count;
        }
    }

    public T Require<T>() {
        return (T)this.Require(typeof(T));
    }

    public object Require(Type type) {
        this.accessTimes_++;
        if (this.deactiveObjects_.Count > 0) {
            this.hitTimes_++;
            var obj = this.deactiveObjects_.Pop();
            this.activeObjects_.Add(obj);
            return obj;
        }
        var grow = new object[this.step_];
        for (int i = 0; i < grow.Length; ++i) {
            grow[i] = Activator.CreateInstance(type);
            this.deactiveObjects_.Push(grow[i]);
        }
        var ret = this.deactiveObjects_.Pop();
        this.activeObjects_.Add(ret);
        return ret;
    }

    public void Recycle(object obj) {
        if (obj != null) {
            this.activeObjects_.Remove(obj);
            if (!this.deactiveObjects_.Contains(obj)) {
                this.deactiveObjects_.Push(obj);
                return;
            }
            Console.WriteLine("Erorr Pool Recycle !" + obj.ToString());
        }
    }

    public void Clear() {
        this.activeObjects_.Clear();
        this.deactiveObjects_.Clear();
    }
    public int GetGrowStep() {
        return this.step_;
    }
}

public class ObjectPool<T> : PoolCore {
    public ObjectPool() {
    }
    public ObjectPool(int step)
    : base(step) {
    }

    public T[] GetActiveObjectsCopy() {
        var ret = new T[this.activeObjects_.Count];
        int index = 0;
        foreach (var it in this.activeObjects_) {
            ret[index++] = (T)it;
        }
        return ret;
    }

    public T Require() {
        return (T)base.Require<T>();
    }

    public void Recycle(T obj) {
        base.Recycle(obj);
    }
}

public class PoolMgr : NCFrame.Singleton<PoolMgr> {
    private Dictionary<Type, PoolCore> pools_ = new Dictionary<Type, PoolCore>();
    public T Require<T>(int step = PoolCore.DefaultGrowStep) {
        return (T)this.Require(typeof(T));
    }
    public object Require(Type type, int step = PoolCore.DefaultGrowStep) {
        var pool = this.pools_.GetValue(type);
        if (pool == null) {
            pool = new PoolCore(step);
            this.pools_.Add(type, pool);
        }
        return pool.Require(type);
    }
    public void Recycle(object obj) {
        if (obj != null) {
            var pool = this.pools_.GetValue(obj.GetType());
            if (pool != null) {
                pool.Recycle(obj);
            }
        }
    }
}
}
