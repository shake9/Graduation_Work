using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handRay : MonoBehaviour
{
    [SerializeField] float _rayDistance = 100;
    OVRHand _hand;
    LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _hand = GetComponent<OVRHand>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //  ƒŒƒC‚ðLineRenderer‚Å•`‰æ
        var positions = new Vector3[]{
            _hand.PointerPose.position,
            _hand.PointerPose.position + _hand.PointerPose.forward * _rayDistance
        };

        _lineRenderer.SetPositions(positions);

        //  PointerPose‚ª—LŒø‚ÈŽž‚Ì‚ÝLineRenderer‚ð•\Ž¦
        _lineRenderer.enabled = _hand.IsPointerPoseValid;

    }
}
