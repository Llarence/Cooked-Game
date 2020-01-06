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
    public string var1;
    public string ruleOperator;
    public string var2;
    public int nextModule;
}

[Serializable]
public struct ModuleInputPacket{
    public string moduleType;
    public InputVar[] inputVars;
    public Rule[] rules;
}

[Serializable]
struct ModulePacket{
    public AIModule module;
    public Rule[] rules;
}

public class AI : MonoBehaviour
{
    public ModuleInputPacket[] inputModules;
    ModulePacket[] modules;
    ModulePacket moduleRunning;

    void Start(){
        modules = new ModulePacket[inputModules.Length];
        for(int i = 0; i < modules.Length; i++){
            ModuleInputPacket aiInputPacket = inputModules[i];
            ModulePacket aiPacket = new ModulePacket();
            aiPacket.module = (AIModule)Activator.CreateInstance(Type.GetType(aiInputPacket.moduleType));
            for(int j = 0; j < aiInputPacket.inputVars.Length; j++){
                aiPacket.module.inputVars.Add(aiInputPacket.inputVars[j].name, Convert.ChangeType(aiInputPacket.inputVars[j].var, Type.GetType(aiInputPacket.inputVars[j].varType)));
            }
            aiPacket.rules = aiInputPacket.rules;
            aiPacket.module.wakeUp(gameObject);
            modules[i] = aiPacket;
        }
        moduleRunning = modules[0];
    }

    void Update(){
        moduleRunning.module.run(gameObject);
        foreach(Rule rule in moduleRunning.rules){
            ifWithString(rule.ruleOperator, moduleRunning.module.outputVars[rule.var1], moduleRunning.module.outputVars[rule.var2], rule.nextModule);
        }
    }

    bool ifWithString(string ruleOperator, dynamic var1, dynamic var2, int nextModule){
        if(ruleOperator == "=="){
            if(var1 == var2){
                moduleRunning = modules[nextModule];
                return true;
            }
            return false;
        }
        if(ruleOperator == "<="){
            if(var1 <= var2){
                moduleRunning = modules[nextModule];
                return true;
            }
            return false;
        }
        if(ruleOperator == ">="){
            if(var1 >= var2){
                moduleRunning = modules[nextModule];
                return true;
            }
            return false;
        }
        if(ruleOperator == ">"){
            if(var1 > var2){
                moduleRunning = modules[nextModule];
                return true;
            }
            return false;
        }
        if(ruleOperator == "<"){
            if(var1 < var2){
                moduleRunning = modules[nextModule];
                return true;
            }
            return false;
        }

        throw new System.ArgumentException("Rule operator doesn't contain ==, <=, =>, <, or >", "Llarence's cool errors");
    }
}
