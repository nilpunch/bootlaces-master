using System.Collections;
using System.Collections.Generic;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class NewTestScript
{
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        EventSource source = new GameObject("Source").AddComponent<EventSource>();
        EventTarget target = new GameObject("Target").AddComponent<EventTarget>();
        
        SelfSubscribingTarget selfSubscribingTarget = new GameObject("SelfSubscribingTarget").AddComponent<SelfSubscribingTarget>();

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses2()
    {
        EventSource source = new GameObject("Source").AddComponent<EventSource>();
        EventTarget target = new GameObject("Target").AddComponent<EventTarget>();
        
        SelfSubscribingTarget selfSubscribingTarget = new GameObject("SelfSubscribingTarget").AddComponent<SelfSubscribingTarget>();

        yield return null;
    }
}
