using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffsetBg : MonoBehaviour
{
    private Renderer meshRendererBgComponent;
    private Material currentMaterial;

    [Header("SETTINGS SCENERY BACKGROUND MOTION")]
    public float speedMoveOffsetBg;
    public float incrementOffsetBg;
    public string sortingLayerNameBg;
    public int orderInLayerNumberBg;

    // Control variables
    private float getOffsetIncrement;

    private void Awake()
    {
        meshRendererBgComponent = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        currentMaterial = meshRendererBgComponent.material;

        // This was the way found to change the position in which the background elements of the scenery appear in the camera
        meshRendererBgComponent.sortingLayerName = sortingLayerNameBg;
        meshRendererBgComponent.sortingOrder = orderInLayerNumberBg;
    }

    private void FixedUpdate()
    {
        getOffsetIncrement += incrementOffsetBg;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(getOffsetIncrement * speedMoveOffsetBg, 0f));
    }
}
