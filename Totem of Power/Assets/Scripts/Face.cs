using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public const string IN_VIEW_LAYER_NAME = "InViewTotem";
    public const string OUT_OF_VIEW_LAYER_NAME = "OutOfView";
    [SerializeField] public bool IsInView { get; set; }
    [SerializeField] public bool IsVisibleLeft { get; set; }
    [SerializeField] public bool IsVisibleRight { get; set; }
    [SerializeField] public bool IsHiddenLeft { get; set; }
    [SerializeField] public bool IsHiddenRight { get; set; }
    Block block;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        block = GetComponentInParent<Block>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set initial values for where each face in in the view for sorting layers
        if (gameObject.name == "Face 1" || gameObject.name == "Face 2")
        {
            IsInView = true;
            IsHiddenLeft = false;
            IsHiddenRight = false;

            if (gameObject.name == "Face 1")
            {
                IsVisibleLeft = true;
                IsVisibleRight = false;
            }
            else
            {
                IsVisibleLeft = false;
                IsVisibleRight = true;
            }
        }
        else
        {
            IsInView = false;
            IsVisibleLeft = false;
            IsVisibleRight = false;

            if (gameObject.name == "Face 3")
            {
                IsHiddenRight = true;
                IsHiddenLeft = false;
            }
            else if (gameObject.name == "Face 4")
            {
                IsHiddenLeft = true;
                IsHiddenRight = false;
            }
            else
            {
                Debug.LogError("Invalid Face object name.");
            }
        }

        if (IsInView)
        {
            spriteRenderer.sortingLayerName = IN_VIEW_LAYER_NAME;
        }
        else
        {
            spriteRenderer.sortingLayerName = OUT_OF_VIEW_LAYER_NAME;
        }

    }

    private void Update()
    {
        if (IsInView)
        {
            spriteRenderer.sortingLayerName = IN_VIEW_LAYER_NAME;
        }
        else
        {
            spriteRenderer.sortingLayerName = OUT_OF_VIEW_LAYER_NAME;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (transform.parent.parent.name != "Chunk 0")
        {
            // Assuming that it's the Head's collider, set it's parent's parent to the Face
            other.transform.parent.parent = transform;
            other.GetComponentInParent<Enemy>().parentFace = gameObject.GetComponent<Face>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Head")
        {
            block.RemoveFromLeavingList(other.GetComponentInParent<Enemy>());
        }
    }

}
