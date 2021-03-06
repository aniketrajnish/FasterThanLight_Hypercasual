using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartScript : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyScript enemy;
    public Renderer bodyPartRenderer;
    public GameObject bodyPartPrefab;
    public bool replaced;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HidePartAndReplace()
    {
        if (replaced)
            return;


        if (bodyPartRenderer!=null)
        bodyPartRenderer.enabled = false;

        GameObject part = new GameObject();
        if (bodyPartPrefab != null)
        {
            part = Instantiate(bodyPartPrefab, transform.position, transform.rotation);
            /*GameObject.Find("PlayerBreak").GetComponent<AudioSource>().Play();*/
        }

        Rigidbody[] rbs = part.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbs)
        {
            r.interpolation = RigidbodyInterpolation.Interpolate;
            r.AddExplosionForce(15, transform.position, 5);
        }

        rb.AddExplosionForce(15, transform.position, 5);

        Destroy(part, 3f);

        this.enabled = false;
        replaced = true;
        AddTagRecursively(enemy.transform, "Untagged");
    }

    void AddTagRecursively(Transform trans, string tag)
    {
        trans.gameObject.tag = tag;
        if (trans.childCount > 0)
            foreach (Transform t in trans)
                AddTagRecursively(t, tag);
    }
}
