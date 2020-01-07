using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct InputVar{
    public string name;
    public string varType;
    public string var;
}

[Serializable]
public struct Rule{
    public string detectModuleType;
    [NonSerialized]
    public AIDetectModule detector;
    public InputVar[] vars;
    public string var1;
    public string ruleOperator;
    public string var2;
    public int nextModule;
}

[Serializable]
public struct ModulePacket{
    public string moduleType;
    [NonSerialized]
    public AIModule mainScript;
    public InputVar[] vars;
    public Rule[] rules;
}

public class AI : MonoBehaviour
{
    public ModulePacket[] modules;
    ModulePacket moduleRunning;

    void Start(){
       setUpForModuleRunning(new ModulePacket(), modules[0]);
    }

    void Update(){
        foreach(Rule rule in moduleRunning.rules){
            ifWithString(rule.ruleOperator, rule.detector.vars[rule.var1], rule.detector.vars[rule.var2], rule.nextModule);
        }
    }

    void setUpForModuleRunning(ModulePacket oldModule, ModulePacket newModule){
        if(oldModule.mainScript != null){
            foreach(Rule rule in oldModule.rules){
                Destroy(rule.detector);
            }
            
            Destroy(oldModule.mainScript);
        }

        moduleRunning = newModule;
        for(int i = 0; i < moduleRunning.rules.Length; i++){
            moduleRunning.rules[i].detector = gameObject.AddComponent(Type.GetType(moduleRunning.rules[i].detectModuleType)) as AIDetectModule;
            foreach(InputVar inputVar in moduleRunning.rules[i].vars){
                moduleRunning.rules[i].detector.vars.Add(inputVar.name, Convert.ChangeType(inputVar.var, Type.GetType(inputVar.varType)));
            }
        }
        moduleRunning.mainScript = gameObject.AddComponent(Type.GetType(moduleRunning.moduleType)) as AIModule;
        foreach(InputVar inputVar in moduleRunning.vars){
            moduleRunning.mainScript.vars.Add(inputVar.name, Convert.ChangeType(inputVar.var, Type.GetType(inputVar.varType)));
        }
    }

    void ifWithString(string ruleOperator, dynamic var1, dynamic var2, int nextModule){
        if(ruleOperator == "=="){
            if(var1 == var2){
                setUpForModuleRunning(moduleRunning, modules[nextModule]);
            }
            return;
        }
        if(ruleOperator == "<="){
            if(var1 <= var2){
                setUpForModuleRunning(moduleRunning, modules[nextModule]);
            }
            return;
        }
        if(ruleOperator == ">="){
            if(var1 >= var2){
                setUpForModuleRunning(moduleRunning, modules[nextModule]);
            }
            return;
        }
        if(ruleOperator == ">"){
            if(var1 > var2){
                setUpForModuleRunning(moduleRunning, modules[nextModule]);
            }
            return;
        }
        if(ruleOperator == "<"){
            if(var1 < var2){
                setUpForModuleRunning(moduleRunning, modules[nextModule]);
            }
            return;
        }

        throw new System.ArgumentException("Rule operator doesn't contain ==, <=, =>, <, or >", "Llarence's cool errors");
    }
}
