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
public struct InputRule{
    public string detectModuleType;
    public InputVar[] inputVars;
    public string var1;
    public string ruleOperator;
    public string var2;
    public int nextModule;
}

[Serializable]
public struct ModuleInputPacket{
    public string moduleType;
    public InputVar[] inputVars;
    public InputRule[] rules;
}

public struct Rule{
    public AIDetectModule detectModule;
    public string var1;
    public string ruleOperator;
    public string var2;
    public int nextModule;
}

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

            foreach(InputVar inputVar in aiInputPacket.inputVars){
                aiPacket.module.vars.Add(inputVar.name, Convert.ChangeType(inputVar.var, Type.GetType(inputVar.varType)));
            }

            aiPacket.rules = new Rule[aiInputPacket.rules.Length];
            for(int j = 0; j < aiInputPacket.rules.Length; j++){
                aiPacket.rules[j].detectModule = (AIDetectModule)Activator.CreateInstance(Type.GetType(aiInputPacket.rules[j].detectModuleType));
                aiPacket.rules[j].detectModule.gameObject = gameObject;
                foreach(InputVar inputVar in aiInputPacket.rules[j].inputVars){
                    aiPacket.rules[j].detectModule.vars.Add(inputVar.name, Convert.ChangeType(inputVar.var, Type.GetType(inputVar.varType)));
                }

                aiPacket.rules[j].nextModule = aiInputPacket.rules[j].nextModule;
                aiPacket.rules[j].var1 = aiInputPacket.rules[j].var1;
                aiPacket.rules[j].var2 = aiInputPacket.rules[j].var2;
                aiPacket.rules[j].ruleOperator = aiInputPacket.rules[j].ruleOperator;

                aiPacket.rules[j].detectModule.wakeUp();
            }

            aiPacket.module.gameObject = gameObject;
            aiPacket.module.wakeUp();
            modules[i] = aiPacket;
        }

        moduleRunning = modules[0];
    }

    void Update(){
        moduleRunning.module.run();
        foreach(Rule rule in moduleRunning.rules){
            rule.detectModule.detect();
            ifWithString(rule.ruleOperator, rule.detectModule.vars[rule.var1], rule.detectModule.vars[rule.var2], rule.nextModule);
        }
    }

    void ifWithString(string ruleOperator, dynamic var1, dynamic var2, int nextModule){
        if(ruleOperator == "=="){
            if(var1 == var2){
                moduleRunning = modules[nextModule];
            }
            return;
        }
        if(ruleOperator == "<="){
            if(var1 <= var2){
                moduleRunning = modules[nextModule];
            }
            return;
        }
        if(ruleOperator == ">="){
            if(var1 >= var2){
                moduleRunning = modules[nextModule];
            }
            return;
        }
        if(ruleOperator == ">"){
            if(var1 > var2){
                moduleRunning = modules[nextModule];
            }
            return;
        }
        if(ruleOperator == "<"){
            if(var1 < var2){
                moduleRunning = modules[nextModule];
            }
            return;
        }

        throw new System.ArgumentException("Rule operator doesn't contain ==, <=, =>, <, or >", "Llarence's cool errors");
    }
}
