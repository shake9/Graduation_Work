using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestureTest : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private OVRSkeleton _skeleton; //右手、もしくは左手の Bone情報
    private OVRHand _oVRHand;

    /// <summary>
    /// 指定した全てのBoneIDが直線状にあるかどうか調べる
    /// </summary>
    /// <param name="threshold">閾値 1に近いほど厳しい</param>
    /// <param name="boneids"></param>
    /// <returns></returns>
    private bool IsStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //調べようがない
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length - 1; index++)
        {
            var v = (_skeleton.Bones[(int)boneids[index + 1]].Transform.position - _skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //内積の値を総乗していく
            }
            oldVec = v;//ひとつ前の指ベクトル
        }
        return dot >= threshold; //指定したBoneIDの内積の総乗が閾値を超えていたら直線とみなす
    }

    // Start is called before the first frame update
    void Start()
    {
        _oVRHand = GetComponent<OVRHand>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ハンドトラッキングがオンになっていないと通らない
        if (!_oVRHand.IsTracked || _oVRHand.HandConfidence.Equals(OVRHand.TrackingConfidence.Low)) return;

        // 親指
        var isThumbStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Thumb0, OVRSkeleton.BoneId.Hand_Thumb1, OVRSkeleton.BoneId.Hand_Thumb2, OVRSkeleton.BoneId.Hand_Thumb3, OVRSkeleton.BoneId.Hand_ThumbTip);
        // 人差し指
        var isIndexStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        // 中指
        var isMiddleStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Middle1, OVRSkeleton.BoneId.Hand_Middle2, OVRSkeleton.BoneId.Hand_Middle3, OVRSkeleton.BoneId.Hand_MiddleTip);
        // 薬指
        var isRingStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        // 小指
        var isPinkyStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        // 親指だけが立っている
        if (!isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight && isThumbStraight)
        {
            Debug.Log("fire");
            animator.SetBool("Fire", true);
        }
        // 親指と人差し指が立っている
        else if (isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight && isThumbStraight)
        {
            Debug.Log("reload");
            animator.SetBool("Fire", false);

        }

        // 親指関係なくそれ以外の指が全て立っている
        if (isIndexStraight && isMiddleStraight && isRingStraight && isPinkyStraight)
        {
            Debug.Log("charge");
            animator.SetBool("Charge", true);
        }
        else if(!isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight)
        {
            Debug.Log("charge");
            animator.SetBool("Charge", false);
        }
    }
}
