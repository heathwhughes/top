using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = .5f;
    public const string IN_VIEW_LAYER_NAME = "InViewEnemy";
    public const string OUT_OF_VIEW_LAYER_NAME = "OutOfViewEnemy";
    private Face parentFace;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        parentFace = GetComponentInParent<Face>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (parentFace.IsInView)
        {
            spriteRenderer.sortingLayerName = IN_VIEW_LAYER_NAME;
        }
        else
        {
            spriteRenderer.sortingLayerName = OUT_OF_VIEW_LAYER_NAME;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parentFace.IsInView)
        {
            spriteRenderer.sortingLayerName = IN_VIEW_LAYER_NAME;
        }
        else
        {
            spriteRenderer.sortingLayerName = OUT_OF_VIEW_LAYER_NAME;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }
    
}
