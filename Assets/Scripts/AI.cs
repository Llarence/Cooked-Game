using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct InputVar{
    public string name;
    public dynamic var;
}

[Serializable]
public struct Rule{
    public string var1;
    public string comparison;
    public string var2;
}

[Serializable]
public struct ModuleInputPacket{
    public string moduleType;
    public InputVar[] inputVars;
    public Rule[] rules;
}

struct ModulePacket{
    public AIModule module;
    public Rule[] rules;
}

public class AI : MonoBehaviour
{
    public ModuleInputPacket[] inputModules;
    ModulePacket[] modules;

    void Start(){
        modules = new ModulePacket[inputModules.Length];
        for(int i = 0; i < modules.Length; i++){
            ModuleInputPacket aiInputPacket = inputModules[i];
            ModulePacket aiPacket = new ModulePacket();
            aiPacket.module = (AIModule)Activator.CreateInstance(Type.GetType(aiInputPacket.moduleType));
            for(int j = 0; j < aiInputPacket.inputVars.Length; j++){
                aiPacket.module.inputVars.Add(aiInputPacket.inputVars[j].name, aiInputPacket.inputVars[j].var);
            }
            aiPacket.rules = aiInputPacket.rules;
            aiPacket.module.wakeUp(gameObject);
            modules[i] = aiPacket;
        }
    }

    void Update(){
        modules[0].module.run(gameObject);
    }
}
