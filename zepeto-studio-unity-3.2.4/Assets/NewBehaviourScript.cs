using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform topTr;
    public Transform bottom;

    private Transform[] tr;
    private Transform[] topTrs;
    private Transform[] bottomTrs;

    private List<Transform> trList1 = new List<Transform>();
    private List<Transform> topTrList = new List<Transform>();

    private List<Transform> trList2 = new List<Transform>();
    private List<Transform> bottomTrList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponentsInChildren<Transform>();
        topTrs = topTr.GetComponentsInChildren<Transform>();
        bottomTrs = bottom.GetComponentsInChildren<Transform>();

        for (int i = 0; i < topTrs.Length; i++)
        {
            for (int j = 0; j < tr.Length; j++)
            {
                if (topTrs[i].name.Equals(tr[j].name))
                {
                    trList1.Add(tr[j]);
                    topTrList.Add(topTrs[i]);
                }

            }
        }

        for (int i = 0; i < bottomTrs.Length; i++)
        {
            for (int j = 0; j < tr.Length; j++)
            {
                if (bottomTrs[i].name.Equals(tr[j].name))
                {
                    trList2.Add(tr[j]);
                    bottomTrList.Add(bottomTrs[i]);
                }

            }
        }

        StartCoroutine(UpdeateTransform());
    }

    IEnumerator UpdeateTransform()
    {
        while (true)
        {
            yield return null;

            for (int i = 0; i < trList1.Count; i++)
            {
                topTrList[i].position = trList1[i].position;
                topTrList[i].localScale = trList1[i].localScale;
                topTrList[i].eulerAngles = trList1[i].eulerAngles;
            }

            for (int i = 0; i < trList2.Count; i++)
            {
                bottomTrList[i].position = trList2[i].position;
                bottomTrList[i].localScale = trList2[i].localScale;
                bottomTrList[i].eulerAngles = trList2[i].eulerAngles;
            }
        }
    }

   
}
