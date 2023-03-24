using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSkinnedMeshBone : MonoBehaviour
{
    [SerializeField]
    private Transform rootBone;

    private SkinnedMeshRenderer targetSkin;

    private void Start()
    {
        targetSkin = GetComponent<SkinnedMeshRenderer>();

        Transform[] newBones = new Transform[targetSkin.bones.Length];
        for (int i = 0; i < targetSkin.bones.Length; i++)
        {
            foreach (var newBone in rootBone.GetComponentsInChildren<Transform>())
            {
                if (newBone.name == targetSkin.bones[i].name)
                {
                    newBones[i] = newBone;
                    continue;
                }
            }
        }

        targetSkin.bones = newBones;
    }

}
