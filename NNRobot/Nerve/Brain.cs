using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NNRobot.Knowledge;

namespace NNRobot.Nerve {
internal class Brain {
    private AnswerInterface answerInterface_;
    private Store store_ ;
    private Queue<Signal> singals_ ;
    private object signalLocker_ ;
    private Thread thinkTread_;
    private bool running_;
    private Robot shell_;
    private Dictionary<string, Unit> units_ ;

    public Brain() {
        this.store_ = new Knowledge.Store();
        this.singals_ = new Queue<Signal>();
        this.signalLocker_ = new object();
        this.units_ = new Dictionary<string, Unit>();

        this.thinkTread_ = new Thread(this.Think);
        this.thinkTread_.Start();
        this.running_ = true;
        this.LoadLocal();
    }

    public void SetShell(Robot robot) {
        this.shell_ = robot;
    }

    public Group GetCoreGroup() {
        return this.store_.GetCoreGroup();
    }

    public void SetAnswer(AnswerInterface answer) {
        this.answerInterface_ = answer;
    }

    public void Close() {
        this.running_ = false;
    }

    public void Dump() {
        if (this.answerInterface_ == null)
            return;
        foreach(var it in this.units_)
            this.answerInterface_.Talk(this.shell_, it.Value.ToString()) ;
    }

    public void OnInput(string content) {
        if (string.IsNullOrEmpty(content))
            return;
        var signal = new Signal(content);
        signal.content = content;
        lock(signalLocker_) {
            this.singals_.Enqueue(signal);
        }
    }

    private void ProcessWithKnowledge_(Signal signal) {
        foreach (var it in this.units_) {
            if (signal.IsCompleted())
                break;
            it.Value.OnIn(signal);
        }
    }

    private void ExtendKnowledge_(Signal signal) {
        var words = new List<string>();
        var sb = new StringBuilder();
        var len = signal.content.Length;
        for(int i = 0; i < len; ++i) {
            if(!signal.processedIndices[i]) {
                sb.Append(signal.content[i]);
                if(i == len - 1)
                    words.Add(sb.ToString());
            } else {
                words.Add(sb.ToString());
                sb.Clear();
            }
        }

        for(int i = 0; i < words.Count; ++i) {
            var word = words[i];
            var unit = Unit.MakeUnit(word);
            if(!this.units_.ContainsKey(word)) {
                this.units_.Add(word, unit);
            }
        }
    }

    private void CreateAnswer(Signal signal) {
    }

    private void Process_(Signal signal) {
        this.ProcessWithKnowledge_(signal);
        if (!signal.IsCompleted()) {
            this.ExtendKnowledge_(signal);
        }
    }

    private void Think() {
        while(this.running_) {
            if (this.singals_.Count > 0) {
                lock (this.signalLocker_) {
                    var signal = this.singals_.Dequeue();
                    this.Process_(signal);
                }
            }
            Thread.Sleep(25);
        }
        this.SaveLocal();
    }

    static readonly string sUnitJsonFile = "../../../units.json";

    private void SaveLocal() {
        if(this.units_.Count > 0) {
            var unitParams = new UnitParams();
            foreach (var it in this.units_) {
                var param = new UnitParam();
                param.id = it.Value.GetID();
                param.content = it.Key;
                param.hitTimes = it.Value.GetHitTiemes();
                unitParams.units.Add(param);
            }
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(unitParams, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(sUnitJsonFile, content);
        }
    }

    private void LoadLocal() {
        if(System.IO.File.Exists(sUnitJsonFile)) {
            var content = System.IO.File.ReadAllText(sUnitJsonFile);
            var unitParams = Newtonsoft.Json.JsonConvert.DeserializeObject<UnitParams>(content);
            if(unitParams != null) {
                foreach(var it in unitParams.units) {
                    var unit = Unit.MakeUnit(it);
                    this.units_.Add(it.content, unit);
                }
            }
        }

    }

    public void AddBaseLogic() {
        var define = Unit.MakeUnit("是");
    }
}
}
